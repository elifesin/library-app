using AutoMapper;
using Ee.Ebs.Application.Contracts.Books;
using Ee.Ebs.Application.Contracts.Loans;
using Ee.Ebs.Application.Contracts.Members;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Members
{
    public class BorrowedBooksModel : PageModel
    {
        private readonly IMemberAppService _memberAppService;
        private readonly ILoanAppService _loanAppService;
        private readonly IBookAppService _bookAppService;
        private readonly IMapper _objectMapper;

        public BorrowedBooksModel(
            IMemberAppService memberAppService,
            ILoanAppService loanAppService,
            IMapper objectMapper, IBookAppService bookAppService)
        {
            _memberAppService = memberAppService;
            _loanAppService = loanAppService;
            _objectMapper = objectMapper;
            _bookAppService = bookAppService;
        }

        public MemberBorrowedBooksVm Vm { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var memberDto = _memberAppService.GetById(id);
            if (memberDto == null) return NotFound();

            var loanDtos = _loanAppService.GetLoansByMemberId(id); 

            Vm = new MemberBorrowedBooksVm
            {
                MemberId = memberDto.ID,
                FullName = $"{memberDto.FirstName} {memberDto.LastName}",
                BorrowedBooks = _objectMapper.Map<List<BorrowedBookItem>>(loanDtos) 
            };

            return Page();
        }

        public class MemberBorrowedBooksVm
        {
            public int MemberId { get; set; }
            public string FullName { get; set; } = string.Empty;
            public List<BorrowedBookItem> BorrowedBooks { get; set; } = new();
        }

        public class BorrowedBookItem
        {
            public int BookId { get; set; }
            public string BookName { get; set; }  
            public DateTime LoanDate { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime? ReturnDate { get; set; } 
        }
    }
}
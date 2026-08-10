using AutoMapper;
using Ee.Ebs.Application.Contracts.Loans;   
using Ee.Ebs.Application.Contracts.Members;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Pages.Members
{
    public class BorrowedBooksModel : PageModel
    {
        private readonly IMemberService _memberService;
        private readonly ILoanService _loanService;
        private readonly IMapper _objectMapper;

        public BorrowedBooksModel(
            IMemberService memberService,
            ILoanService loanService,
            IMapper objectMapper)
        {
            _memberService = memberService;
            _loanService = loanService;
            _objectMapper = objectMapper;
        }

        public MemberBorrowedBooksVm Vm { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var memberDto = _memberService.GetById(id);

            if (memberDto == null)
            {
                return NotFound();
            }

            var loanDtos = _loanService.GetLoansByMemberId(id);

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
            public string BookTitle { get; set; } = string.Empty; 
            public DateTime LoanDate { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime? ReturnDate { get; set; } 
        }
    }
}
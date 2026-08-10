using AutoMapper;
using Ee.Ebs.Application.Contracts.Books;
using Ee.Ebs.Application.Contracts.Books.DTOs; 
using Ee.Ebs.Application.Contracts.Loans;
using Ee.Ebs.Application.Contracts.Loans.DTOs;
using Ee.Ebs.Application.Contracts.Members;
using Ee.Ebs.Application.Contracts.Members.DTOs; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Pages.Loans
{
    public class CreateModel : PageModel
    {
        private readonly ILoanService _loanService;
        private readonly IBookService _bookService;
        private readonly IMemberService _memberService;
        private readonly IMapper _objectMapper;

        public CreateModel(
            ILoanService loanService,
            IBookService bookService,
            IMemberService memberService,
            IMapper objectMapper)
        {
            _loanService = loanService;
            _bookService = bookService;
            _memberService = memberService;
            _objectMapper = objectMapper;
        }

        [BindProperty]
        public LoanCreateVm Vm { get; set; } = new();

        public List<MemberDto> Members { get; set; } = new();
        public List<BookDto> Books { get; set; } = new();

        public IActionResult OnGet()
        {
            Vm = new LoanCreateVm
            {
                LoanDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(15)
            };

            LoadData();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                LoadData();
                return Page();
            }

            try
            {
                var dto = _objectMapper.Map<LoanCreateDto>(Vm);
                _loanService.Insert(dto);
                
                return RedirectToPage("./Index");
            }
            catch (ArgumentException ex) // Eski Controller'daki ArgumentException kontrolü[cite: 11]
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                LoadData();
                return Page();
            }
        }
        private void LoadData()
        {
            Members = _memberService.GetAll().ToList();
            Books = _bookService.GetAll().ToList();
        }

        public class LoanCreateVm
        {
            public int MemberID { get; set; }
            public int BookID { get; set; }
            public DateTime LoanDate { get; set; }
            public DateTime DueDate { get; set; }
        }
    }
}
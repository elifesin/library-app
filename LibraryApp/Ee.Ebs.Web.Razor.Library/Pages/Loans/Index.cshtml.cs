using AutoMapper;
using Ee.Ebs.Application.Contracts.Loans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Pages.Loans
{
    public class IndexModel : PageModel
    {
        private readonly ILoanService _loanService;
        private readonly IMapper _objectMapper;

        public IndexModel(ILoanService loanService, IMapper objectMapper)
        {
            _loanService = loanService;
            _objectMapper = objectMapper;
        }

        public List<LoanVm> Loans { get; set; } = new();

        public void OnGet()
        {
            var loanDtos = _loanService.GetAll();
            Loans = _objectMapper.Map<List<LoanVm>>(loanDtos);
        }

        public IActionResult OnPostReturnBook(int id)
        {
            try
            {
                _loanService.ReturnBook(id);
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }

        public class LoanVm
        {
            public int Id { get; set; }
            public DateTime LoanDate { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime? ReturnDate { get; set; } // Null hatası almamak için "?" eklendi
            public string BookName { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
        }
    }
}
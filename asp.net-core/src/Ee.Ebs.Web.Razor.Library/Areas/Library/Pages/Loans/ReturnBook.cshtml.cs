using System;
using System.Collections.Generic;
using AutoMapper;
using Ee.Ebs.Application.Contracts.Loans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Loans
{
    public class ReturnBookModel : PageModel
    {
        private readonly ILoanAppService _loanAppService;
        private readonly IMapper _objectMapper;

        public ReturnBookModel(ILoanAppService loanAppService, IMapper objectMapper)
        {
            _loanAppService = loanAppService;
            _objectMapper = objectMapper;
        }

        [BindProperty]
        public LoanVm Vm { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var loanDto = _loanAppService.GetById(id); 

            if (loanDto == null)
            {
                return NotFound();
            }
            
            Vm = new LoanVm
            {
                Id = loanDto.Id,
                BookName = loanDto.BookName, 
                FullName = loanDto.FullName, 
                DueDate = loanDto.DueDate
            };

            return Page();
        }
        
        public IActionResult OnPost()
        {
            try
            {
                _loanAppService.ReturnBook(Vm.Id);
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
            public string BookName { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public DateTime DueDate { get; set; } 
        }
    }
}
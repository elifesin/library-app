using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Ee.Ebs.Application.Contracts.Books;
using Ee.Ebs.Application.Contracts.Books.DTOs;
using Ee.Ebs.Application.Contracts.Loans;
using Ee.Ebs.Application.Contracts.Loans.DTOs;
using Ee.Ebs.Application.Contracts.Members;
using Ee.Ebs.Application.Contracts.Members.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Loans
{
    public class CreateModel : PageModel
    {
        private readonly ILoanAppService _loanAppService;
        private readonly IBookAppService _bookAppService;
        private readonly IMemberAppService _memberAppService;
        private readonly IMapper _objectMapper;

        public CreateModel(
            ILoanAppService loanAppService,
            IBookAppService bookAppService,
            IMemberAppService memberAppService,
            IMapper objectMapper)
        {
            _loanAppService = loanAppService;
            _bookAppService = bookAppService;
            _memberAppService = memberAppService;
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
                _loanAppService.Insert(dto);
                
                return RedirectToPage("./Index");
            }
            catch (ArgumentException ex) 
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                LoadData();
                return Page();
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
        private void LoadData()
        {
            Members = _memberAppService.GetAll().ToList();
            Books = _bookAppService.GetAvailableBooks().ToList();
        }

        public class LoanCreateVm
        {
            [Required]
            public int ? MemberID { get; set; }
            [Required]
            public int ? BookID { get; set; }
            public DateTime LoanDate { get; set; }
            public DateTime DueDate { get; set; }
        }
    }
}
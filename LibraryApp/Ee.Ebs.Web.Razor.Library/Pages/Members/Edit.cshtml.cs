using AutoMapper;
using Ee.Ebs.Application.Contracts.Loans;
using Ee.Ebs.Application.Contracts.Members;
using Ee.Ebs.Application.Contracts.Members.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Pages.Members;

public class EditModel : PageModel
{
    private readonly IMemberService _memberService;
    private readonly ILoanService _loanService;
    private readonly IMapper _mapper;

    public EditModel(IMemberService memberService, ILoanService loanService, IMapper mapper)
    {
        _memberService = memberService;
        _loanService = loanService;
        _mapper = mapper;
    }
    
    [BindProperty]
    public MemberVm Vm { get; set; }
    
    [HttpGet]
    public IActionResult OnGet(int id)
    {
        var memberDto = _memberService.GetById(id);

        if (memberDto == null)
        {
            return NotFound();
        }
        
        Vm = _mapper.Map<MemberVm>(memberDto);
        return Page();
    }
    
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var memberDto = _mapper.Map<MemberDto>(Vm);
        _memberService.Update(memberDto);

        return RedirectToPage("./Index");
    }
    
    public class MemberVm
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string FullName => $"{FirstName} {LastName}";

        public bool IsActive { get; set; }
    }
}
using AutoMapper;
using Ee.Ebs.Application.Contracts.Members;
using Ee.Ebs.Application.Contracts.Members.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Pages.Members;

public class CreateModel : PageModel
{
    private readonly IMemberService _memberService;
    private readonly IMapper _objectMapper;

    public CreateModel(IMemberService memberService, IMapper objectMapper)
    {
        _memberService = memberService;
        _objectMapper = objectMapper;
    }

    [BindProperty]
    public MemberCreateVm Vm { get; set; }

    public IActionResult OnGet()
    {
        Vm = new MemberCreateVm();
    
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var memberDto = _objectMapper.Map<MemberCreateDto>(Vm);
        _memberService.Insert(memberDto);

        return RedirectToPage("./Index");
    }

    public class MemberCreateVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

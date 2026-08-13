using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Ee.Ebs.Application.Contracts.Members;
using Ee.Ebs.Application.Contracts.Members.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Members;

public class CreateModel : PageModel
{
    private readonly IMemberAppService _memberAppService;
    private readonly IMapper _objectMapper;

    public CreateModel(IMemberAppService memberAppService, IMapper objectMapper)
    {
        _memberAppService = memberAppService;
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

        try
        {
            var memberDto = _objectMapper.Map<MemberCreateDto>(Vm);
            _memberAppService.Insert(memberDto);

            return RedirectToPage("./Index");
    
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
    }

    public class MemberCreateVm
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
    }
}

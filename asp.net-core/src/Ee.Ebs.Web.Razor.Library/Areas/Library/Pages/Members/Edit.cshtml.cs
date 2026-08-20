using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Ee.Ebs.Application.Contracts.Loans;
using Ee.Ebs.Application.Contracts.Members;
using Ee.Ebs.Application.Contracts.Members.DTOs;
using Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Authors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Members;

public class EditModel : PageModel
{
    private readonly IMemberAppService _memberAppService;
    private readonly IMapper _mapper;

    public EditModel(IMemberAppService memberAppService, IMapper mapper)
    {
        _memberAppService = memberAppService;
        _mapper = mapper;
    }

    public int ID { get; set; }
    
    [BindProperty]
    public MemberCreateOrEditVm Vm { get; set; }
    
    [HttpGet]
    public IActionResult OnGet(int id)
    {
        ID = id;
        var memberDto = _memberAppService.GetById(id);

        if (memberDto == null)
        {
            return NotFound();
        }
        
        Vm = _mapper.Map<MemberCreateOrEditVm>(memberDto);
        return Page();
    }
    
    public IActionResult OnPost(int id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var memberDto = _mapper.Map<MemberDto>(Vm);
        memberDto.ID = id;
        
        _memberAppService.Update(id, memberDto);

        return RedirectToPage("./Index");
    }
}
using System.Collections.Generic;
using AutoMapper;
using Ee.Ebs.Application.Contracts.Members;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Members;

public class IndexModel : PageModel
{
    private readonly IMemberAppService _memberAppService;
    private readonly IMapper _mapper;

    public IndexModel(IMemberAppService memberAppService, IMapper mapper)
    {
        _memberAppService = memberAppService;
        _mapper = mapper;
    }
    
    [BindProperty]
    public List<MemberVm> Members { get; set; }

    [HttpGet]
    public IActionResult OnGet()
    {
        var memberDto = _memberAppService.GetAll();
        Members = _mapper.Map<List<MemberVm>>(memberDto);
        
        return Page();
    }

    [HttpPost]
    public IActionResult OnPostDelete(int id)
    {
        _memberAppService.Delete(id);
        
        return RedirectToPage();
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
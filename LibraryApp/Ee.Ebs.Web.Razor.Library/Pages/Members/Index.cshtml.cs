using AutoMapper;
using Ee.Ebs.Application.Contracts.Members;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Pages.Members;

public class IndexModel : PageModel
{
    private readonly IMemberService _memberService;
    private readonly IMapper _mapper;

    public IndexModel(IMemberService memberService, IMapper mapper)
    {
        _memberService = memberService;
        _mapper = mapper;
    }
    
    [BindProperty]
    public List<MemberVm> Members { get; set; }

    [HttpGet]
    public IActionResult OnGet()
    {
        var memberDto = _memberService.GetAll();
        Members = _mapper.Map<List<MemberVm>>(memberDto);
        
        return Page();
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
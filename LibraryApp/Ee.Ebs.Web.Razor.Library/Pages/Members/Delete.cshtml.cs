using AutoMapper;
using Ee.Ebs.Application.Contracts.Members;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Pages.Members;

public class DeleteModel : PageModel
{
    private readonly IMemberService _memberService;
    private readonly IMapper _objectMapper;

    public DeleteModel(IMemberService memberService, IMapper objectMapper)
    {
        _memberService = memberService;
        _objectMapper = objectMapper;
    }

    [BindProperty]
    public MemberVm Vm { get; set; } = new();
    
    [HttpGet]
    public IActionResult OnGet(int id)
    {
        var memberDto = _memberService.GetById(id);

        if (memberDto == null)
        {
            return NotFound();
        }

        Vm = _objectMapper.Map<MemberVm>(memberDto);
            
        return Page();
    }

    [HttpPost]
    public IActionResult OnPost()
    {
        _memberService.Delete(Vm.ID);
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
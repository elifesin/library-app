using AutoMapper;
using Ee.Ebs.Application.Contracts.Authors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Authors;

public class DeleteModel : PageModel
{
    private readonly IAuthorAppService _authorAppService;
    private readonly IMapper _objectMapper;

    [BindProperty] 
    public AuthorVm Vm { get; set; }

    public DeleteModel(IAuthorAppService authorAppService, IMapper objectMapper)
    {
        _authorAppService = authorAppService;
        _objectMapper = objectMapper;
    }
    
    [HttpGet]
    public IActionResult OnGet(int id)
    {
        var dto = _authorAppService.GetById(id);

        if (dto == null)
        {
            return NotFound();
        }
        
        Vm = _objectMapper.Map<AuthorVm>(dto);
        
        return Page();
    }

    [HttpPost]
    public IActionResult OnPost(int id)
    {
        _authorAppService.Delete(id);
 
        return RedirectToPage("./Index");
    }
    
    
    public class AuthorVm
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsActive { get; set; } = true;
        public string FullName => $"{FirstName} {LastName}";
    }
}
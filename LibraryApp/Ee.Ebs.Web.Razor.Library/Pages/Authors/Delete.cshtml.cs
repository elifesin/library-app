using AutoMapper;
using Ee.Ebs.Application.Contracts.Authors;
using Ee.Ebs.Application.Contracts.Authors.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Pages.Authors;

public class DeleteModel : PageModel
{
    private readonly IAuthorService _authorService;
    private readonly IMapper _objectMapper;

    [BindProperty] 
    public AuthorVm Vm { get; set; }

    public DeleteModel(IAuthorService authorService, IMapper objectMapper)
    {
        _authorService = authorService;
        _objectMapper = objectMapper;
    }
    
    [HttpGet]
    public IActionResult OnGet(int id)
    {
        var dto = _authorService.GetById(id);

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
        _authorService.Delete(id);
 
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
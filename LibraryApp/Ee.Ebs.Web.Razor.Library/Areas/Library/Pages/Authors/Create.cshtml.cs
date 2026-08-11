using AutoMapper;
using Ee.Ebs.Application.Contracts.Authors;
using Ee.Ebs.Application.Contracts.Authors.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Authors;

public class CreateModel : PageModel
{
    private readonly IAuthorAppService _authorAppService;
    private readonly IMapper _objectMapper;

    [BindProperty] 
    public AuthorCreateVm Vm { get; set; }

    public CreateModel(IAuthorAppService authorAppService, IMapper objectMapper)
    {
        _authorAppService = authorAppService;
        _objectMapper = objectMapper;
    }

    public IActionResult OnGet()
    {
        Vm = new AuthorCreateVm();

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var authorDto = _objectMapper.Map<AuthorCreateDto>(Vm);
        
        _authorAppService.Insert(authorDto);
        
        return RedirectToPage("./Index");
    }

    public class AuthorCreateVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
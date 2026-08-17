using AutoMapper;
using Ee.Ebs.Application.Contracts.Authors;
using Ee.Ebs.Application.Contracts.Authors.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Authors;

public class EditModel : PageModel
{
    private readonly IAuthorAppService _authorAppService;
    private readonly IMapper _mapper;

    public EditModel(IAuthorAppService authorAppService, IMapper mapper)
    {
        _authorAppService = authorAppService;
        _mapper = mapper;
    }

    public int Id { get; set; }

    [BindProperty] public AuthorCreateOrEditVm Vm { get; set; }

    public IActionResult OnGet(int id)
    {
        Id = id;
        
        var author = _authorAppService.GetById(id);
        if (author == null)
        {
            return NotFound();
        }
        
        Vm = _mapper.Map<AuthorCreateOrEditVm>(author);

        return Page();
    }

    public IActionResult OnPost(int id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var updateAuthorDto = _mapper.Map<AuthorEditDto>(Vm);
        updateAuthorDto.Id = id;
        
        _authorAppService.Update(updateAuthorDto);

        return RedirectToPage("./Index");
    }
}
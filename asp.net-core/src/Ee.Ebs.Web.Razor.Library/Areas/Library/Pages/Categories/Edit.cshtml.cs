using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Ee.Ebs.Application.Contracts.Categories;
using Ee.Ebs.Application.Contracts.Categories.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Categories;

public class EditModel : PageModel
{
    private readonly ICategoryAppService _categoryAppService;
    private readonly IMapper _mapper;

    public int Id { get; set; }
    
    [BindProperty] public CategoryCreateOrEditVm Vm { get; set; }
    
    public EditModel(IMapper mapper, ICategoryAppService categoryAppService)
    {
        _mapper = mapper;
        _categoryAppService = categoryAppService;
    }

    [HttpGet]
    public IActionResult OnGet(int id)
    {
        Id = id;
        var categoryDto =  _categoryAppService.GetById(id);

        if (categoryDto == null)
        {
            return NotFound();
        }
        Vm = _mapper.Map<CategoryCreateOrEditVm>(categoryDto);
        
        
        return Page();
    }

    [HttpPost]
    public IActionResult OnPost(int id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var categoryDto = _mapper.Map<CategoryDto>(Vm);
        categoryDto.Id = id;
        
        _categoryAppService.Update(id, categoryDto);
        
        return RedirectToPage("./Index");
    }
}
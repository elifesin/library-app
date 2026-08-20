using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Ee.Ebs.Application.Contracts.Categories;
using Ee.Ebs.Application.Contracts.Categories.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Categories;

public class CreateModel : PageModel
{
    private readonly ICategoryAppService _categoryAppService;
    private readonly IMapper _mapper;
    
    [BindProperty]
    public CategoryCreateOrEditVm Vm { get; set; }
    
    public CreateModel(ICategoryAppService categoryAppService, IMapper mapper)
    {
        _categoryAppService = categoryAppService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public IActionResult OnGet()
    {
        Vm = new CategoryCreateOrEditVm();

        return Page();
    }

    [HttpPost]
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var categoryDto = _mapper.Map<CategoryDto>(Vm);
            _categoryAppService.Insert(categoryDto);
        
            return RedirectToPage("./Index");
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
    }
    
}
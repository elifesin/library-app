using AutoMapper;
using Ee.Ebs.Application.Contracts.Categories;
using Ee.Ebs.Application.Contracts.Categories.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Pages.Categories;

public class CreateModel : PageModel
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    
    [BindProperty]
    public CategoryVm Vm { get; set; }
    
    public CreateModel(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public IActionResult OnGet()
    {
        Vm = new CategoryVm();

        return Page();
    }

    [HttpPost]
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var categoryDto = _mapper.Map<CategoryDto>(Vm);
        _categoryService.Insert(categoryDto);
        
        return RedirectToPage("./Index");
    }
    
    public class CategoryVm
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
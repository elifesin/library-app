using AutoMapper;
using Ee.Ebs.Application.Contracts.Categories;
using Ee.Ebs.Application.Contracts.Categories.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Pages.Categories;

public class EditModel : PageModel
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    [BindProperty]
    public CategoryVm Vm { get; set; }
    
    public EditModel(IMapper mapper, ICategoryService categoryService)
    {
        _mapper = mapper;
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult OnGet(int id)
    {
        var categoryDto =  _categoryService.GetById(id);

        if (categoryDto == null)
        {
            return NotFound();
        }
        Vm = _mapper.Map<CategoryVm>(categoryDto);
        
        
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
        _categoryService.Update(categoryDto);
        
        return RedirectToPage("./Index");
    }
    
    public class CategoryVm
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
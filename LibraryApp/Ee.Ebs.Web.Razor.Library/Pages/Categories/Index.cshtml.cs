using AutoMapper;
using Ee.Ebs.Application.Contracts.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    public IndexModel(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }
    
    [BindProperty]
    public List<CategoryVm>  Categories { get; set; }
    
    [HttpGet]
    public IActionResult OnGet()
    {
        var categoryDtos = _categoryService.GetAll();
        Categories = _mapper.Map<List<CategoryVm>>(categoryDtos);

        return Page();
    }
    
    public class CategoryVm
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
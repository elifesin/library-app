using System.Collections.Generic;
using AutoMapper;
using Ee.Ebs.Application.Contracts.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly ICategoryAppService _categoryAppService;
    private readonly IMapper _mapper;
    public IndexModel(ICategoryAppService categoryAppService, IMapper mapper)
    {
        _categoryAppService = categoryAppService;
        _mapper = mapper;
    }
    
    [BindProperty]
    public List<CategoryVm>  Categories { get; set; }
    
    [HttpGet]
    public IActionResult OnGet()
    {
        var categoryDtos = _categoryAppService.GetAll();
        Categories = _mapper.Map<List<CategoryVm>>(categoryDtos);

        return Page();
    }

    [HttpPost]
    public IActionResult OnPostDelete(int id)
    {
        _categoryAppService.Delete(id);
        
        return RedirectToPage();
    }
    
    public class CategoryVm
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
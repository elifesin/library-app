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

    [BindProperty]
    public CategoryVm Vm { get; set; }
    
    public EditModel(IMapper mapper, ICategoryAppService categoryAppService)
    {
        _mapper = mapper;
        _categoryAppService = categoryAppService;
    }

    [HttpGet]
    public IActionResult OnGet(int id)
    {
        var categoryDto =  _categoryAppService.GetById(id);

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
        _categoryAppService.Update(categoryDto);
        
        return RedirectToPage("./Index");
    }
    
    public class CategoryVm
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
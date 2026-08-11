using AutoMapper;
using Ee.Ebs.Application.Contracts.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Categories;

public class DeleteModel : PageModel
{
    private readonly ICategoryAppService _categoryAppService;
    private readonly IMapper _mapper;

    [BindProperty]
    public CategoryVm Vm { get; set; }
    
    public DeleteModel(IMapper mapper, ICategoryAppService categoryAppService)
    {
        _mapper = mapper;
        _categoryAppService = categoryAppService;
    }
    
    [HttpGet]
    public IActionResult OnGet(int id)
    {
        var categoryDto = _categoryAppService.GetById(id);
        
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
        _categoryAppService.Delete(Vm.Id);
        return RedirectToPage("./Index");
    }
    
    public class CategoryVm
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
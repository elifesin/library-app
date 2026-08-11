using AutoMapper;
using Ee.Ebs.Application.Contracts.Publishers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Publishers;

public class DeleteModel : PageModel
{
    private readonly IPublisherAppService _publisherAppService;
    private readonly IMapper _autoMapper;

    public DeleteModel(IMapper autoMapper, IPublisherAppService publisherAppService)
    {
        _autoMapper = autoMapper;
        _publisherAppService = publisherAppService;
    }

    [BindProperty]
    public PublisherVm Vm { get; set; }
    
    [HttpGet]
    public IActionResult OnGet(int id)
    {
        var publisherDto = _publisherAppService.GetById(id);

        if (publisherDto == null)
        {
            return NotFound();
        }
        
        Vm = _autoMapper.Map<PublisherVm>(publisherDto);
        return Page();
    }

    [HttpPost]
    public IActionResult OnPost()
    { 
        _publisherAppService.Delete(Vm.Id); 
        return RedirectToPage("./Index");
    }
    
    public class PublisherVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
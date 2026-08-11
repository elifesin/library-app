using AutoMapper;
using Ee.Ebs.Application.Contracts.Publishers;
using Ee.Ebs.Application.Contracts.Publishers.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Publishers;

public class EditModel : PageModel
{
    private readonly IPublisherAppService _publisherAppService;
    private readonly IMapper _mapper;

    [BindProperty]
    public PublisherVm Vm { get; set; }
    
    public EditModel(IMapper mapper, IPublisherAppService publisherAppService)
    {
        _mapper = mapper;
        _publisherAppService = publisherAppService;
    }
    
    [HttpGet]
    public IActionResult OnGet(int id)
    {
        var publisherDto =  _publisherAppService.GetById(id);

        if (publisherDto == null)
        {
            return NotFound();
        }
        
        Vm = _mapper.Map<PublisherVm>(publisherDto);
        return Page();
    }

    [HttpPost]
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var publisherDto = _mapper.Map<PublisherDto>(Vm);
        _publisherAppService.Update(publisherDto);
        
        return RedirectToPage("./Index");
    }
    
    public class PublisherVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
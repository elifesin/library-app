using AutoMapper;
using Ee.Ebs.Application.Contracts.Publishers;
using Ee.Ebs.Application.Contracts.Publishers.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Pages.Publishers;

public class EditModel : PageModel
{
    private readonly IPublisherService _publisherService;
    private readonly IMapper _mapper;

    [BindProperty]
    public PublisherVm Vm { get; set; }
    
    public EditModel(IMapper mapper, IPublisherService publisherService)
    {
        _mapper = mapper;
        _publisherService = publisherService;
    }
    
    [HttpGet]
    public IActionResult OnGet(int id)
    {
        var publisherDto =  _publisherService.GetById(id);

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
        _publisherService.Update(publisherDto);
        
        return RedirectToPage("./Index");
    }
    
    public class PublisherVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
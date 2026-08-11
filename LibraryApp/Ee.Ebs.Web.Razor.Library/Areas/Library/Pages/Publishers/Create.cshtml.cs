using AutoMapper;
using Ee.Ebs.Application.Contracts.Publishers;
using Ee.Ebs.Application.Contracts.Publishers.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Publishers;

public class CreateModel : PageModel
{
    private readonly IPublisherAppService _publisherAppService;
    private readonly IMapper _mapper;
    
    [BindProperty]
    public PublisherVm Vm { get; set; }

    public CreateModel(IPublisherAppService publisherAppService, IMapper mapper)
    {
        _publisherAppService = publisherAppService;
        _mapper = mapper;
    }
    [HttpGet]
    public IActionResult OnGet()
    {
        Vm = new PublisherVm();
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
        _publisherAppService.Insert(publisherDto);
        
        return RedirectToPage("./Index");
    }
    public class PublisherVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
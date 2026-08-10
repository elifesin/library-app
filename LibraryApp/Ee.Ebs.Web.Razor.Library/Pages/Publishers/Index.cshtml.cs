using AutoMapper;
using Ee.Ebs.Application.Contracts.Publishers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Pages.Publishers;

public class IndexModel : PageModel
{
    private readonly IPublisherService _publisherService;
    private readonly IMapper _mapper;
    
    [BindProperty]
    public List<PublisherVm> Vm { get; set; }

    public IndexModel(IPublisherService publisherService, IMapper mapper)
    {
        _publisherService = publisherService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public IActionResult OnGet()
    {
        var publishers  = _publisherService.GetAll();
        Vm = _mapper.Map<List<PublisherVm>>(publishers);

        return Page();
    }
    
    public class PublisherVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
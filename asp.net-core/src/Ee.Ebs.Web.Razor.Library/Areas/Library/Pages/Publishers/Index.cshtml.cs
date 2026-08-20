using System.Collections.Generic;
using AutoMapper;
using Ee.Ebs.Application.Contracts.Publishers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Publishers;

public class IndexModel : PageModel
{
    private readonly IPublisherAppService _publisherAppService;
    private readonly IMapper _mapper;
    
    [BindProperty]
    public List<PublisherVm> Vm { get; set; }

    public IndexModel(IPublisherAppService publisherAppService, IMapper mapper)
    {
        _publisherAppService = publisherAppService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public IActionResult OnGet()
    {
        var publishers  = _publisherAppService.GetAll();
        Vm = _mapper.Map<List<PublisherVm>>(publishers);

        return Page();
    }

    [HttpPost]
    public IActionResult OnPostDelete(int id)
    {
        _publisherAppService.Delete(id);
        
        return RedirectToPage();
    }
    
    public class PublisherVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
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

    public int Id { get; set; }
    
    [BindProperty] public PublisherCreateOrEditVm Vm { get; set; }
    
    public EditModel(IMapper mapper, IPublisherAppService publisherAppService)
    {
        _mapper = mapper;
        _publisherAppService = publisherAppService;
    }
    
    [HttpGet]
    public IActionResult OnGet(int id)
    {
        Id = id;
        var publisherDto =  _publisherAppService.GetById(id);

        if (publisherDto == null)
        {
            return NotFound();
        }
        
        Vm = _mapper.Map<PublisherCreateOrEditVm>(publisherDto);
        
        return Page();
    }

    [HttpPost]
    public IActionResult OnPost(int id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var publisherDto = _mapper.Map<PublisherDto>(Vm);
        publisherDto.Id = id;
        
        _publisherAppService.Update(id, publisherDto);
        
        return RedirectToPage("./Index");
    }
}
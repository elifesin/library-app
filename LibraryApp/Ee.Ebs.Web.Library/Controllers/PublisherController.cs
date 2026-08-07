using AutoMapper;
using Ee.Ebs.Application.Publishers;
using Ee.Ebs.Application.Publishers.DTOs;
using Ee.Ebs.Web.Library.ViewModels.Publisher;
using Microsoft.AspNetCore.Mvc;

namespace Ee.Ebs.Web.Library.Controllers;

public class PublisherController : Controller
{
    private readonly IPublisherService _publisherService;
    private readonly IMapper _mapper;

    public PublisherController(IPublisherService publisherService, IMapper mapper)
    {
        _publisherService = publisherService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var publisherDtos = _publisherService.GetAll();
        var publisherViewModels = _mapper.Map<List<PublisherVm>>(publisherDtos);
        
        return View(publisherViewModels);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new PublisherVm());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PublisherVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        
        var publisherDto = _mapper.Map<PublisherDto>(vm);
        _publisherService.Insert(publisherDto);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var publisherDto = _publisherService.GetById(id);
        if (publisherDto == null) return NotFound();
        
        var vm = _mapper.Map<PublisherVm>(publisherDto);
        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(PublisherVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        
        var publisherDto = _mapper.Map<PublisherDto>(vm);
        _publisherService.Update(publisherDto);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id) 
    {
        var publisherDto = _publisherService.GetById(id);
        if (publisherDto == null) return NotFound();
        
        var vm = _mapper.Map<PublisherVm>(publisherDto);
        return View(vm);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _publisherService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
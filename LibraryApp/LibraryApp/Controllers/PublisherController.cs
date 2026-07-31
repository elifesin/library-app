using AutoMapper;
using Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using LibraryApp.Models.Publisher;

namespace LibraryApp.Controllers;

public class PublisherController : Controller
{
    private readonly PublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public PublisherController(PublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var publisherEntities = _publisherRepository.GetAllPublishers();
        var publisherVms = _mapper.Map<List<PublisherVm>>(publisherEntities);
        return View(publisherVms);
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
        
        var publisher =  _mapper.Map<Publisher>(vm);
        _publisherRepository.Insert(publisher);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var vm = _publisherRepository.GetPublisherById(id);
        
        if (vm == null)
        {
            return NotFound();
        }
        
        var publisher = _mapper.Map<PublisherVm>(vm);
        
        return View(publisher);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(PublisherVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        
        var publisher = _mapper.Map<Publisher>(vm);
        _publisherRepository.Update(publisher);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var vm = _publisherRepository.GetPublisherById(id);
        
        if (vm == null)
        {
            return NotFound();
        }
        
        var publisher = _mapper.Map<PublisherVm>(vm);
        
        return View(publisher);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    { 
        _publisherRepository.Delete(id);
        
        return RedirectToAction(nameof(Index));
    }
}
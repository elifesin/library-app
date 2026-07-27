using Microsoft.AspNetCore.Mvc;
using LibraryApp.Data;
using LibraryApp.Models.Publisher;

namespace LibraryApp.Controllers;

public class PublisherController : Controller
{
    private readonly PublisherRepository _publisherRepository;

    public PublisherController(PublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        List<PublisherVm> vmList = _publisherRepository.GetAllPublishers();
        return View(vmList);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PublisherVm vm)
    {
        if (ModelState.IsValid)
        {
            _publisherRepository.Insert(vm);
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var vm = _publisherRepository.GetPublisherById(id);
        if (vm == null)
        {
            return NotFound();
        }
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(PublisherVm vm)
    {
        if (ModelState.IsValid)
        {
            _publisherRepository.Update(vm);
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var vm = _publisherRepository.GetPublisherById(id);
        if (vm == null)
        {
            return NotFound();
        }
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
            _publisherRepository.Delete(id);
            return RedirectToAction(nameof(Index));
    }
}
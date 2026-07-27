using LibraryApp.Data;
using Microsoft.AspNetCore.Mvc;
using LibraryApp.Models.Author;

namespace LibraryApp.Controllers;

public class AuthorController : Controller
{
    private readonly AuthorRepository _authorRepository;

    public AuthorController(AuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        List<AuthorVm> vmList = _authorRepository.GetAllAuthors();
        return View(vmList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AuthorCreateVm vm)
    {
        if (ModelState.IsValid)
        {
            _authorRepository.Insert(vm);
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var author = _authorRepository.GetAuthorById(id);
        if (author == null)
        {
            return NotFound();
        }
        
        var vm = new AuthorEditVm
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AuthorEditVm vm)
    {
        if (ModelState.IsValid)
        {
            _authorRepository.Update(vm);
            return RedirectToAction(nameof(Index));
        }

        return View(vm);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var vm = _authorRepository.GetAuthorById(id);
        if (vm == null)
        {
            return NotFound();
        }
            
        return View(vm);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _authorRepository.Delete(id);
        
        return RedirectToAction(nameof(Index));
    }
    
    
}
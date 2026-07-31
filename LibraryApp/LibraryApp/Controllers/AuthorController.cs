using AutoMapper;
using Entities;
using Data.Repositories;
using LibraryApp.Models.Author;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

public class AuthorController : Controller
{
    private readonly AuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public AuthorController(AuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var authorEntities = _authorRepository.GetAllAuthors();
        
        var authorViewModels = _mapper.Map<List<AuthorVm>>(authorEntities);
        
        return View(authorViewModels);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new AuthorCreateVm());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AuthorCreateVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        
        var authorEntity = _mapper.Map<Author>(vm);
        _authorRepository.Insert(authorEntity);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var author = _authorRepository.GetAuthorById(id);
        
        if (author == null)
        {
            return NotFound();
        }
        
        var authorVm = _mapper.Map<AuthorEditVm>(author);
        
        return View(authorVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AuthorEditVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var authorEntity = _mapper.Map<Author>(vm);
        _authorRepository.Update(authorEntity);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var author = _authorRepository.GetAuthorById(id);
        
        if (author == null)
        {
            return NotFound();
        }
        
        var authorVm = _mapper.Map<AuthorVm>(author);

        return View(authorVm);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _authorRepository.Delete(id);
        
        return RedirectToAction(nameof(Index));
    }
}
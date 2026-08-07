using AutoMapper;
using Ee.Ebs.Application.Authors;
using Ee.Ebs.Application.Contracts.Authors;
using Ee.Ebs.Application.Contracts.Authors.DTOs;
using Ee.Ebs.Web.Library.ViewModels.Author;
using Microsoft.AspNetCore.Mvc;

namespace Ee.Ebs.Web.Library.Controllers;

public class AuthorController : Controller
{
    private readonly IAuthorService _authorService;
    private  readonly IMapper _mapper;
    public AuthorController(IAuthorService authorService, IMapper mapper)
    {
        _authorService = authorService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var authorDtos = _authorService.GetAll();
        var authorViewModels = _mapper.Map<List<AuthorVm>>(authorDtos);
        
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
        
        var authorDto = _mapper.Map<AuthorCreateDto>(vm);
        _authorService.Insert(authorDto);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var authorDto = _authorService.GetById(id);
        
        if (authorDto == null)
        {
            return NotFound();
        }
        
        // Düzenleme ekranında göstermek üzere DTO, View'ın anladığı UpdateVm'ye dönüştürülür
        var authorVm = _mapper.Map<AuthorEditVm>(authorDto);
        
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
        
        var authorDto = _mapper.Map<AuthorEditDto>(vm);
        _authorService.Update(authorDto);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var authorDto = _authorService.GetById(id);
    
        if (authorDto == null)
        {
            return NotFound();
        }
    
        var authorVm = _mapper.Map<AuthorVm>(authorDto);
    
        return View(authorVm);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _authorService.Delete(id);
    
        return RedirectToAction(nameof(Index));
    }
}
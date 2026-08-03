using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using LibraryApp.Models.Category;

namespace LibraryApp.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var categoryEntities = _categoryRepository.GetAll();
        var categoryViewModels = _mapper.Map<List<CategoryVm>>(categoryEntities);
        return View(categoryViewModels);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CategoryVm());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CategoryVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        var categoryEntity = _mapper.Map<Category>(vm);
        _categoryRepository.Insert(categoryEntity);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var vm = _categoryRepository.GetById(id);
        
        if (vm == null) 
        {
            return NotFound();
        }
        
        var category =  _mapper.Map<CategoryVm>(vm);
        
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CategoryVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        
        var categoryEntity = _mapper.Map<Category>(vm);
        _categoryRepository.Update(categoryEntity);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    { 
        var vm = _categoryRepository.GetById(id);
        
        if (vm == null) 
        {
            return NotFound();
        }
        
        var category = _mapper.Map<CategoryVm>(vm);
        
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _categoryRepository.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
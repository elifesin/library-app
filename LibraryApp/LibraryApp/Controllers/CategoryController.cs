using LibraryApp.Data;
using Microsoft.AspNetCore.Mvc;
using LibraryApp.Models.Category;

namespace LibraryApp.Controllers;

public class CategoryController : Controller
{
    // Artık connection string'e ihtiyacımız yok, sadece Repository'yi kullanacağız.
    private readonly CategoryRepository _categoryRepository;

    public CategoryController(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<CategoryVm> vmList = _categoryRepository.GetAll();
        return View(vmList);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CategoryVm vm)
    {
        if (ModelState.IsValid)
        {
            _categoryRepository.Insert(vm);
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var vm = _categoryRepository.GetById(id);
        
        if (vm == null) 
        {
            return NotFound();
        }
        
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CategoryVm vm)
    {
        if (ModelState.IsValid)
        {
            _categoryRepository.Update(vm);
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    { 
        var vm = _categoryRepository.GetById(id);
        
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
        _categoryRepository.Delete(id);
        
        return RedirectToAction(nameof(Index));
    }
}
using AutoMapper;
using Ee.Ebs.Application.Categories;
using Ee.Ebs.Application.Categories.DTOs;
using Ee.Ebs.Web.Library.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace Ee.Ebs.Web.Library.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    public CategoryController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        // 1. Servisten (Ee.Ebs.Application katmanından) DTO listesini çekiyoruz
        var categoryDtos = _categoryService.GetAll();
        
        // 2. View'ın (UI katmanının) anladığı VM listesine dönüştürüyoruz
        var categoryViewModels = _mapper.Map<List<CategoryVm>>(categoryDtos);
        
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
        
        // Ekrandan gelen VM, servisin beklediği DTO'ya dönüştürülüyor
        var categoryDto = _mapper.Map<CategoryDto>(vm);
        
        // Servise iletiyoruz (İçeride Entity'e çevrilip IsActive=true vb. yapılarak eklenecek)
        _categoryService.Insert(categoryDto);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var categoryDto = _categoryService.GetById(id);
        
        if (categoryDto == null)
        {
            return NotFound();
        }
        
        // Ekranda (View) göstermek için DTO'yu UpdateVm'e (veya EditVm) dönüştürüyoruz
        var vm = _mapper.Map<CategoryVm>(categoryDto);
        
        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CategoryVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        
        // Güncellenmiş VM'i, servise göndermek üzere DTO'ya çeviriyoruz
        var categoryDto = _mapper.Map<CategoryDto>(vm);
        
        _categoryService.Update(categoryDto);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id) 
    {
        var categoryDto = _categoryService.GetById(id);
        
        if (categoryDto == null)
        {
            return NotFound();
        }
        
        // Emin misiniz? ekranı için DTO'yu VM'e çevirip gönderiyoruz
        var vm = _mapper.Map<CategoryVm>(categoryDto); // veya sadece CategoryVm kullanıyorsanız o
        
        return View(vm);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        // Sadece ID bilgisini servise iletiyoruz. 
        // Yeni bir entity örneği (new Category { Id = id }) yaratma işini controller'dan kaldırdık.
        _categoryService.Delete(id);
        
        return RedirectToAction(nameof(Index));
    }
}
using AutoMapper;
using Ee.Ebs.Application.Contracts.Authors;
using Ee.Ebs.Application.Contracts.Books;
using Ee.Ebs.Application.Contracts.Books.DTOs;
using Ee.Ebs.Application.Contracts.Categories;
using Ee.Ebs.Application.Contracts.Publishers;
using Ee.Ebs.Application.Publishers;
using Ee.Ebs.Web.Library.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;

namespace Ee.Ebs.Web.Library.Controllers;

public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;
    private readonly ICategoryService _categoryService;
    private readonly IPublisherService _publisherService; 
    private readonly IMapper _mapper;

    public BookController(
        IAuthorService authorService, 
        ICategoryService categoryService, 
        IBookService bookService, 
        IPublisherService publisherService, 
        IMapper mapper)
    {
        _authorService = authorService;
        _categoryService = categoryService;
        _bookService = bookService;
        _publisherService = publisherService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var bookDtos = _bookService.GetAll();
        
        var bookViewModels = _mapper.Map<List<BookVm>>(bookDtos);
        
        ViewBag.Authors = _authorService.GetAll();
        ViewBag.Categories = _categoryService.GetAll();
        
        return View(bookViewModels);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Authors = _authorService.GetAll();
        ViewBag.Categories = _categoryService.GetAll();
        ViewBag.Publishers = _publisherService.GetAll();
       
        return View(new BookCreateVm());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BookCreateVm vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Authors = _authorService.GetAll();
            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Publishers = _publisherService.GetAll();
            return View(vm);
        }
        
        // UI'dan gelen VM, Ee.Ebs.Application katmanının anladığı DTO'ya dönüştürülüyor
        var bookDto = _mapper.Map<BookCreateDto>(vm);
        
        // DTO'yu servise gönderiyoruz. 
        _bookService.Insert(bookDto);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var bookDto = _bookService.GetById(id);
        
        if (bookDto == null)
        {
            return NotFound();
        }
        
        // View'a DTO değil, VM gönderiyoruz
        var vm = _mapper.Map<BookEditVm>(bookDto);

        ViewBag.Authors = _authorService.GetAll();
        ViewBag.Publishers = _publisherService.GetAll();
        ViewBag.Categories = _categoryService.GetAll();
        
        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(BookEditVm vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Authors = _authorService.GetAll();
            ViewBag.Publishers = _publisherService.GetAll();
            ViewBag.Categories = _categoryService.GetAll();
            
            return View(vm);
        }
        
        // VM'den DTO'ya dönüşüm
        var bookDto = _mapper.Map<BookEditDto>(vm); 
        
        _bookService.Update(bookDto);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id) 
    {
        var bookDto = _bookService.GetById(id);
        
        if (bookDto == null)
        {
            return NotFound();
        }
        
        // Emin misiniz ekranı için DTO -> VM
        var vm = _mapper.Map<BookDeleteVm>(bookDto);
        
        return View(vm);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        // Controller sadece Id'yi servise iletiyor
        _bookService.Delete(id);
        
        return RedirectToAction(nameof(Index));
    }
}
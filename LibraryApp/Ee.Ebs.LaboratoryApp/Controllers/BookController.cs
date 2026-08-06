using AutoMapper;
using Ee.Ebs.Application.Books;
using Ee.Ebs.Application.Authors;
using Ee.Ebs.Application.Categories;
using Ee.Ebs.LaboratoryApp.Models.Book;
using Microsoft.AspNetCore.Mvc;

namespace Ee.Ebs.LaboratoryApp.Controllers;

public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;
    private readonly IAuthorService _authorService;
    private readonly ICategoryService _categoryService;

    public BookController(IBookService bookService, 
        IMapper mapper, IAuthorService authorService, 
        ICategoryService categoryService)
    {
        _bookService = bookService;
        _mapper = mapper;
        _authorService = authorService;
        _categoryService = categoryService;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var bookDtos = _bookService.GetAll();
        
        // 2. Ekranda göstermek için DTO'yu View Model'e dönüştürüyoruz
        var bookViewModels = _mapper.Map<List<BookListVm>>(bookDtos);
        
        ViewBag.Authors = _authorService.GetAll();
        ViewBag.Categories = _categoryService.GetAll();
        
        return View(bookViewModels);
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Domain.Repositories;
using LaboratoryApp.Models.Book;

namespace LaboratoryApp.Controllers;

public class BookController : Controller
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _authorRepository;

    public BookController(IBookRepository bookRepository, IMapper mapper, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _authorRepository = authorRepository;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var bookEntities = _bookRepository.GetAll();
        var bookViewModel = _mapper.Map<List<BookListVm>>(bookEntities);
        
        ViewBag.Authors = _authorRepository.GetAll();
        
        return View(bookViewModel);
    }
}
using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Repositories;
using LaboratoryApp.Models.Book;

namespace LaboratoryApp.Controllers;

public class BookController : Controller
{
    private readonly BookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly AuthorRepository _authorRepository;

    public BookController(BookRepository bookRepository, IMapper mapper, AuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _authorRepository = authorRepository;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var bookEntities = _bookRepository.GetAllBooks();
        var bookViewModel = _mapper.Map<List<BookListVm>>(bookEntities);
        
        ViewBag.Authors = _authorRepository.GetAllAuthors();
        
        return View(bookViewModel);
    }
}
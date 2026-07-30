using AutoMapper;
using Data.Entities;
using Data.Repositories;
using LibraryApp.Models.Book;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

public class BookController : Controller
{
    private readonly BookRepository _bookRepository;
    private readonly AuthorRepository _authorRepository;
    private readonly CategoryRepository _categoryRepository;
    private readonly PublisherRepository _publisherRepository; 
    private readonly IMapper _mapper;
    public BookController(AuthorRepository authorRepository, CategoryRepository categoryRepository, BookRepository bookRepository, PublisherRepository publisherRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _categoryRepository = categoryRepository;
        _bookRepository = bookRepository;
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var bookEntities = _bookRepository.GetAllBooks();
        var bookViewModels = _mapper.Map<List<BookVm>>(bookEntities);
        
        ViewBag.Authors = _authorRepository.GetAllAuthors();
        ViewBag.Categories = _categoryRepository.GetAll();
        
        return View(bookViewModels);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Authors = _authorRepository.GetAllAuthors();
        ViewBag.Categories = _categoryRepository.GetAll();
        ViewBag.Publishers = _publisherRepository.GetAllPublishers();
       
        return View(new BookCreateVm());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BookCreateVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        
        ViewBag.Authors = _authorRepository.GetAllAuthors();
        ViewBag.Categories = _categoryRepository.GetAll();
        ViewBag.Publishers = _publisherRepository.GetAllPublishers();
        
        var bookEntity = _mapper.Map<Book>(vm);
        _bookRepository.Insert(bookEntity);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var book = _bookRepository.GetBookById(id);
        
        if (book == null)
        {
            return NotFound();
        }
        
        var vm = _mapper.Map<BookEditVm>(book);

        ViewBag.Authors = _authorRepository.GetAllAuthors();
        ViewBag.Publishers = _publisherRepository.GetAllPublishers();
        ViewBag.Categories = _categoryRepository.GetAll();
        
        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(BookEditVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        
        var bookEntity = _mapper.Map<Book>(vm);
        _bookRepository.Update(bookEntity);
        
        ViewBag.Authors = _authorRepository.GetAllAuthors();
        ViewBag.Publishers = _publisherRepository.GetAllPublishers();
        ViewBag.Categories = _categoryRepository.GetAll();
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id) 
    {
        var book = _bookRepository.GetBookById(id);
        
        if (book == null)
        {
            return NotFound();
        }
        
        var vm = _mapper.Map<BookDeleteVm>(book);
        
        return View(vm);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var book = new Book { Id = id };
        _bookRepository.Delete(book);
        
        return RedirectToAction(nameof(Index));
    }
}
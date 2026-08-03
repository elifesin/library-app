using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using LibraryApp.Models.Book;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

public class BookController : Controller
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPublisherRepository _publisherRepository; 
    private readonly IMapper _mapper;
    public BookController(IAuthorRepository authorRepository, 
        ICategoryRepository categoryRepository, 
        IBookRepository bookRepository, 
        IPublisherRepository publisherRepository, 
        IMapper mapper)
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
        var bookEntities = _bookRepository.GetAll();
        var bookViewModels = _mapper.Map<List<BookVm>>(bookEntities);
        
        ViewBag.Authors = _authorRepository.GetAll();
        ViewBag.Categories = _categoryRepository.GetAll();
        
        return View(bookViewModels);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Authors = _authorRepository.GetAll();
        ViewBag.Categories = _categoryRepository.GetAll();
        ViewBag.Publishers = _publisherRepository.GetAll();
       
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
        
        ViewBag.Authors = _authorRepository.GetAll();
        ViewBag.Categories = _categoryRepository.GetAll();
        ViewBag.Publishers = _publisherRepository.GetAll();
        
        var bookEntity = _mapper.Map<Book>(vm);
        bookEntity.IsActive = true;
        _bookRepository.Insert(bookEntity);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var book = _bookRepository.GetById(id);
        
        if (book == null)
        {
            return NotFound();
        }
        
        var vm = _mapper.Map<BookEditVm>(book);

        ViewBag.Authors = _authorRepository.GetAll();
        ViewBag.Publishers = _publisherRepository.GetAll();
        ViewBag.Categories = _categoryRepository.GetAll();
        
        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(BookEditVm vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Authors = _authorRepository.GetAll();
            ViewBag.Publishers = _publisherRepository.GetAll();
            ViewBag.Categories = _categoryRepository.GetAll();
            
            return View(vm);
        }
        
        var bookEntity = _mapper.Map<Book>(vm);
        bookEntity.IsActive = true;
        _bookRepository.Update(bookEntity);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id) 
    {
        var book = _bookRepository.GetById(id);
        
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
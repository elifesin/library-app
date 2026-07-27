using LibraryApp.Data;
using Microsoft.AspNetCore.Mvc;
using LibraryApp.Models.Book;

namespace LibraryApp.Controllers;

public class BookController : Controller
{
    private readonly BookRepository _bookRepository;
    private readonly AuthorRepository _authorRepository;
    private readonly CategoryRepository _categoryRepository;

    public BookController(AuthorRepository authorRepository, CategoryRepository categoryRepository, BookRepository bookRepository)
    {
        _authorRepository = authorRepository;
        _categoryRepository = categoryRepository;
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<BookVm> vmList = _bookRepository.GetAllBooks();
        
        ViewBag.Authors = _authorRepository.GetAllAuthors();
        ViewBag.Categories = _categoryRepository.GetAll();
        
        return View(vmList);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Authors = _authorRepository.GetAllAuthors();
        ViewBag.Categories = _categoryRepository.GetAll();
       
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BookCreateVm vm)
    {
        if (ModelState.IsValid)
        {
            _bookRepository.Insert(vm);
            return RedirectToAction(nameof(Index));
        }
        
        ViewBag.Authors = _authorRepository.GetAllAuthors();
        ViewBag.Categories = _categoryRepository.GetAll();
        return View(vm);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var book = _bookRepository.GetBookById(id);
        
        if (book == null)
        {
            return NotFound();
        }

        var vm = new BookEditVm
        {
            Id = book.Id,
            Title = book.Title,
            IsBorrowed =book.IsBorrowed
        };
        
        ViewBag.Authors = _authorRepository.GetAllAuthors();
        
        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(BookEditVm vm)
    {
        if (ModelState.IsValid)
        {
            _bookRepository.Update(vm);
            return RedirectToAction(nameof(Index));
        }
        
        ViewBag.Authors = _authorRepository.GetAllAuthors();
        return View(vm);
    }

    [HttpGet]
    public IActionResult Delete(int id) 
    {
        var book = _bookRepository.GetBookById(id);
        
        if (book == null)
        {
            return NotFound();
        }
        var vm = new BookDeleteVm
        {
            Id = book.Id
        };
        
        return View(vm);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var vm = new BookDeleteVm { Id = id };
        
        _bookRepository.Delete(vm);
        return RedirectToAction(nameof(Index));
    }
}
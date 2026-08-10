using AutoMapper;
using Ee.Ebs.Application.Contracts.Authors;
using Ee.Ebs.Application.Contracts.Authors.DTOs;
using Ee.Ebs.Application.Contracts.Books;
using Ee.Ebs.Application.Contracts.Books.DTOs;
using Ee.Ebs.Application.Contracts.Categories;
using Ee.Ebs.Application.Contracts.Categories.DTOs;
using Ee.Ebs.Application.Contracts.Publishers;
using Ee.Ebs.Application.Contracts.Publishers.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Pages.Books;

public class EditModel : PageModel
{
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;
    private readonly ICategoryService _categoryService;
    private readonly IPublisherService _publisherService;
    private readonly IMapper _objectMapper;

    public EditModel(
        IBookService bookService,
        IAuthorService authorService,
        ICategoryService categoryService,
        IPublisherService publisherService,
        IMapper objectMapper)
    {
        _bookService = bookService;
        _authorService = authorService;
        _categoryService = categoryService;
        _publisherService = publisherService;
        _objectMapper = objectMapper;
    }

    [BindProperty]
    public BookEditVm Vm { get; set; } = new();

    public List<AuthorDto> Authors { get; set; } = new();
    public List<CategoryDto> Categories { get; set; } = new();
    public List<PublisherDto> Publishers { get; set; } = new();
    
    [HttpGet]
    public IActionResult OnGet(int id)
    {
        var bookDto = _bookService.GetById(id);

        if (bookDto == null)
        {
            return NotFound(); 
        }
        
        Vm = _objectMapper.Map<BookEditVm>(bookDto);

        LoadData();
            
        return Page();
    }
    
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {  
            LoadData();
            return Page();
        }
        
        var bookDto = _objectMapper.Map<BookEditDto>(Vm); 
            
        _bookService.Update(bookDto);

        return RedirectToPage("./Index");
    }
    
    private void LoadData()
    {
        Authors = _authorService.GetAll().ToList();
        Categories = _categoryService.GetAll().ToList();
        Publishers = _publisherService.GetAll().ToList();
    }
    public class BookEditVm
    {
        public int Id { get; set; }
        public int AuthorID { get; set; }
        public string Title { get; set; }
        public int PublishYear { get; set; }
        public bool IsBorrowed { get; set; }
        public int PublisherId { get; set; }
        public int CategoryID { get; set; }
    }
}
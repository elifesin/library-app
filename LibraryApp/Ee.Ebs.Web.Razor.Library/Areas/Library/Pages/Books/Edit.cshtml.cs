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

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Books;

public class EditModel : PageModel
{
    private readonly IBookAppService _bookAppService;
    private readonly IAuthorAppService _authorAppService;
    private readonly ICategoryAppService _categoryAppService;
    private readonly IPublisherAppService _publisherAppService;
    private readonly IMapper _objectMapper;

    public EditModel(
        IBookAppService bookAppService,
        IAuthorAppService authorAppService,
        ICategoryAppService categoryAppService,
        IPublisherAppService publisherAppService,
        IMapper objectMapper)
    {
        _bookAppService = bookAppService;
        _authorAppService = authorAppService;
        _categoryAppService = categoryAppService;
        _publisherAppService = publisherAppService;
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
        var bookDto = _bookAppService.GetById(id);

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
            
        _bookAppService.Update(bookDto);

        return RedirectToPage("./Index");
    }
    
    private void LoadData()
    {
        Authors = _authorAppService.GetAll().ToList();
        Categories = _categoryAppService.GetAll().ToList();
        Publishers = _publisherAppService.GetAll().ToList();
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
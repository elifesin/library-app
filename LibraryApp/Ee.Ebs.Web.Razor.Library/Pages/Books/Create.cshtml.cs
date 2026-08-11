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

namespace Ee.Ebs.Web.Razor.Library.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly IAuthorAppService _authorAppService;
        private readonly ICategoryService _categoryService;
        private readonly IPublisherService _publisherService;
        private readonly IMapper _objectMapper;

        public CreateModel(
            IBookService bookService,
            IAuthorAppService authorAppService,
            ICategoryService categoryService,
            IPublisherService publisherService,
            IMapper objectMapper)
        {
            _bookService = bookService;
            _authorAppService = authorAppService;
            _categoryService = categoryService;
            _publisherService = publisherService;
            _objectMapper = objectMapper;
        }

        [BindProperty]
        public BookCreateVm Vm { get; set; } = new();

        public List<AuthorDto> Authors { get; set; } = new();
        public List<CategoryDto> Categories { get; set; } = new();
        public List<PublisherDto> Publishers { get; set; } = new();

        public void OnGet()
        {
            LoadData();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                LoadData();
                return Page();
            }

            var bookDto = _objectMapper.Map<BookCreateDto>(Vm);
            _bookService.Insert(bookDto);

            return RedirectToPage("./Index");
        }

        private void LoadData()
        {
            Authors = _authorAppService.GetAll().ToList();
            Categories = _categoryService.GetAll().ToList();
            Publishers = _publisherService.GetAll().ToList();
        }

        public class BookCreateVm
        {
            public int AuthorID { get; set; }
            public string Title { get; set; }
            public int PublishYear { get; set; }
            public int CategoryID { get; set; } 
            public int PublisherId { get; set; }
            public bool IsBorrowed { get; set; }
        }
    }
}
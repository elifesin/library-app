using AutoMapper;
using Ee.Ebs.Application.Contracts.Books;
using Ee.Ebs.Application.Contracts.Books.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly IMapper _objectMapper;

        public IndexModel(IBookService bookService, IMapper objectMapper)
        {
            _bookService = bookService;
            _objectMapper = objectMapper;
        }

        public List<BookVm> Books { get; set; }
        
        [HttpGet]
        public IActionResult OnGet()
        {
            var bookDtos = _bookService.GetAll();
            
            Books = _objectMapper.Map<List<BookVm>>(bookDtos);
            
            return Page();
        }
        
        public class BookVm
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string AuthorName { get; set; }
            public bool IsBorrowed { get; set; }
            public string CategoryName { get; set; }
            public string PublisherName { get; set; }
            public int PublishYear { get; set; }
        }
    }
}
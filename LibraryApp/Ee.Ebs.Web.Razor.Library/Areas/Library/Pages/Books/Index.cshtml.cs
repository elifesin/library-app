using AutoMapper;
using Ee.Ebs.Application.Contracts.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly IBookAppService _bookAppService;
        private readonly IMapper _objectMapper;

        public IndexModel(IBookAppService bookAppService, IMapper objectMapper)
        {
            _bookAppService = bookAppService;
            _objectMapper = objectMapper;
        }

        public List<BookVm> Books { get; set; }
        
        [HttpGet]
        public IActionResult OnGet()
        {
            var bookDtos = _bookAppService.GetAll();
            
            Books = _objectMapper.Map<List<BookVm>>(bookDtos);
            
            return Page();
        }

        [HttpPost]
        public IActionResult OnPostDelete(int id)
        {
            _bookAppService.Delete(id);
            
            return RedirectToPage();
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
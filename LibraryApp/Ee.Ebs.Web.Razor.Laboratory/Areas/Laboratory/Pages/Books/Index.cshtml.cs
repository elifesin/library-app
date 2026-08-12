using AutoMapper;
using Ee.Ebs.Application.Contracts.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Laboratory.Areas.Laboratory.Pages.Books;

public class IndexModel : PageModel
{
    private readonly IBookAppService _bookAppService;
    private readonly IMapper _mapper;

    public IndexModel(IBookAppService bookAppService, IMapper mapper)
    {
        _bookAppService = bookAppService;
        _mapper = mapper;
    }

    public List<BookListVm> Books { get; set; }
    
    public IActionResult OnGet()
    {
        var bookDtos = _bookAppService.GetBooksByCategory(3002);
        Books = _mapper.Map<List<BookListVm>>(bookDtos);
            
        return Page();
    }
    
    public class BookListVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string FullName { get; set; } = string.Empty;
        public bool IsBorrowed { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string PublisherName { get; set; } = string.Empty;
        public int PublishYear { get; set; }
    }
}
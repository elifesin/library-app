using AutoMapper;
using Ee.Ebs.Application.Contracts.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Books;

public class DeleteModel : PageModel
{
    private readonly IBookAppService _bookAppService;
    private readonly IMapper _mapper;

    public DeleteModel(IBookAppService bookAppService, IMapper mapper)
    {
        _bookAppService = bookAppService;
        _mapper = mapper;
    } 
    
    [BindProperty]
    public BookDeleteVm Vm { get; set; }
    
    [HttpGet]
    public IActionResult OnGet(int id)
    {
        var bookDto = _bookAppService.GetById(id);

        if (bookDto == null)
        {
            return NotFound();
        }

        // Ekranda göstermek üzere DTO'yu View Model'e çeviriyoruz
        Vm = _mapper.Map<BookDeleteVm>(bookDto);
            
        return Page();
    }
    
    public IActionResult OnPost()
    {
        // Formdan gelen ID bilgisi ile silme işlemini gerçekleştir
        _bookAppService.Delete(Vm.Id);
 
        return RedirectToPage("./Index");
    }
    
    public class BookDeleteVm
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Title { get; set; }
        public int PublishYear { get; set; }
        public bool IsBorrowed { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string PublisherName { get; set; } = string.Empty;
    }
}
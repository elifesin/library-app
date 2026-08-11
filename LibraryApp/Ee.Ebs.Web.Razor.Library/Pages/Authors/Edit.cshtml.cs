using AutoMapper;
using Ee.Ebs.Application.Contracts.Authors;
using Ee.Ebs.Application.Contracts.Authors.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Pages.Authors;

public class EditModel : PageModel
{
    private readonly IAuthorAppService _authorAppService;
    private  readonly IMapper _mapper;
    
    [BindProperty]
    public AuthorEditVm Vm { get; set; }
    
    public EditModel(IAuthorAppService authorAppService, IMapper mapper)
    {
        _authorAppService = authorAppService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public IActionResult OnGet(int id)
    {
        var authorDto = _authorAppService.GetById(id);
 
        if (authorDto == null)
        {
            return NotFound();
        }
 
        Vm = _mapper.Map<AuthorEditVm>(authorDto);
        return Page(); 
    }


    [HttpPost]
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
 
        var authorDto = _mapper.Map<AuthorEditDto>(Vm);
        _authorAppService.Update(authorDto);
        
        return RedirectToPage("./Index");
    }
    
    public class AuthorEditVm
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
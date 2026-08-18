using System.ComponentModel.DataAnnotations;
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

    public int Id { get; set; }
    
    [BindProperty] public BookCreateOrEditVm Vm { get; set; }
    
    [HttpGet]
    public IActionResult OnGet(int id)
    {
        Id = id;
        var bookDto = _bookAppService.GetById(id);

        if (bookDto == null)
        {
            return NotFound(); 
        }
        
        Vm = _objectMapper.Map<BookCreateOrEditVm>(bookDto);

        LoadData();
            
        return Page();
    }
    
    public IActionResult OnPost(int id)
    {
        if (!ModelState.IsValid)
        {  
            LoadData();
            return Page();
        }
        
        var bookDto = _objectMapper.Map<BookEditDto>(Vm); 
        bookDto.Id = id;    
        
        _bookAppService.Update(id, bookDto);
        
        return RedirectToPage("./Index");
    }
    
    private void LoadData()
    {
        Vm.Authors = _authorAppService.GetAll().ToList();
        Vm.Categories = _categoryAppService.GetAll().ToList();
        Vm.Publishers = _publisherAppService.GetAll().ToList();
    }
}
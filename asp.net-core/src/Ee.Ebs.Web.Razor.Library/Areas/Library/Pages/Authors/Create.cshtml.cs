using System;
using AutoMapper;
using Ee.Ebs.Application.Contracts.Authors;
using Ee.Ebs.Application.Contracts.Authors.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Authors;

public class CreateModel : PageModel
{
    private readonly IAuthorAppService _authorAppService;
    private readonly IMapper _mapper;

    public CreateModel(IAuthorAppService authorAppService, IMapper mapper)
    {
        _authorAppService = authorAppService;
        _mapper = mapper;
    }

    [BindProperty] public AuthorCreateOrEditVm Vm { get; set; }

    public IActionResult OnGet(int? id)
    {
        Vm = new AuthorCreateOrEditVm();

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        try
        {
            var createAuthorDto = _mapper.Map<AuthorCreateDto>(Vm);
            _authorAppService.Insert(createAuthorDto);

            return RedirectToPage("./Index");
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
    }
}
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

namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly IBookAppService _bookAppService;
        private readonly IAuthorAppService _authorAppService;
        private readonly ICategoryAppService _categoryAppService;
        private readonly IPublisherAppService _publisherAppService;
        private readonly IMapper _objectMapper;

        public CreateModel(
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
        public BookCreateOrEditVm Vm { get; set; } = new();

        public List<AuthorDto> Authors { get; set; } = new();
        public List<CategoryDto> Categories { get; set; } = new();
        public List<PublisherDto> Publishers { get; set; } = new();

        public void OnGet()
        {
            Vm.Authors = _authorAppService.GetAll(); 
            Vm.Categories = _categoryAppService.GetAll();
            Vm.Publishers = _publisherAppService.GetAll();

            LoadData();
        }

        public IActionResult OnPost()
        {
            
            if (!ModelState.IsValid)
            {
                LoadData();
                return Page();
            }

            try
            {
                var bookDto = _objectMapper.Map<BookCreateDto>(Vm);
                _bookAppService.Insert(bookDto);

                return RedirectToPage("./Index");
            }
            catch(InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }

        private void LoadData()
        {
            Authors = _authorAppService.GetAll().ToList();
            Categories = _categoryAppService.GetAll().ToList();
            Publishers = _publisherAppService.GetAll().ToList();
        }
    }
}
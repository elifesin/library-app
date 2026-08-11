using AutoMapper;
using Ee.Ebs.Application.Contracts.Authors;
using Ee.Ebs.Application.Contracts.Authors.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ee.Ebs.Web.Razor.Library.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly IAuthorAppService _authorAppService;
        private readonly IMapper _objectMapper;

        public IndexModel(IAuthorAppService authorAppService, IMapper objectMapper)
        {
            _authorAppService = authorAppService;
            _objectMapper = objectMapper;
        }

        public List<AuthorVm> Authors { get; set; }
        
        [HttpGet]
        public IActionResult OnGet()
        {
            var authorDtos = _authorAppService.GetAll();
            
            Authors = _objectMapper.Map<List<AuthorVm>>(authorDtos);
            
            return Page();
        }
        
        public class AuthorVm
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public bool IsActive { get; set; } = true;
            public string FullName => $"{FirstName} {LastName}";
        }
    }
}
using Ee.Ebs.Application.Contracts.Books;
using Ee.Ebs.Application.Contracts.Categories;
using Ee.Ebs.Application.Contracts.Categories.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ee.Ebs.Web.Api.Categories.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase, ICategoryAppService
{
    private readonly ICategoryAppService _categoryAppService;
    private readonly IBookAppService _bookAppService;

    public CategoryController(ICategoryAppService categoryAppService, IBookAppService bookAppService)
    {
        _categoryAppService = categoryAppService;
        _bookAppService = bookAppService;
    }

    [HttpGet]
    public List<CategoryDto> GetAll()
    {
        return _categoryAppService.GetAll();
    }

    [HttpGet("{id}")]
    public CategoryDto GetById(int id)
    {
        return _categoryAppService.GetById(id);
    }
    
    [HttpPost]
    public void Insert(CategoryDto category)
    {
        _categoryAppService.Insert(category);
    }

    [HttpPut("{id}")]
    public void Update(int id, CategoryDto dto)
    {
        _categoryAppService.Update(id, dto);
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _categoryAppService.Delete(id);
    }
}
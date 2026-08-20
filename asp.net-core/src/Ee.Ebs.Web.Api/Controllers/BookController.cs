using System.Collections.Generic;
using Ee.Ebs.Application.Contracts.Books;
using Ee.Ebs.Application.Contracts.Books.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ee.Ebs.Web.Api.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase, IBookAppService
{
    private readonly IBookAppService _bookAppService;

    public BookController(IBookAppService bookAppService)
    {
        _bookAppService = bookAppService;
    }

    [HttpGet]
    public List<BookDto> GetAll()
    {
        return _bookAppService.GetAll();
    }

    [HttpGet("{id}")]
    public BookDto GetById(int id)
    {
        return _bookAppService.GetById(id);
    }

    [HttpPost]
    public void Insert(BookCreateDto book)
    {
        _bookAppService.Insert(book);
    }

    [HttpPut("{id}")]
    public void Update(int id, BookEditDto book)
    {
        _bookAppService.Update(id, book);
    }
    
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _bookAppService.Delete(id);
    }

    [HttpGet("avaliable")]
    public List<BookDto> GetAvailableBooks()
    {
        return _bookAppService.GetAvailableBooks();
    }

    [HttpGet("{categoryId}/books")]
    public List<BookDto> GetBooksByCategory(int categoryId)
    {
        return  _bookAppService.GetBooksByCategory(categoryId);
    }
}
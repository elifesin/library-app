using System.Collections.Generic;
using Ee.Ebs.Application.Contracts.Authors;
using Ee.Ebs.Application.Contracts.Authors.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ee.Ebs.Web.Api.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorController : ControllerBase, IAuthorAppService
{
    private readonly IAuthorAppService _authorAppService;

    public AuthorController(IAuthorAppService authorAppService)
    {
        _authorAppService = authorAppService;
    }

    [HttpGet]
    public List<AuthorDto> GetAll()
    {
        return _authorAppService.GetAll();
    }

    [HttpGet("{id}")]
    public AuthorDto GetById(int id)
    {
        return _authorAppService.GetById(id);
    }

    [HttpPost]
    public void Insert(AuthorCreateDto dto)
    {
        _authorAppService.Insert(dto);
    }

    [HttpPut("{id}")]
    public void Update(int id, AuthorEditDto dto)
    {
        _authorAppService.Update(id, dto);
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _authorAppService.Delete(id);
    }
}
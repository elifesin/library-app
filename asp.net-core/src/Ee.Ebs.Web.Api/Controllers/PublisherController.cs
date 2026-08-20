using System.Collections.Generic;
using Ee.Ebs.Application.Contracts.Publishers;
using Ee.Ebs.Application.Contracts.Publishers.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ee.Ebs.Web.Api.Controllers;

[ApiController]
[Route("api/publishers")]
public class PublisherController : ControllerBase, IPublisherAppService
{
    private readonly IPublisherAppService _publisherAppService;

    public PublisherController(IPublisherAppService publisherAppService)
    {
        _publisherAppService = publisherAppService;
    }

    [HttpGet]
    public List<PublisherDto> GetAll()
    {
        return _publisherAppService.GetAll();
    }

    [HttpGet("{id}")]
    public PublisherDto GetById(int id)
    {
        return _publisherAppService.GetById(id);
    }

    [HttpPost]
    public void Insert(PublisherDto dto)
    {
        _publisherAppService.Insert(dto);
    }

    [HttpPut("{id}")]
    public void Update(int id, PublisherDto dto)
    {
        _publisherAppService.Update(id, dto);
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _publisherAppService.Delete(id);
    }
}
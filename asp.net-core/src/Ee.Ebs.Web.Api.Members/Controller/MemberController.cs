using Ee.Ebs.Application.Contracts.Members;
using Ee.Ebs.Application.Contracts.Members.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ee.Ebs.Web.Api.Members.Controller;

[ApiController]
[Route("api/members")]
public class MemberController : ControllerBase, IMemberAppService
{
    private readonly IMemberAppService _memberAppService;

    public MemberController(IMemberAppService memberAppService)
    {
        _memberAppService = memberAppService;
    }

    [HttpGet]
    public List<MemberDto> GetAll()
    {
        return _memberAppService.GetAll();
    }

    [HttpGet("{id}")]
    public MemberDto GetById(int id)
    {
        return  _memberAppService.GetById(id);
    }

    [HttpPost]
    public void Insert(MemberCreateDto member)
    {
        _memberAppService.Insert(member);
    }

    [HttpPut("{id}")]
    public void Update(int id, MemberDto dto)
    {
        _memberAppService.Update(id, dto);
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _memberAppService.Delete(id);
    }
}
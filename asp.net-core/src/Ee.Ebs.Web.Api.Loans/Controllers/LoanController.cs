using Ee.Ebs.Application.Contracts.Loans;
using Ee.Ebs.Application.Contracts.Loans.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ee.Ebs.Web.Api.Loans.Controllers;

[ApiController]
[Route("api/loans")]
public class LoanController : ControllerBase, ILoanAppService
{
    private readonly ILoanAppService _loanAppService;

    public LoanController(ILoanAppService loanAppService)
    {
        _loanAppService = loanAppService;
    }

    [HttpGet]
    public List<LoanDto> GetAll()
    {
        return  _loanAppService.GetAll();
    }

    [HttpPost]
    public void Insert(LoanCreateDto dto)
    {
        _loanAppService.Insert(dto);
    }

    [HttpPost("return/{loanId}")]
    public void ReturnBook(int loanId)
    {
        _loanAppService.ReturnBook(loanId);
    }

    [HttpGet("member/{memberId}")]
    public List<LoanDto> GetLoansByMemberId(int memberId)
    {
        return  _loanAppService.GetLoansByMemberId(memberId);
    }

    [HttpGet("{id}")]
    public LoanDto GetById(int id)
    {
        return _loanAppService.GetById(id);
    }
}
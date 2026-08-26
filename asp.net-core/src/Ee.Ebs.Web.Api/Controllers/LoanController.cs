using System.Collections.Generic;
using Ee.Ebs.Application.Contracts.Loans;
using Ee.Ebs.Application.Contracts.Loans.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ee.Ebs.Web.Api.Controllers;

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

    [HttpPost("{loanId}")]
    public void ReturnBook(int loanId)
    {
        _loanAppService.ReturnBook(loanId);
    }

    [HttpGet("{id}")]
    public List<LoanDto> GetLoansByMemberId(int id)
    {
        return  _loanAppService.GetLoansByMemberId(id);
    }

    [HttpGet("{id}")]
    public LoanDto GetById(int id)
    {
        return _loanAppService.GetById(id);
    }
}
using System.Collections.Generic;
using Ee.Ebs.Application.Contracts.Loans.DTOs;

namespace Ee.Ebs.Application.Contracts.Loans;

public interface ILoanAppService
{
    List<LoanDto> GetAll();
    void Insert(LoanCreateDto dto);
    void ReturnBook(int loanId);
    List<LoanDto> GetLoansByMemberId(int memberId);
    public LoanDto GetById(int id); 
}
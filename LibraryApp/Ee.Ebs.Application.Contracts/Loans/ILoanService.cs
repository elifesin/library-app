using Ee.Ebs.Application.Contracts.Loans.DTOs;

namespace Ee.Ebs.Application.Contracts.Loans;

public interface ILoanService
{
    List<LoanDto> GetAll();
    void Insert(LoanCreateDto dto);
    void ReturnBook(int loanId);
    List<LoanDto> GetLoansByMemberId(int id);
    public LoanDto GetById(int id); 
}
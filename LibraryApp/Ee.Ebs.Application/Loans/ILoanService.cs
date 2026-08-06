using Ee.Ebs.Application.Loans.DTOs;

namespace Ee.Ebs.Application.Loans;

public interface ILoanService
{
    List<LoanDto> GetAll();
    LoanDto GetById(int id);
    void Insert(LoanCreateDto dto);
    void ReturnBook(int loanId);
    List<LoanDto> GetLoansByMemberId(int id);
}
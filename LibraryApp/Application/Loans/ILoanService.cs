using Application.Loans.DTOs;

namespace Application.Loans;

public interface ILoanService
{
    List<LoanDto> GetAll();
    LoanDto GetById(int id);
    void Insert(LoanCreateDto dto);
    void ReturnBook(int loanId);
    List<LoanDto> GetLoansByMemberId(int id);
}
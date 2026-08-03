using Domain.Entities;

namespace Domain.Repositories;

public interface ILoanRepository
{
    public List<Loan> GetAll();
    public void Insert(Loan loan);
    public List<Loan> GetLoansByMemberId(int memberId);
    public void ReturnBook(int loanId);
}
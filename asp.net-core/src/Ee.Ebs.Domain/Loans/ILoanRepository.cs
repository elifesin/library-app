using System.Collections.Generic;

namespace Ee.Ebs.Domain.Loans;

public interface ILoanRepository
{
    public List<Loan> GetAll();
    public void Insert(Loan loan);
    public List<Loan> GetLoansByMemberId(int id);
    public void ReturnBook(int loanId);
    public Loan GetById(int id); 

}
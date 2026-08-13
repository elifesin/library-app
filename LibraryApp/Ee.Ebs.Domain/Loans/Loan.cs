using Ee.Ebs.Domain.Authors;
using Ee.Ebs.Domain.Books;
using Ee.Ebs.Domain.Members;

namespace Ee.Ebs.Domain.Loans;

public class Loan
{
    public int Id { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    
    public int BookID { get; set; }
    public int MemberID { get; set; }
    public Book Book { get; set; }
    public Member Member { get; set; }
    public bool IsReturned => ReturnDate.HasValue;

    
    private Loan() { }
    
    public Loan(int bookId, int memberId, DateTime loanDate, DateTime dueDate)
    {
        if (loanDate > dueDate)
        {
            throw new ArgumentException("Veriliş Tarihi, Son Teslim Tarihi'nden sonra olamaz!");
        }

        BookID = bookId;
        MemberID = memberId;
        LoanDate = loanDate;
        DueDate = dueDate;
        ReturnDate = null;
    }
    
    public void Return(DateTime returnDate)
    {
        if (IsReturned)
            throw new InvalidOperationException("Bu ödünç kaydı zaten iade edilmiş.");

        if (returnDate < LoanDate)
            throw new ArgumentException("İade tarihi, ödünç alma tarihinden önce olamaz.");
        
        ReturnDate = returnDate;
    }
}
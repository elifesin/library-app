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
    
    private Loan() { }
    
    public Loan(int bookId, int memberId, DateTime loanDate, DateTime dueDate)
    {
        // İŞ KURALI (Business Logic): LoanDate, DueDate'den sonra olamaz!
        if (loanDate > dueDate)
        {
            throw new ArgumentException("Veriliş Tarihi, Son Teslim Tarihi'nden sonra olamaz!");
        }

        BookID = bookId;
        MemberID = memberId;
        LoanDate = loanDate;
        DueDate = dueDate;
        ReturnDate = null; // Yeni kayıtta iade tarihi her zaman boştur
    }
}
using Domain.Books;
using Domain.Members;

namespace Domain.Loans;

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
}
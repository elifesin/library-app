namespace LibraryApp.Models.Loan;

public class LoanCreateVm
{
    public int Id { get; set; }
    public int MemberID { get; set; }
    public int BookID { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime ReturnDate { get; set; }
    
}
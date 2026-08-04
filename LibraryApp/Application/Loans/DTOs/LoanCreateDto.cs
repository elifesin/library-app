namespace Application.Loans.DTOs;

public class LoanCreateDto
{
    public int MemberID { get; set; }
    public int BookID { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
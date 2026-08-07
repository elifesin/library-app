namespace Ee.Ebs.Web.Library.ViewModels.Loan;

public class LoanVm
{
    public int Id { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string BookName { get; set; } = string.Empty;
    public  string FullName { get; set; } = string.Empty;
}
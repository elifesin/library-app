namespace Ee.Ebs.LibraryApp.Models.Members;

public class MemberBorrowedBooksVm
{
    public int MemberId { get; set; }
    
    public string FullName { get; set; } 
    
    public List<BorrowedBookItem> BorrowedBooks { get; set; } = new List<BorrowedBookItem>();
}

public class BorrowedBookItem
{
    public string BookName { get; set; } 
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime ReturnDate { get; set; }
}
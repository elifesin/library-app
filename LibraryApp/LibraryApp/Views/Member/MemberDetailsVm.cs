namespace LibraryApp.Views.Member;

public class MemberDetailsVm
{
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
        
    // Üyenin aldığı kitapların listesini burada tutacağız
    public List<MemberLoanItem> Loans { get; set; } = new List<MemberLoanItem>();
}

// Listedeki her bir satırı temsil edecek alt sınıf
public class MemberLoanItem
{
    public string BookTitle { get; set; } = string.Empty;
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
        
    // İade tarihi henüz gelmemiş olabileceği için nullable (?) yapıyoruz
    public DateTime? ReturnDate { get; set; }
}
namespace Application.Loans.DTOs;

public class LoanDto
{
    public int Id { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
        
    // İade edilmeme durumuna karşı nullable (?) yapıldı
    public DateTime? ReturnDate { get; set; } 
        
    // AutoMapper ile Member ve Book tablolarından çekilecek alanlar
    public string BookName { get; set; } 
    public string FullName { get; set; }
}
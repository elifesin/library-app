namespace Ee.Ebs.Application.Contracts.Books.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; } 
    public string Title { get; set; }
    public int PublishYear { get; set; }
    public bool IsBorrowed { get; set; }
        
    // Navigation property'lerden AutoMapper ile doldurulacak alanlar
    public string AuthorName { get; set; }
    public string CategoryName { get; set; }
    public string PublisherName { get; set; }
}
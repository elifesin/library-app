namespace Data.Entities;

public class Book
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string Title { get; set; }
    public int PublishYear { get; set; }
    public bool IsBorrowed { get; set; }
    public int CategoryID{ get; set; }
    public int AuthorID { get; set; }
    public int PublisherId { get; set; }
    
    public string AuthorName { get; set; }
    public string CategoryName { get; set; }
    public string PublisherName { get; set; }
    
    public  Category Category { get; set; }
    public  Author Author { get; set; }
    public  Publisher Publisher { get; set; }
}
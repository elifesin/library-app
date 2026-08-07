namespace Ee.Ebs.Application.Contracts.Books.DTOs;

public class BookEditDto
{
    public int Id { get; set; }
    public int AuthorID { get; set; }
    public string Title { get; set; }
    public int PublishYear { get; set; }
    public bool IsBorrowed { get; set; }
    public int PublisherId { get; set; }
    public int CategoryID { get; set; }
}
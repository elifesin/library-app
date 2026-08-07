namespace Ee.Ebs.Web.Laboratory.ViewModels.Book;

public class BookListVm
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsActive { get; set; }
    public string FullName { get; set; } = string.Empty;
    public bool IsBorrowed { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string PublisherName { get; set; } = string.Empty;
    public int PublishYear { get; set; }
}
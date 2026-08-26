using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ee.Ebs.Domain.Authors;
using Ee.Ebs.Domain.Categories;
using Ee.Ebs.Domain.Publishers;

namespace Ee.Ebs.Domain.Books;

public class Book
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    [Required] // Required keyword isn't enough just by itself. It should be initialized with constructor. Also be private set.
    public string Title { get; set; }
    public int PublishYear { get; set; }
    [NotMapped]
    public bool IsBorrowed { get; set; }
    public int CategoryID{ get; set; }
    public int AuthorID { get; set; }
    public int PublisherId { get; set; }
    
    [NotMapped]
    public string AuthorName { get; set; }
    [NotMapped]
    public string CategoryName { get; set; }
    [NotMapped]
    public string PublisherName { get; set; }
    
    public  Category Category { get; set; }
    public  Author Author { get; set; }
    public  Publisher Publisher { get; set; }
}
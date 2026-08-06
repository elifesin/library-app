namespace Ee.Ebs.LibraryApp.Models.Book
{
    public class BookCreateVm
    {
        public int AuthorID { get; set; }
        public string Title { get; set; }
        public int PublishYear { get; set; }
        public int CategoryID { get; set; } 
        public int PublisherId { get; set; }
    }
}
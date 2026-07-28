using LibraryApp.Models.Author;

namespace LibraryApp.Models.Book
{
    public class BookVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublishYear { get; set; }
        public string AuthorFullName { get; set; } = string.Empty;
        public bool IsBorrowed { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string PublisherName { get; set; } = string.Empty;
    }
}
namespace LibraryApp.Models.Book
{
    public class BookVm
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Title { get; set; }
        public int PublishYear { get; set; }
        public string FullName { get; set; } = string.Empty;
        public bool IsBorrowed { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string PublisherName { get; set; } = string.Empty;
    }
}
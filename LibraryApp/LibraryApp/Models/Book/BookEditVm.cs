namespace LibraryApp.Models.Book
{
    public class BookEditVm
    {
        public int Id { get; set; }
        public int AuthorID { get; set; }
        public string Title { get; set; }
        public int PublishYear { get; set; }
        public bool IsBorrowed { get; set; }
    }
}
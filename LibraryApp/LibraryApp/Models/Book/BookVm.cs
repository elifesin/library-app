namespace LibraryApp.Models.Book
{
    public class BookVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublishYear { get; set; }
        public int AuthorID { get; set; }
        public bool IsActive { get; set; } = true;
        
        public string FullName { get; set; } = string.Empty;
        
    }
}
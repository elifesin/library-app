using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryApp.Models.Book
{
    public class BookCreateVm
    {
        public int AuthorID { get; set; }
        public string Title { get; set; }
        public int PublishYear { get; set; }
      
    }
}
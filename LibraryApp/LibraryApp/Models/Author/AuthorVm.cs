namespace LibraryApp.Models.Author
{
    public class AuthorVm
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsActive { get; set; } = true;
        public string FullName { get; set; } = string.Empty;
    }
}
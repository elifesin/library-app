namespace Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsActive { get; set; } = true;
        
        public string AuthorFullName => $"{FirstName} {LastName}";
    }
}
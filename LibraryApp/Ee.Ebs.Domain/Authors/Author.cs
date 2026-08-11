using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Ee.Ebs.Domain.Authors
{
    public class Author
    {
        public int Id { get; set; }
        
        [NotNull]
        [Required]
        public string FirstName { get;  set; }
        
        [Required]
        [NotNull]
        public string LastName { get;  set; }

        public bool IsActive { get; set; } = true;
        
        public string FullName => $"{FirstName} {LastName}";
    }
}
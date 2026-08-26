using System;
using System.ComponentModel.DataAnnotations;
using Ee.Ebs.Domain.Shared.Authors;
using JetBrains.Annotations;

namespace Ee.Ebs.Domain.Authors
{
    public class Author
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(AuthorConsts.FirstNameMaxLength)] 
        [NotNull]
        public string FirstName { get;  private set; }

        [Required]
        [MaxLength(AuthorConsts.LastNameMaxlength)] 
        [NotNull]
        public string LastName { get;  private set; }

        public bool IsActive { get; set; } = true;
        
        public string FullName => $"{FirstName} {LastName}";
        
        public Author([NotNull]string firstName, [NotNull]string lastName)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
        }

        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("First name cannot be null or empty");
            }
            
            FirstName = firstName;
        }
        
        public void SetLastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("Last name cannot be null or empty");
            }
            
            LastName = lastName;
        }
    }
}
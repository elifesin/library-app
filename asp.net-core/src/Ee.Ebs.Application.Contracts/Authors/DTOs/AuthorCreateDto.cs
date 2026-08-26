using System.ComponentModel.DataAnnotations;
using Ee.Ebs.Domain.Shared.Authors;

namespace Ee.Ebs.Application.Contracts.Authors.DTOs;

public class AuthorCreateDto
{
    [Required]
    [MaxLength(AuthorConsts.FirstNameMaxLength)] 
    public string FirstName { get; set; }
    
    [Required] 
    [MaxLength(AuthorConsts.LastNameMaxlength)] 
    public string LastName { get; set; }
}
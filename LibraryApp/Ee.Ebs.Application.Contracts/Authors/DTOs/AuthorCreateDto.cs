using System.ComponentModel.DataAnnotations;

namespace Ee.Ebs.Application.Contracts.Authors.DTOs;
using FluentValidation;

public class AuthorCreateDto
{
    [Required]
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
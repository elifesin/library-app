using System.ComponentModel.DataAnnotations;

namespace Ee.Ebs.Application.Contracts.Authors.DTOs;

public class AuthorCreateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
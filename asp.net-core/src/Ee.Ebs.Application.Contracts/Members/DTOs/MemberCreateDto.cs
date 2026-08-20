namespace Ee.Ebs.Application.Contracts.Members.DTOs;

public class MemberCreateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}
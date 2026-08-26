namespace Ee.Ebs.Application.Contracts.Members.DTOs;

public class MemberDto
{
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
        
    public string MemberFullName => $"{FirstName} {LastName}";

    public bool IsActive { get; set; }
}
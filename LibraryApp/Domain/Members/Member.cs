namespace Domain.Members;

public class Member
{
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
        
    public string FullName => $"{FirstName} {LastName}";

    public bool IsActive { get; set; } = true;
    
    
}
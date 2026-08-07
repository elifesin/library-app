namespace Ee.Ebs.Web.Library.ViewModels.Members
{
    public class MemberVm
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string FullName => $"{FirstName} {LastName}";

        public bool IsActive { get; set; }
    }
}
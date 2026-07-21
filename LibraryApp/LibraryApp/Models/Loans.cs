namespace LibraryApp.Models
{
    public class Loans
    {
        public int Id { get; set; }
        public int BookID { get; set; }
        public int MemberId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
namespace Application.Members.DTOs
{
    public class MemberBorrowedBooksDto
    {
        public int MemberId { get; set; }
        
        public string FullName { get; set; } 
        
        public List<BorrowedBookItemDto> BorrowedBooks { get; set; } = new List<BorrowedBookItemDto>();
    }

    public class BorrowedBookItemDto
    {
        public string BookTitle { get; set; } 
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; } 
    }    
}

using LibraryApp.Models.Loan;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryApp.Data;

public class LoanRepository : RepositoryBase
{
    public LoanRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public List<LoanVm> GetAll()
    {
        // SQL sorgusundaki virgüller ve boşluklar tamamen düzeltildi
        string sql = @"
        SELECT 
            l.Id, 
            b.Title AS BookName, 
            a.FirstName + ' ' + a.LastName AS AuthorName, 
            l.LoanDate, 
            l.DueDate, 
            l.ReturnDate
        FROM Loans l
        INNER JOIN Books b ON l.BookID = b.Id
        INNER JOIN Authors a ON b.AuthorID = a.Id
        INNER JOIN Members m ON l.MemberID = m.Id";

        return ExecuteReadQuery<LoanVm>(sql);
    }

    public LoanVm GetById(int id)
    {
        string sql = @"
            SELECT 
                l.Id, 
                b.Title AS BookName, 
                a.FirstName + ' ' + a.LastName AS AuthorName, 
                l.LoanDate, 
                l.DueDate, 
                l.ReturnDate
            FROM Loans l
            INNER JOIN Books b ON l.BookID = b.Id
            INNER JOIN Authors a ON b.AuthorID = a.Id
            INNER JOIN Members m ON l.MemberID = m.Id
            WHERE l.Id = @Id";
        return ExecuteReadSingle<LoanVm>(sql, new { Id = id });

    }

    public void Insert(LoanCreateVm vm)
    {
        string sql = "INSERT INTO Loans (BookID, MemberID, LoanDate, DueDate) VALUES (@BookID, @MemberID, @LoanDate, @DueDate)";
        
        ExecuteCommand(sql, vm);
    }

    // YENİ: Üyeleri Dropdown için getiren jenerik metot
    public List<SelectListItem> GetActiveMembers()
    {
        string sql = "SELECT ID, FirstName, LastName FROM Members WHERE IsActive = 1";
        return ExecuteReadQuery<SelectListItem>(sql);
    }

    // YENİ: Müsait Kitapları Dropdown için getiren jenerik metot
    public List<SelectListItem> GetAvailableBooks()
    {
        string sql = "SELECT Id, Title FROM Books WHERE IsActive = 1 AND Id NOT IN (SELECT BookID FROM Loans WHERE ReturnDate IS NULL)";
        return ExecuteReadQuery<SelectListItem>(sql);
    }
    
    public List<LoanVm> GetLoansByMemberId(int memberId)
    {
        string sql = @"
        SELECT 
            l.Id, 
            b.Title AS BookName, 
            a.FirstName + ' ' + a.LastName AS AuthorName, 
            l.LoanDate, 
            l.DueDate, 
            l.ReturnDate
        FROM Loans l
        INNER JOIN Books b ON l.BookID = b.Id
        INNER JOIN Authors a ON b.AuthorID = a.Id
        WHERE l.MemberID = @MemberID";
    
        return ExecuteReadQuery<LoanVm>(sql, new {memberID =  memberId});
    }
    
    public void ReturnBook(int loanId)
    {
        // Sadece iade tarihini şu anki zaman olarak güncelliyoruz
        string sql = "UPDATE Loans SET ReturnDate = GETDATE() WHERE Id = @Id";
    
        ExecuteCommand(sql, new{Id = loanId});
    }
    }
    
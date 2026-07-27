using LibraryApp.Models.Loan;
using Microsoft.Data.SqlClient;
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
    
        List<LoanVm> vmList = ExecuteReadQuery<LoanVm>(sql, reader => new LoanVm
        {
            Id = Convert.ToInt32(reader["Id"]),
            LoanDate = Convert.ToDateTime(reader["LoanDate"]),
            DueDate = Convert.ToDateTime(reader["DueDate"]),
        
            // ReturnDate boş gelebileceği için kontrolümüz
            ReturnDate = reader["ReturnDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReturnDate"]) : default,
        
            BookName = reader["BookName"].ToString()!,
            BookAuthor = reader["AuthorName"].ToString()!
        });
    
        return vmList;
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
        LoanVm loan = ExecuteReadSingle<LoanVm>(sql, reader => new LoanVm
            {
                Id = Convert.ToInt32(reader["Id"]),
                LoanDate = Convert.ToDateTime(reader["LoanDate"]),
                ReturnDate = Convert.ToDateTime(reader["ReturnDate"]),
                DueDate = Convert.ToDateTime(reader["DueDate"]),
                BookName = reader["BookName"].ToString()!,
                BookAuthor = reader["AuthorName"].ToString()!
            },
            new SqlParameter("@Id", id)
        );
        return loan;
    }

    public void Insert(LoanCreateVm vm)
    {
        string sql = "INSERT INTO Loans (BookID, MemberID, LoanDate, DueDate) VALUES (@BookID, @MemberID, @LoanDate, @DueDate)";
        
        ExecuteCommand(sql, 
            new SqlParameter("@BookID", vm.BookID),
            new SqlParameter("@MemberID", vm.MemberID),
            new SqlParameter("@LoanDate", vm.LoanDate),
            new SqlParameter("@DueDate", vm.DueDate)
        );
    }

    // YENİ: Üyeleri Dropdown için getiren jenerik metot
    public List<SelectListItem> GetActiveMembers()
    {
        string sql = "SELECT ID, FirstName, LastName FROM Members WHERE IsActive = 1";
        return ExecuteReadQuery(sql, reader => new SelectListItem
        {
            Value = reader["ID"].ToString(),
            Text = reader["FirstName"].ToString() + " " + reader["LastName"].ToString()
        });
    }

    // YENİ: Müsait Kitapları Dropdown için getiren jenerik metot
    public List<SelectListItem> GetAvailableBooks()
    {
        string sql = "SELECT Id, Title FROM Books WHERE IsActive = 1 AND Id NOT IN (SELECT BookID FROM Loans WHERE ReturnDate IS NULL)";
        return ExecuteReadQuery(sql, reader => new SelectListItem
        {
            Value = reader["Id"].ToString(),
            Text = reader["Title"].ToString()
        });
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
    
        return ExecuteReadQuery<LoanVm>(sql, reader => new LoanVm
        {
            Id = Convert.ToInt32(reader["Id"]),
            LoanDate = Convert.ToDateTime(reader["LoanDate"]),
            DueDate = Convert.ToDateTime(reader["DueDate"]),
            ReturnDate = reader["ReturnDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReturnDate"]) : default,
            BookName = reader["BookName"].ToString()!,
            BookAuthor = reader["AuthorName"].ToString()!
        }, new SqlParameter("@MemberID", memberId));
    }
    }
    
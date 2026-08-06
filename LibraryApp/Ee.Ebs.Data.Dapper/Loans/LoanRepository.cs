using Dapper;
using Ee.Ebs.Domain.Books;
using Ee.Ebs.Domain.Loans;
using Ee.Ebs.Data.Dapper.Base;
using Ee.Ebs.Data.Dapper.Connection;

namespace Ee.Ebs.Data.Dapper.Loans;

public class LoanRepository : RepositoryBase, ILoanRepository
{
    public LoanRepository(DbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }

    public List<Loan> GetAll()
    {
        string sql = @"
    SELECT 
        l.Id, 
        l.LoanDate, 
        l.DueDate, 
        b.Id,
        b.Title,
        a.FirstName + ' ' + a.LastName AS AuthorFullName
    FROM Loans l
    INNER JOIN Books b ON l.BookID = b.Id
    INNER JOIN Authors a ON b.AuthorID = a.Id
    WHERE ReturnDate IS NULL";

        return Connection.Query<Loan, Book, Loan>(
            sql,
            (loan, book) =>
            {
                loan.Book = book; 
                return loan;
            },
            splitOn: "Id"
        ).ToList();
    }
    public void Insert(Loan vm)
    {
        string sql = "INSERT INTO Loans (BookID, MemberID, LoanDate, DueDate) VALUES (@BookID, @MemberID, @LoanDate, @DueDate)";
        
        Connection.Execute(sql, vm);
    }
    public List<Loan> GetLoansByMemberId(int memberId)
    {
        string sql = @"
            SELECT
                l.*,
                b.*,
                a.FirstName + ' ' + a.LastName AS AuthorFullName
                FROM Loans l
                INNER JOIN Books b ON l.BookID = b.Id
                INNER JOIN Authors a ON b.AuthorID = a.Id
                WHERE l.MemberID = @MemberID";
    
        return Connection.Query<Loan, Book, Loan>(
            sql,
            (loan, book) =>
            {
                loan.Book = book;
                return loan;
            },
            new { MemberID = memberId },
            splitOn: "Id"
        ).ToList();
    }
    
    public void ReturnBook(int loanId)
    {
        string sql = "UPDATE Loans SET ReturnDate = GETDATE() WHERE Id = @Id";
    
        Connection.Execute(sql, new{Id = loanId});
    }
    }
    
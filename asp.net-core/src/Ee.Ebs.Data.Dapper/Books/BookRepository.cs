using System.Collections.Generic;
using System.Linq;
using Dapper;
using Ee.Ebs.Domain.Books;
using Ee.Ebs.Data.Dapper.Base;
using Ee.Ebs.Data.Dapper.Connection;

namespace Ee.Ebs.Data.Dapper.Books;

public class BookRepository : RepositoryBase, IBookRepository
{
    public BookRepository(DbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }
    public List<Book> GetAll() 
    {
        string sql = @" 
        SELECT 
            b.Id, 
            b.Title, 
            b.PublishYear,
            a.FirstName + ' ' + a.LastName AS AuthorName,
            c.CategoryName,
            p.Name AS PublisherName,
            CASE WHEN EXISTS (SELECT 1 FROM Loans l WHERE l.BookID = b.Id AND l.ReturnDate IS NULL) THEN 1 ELSE 0 END AS IsBorrowed
        FROM Books b   
        INNER JOIN Authors a ON b.AuthorID = a.Id
        LEFT JOIN Categories c ON b.CategoryID = c.Id
        LEFT JOIN Publishers p ON b.PublisherId = p.Id
        WHERE b.IsActive = 1";
        
        return Connection.Query<Book>(sql).ToList();
    }

    public Book GetById(int id)
    {
        string sql = @" 
    SELECT 
        b.Id AS BookId, 
        b.Title AS BookName, 
        a.FirstName + ' ' + a.LastName AS AuthorName, 
        c.CategoryName,
        CASE WHEN EXISTS (SELECT 1 FROM Loans l WHERE l.BookID = b.Id AND l.ReturnDate IS NULL) THEN 1 ELSE 0 END AS IsBorrowed 
    FROM Books b
    LEFT JOIN Authors a ON b.AuthorID = a.Id   
    LEFT JOIN Categories c ON b.CategoryID = c.Id 
    WHERE b.Id = @Id AND b.IsActive = 1";

        return Connection.QueryFirstOrDefault<Book>(sql, new { Id = id });   }

    public void Insert(Book book)
    {
        string sql =
            "INSERT INTO Books(Title, PublishYear, AuthorID, CategoryID, PublisherId) VALUES (@Title, @PublishYear, @AuthorID, @CategoryID, @PublisherId)";

        Connection.Execute(sql, book);
    }

    public void Update(Book bookVm)
    {
        string sql =
            "UPDATE Books SET Title = @Title, PublishYear = @PublishYear, PublisherId = @PublisherId, CategoryID = @CategoryID WHERE Id = @Id";

        Connection.Execute(sql, bookVm);
    }

    public void Delete(Book book)
    {
        string sql = "UPDATE Books SET IsActive = 0 WHERE Id = @Id";
        
        Connection.Execute(sql, book);
    }
    
    public List<Book> GetAvailableBooks()
    {
        string sql = "SELECT *  FROM Books WHERE IsActive = 1 AND Id NOT IN (SELECT BookID FROM Loans WHERE ReturnDate IS NULL)";
        return Connection.Query<Book>(sql).ToList();
    }

    public List<Book> GetBooksByCategory(int categoryId)
    {
        string sql = @"SELECT
                     b.*, 
                     c.*
                     FROM Books b
                     INNER JOIN Categories c ON b.CategoryId = c.Id
                     WHERE b.CategoryId = @categoryId";
        return Connection.Query<Book>(sql).ToList();
    }
}
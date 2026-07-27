using LibraryApp.Models.Book;
using Microsoft.Data.SqlClient;

namespace LibraryApp.Data;

public class BookRepository : RepositoryBase
{
    public BookRepository(IConfiguration configuration) : base(configuration) { }
    public List<BookVm> GetAllBooks()
    {
        string sql = @" SELECT b.Id AS BookId, 
        b.Title AS BookName, 
        a.Firstname + ' ' + a.LastName AS FullName,
        c.CategoryName,
        p.Name AS PublisherName,
        CASE WHEN EXISTS (SELECT 1 FROM Loans l WHERE l.BookID = b.Id AND l.ReturnDate IS NULL) THEN 1 ELSE 0 END AS IsBorrowed
    FROM Books b
    INNER JOIN Authors a ON b.AuthorID = a.Id
    LEFT JOIN Categories c ON b.CategoryID = c.Id
    LEFT JOIN Publishers p ON b.PublisherID = p.Id";

        List<BookVm> vmList = ExecuteReadQuery<BookVm>(sql, reader => new BookVm
        {
            Id = Convert.ToInt32(reader["BookId"]),
            Title = reader["BookName"].ToString()!,
            FullName = reader["FullName"].ToString()!,
            CategoryName = reader["CategoryName"].ToString()!,
            IsBorrowed = Convert.ToBoolean(reader["IsBorrowed"]),
            PublisherName = reader["PublisherName"] != DBNull.Value ? reader["PublisherName"].ToString()! : "Belirtilmemiş",
        });
        return vmList;
    }

    public BookVm GetBookById(int id)
    {
        string sql = @" 
    SELECT 
        b.Id AS BookId, 
        b.Title AS BookName, 
        a.FirstName + ' ' + a.LastName AS FullName, 
        c.CategoryName, 
        CASE WHEN EXISTS (SELECT 1 FROM Loans l WHERE l.BookID = b.Id AND l.ReturnDate IS NULL) THEN 1 ELSE 0 END AS IsBorrowed 
    FROM Books b
    LEFT JOIN Authors a ON b.AuthorID = a.Id   -- INNER yerine LEFT yaptık (Yazarı silinmişse bile kitap gelsin)
    LEFT JOIN Categories c ON b.CategoryID = c.Id -- INNER yerine LEFT yaptık (Kategorisi yoksa bile kitap gelsin)
    WHERE b.Id = @Id";

        BookVm bookVm = ExecuteReadSingle<BookVm>(sql, reader => new BookVm
            {
                Id = Convert.ToInt32(reader["BookId"]),
                Title = reader["BookName"].ToString()!,
                FullName = reader["FullName"].ToString()!,
                CategoryName = reader["CategoryName"].ToString()!,
                IsBorrowed = Convert.ToBoolean(reader["IsBorrowed"])
            },
            new SqlParameter("@Id", id)
        );
        return bookVm;
    }

    public void Insert(BookCreateVm bookVm)
    {
        string sql =
            "INSERT INTO Books(Title, PublishYear, AuthorID, CategoryID, PublisherId) VALUES (@Title, @PublishYear, @AuthorID, @CategoryID, @PublisherId)";

        ExecuteCommand(sql, new SqlParameter("@Title", bookVm.Title),
            new SqlParameter("@PublishYear", bookVm.PublishYear),
            new SqlParameter("@AuthorID", bookVm.AuthorID),
            new SqlParameter("@CategoryID", bookVm.CategoryID),
            new SqlParameter("@PublisherId", bookVm.PublisherId)
        );
    }

    public void Update(BookEditVm bookVm)
    {
        string sql =
            "UPDATE Books SET Title = @Title, PublishYear = @PublishYear, PublisherId = @PublisherId, CategoryID = @CategoryID WHERE Id = @Id";

        ExecuteCommand(sql, new SqlParameter("@Title", bookVm.Title),
            new SqlParameter("@PublishYear", bookVm.PublishYear),
            new SqlParameter("@PublisherId", bookVm.PublisherId),
            new SqlParameter("@CategoryID", bookVm.CategoryID),
            new SqlParameter("@Id", bookVm.Id)

        );
    }

    public void Delete(BookDeleteVm bookVm)
    {
        string sql = "UPDATE Books SET IsActive = 0 WHERE Id = @Id";
        
        ExecuteCommand(sql, new SqlParameter("@Id", bookVm.Id));
    }
}
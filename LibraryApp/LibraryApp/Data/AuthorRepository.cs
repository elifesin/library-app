using LibraryApp.Models.Author;
using Microsoft.Data.SqlClient;
using Dapper;

namespace LibraryApp.Data;

public class AuthorRepository : RepositoryBase
{
    public AuthorRepository(DbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }

    public List<AuthorVm> GetAllAuthors()
    {
        string sql = "SELECT * FROM Authors WHERE IsActive = 1";
        
        return Connection.Query<AuthorVm>(sql).ToList();
    }

    public AuthorVm GetAuthorById(int id)
    {
        string sql = "SELECT * FROM Authors WHERE Id = @Id"; 
        return Connection.QuerySingleOrDefault<AuthorVm>(sql, new { Id = id });
    }

    public void Insert(AuthorCreateVm authorVm)
    {
        string sql = "INSERT INTO Authors(FirstName, LastName) VALUES (@FirstName, @LastName)";
        
        Connection.Execute(sql, authorVm);

    }

    public void Update(AuthorEditVm authorVm)
    {
        string sql = "UPDATE Authors SET FirstName = @FirstName, LastName = @LastName WHERE Id = @Id";
        
        Connection.Execute(sql, authorVm);
    }

    public void Delete(int id)
    {
        string sql = "UPDATE Authors SET IsActive = 0 WHERE Id = @Id";
        
        Connection.Execute(sql, new { Id = id });
    }
}
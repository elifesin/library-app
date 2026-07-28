using LibraryApp.Models.Author;
using Microsoft.Data.SqlClient;

namespace LibraryApp.Data;

public class AuthorRepository : RepositoryBase
{
    public AuthorRepository(IConfiguration configuration) : base(configuration){}

    public List<AuthorVm> GetAllAuthors()
    {
        string sql = "SELECT * FROM Authors WHERE IsActive = 1";
        
        return ExecuteReadQuery<AuthorVm>(sql);
    }

    public AuthorVm GetAuthorById(int id)
    {
        string sql = "SELECT * FROM Authors WHERE Id = @Id"; 
        return ExecuteReadSingle<AuthorVm>(sql, new { Id = id });
    }

    public void Insert(AuthorCreateVm authorVm)
    {
        string sql = "INSERT INTO Authors(FirstName, LastName) VALUES (@FirstName, @LastName)";
        
        ExecuteCommand(sql, authorVm);

    }

    public void Update(AuthorEditVm authorVm)
    {
        string sql = "UPDATE Authors SET FirstName = @FirstName, LastName = @LastName WHERE Id = @Id";
        
        ExecuteCommand(sql, authorVm);
    }

    public void Delete(int id)
    {
        string sql = "UPDATE Authors SET IsActive = 0 WHERE Id = @Id";
        
        ExecuteCommand(sql, new { Id = id });
    }
}
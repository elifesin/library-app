using LibraryApp.Models.Author;
using Microsoft.Data.SqlClient;

namespace LibraryApp.Data;

public class AuthorRepository : RepositoryBase
{
    public AuthorRepository(IConfiguration configuration) : base(configuration){}

    public List<AuthorVm> GetAllAuthors()
    {
        string sql = "SELECT * FROM Authors WHERE IsActive = 1";

        List<AuthorVm> vmList = ExecuteReadQuery<AuthorVm>(sql, reader => new AuthorVm
        {
            Id = Convert.ToInt32(reader["Id"]),
            FirstName = reader["FirstName"].ToString()!,
            LastName = reader["LastName"].ToString()!
        });
        return vmList;
    }

    public AuthorVm GetAuthorById(int id)
    {
        string sql = "SELECT * FROM Authors WHERE Id = @Id";

        AuthorVm authorVm = ExecuteReadSingle<AuthorVm>(sql, reader => new AuthorVm
            {
                Id = Convert.ToInt32(reader["Id"]),
                FirstName = reader["FirstName"].ToString()!,
                LastName = reader["LastName"].ToString()!
            },
            new SqlParameter("@Id", id)
        );
        return authorVm;
    }

    public void Insert(AuthorCreateVm authorVm)
    {
        string sql = "INSERT INTO Authors(FirstName, LastName) VALUES (@FirstName, @LastName)";
        
        ExecuteCommand(sql, new SqlParameter("@FirstName", authorVm.FirstName),
            new SqlParameter("@LastName", authorVm.LastName));

    }

    public void Update(AuthorEditVm authorVm)
    {
        string sql = "UPDATE Authors SET FirstName = @FirstName, LastName = @LastName WHERE Id = @Id";
        
        ExecuteCommand(sql, new SqlParameter("@FirstName", authorVm.FirstName),
            new SqlParameter("@LastName", authorVm.LastName),
            new SqlParameter("@Id", authorVm.Id));
    }

    public void Delete(int id)
    {
        string sql = "UPDATE Authors SET IsActive = 0 WHERE Id = @Id";
        
        ExecuteCommand(sql, new SqlParameter("@Id", id));
    }
}
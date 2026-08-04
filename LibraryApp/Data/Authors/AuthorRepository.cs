using Dapper;
using Data.Base;
using Data.Connection;
using Domain.Authors;

namespace Data.Authors;

public class AuthorRepository : RepositoryBase, IAuthorRepository
{
    public AuthorRepository(DbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) {}

    public List<Author> GetAll()
    {
        string sql = "SELECT * FROM Authors WHERE IsActive = 1";
        
        return Connection.Query<Author>(sql).ToList();
    }

    public Author GetById(int id)
    {
        string sql = "SELECT * FROM Authors WHERE Id = @Id"; 

        return Connection.QuerySingleOrDefault<Author>(sql, new { Id = id });
    }

    public void Insert(Author author)
    {
        string sql = "INSERT INTO Authors(FirstName, LastName) VALUES (@FirstName, @LastName)";
        
        Connection.Execute(sql, author);
    }

    public void Update(Author author)
    {
        string sql = "UPDATE Authors SET FirstName = @FirstName, LastName = @LastName WHERE Id = @Id";

        Connection.Execute(sql, author);
    }

    public void Delete(int id)
    {
        string sql = "UPDATE Authors SET IsActive = 0 WHERE Id = @Id";
        
        Connection.Execute(sql, new {Id = id});
    }
}
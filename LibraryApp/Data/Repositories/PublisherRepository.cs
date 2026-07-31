using Dapper;
using Domain;

namespace Data.Repositories;

public class PublisherRepository : RepositoryBase
{
    // 1. Dependency Injection (DI) artık IConfiguration değil, DbConnectionFactory üzerinden sağlanıyor
    public PublisherRepository(DbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
    {
    }

    public List<Publisher> GetAll()
    {
        string sql = "SELECT * FROM Publishers WHERE IsActive = 1";

        return Connection.Query<Publisher>(sql).ToList();
    }

    public Publisher GetById(int id)
    {
        string sql = "SELECT * FROM Publishers WHERE Id = @Id";

        return Connection.QuerySingleOrDefault<Publisher>(sql, new { Id = id });
    }

    // 2. Vm takıları kaldırıldı, 3. Dapper'ın Execute metodu eklendi
    public void Insert(Publisher publisher)
    {
        string sql = "INSERT INTO Publishers (Name) VALUES (@Name)";

        Connection.Execute(sql, publisher);
    }

    public void Update(Publisher publisher)
    {
        string sql = "UPDATE Publishers SET Name = @Name WHERE Id = @Id";

        Connection.Execute(sql, publisher);
    }

    public void Delete(int id)
    {
        string sql = "UPDATE Publishers SET IsActive = 0 WHERE Id = @Id";

        Connection.Execute(sql, new { Id = id });
    }
}
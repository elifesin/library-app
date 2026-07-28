using LibraryApp.Models.Publisher;
using Dapper;

namespace LibraryApp.Data;

public class PublisherRepository : RepositoryBase
{
    
    public PublisherRepository(DbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }

    public List<PublisherVm> GetAllPublishers()
    {
        string sql = "SELECT * FROM Publishers WHERE IsActive = 1";

        return Connection.Query<PublisherVm>(sql).ToList();
    }

    public PublisherVm GetPublisherById(int id)
    {
        string sql = "SELECT * FROM Publishers WHERE Id = @Id";

        return Connection.QuerySingleOrDefault<PublisherVm>(sql, new{Id = id});
    }

    public void Insert(PublisherVm publisherVm)
    {
        string sql = "INSERT INTO Publishers(Name) VALUES(@Name)";
        
        Connection.Execute(sql, publisherVm);
    }

    public void Update(PublisherVm publisherVm)
    {
        string sql = "UPDATE Publishers SET Name = @Name WHERE Id = @Id";
        
        Connection.Execute(sql, publisherVm);
    }

    public void Delete(int id)
    {
        string sql = "UPDATE Publishers SET IsActive = 0 WHERE Id = @Id";
        
        Connection.Execute(sql, new {Id = id});
    }
}
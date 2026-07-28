using LibraryApp.Models.Publisher;

namespace LibraryApp.Data;

public class PublisherRepository : RepositoryBase
{
    
    public PublisherRepository(IConfiguration configuration) : base(configuration) { }

    public List<PublisherVm> GetAllPublishers()
    {
        string sql = "SELECT * FROM Publishers WHERE IsActive = 1";

        return ExecuteReadQuery<PublisherVm>(sql);
    }

    public PublisherVm GetPublisherById(int id)
    {
        string sql = "SELECT * FROM Publishers WHERE Id = @Id";

        return ExecuteReadSingle<PublisherVm>(sql, new{Id = id});
    }

    public void Insert(PublisherVm publisherVm)
    {
        string sql = "INSERT INTO Publishers(Name) VALUES(@Name)";
        
        ExecuteCommand(sql, publisherVm);
    }

    public void Update(PublisherVm publisherVm)
    {
        string sql = "UPDATE Publishers SET Name = @Name WHERE Id = @Id";
        
        ExecuteCommand(sql, publisherVm);
    }

    public void Delete(int id)
    {
        string sql = "UPDATE Publishers SET IsActive = 0 WHERE Id = @Id";
        
        ExecuteCommand(sql, new {Id = id});
    }
}
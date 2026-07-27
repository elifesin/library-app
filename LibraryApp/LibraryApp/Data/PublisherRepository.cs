using LibraryApp.Models.Publisher;
using Microsoft.Data.SqlClient;

namespace LibraryApp.Data;

public class PublisherRepository : RepositoryBase
{
    
    public PublisherRepository(IConfiguration configuration) : base(configuration) { }

    public List<PublisherVm> GetAllPublishers()
    {
        string sql = "SELECT * FROM Publishers WHERE IsActive = 1";

        List<PublisherVm> vmList = ExecuteReadQuery<PublisherVm>(sql, reader => new PublisherVm
        {
            Id = Convert.ToInt32(reader["Id"]),
            Name = reader["Name"].ToString()!,
            IsActive = Convert.ToBoolean(reader["IsActive"])
        });
        return vmList;
    }

    public PublisherVm GetPublisherById(int id)
    {
        string sql = "SELECT * FROM Publishers WHERE Id = @Id";

        PublisherVm publisherVm = ExecuteReadSingle<PublisherVm>(sql, reader => new PublisherVm
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString()!
            },
            new SqlParameter("@Id", id)
        );
        return publisherVm;
    }

    public void Insert(PublisherVm publisherVm)
    {
        string sql = "INSERT INTO Publishers (Name) VALUES (@Name)";
        
        ExecuteCommand(sql, new SqlParameter("@Name", publisherVm.Name));
    }

    public void Update(PublisherVm publisherVm)
    {
        string sql = "UPDATE Publishers SET Name = @Name WHERE Id = @Id";
        
        ExecuteCommand(sql, new SqlParameter("@Name", publisherVm.Name));
    }

    public void Delete(int id)
    {
        string sql = "UPDATE Publishers SET IsActive = 0 WHERE Id = @Id";
        
        ExecuteCommand(sql, new SqlParameter("@Id", id));
    }
}
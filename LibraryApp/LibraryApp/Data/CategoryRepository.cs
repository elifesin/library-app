using LibraryApp.Models.Category;
using Microsoft.Data.SqlClient;

namespace LibraryApp.Data;

public class CategoryRepository : RepositoryBase
{
    // Constructor (Yapıcı Metot) base sınıfa gönderiliyor
    public CategoryRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public List<CategoryVm> GetAll()
    {
        var sql = "SELECT * FROM Categories WHERE IsActive = 1";

        return ExecuteReadQuery<CategoryVm>(sql);
    }

    public CategoryVm GetById(int id)
    {
        string sql = "SELECT * FROM Categories WHERE Id = @Id";
        
        return ExecuteReadSingle<CategoryVm>(sql, new {Id = id});
    }
    
    public void Insert(CategoryVm categoryVm)
    {
        string sql = "INSERT INTO Categories(CategoryName) VALUES (@CategoryName)";

        ExecuteCommand(sql, categoryVm);
    }
    
    public void Update(CategoryVm categoryVm)
    {
        string sql = "UPDATE Categories SET CategoryName = @CategoryName WHERE Id = @Id";
        
        ExecuteCommand(sql, categoryVm);
    }

    // DELETE (SOFT DELETE) METODU
    public void Delete(int id)
    {
        string sql = "UPDATE Categories SET IsActive = 0 WHERE Id = @Id";
        
        ExecuteCommand(sql, new {Id = id});
    }
}
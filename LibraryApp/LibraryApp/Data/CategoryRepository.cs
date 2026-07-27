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

        List<CategoryVm> vmList = ExecuteReadQuery<CategoryVm>(sql, reader => new CategoryVm
        {
            Id = Convert.ToInt32(reader["Id"]),
            CategoryName = reader["CategoryName"].ToString()!,
            IsActive = Convert.ToBoolean(reader["IsActive"])
        });
        return vmList;
    }

    public CategoryVm GetById(int id)
    {
        string sql = "SELECT * FROM Categories WHERE Id = @Id";

        CategoryVm category = ExecuteReadSingle<CategoryVm>(sql, reader => new CategoryVm
            {
                Id = Convert.ToInt32(reader["Id"]),
                CategoryName = reader["CategoryName"].ToString()!,
                IsActive = Convert.ToBoolean(reader["IsActive"])
            },
            new SqlParameter("@Id", id)
        );
        return category;
    }
    
    public void Insert(CategoryVm categoryVm)
    {
        string sql = "INSERT INTO Categories(CategoryName) VALUES (@CategoryName)";

        // Sadece ExecuteCommand çağrılır ve parametre gönderilir
        ExecuteCommand(sql, new SqlParameter("@CategoryName", categoryVm.CategoryName));
    }
    
    public void Update(CategoryVm categoryVm)
    {
        string sql = "UPDATE Categories SET CategoryName = @CategoryName WHERE Id = @Id";
        
        // ExecuteCommand'a birden fazla parametre virgülle eklenebilir
        ExecuteCommand(sql, 
            new SqlParameter("@CategoryName", categoryVm.CategoryName),
            new SqlParameter("@Id", categoryVm.Id)
        );
    }

    // DELETE (SOFT DELETE) METODU
    public void Delete(int id)
    {
        string sql = "UPDATE Categories SET IsActive = 0 WHERE Id = @Id";
        
        ExecuteCommand(sql, new SqlParameter("@Id", id));
    }
}
using LibraryApp.Models.Category;
using Dapper;
using Microsoft.Data.SqlClient;

namespace LibraryApp.Data;

public class CategoryRepository : RepositoryBase
{
    // Constructor (Yapıcı Metot) base sınıfa gönderiliyor
    public CategoryRepository(DbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }

    public List<CategoryVm> GetAll()
    {
        var sql = "SELECT * FROM Categories WHERE IsActive = 1";

        return Connection.Query<CategoryVm>(sql).ToList();
    }

    public CategoryVm GetById(int id)
    {
        string sql = "SELECT * FROM Categories WHERE Id = @Id";
        
        return Connection.QuerySingleOrDefault<CategoryVm>(sql, new {Id = id});
    }
    
    public void Insert(CategoryVm categoryVm)
    {
        string sql = "INSERT INTO Categories(CategoryName) VALUES (@CategoryName)";

        Connection.Execute(sql, categoryVm);
    }
    
    public void Update(CategoryVm categoryVm)
    {
        string sql = "UPDATE Categories SET CategoryName = @CategoryName WHERE Id = @Id";
        
        Connection.Execute(sql, categoryVm);
    }

    // DELETE (SOFT DELETE) METODU
    public void Delete(int id)
    {
        string sql = "UPDATE Categories SET IsActive = 0 WHERE Id = @Id";
        
        Connection.Execute(sql, new {Id = id});
    }
}
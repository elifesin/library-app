using Dapper;
using Ee.Ebs.Domain.Categories;
using Ee.Ebs.Data.Dapper.Base;
using Ee.Ebs.Data.Dapper.Connection;

namespace Ee.Ebs.Data.Dapper.Categories;

public class CategoryRepository : RepositoryBase, ICategoryRepository
{
    // Constructor (Yapıcı Metot) base sınıfa gönderiliyor
    public CategoryRepository(DbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }

    public List<Category> GetAll()
    {
        var sql = "SELECT * FROM Categories WHERE IsActive = 1";

        return Connection.Query<Category>(sql).ToList();
    }

    public Category GetById(int id)
    {
        string sql = "SELECT * FROM Categories WHERE Id = @Id";
        
        return Connection.QuerySingleOrDefault<Category>(sql, new {Id = id});
    }
    
    public void Insert(Category categoryVm)
    {
        string sql = "INSERT INTO Categories(CategoryName) VALUES (@CategoryName)";

        // Sadece ExecuteCommand çağrılır ve parametre gönderilir
        Connection.Execute(sql, new { CategoryName = categoryVm.CategoryName });
    }
    
    public void Update(Category category)
    {
        string sql = "UPDATE Categories SET CategoryName = @CategoryName WHERE Id = @Id";

        // ExecuteCommand'a birden fazla parametre virgülle eklenebilir
        Connection.Execute(sql, new { CategoryName = category.CategoryName, Id = category.Id });
    }

    // DELETE (SOFT DELETE) METODU
    public void Delete(int id)
    {
        string sql = "UPDATE Categories SET IsActive = 0 WHERE Id = @Id";
        
        Connection.Execute(sql, new { Id =  id});
    }
}
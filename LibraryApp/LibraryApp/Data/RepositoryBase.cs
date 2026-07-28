using Microsoft.Data.SqlClient;
using Dapper;

namespace LibraryApp.Data;

public class RepositoryBase
{
    public readonly string _connectionString;

    public RepositoryBase(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    public List<T> ExecuteReadQuery<T>(string sqlQuery, object parameters = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            return connection.Query<T>(sqlQuery, parameters).ToList(); // Birden fazla satır döndüren SELECT sorguları için 
        }
    }
    
    public T ExecuteReadSingle<T>(string sqlQuery, object parameters = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            return connection.QueryFirstOrDefault<T>(sqlQuery, parameters); // Tek bir satır dönmesi beklenen sorgular için
        }
    }
    
    public int ExecuteCommand(string sqlQuery, object parameters = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            return connection.Execute(sqlQuery, parameters); // Veri değiştiren sorgular için (INSERT / UPDATE / DELETE)
        }
    }
}
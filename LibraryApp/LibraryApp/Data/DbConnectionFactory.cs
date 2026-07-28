using System.Data;
using Microsoft.Data.SqlClient;

namespace LibraryApp.Data;

public class DbConnectionFactory
{
    private readonly string _connectionString;
    private IDbConnection _connection;

    public DbConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IDbConnection Connection
    
    {
        get
        {
            // Eğer bağlantı henüz hiç açılmadıysa oluştur
            if (_connection == null)
            {
                _connection = new SqlConnection(_connectionString);
            }

            // Zaten oluşturulmuşsa elindekini döndür (REUSE)
            return _connection;
        }
    }
}
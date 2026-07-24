using Microsoft.Data.SqlClient;

namespace LibraryApp.Data;

public class RepositoryBase
{
    public readonly string _connectionString;

    public RepositoryBase(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    public List<T> ExecuteReadQuery<T>(string sqlQuery, Func<SqlDataReader, T> mapper)
    {
        List<T> resultList = new List<T>();

        using (var connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T mappedModel = mapper(reader);
                        resultList.Add(mappedModel);
                    }
                }
            } 
        }
    
        return resultList;
    }
    
    public T ExecuteReadSingle<T>(string sqlQuery, Func<SqlDataReader, T> mapper, params SqlParameter[] parameters)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                // Parametre varsa ekle
                if (parameters != null && parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    // while yerine if kullanıyoruz çünkü tek bir satır bekliyoruz
                    if (reader.Read())
                    {
                        return mapper(reader); // Kayıt bulunduysa map et ve döndür
                    }
                }
            } 
        }
        // Kayıt bulunamazsa tipin varsayılan değerini (class ise null) döndür
        return default(T)!; 
    }
    
    public int ExecuteCommand(string sqlQuery, params SqlParameter[] parameters)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                // Parametre varsa ekle
                if (parameters != null && parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();
                // ExecuteNonQuery, etkilenen satır sayısını döndürür
                return command.ExecuteNonQuery(); 
            }
        }
    }

}
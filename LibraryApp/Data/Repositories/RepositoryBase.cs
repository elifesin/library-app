using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Data.Repositories;

public class RepositoryBase
{
    // Sadece bu sınıfı miras alan (inherit eden) sınıfların erişebilmesi için "protected" yapıyoruz.
    protected readonly IDbConnection Connection;

    // Dependency Injection (DI) üzerinden Factory'i alıyoruz.
    protected RepositoryBase(DbConnectionFactory dbConnectionFactory)
    {
        // Factory'nin içindeki Connection özelliğini çağırıp, miras vereceğimiz Connection'a eşitliyoruz.
        Connection = dbConnectionFactory.Connection;    }
}

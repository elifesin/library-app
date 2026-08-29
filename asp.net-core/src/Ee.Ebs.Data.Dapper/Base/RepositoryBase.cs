using System.Data;
using Ee.Ebs.Data.Dapper.Connection;

namespace Ee.Ebs.Data.Dapper.Base;

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

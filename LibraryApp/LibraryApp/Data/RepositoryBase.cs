using System.Data;

namespace LibraryApp.Data;

public class RepositoryBase
{
    protected readonly DbConnectionFactory _dbConnectionFactory;

    public RepositoryBase(DbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public IDbConnection Connection => _dbConnectionFactory.Connection;
}
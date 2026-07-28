using Dapper;
using LibraryApp.Models.Members;
using Microsoft.Data.SqlClient;

namespace LibraryApp.Data;

public class MemberRepository : RepositoryBase
{
    public MemberRepository(DbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }

    public List<MemberVm> GetAllMembers()
    {
        string sql = "SELECT * FROM Members WHERE IsActive = 1";

        return Connection.Query<MemberVm>(sql).ToList();
    }

    public MemberVm GetMemberById(int id)
    {
        string sql = "SELECT * FROM Members WHERE ID = @ID";

        return Connection.QueryFirstOrDefault<MemberVm>(sql, new{ Id = id});
    }

    public void Insert(MemberCreateVm memberVm)
    {
        string sql = "INSERT INTO Members(FirstName, LastName) VALUES(@FirstName, @LastName)";
        
        Connection.Execute(sql, memberVm);
    }

    public void Update(MemberVm memberVm)
    {
        string sql = "UPDATE Members SET FirstName = @FirstName, LastName = @LastName WHERE ID = @ID";
        
        Connection.Execute(sql, memberVm);
    }

    public void Delete(int id)
    {
        string sql = "UPDATE Members SET IsActive = 0 WHERE ID = @ID";
        
        Connection.Execute(sql, new { Id = id });
    }
}
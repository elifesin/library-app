using System.Collections.Generic;
using System.Linq;
using Dapper;
using Ee.Ebs.Domain.Members;
using Ee.Ebs.Data.Dapper.Base;
using Ee.Ebs.Data.Dapper.Connection;

namespace Ee.Ebs.Data.Dapper.Members;

public class MemberRepository : RepositoryBase, IMemberRepository
{
    public MemberRepository(DbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }

    public List<Member> GetAll()
    {
        string sql = "SELECT * FROM Members WHERE IsActive = 1";

        return Connection.Query<Member>(sql).ToList();
    }

    public Member GetById(int id)
    {
        string sql = "SELECT * FROM Members WHERE ID = @ID";

        return Connection.QueryFirstOrDefault<Member>(sql, new{ Id = id});
    }

    public void Insert(Member member)
    {
        string sql = "INSERT INTO Members(FirstName, LastName) VALUES(@FirstName, @LastName)";

        Connection.Execute(sql, member);
    }

    public void Update(Member member)
    {
        string sql = "UPDATE Members SET FirstName = @FirstName, LastName = @LastName WHERE ID = @ID";

        Connection.Execute(sql, member);
    }

    public void Delete(int id)
    {
        string sql = "UPDATE Members SET IsActive = 0 WHERE ID = @ID";

        // SQL sorgusundaki @ID parametresini, metoda dışarıdan gelen 'id' değişkeni ile eşleştiriyoruz
        Connection.Execute(sql, new { ID = id });
    }

    public List<Member> GetActiveMembers()
    {
        string sql = "SELECT * FROM Members WHERE IsActive = 1";
        return Connection.Query<Member>(sql).ToList();
    }
}
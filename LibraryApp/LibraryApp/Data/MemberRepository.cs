using LibraryApp.Models.Members;
using Microsoft.Data.SqlClient;

namespace LibraryApp.Data;

public class MemberRepository : RepositoryBase
{
    public MemberRepository(IConfiguration configuration) : base(configuration) { }

    public List<MemberVm> GetAllMembers()
    {
        string sql = "SELECT * FROM Members WHERE IsActive = 1";

        List<MemberVm> vmList = ExecuteReadQuery<MemberVm>(sql, reader => new MemberVm
        {
            ID = Convert.ToInt32(reader["Id"]),
            FirstName = reader["FirstName"].ToString(),
            LastName = reader["LastName"].ToString(),
        });
        return vmList;
    }

    public MemberVm GetMemberById(int id)
    {
        string sql = "SELECT * FROM Members WHERE ID = @ID";

        MemberVm memberVm = ExecuteReadSingle<MemberVm>(sql, reader => new MemberVm
        {
            ID = Convert.ToInt32(reader["Id"]),
            FirstName = reader["FirstName"].ToString(),
            LastName = reader["LastName"].ToString(),
        },
            new SqlParameter("ID", id)
            );
        return memberVm;
    }

    public void Insert(MemberCreateVm memberVm)
    {
        string sql = "INSERT INTO Members(FirstName, LastName) VALUES(@FirstName, @LastName)";
        
        ExecuteCommand(sql, new SqlParameter("FirstName", memberVm.FirstName),
            new SqlParameter("LastName", memberVm.LastName)
            );
    }

    public void Update(MemberVm memberVm)
    {
        string sql = "UPDATE Members SET FirstName = @FirstName, LastName = @LastName WHERE ID = @ID";
        
        ExecuteCommand(sql, new SqlParameter("FirstName", memberVm.FirstName),
            new SqlParameter("LastName", memberVm.LastName),
            new SqlParameter("ID", memberVm.ID)
        );
    }

    public void Delete(int id)
    {
        string sql = "UPDATE Members SET IsActive = 0 WHERE ID = @ID";
        
        ExecuteCommand(sql, new SqlParameter("ID", id));
    }
}
using Ee.Ebs.Domain.Members;
using Ee.Ebs.Data.EfCore.Contexts;

namespace Ee.Ebs.Data.EfCore.Members;

public class MemberRepository : IMemberRepository
{
    private readonly AppDbContext _context;
    
    public MemberRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Member> GetAll()
    {
        // SELECT WHERE IsActive = 1
        return _context.Members.Where(m => m.IsActive).ToList(); 
    }

    public Member GetById(int id)
    {
        // SELECT WHERE @ID = Id
        return _context.Members.FirstOrDefault(m => m.ID == id &&  m.IsActive);
    }

    public void Insert(Member member)
    {
        // INSERT INTO
        _context.Members.Add(member);
        _context.SaveChanges();
    }

    public void Update(Member member)
    {
        // UPDATE Members SET FirstName = @FirstName
        _context.Members.Update(member);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        // UPDATE Members SET IsActive = 0 WHERE Id = @ID 
        var member = _context.Members.FirstOrDefault(m => m.ID == id);
            
        if (member != null)
        {
            member.IsActive = false; 
            _context.SaveChanges();
        }
    }
}
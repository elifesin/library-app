namespace Ee.Ebs.Domain.Members;

public interface IMemberRepository
{
    public List<Member> GetAll();
    public Member GetById(int id);
    public void Insert(Member member);
    public void Update(Member member);
    public void Delete(int id);
}
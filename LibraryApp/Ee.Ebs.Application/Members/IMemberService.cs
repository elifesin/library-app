using Ee.Ebs.Application.Members.DTOs;

namespace Ee.Ebs.Application.Members;

public interface IMemberService
{
    List<MemberDto> GetAll();
    MemberDto GetById(int id);
    void Insert(MemberCreateDto member);
    void Update(MemberDto member);
    void Delete(int id);
}
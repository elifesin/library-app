using Ee.Ebs.Application.Contracts.Members.DTOs;

namespace Ee.Ebs.Application.Contracts.Members;

public interface IMemberAppService
{
    List<MemberDto> GetAll();
    MemberDto GetById(int id);
    void Insert(MemberCreateDto member);
    void Update(int id, MemberDto dto);
    void Delete(int id);
}
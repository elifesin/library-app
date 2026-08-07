using AutoMapper;
using Ee.Ebs.Application.Contracts.Members;
using Ee.Ebs.Application.Contracts.Members.DTOs;
using Ee.Ebs.Domain.Loans;
using Ee.Ebs.Domain.Members;

namespace Ee.Ebs.Application.Members;

public class MemberService : IMemberService
{
    private readonly IMapper _mapper;
    private readonly IMemberRepository _memberRepository;
    private readonly ILoanRepository _loanRepository;

    public MemberService(IMapper mapper, IMemberRepository memberRepository, ILoanRepository loanRepository)
    {
        _mapper = mapper;
        _memberRepository = memberRepository;
        _loanRepository = loanRepository;
    }

    public List<MemberDto> GetAll()
    {
        var members = _memberRepository.GetAll();
        return _mapper.Map<List<MemberDto>>(members);
    }

    public MemberDto GetById(int id)
    {
        var member = _memberRepository.GetById(id);
        return _mapper.Map<MemberDto>(member);
    }

    public void Insert(MemberCreateDto member)
    {
        var memberEntity = _mapper.Map<Member>(member);
        memberEntity.IsActive = true; 
        _memberRepository.Insert(memberEntity);
    }

    public void Update(MemberDto member)
    {
        var memberEntity = _mapper.Map<Member>(member);
        memberEntity.IsActive = true;
        _memberRepository.Update(memberEntity);
    }
    
    public void Delete(int id)
    {
        _memberRepository.Delete(id);
    }
}
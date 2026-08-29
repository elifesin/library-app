using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Ee.Ebs.Application.Contracts.Members;
using Ee.Ebs.Application.Contracts.Members.DTOs;
using Ee.Ebs.Domain.Loans;
using Ee.Ebs.Domain.Members;

namespace Ee.Ebs.Application.Members;

public class MemberAppService : IMemberAppService
{
    private readonly IMapper _mapper;
    private readonly IMemberRepository _memberRepository;
    private readonly ILoanRepository _loanRepository;

    public MemberAppService(IMapper mapper, IMemberRepository memberRepository, ILoanRepository loanRepository)
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
        bool ifExists = _memberRepository.GetAll().Any(m =>
            m.FirstName.ToLower() == member.FirstName.ToLower() &&
            m.LastName.ToLower() == member.LastName.ToLower());

        if (ifExists)
        {
            throw new InvalidOperationException($"Kayıtlı bir üyeyi tekrar kayıt edemezsiniz!");
        }
        var memberEntity = _mapper.Map<Member>(member);
        memberEntity.IsActive = true; 
        _memberRepository.Insert(memberEntity);
    }

    public void Update(int id, MemberDto dto)
    {
        var member = _memberRepository.GetById(id);
        _mapper.Map(dto, member);
        _memberRepository.Update(member);
    }
    
    public void Delete(int id)
    {
        _memberRepository.Delete(id);
    }
}
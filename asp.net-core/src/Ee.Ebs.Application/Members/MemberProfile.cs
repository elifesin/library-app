using AutoMapper;
using Ee.Ebs.Application.Contracts.Members.DTOs;
using Ee.Ebs.Domain.Members;

namespace Ee.Ebs.Application.Members;

public class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<MemberDto, Member>();
        CreateMap<Member, MemberDto>();
        
        CreateMap<MemberCreateDto, Member>();
        CreateMap<Member,  MemberCreateDto>();
        
        CreateMap<MemberBorrowedBooksDto, Member>();
        CreateMap<Member, MemberBorrowedBooksDto>();
    }
}
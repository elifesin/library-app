using Application.Members.DTOs;
using AutoMapper;
using Domain.Members;

namespace Application.Members;

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
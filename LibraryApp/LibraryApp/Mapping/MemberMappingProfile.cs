using AutoMapper;
using Data.Entities;
using LibraryApp.Models.Members;

namespace LibraryApp.Mapping;

public class MemberMappingProfile : Profile
{
    public MemberMappingProfile()
    {
        CreateMap<Member, MemberVm>();
        CreateMap<MemberVm, Member>();

        CreateMap<Member, MemberCreateVm>();
        CreateMap<MemberCreateVm, Member>();
    }
}
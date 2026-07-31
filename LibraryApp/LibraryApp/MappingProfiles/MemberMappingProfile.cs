using AutoMapper;
using Domain;
using LibraryApp.Models.Members;

namespace LibraryApp.MappingProfiles;

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
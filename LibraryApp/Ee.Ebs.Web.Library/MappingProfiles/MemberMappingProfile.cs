using AutoMapper;
using Ee.Ebs.Application.Contracts.Members.DTOs;
using Ee.Ebs.Web.Library.ViewModels.Members;

namespace Ee.Ebs.Web.Library.MappingProfiles;

public class MemberMappingProfile : Profile
{
    public MemberMappingProfile()
    {
        CreateMap<MemberDto, MemberVm>().ReverseMap();

        CreateMap<MemberCreateDto, MemberCreateVm>().ReverseMap();
        
        CreateMap<MemberBorrowedBooksDto, MemberBorrowedBooksVm>().ReverseMap();
    }
}
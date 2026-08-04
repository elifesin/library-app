using Application.Members.DTOs;
using AutoMapper;
using LibraryApp.Models.Members;

namespace LibraryApp.MappingProfiles;

public class MemberMappingProfile : Profile
{
    public MemberMappingProfile()
    {
        CreateMap<MemberDto, MemberVm>().ReverseMap();

        CreateMap<MemberCreateDto, MemberCreateVm>().ReverseMap();
        
        CreateMap<MemberBorrowedBooksDto, MemberBorrowedBooksVm>().ReverseMap();
    }
}
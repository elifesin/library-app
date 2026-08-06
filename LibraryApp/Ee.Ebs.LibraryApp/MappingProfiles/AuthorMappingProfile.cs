using AutoMapper;
using Ee.Ebs.Application.Authors.DTOs;
using Ee.Ebs.LibraryApp.Models.Author;

namespace Ee.Ebs.LibraryApp.MappingProfiles;

public class AuthorMappingProfile : Profile
{
    public AuthorMappingProfile()
    {
        CreateMap<AuthorDto, AuthorVm>().ReverseMap();
        
        CreateMap<AuthorCreateDto, AuthorCreateVm>().ReverseMap();
        
        CreateMap<AuthorEditDto, AuthorEditVm>().ReverseMap();
        
        CreateMap<AuthorDto, AuthorEditVm>().ReverseMap();
    }
}
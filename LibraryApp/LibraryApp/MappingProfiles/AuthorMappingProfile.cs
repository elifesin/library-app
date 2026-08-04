using Application.Authors.DTOs;
using AutoMapper;
using LibraryApp.Models.Author;

namespace LibraryApp.MappingProfiles;

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
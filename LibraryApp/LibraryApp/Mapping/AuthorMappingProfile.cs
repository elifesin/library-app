using AutoMapper;
using Data.Entities;
using LibraryApp.Models.Author;

namespace LibraryApp.Mapping;

public class AuthorMappingProfile : Profile
{
    public AuthorMappingProfile()
    {
        CreateMap<Author, AuthorVm>();
        CreateMap<AuthorVm, Author>();
        
        CreateMap<Author, AuthorCreateVm>();
        CreateMap<AuthorCreateVm, Author>();
        
        CreateMap<Author, AuthorEditVm>();
        CreateMap<AuthorEditVm, Author>();
    }
}
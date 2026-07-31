using AutoMapper;
using Domain;
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
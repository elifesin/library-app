using AutoMapper;
using Ee.Ebs.Domain.Authors;
using Ee.Ebs.Application.Authors.DTOs;

namespace Ee.Ebs.Application.Authors;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        // Veritabanından (Entity) -> Ekrana (DTO) giderken
        CreateMap<Author, AuthorDto>();
        CreateMap<AuthorDto, Author>();

        // Ekrandan (DTO) -> Veritabanına (Entity) gelirken
        CreateMap<AuthorCreateDto, Author>();
        CreateMap<Author, AuthorCreateDto>();
        
        CreateMap<AuthorEditDto, Author>();
        CreateMap<Author, AuthorEditDto>();

    }
}
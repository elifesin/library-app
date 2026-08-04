using Application.Authors.DTOs;
using AutoMapper;
using Domain.Authors;

namespace Application.Authors;

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
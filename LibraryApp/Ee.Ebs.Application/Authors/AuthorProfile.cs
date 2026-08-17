using AutoMapper;
using Ee.Ebs.Application.Contracts.Authors.DTOs;
using Ee.Ebs.Domain.Authors;

namespace Ee.Ebs.Application.Authors;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<AuthorDto, Author>();

        CreateMap<AuthorCreateDto, Author>();
        CreateMap<Author, AuthorCreateDto>();
        
        CreateMap<AuthorEditDto, Author>();
        CreateMap<Author, AuthorEditDto>();
    }
}
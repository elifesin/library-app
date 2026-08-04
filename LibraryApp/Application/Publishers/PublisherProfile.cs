using Application.Publishers.DTOs;
using AutoMapper;
using Domain.Publishers;

namespace Application.Publishers;

public class PublisherProfile : Profile
{
    public PublisherProfile()
    {
        CreateMap<Publisher, PublisherDto>().ReverseMap();
        CreateMap<PublisherDto, Publisher>().ReverseMap();
    }
}
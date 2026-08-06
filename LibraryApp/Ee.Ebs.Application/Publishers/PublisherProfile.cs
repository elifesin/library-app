using AutoMapper;
using Ee.Ebs.Domain.Publishers;
using Ee.Ebs.Application.Publishers.DTOs;

namespace Ee.Ebs.Application.Publishers;

public class PublisherProfile : Profile
{
    public PublisherProfile()
    {
        CreateMap<Publisher, PublisherDto>().ReverseMap();
        CreateMap<PublisherDto, Publisher>().ReverseMap();
    }
}
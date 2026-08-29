using AutoMapper;
using Ee.Ebs.Application.Contracts.Publishers.DTOs;
using Ee.Ebs.Domain.Publishers;

namespace Ee.Ebs.Application.Publishers;

public class PublisherProfile : Profile
{
    public PublisherProfile()
    {
        CreateMap<Publisher, PublisherDto>().ReverseMap();
        CreateMap<PublisherDto, Publisher>().ReverseMap();
    }
}
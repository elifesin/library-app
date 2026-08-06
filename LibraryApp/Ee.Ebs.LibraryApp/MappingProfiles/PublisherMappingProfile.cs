using AutoMapper;
using Ee.Ebs.Application.Publishers.DTOs;
using Ee.Ebs.LibraryApp.Models.Publisher;

namespace Ee.Ebs.LibraryApp.MappingProfiles;

public class PublisherMappingProfile : Profile
{
    public PublisherMappingProfile()
    {
        CreateMap<PublisherDto, PublisherVm>().ReverseMap();
    }
}
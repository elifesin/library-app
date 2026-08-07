using AutoMapper;
using Ee.Ebs.Application.Contracts.Publishers.DTOs;
using Ee.Ebs.Web.Library.ViewModels.Publisher;

namespace Ee.Ebs.Web.Library.MappingProfiles;

public class PublisherMappingProfile : Profile
{
    public PublisherMappingProfile()
    {
        CreateMap<PublisherDto, PublisherVm>().ReverseMap();
    }
}
using AutoMapper;
using Entities;
using LibraryApp.Models.Publisher;

namespace LibraryApp.Mapping;

public class PublisherMappingProfile : Profile
{
    public PublisherMappingProfile()
    {
        CreateMap<Publisher, PublisherVm>();
        CreateMap<PublisherVm, Publisher>();
    }
}
using AutoMapper;
using Domain;
using LibraryApp.Models.Publisher;

namespace LibraryApp.MappingProfiles;

public class PublisherMappingProfile : Profile
{
    public PublisherMappingProfile()
    {
        CreateMap<Publisher, PublisherVm>();
        CreateMap<PublisherVm, Publisher>();
    }
}
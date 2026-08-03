using AutoMapper;
using Domain;
using Domain.Entities;
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
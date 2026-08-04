using Application.Publishers.DTOs;
using AutoMapper;
using LibraryApp.Models.Publisher;

namespace LibraryApp.MappingProfiles;

public class PublisherMappingProfile : Profile
{
    public PublisherMappingProfile()
    {
        CreateMap<PublisherDto, PublisherVm>().ReverseMap();
    }
}
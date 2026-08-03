using AutoMapper;
using Domain;
using Domain.Entities;
using LaboratoryApp.Models.Book;

namespace LaboratoryApp.MappingProfiles;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<Book, BookListVm>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AuthorName));
        CreateMap<BookListVm, Book>();
        
    }
}
using AutoMapper;
using Data.Entities;
using LaboratoryApp.Models.Book;

namespace LaboratoryApp.Profiles;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<Book, BookListVm>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AuthorName));
        CreateMap<BookListVm, Book>();
        
    }
}
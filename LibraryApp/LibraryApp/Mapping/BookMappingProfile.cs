using AutoMapper;
using Data.Entities;
using LibraryApp.Models.Book;

namespace LibraryApp.Mapping;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<Book, BookVm>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AuthorName));
        CreateMap<BookVm, Book>();
        
        CreateMap<Book, BookEditVm>();
        CreateMap<BookEditVm, Book>();
        
        CreateMap<Book, BookCreateVm>();
        CreateMap<BookCreateVm, Book>();
        
        CreateMap<Book, BookDeleteVm>();
        CreateMap<BookDeleteVm, Book>();
    }
}
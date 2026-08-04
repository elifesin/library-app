using Application.Books.DTOs;
using AutoMapper;
using LibraryApp.Models.Book;

namespace LibraryApp.MappingProfiles;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<BookDto, BookVm>().ReverseMap(); 
        
        CreateMap<BookEditDto, BookEditVm>().ReverseMap();
        
        CreateMap<BookCreateDto, BookCreateVm>().ReverseMap();
        
        CreateMap<BookDto, BookDeleteVm>().ReverseMap();
        
        CreateMap<BookDto, BookEditVm>().ReverseMap();
    }
}
using AutoMapper;
using Ee.Ebs.Application.Books.DTOs;
using Ee.Ebs.LibraryApp.Models.Book;

namespace Ee.Ebs.LibraryApp.MappingProfiles;

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
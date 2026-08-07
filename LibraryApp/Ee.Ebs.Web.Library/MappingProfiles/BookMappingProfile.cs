using AutoMapper;
using Ee.Ebs.Application.Books.DTOs;
using Ee.Ebs.Web.Library.ViewModels.Book;

namespace Ee.Ebs.Web.Library.MappingProfiles;

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
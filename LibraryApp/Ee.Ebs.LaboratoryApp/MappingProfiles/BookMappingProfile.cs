using AutoMapper;
using Ee.Ebs.Application.Books.DTOs;
using Ee.Ebs.Domain.Books;
using Ee.Ebs.LaboratoryApp.Models.Book;

namespace Ee.Ebs.LaboratoryApp.MappingProfiles;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<BookDto, BookListVm>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AuthorName));
        CreateMap<BookListVm, BookDto>();
        
    }
}
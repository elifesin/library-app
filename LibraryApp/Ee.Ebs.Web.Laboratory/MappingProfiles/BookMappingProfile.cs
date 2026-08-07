using AutoMapper;
using Ee.Ebs.Application.Contracts.Books.DTOs;
using Ee.Ebs.Domain.Books;
using Ee.Ebs.Web.Laboratory.ViewModels.Book;

namespace Ee.Ebs.Web.Laboratory.MappingProfiles;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<BookDto, BookListVm>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AuthorName));
        CreateMap<BookListVm, BookDto>();
        
    }
}
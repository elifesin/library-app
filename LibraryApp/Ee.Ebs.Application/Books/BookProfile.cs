using AutoMapper;
using Ee.Ebs.Domain.Books;
using Ee.Ebs.Application.Books.DTOs;

namespace Ee.Ebs.Application.Books;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDto>().ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author != null ? src.Author.FirstName + " " + src.Author.LastName : string.Empty))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : string.Empty))
            .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher != null ? src.Publisher.Name : string.Empty)).ReverseMap();
       
        CreateMap<Book, BookCreateDto>().ReverseMap();
        
        CreateMap<Book, BookEditDto>().ReverseMap();
    }

}
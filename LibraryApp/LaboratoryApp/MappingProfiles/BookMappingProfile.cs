using AutoMapper;
using Application.Books.DTOs;
using Domain.Books;
using LaboratoryApp.Models.Book;

namespace LaboratoryApp.MappingProfiles;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<BookDto, BookListVm>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AuthorName));
        CreateMap<BookListVm, BookDto>();
        
    }
}
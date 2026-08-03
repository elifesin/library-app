using AutoMapper;
using Domain.Entities;
using LibraryApp.Models.Book;

namespace LibraryApp.MappingProfiles;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<Book, BookVm>()
            // Yazar adını Author tablosunun içindeki FirstName (veya Name) özelliğinden al
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName)) 
            
            // Kategori adını Category tablosunun içinden al
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
            
            // Yayınevi adını Publisher tablosunun içinden al
            .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher.Name));

        CreateMap<BookVm, Book>();
        
        CreateMap<Book, BookEditVm>();
        CreateMap<BookEditVm, Book>();
        
        CreateMap<Book, BookCreateVm>();
        CreateMap<BookCreateVm, Book>();
        
        CreateMap<Book, BookDeleteVm>();
        CreateMap<BookDeleteVm, Book>();
    }
}
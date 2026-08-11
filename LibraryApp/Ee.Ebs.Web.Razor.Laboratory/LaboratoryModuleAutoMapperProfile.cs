using AutoMapper;
using Ee.Ebs.Application.Contracts.Books.DTOs;
using Ee.Ebs.Web.Razor.Laboratory.Pages;
using Ee.Ebs.Web.Razor.Laboratory.Pages.Books;

namespace Ee.Ebs.Web.Razor.Laboratory;

public class LaboratoryModuleAutoMapperProfile : Profile
{
    public LaboratoryModuleAutoMapperProfile()
    {
        CreateMapForBooks();
    }
    
    private void CreateMapForBooks()
    {
        CreateMap<BookDto, IndexModel.BookListVm>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AuthorName));
        CreateMap<IndexModel.BookListVm, BookDto>();
    }
}
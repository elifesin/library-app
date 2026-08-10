using AutoMapper;
using Ee.Ebs.Application.Contracts.Authors.DTOs;
using Ee.Ebs.Application.Contracts.Books.DTOs;
using Ee.Ebs.Application.Contracts.Categories.DTOs;
using Ee.Ebs.Application.Contracts.Loans.DTOs;
using Ee.Ebs.Application.Contracts.Members.DTOs;
using Ee.Ebs.Application.Contracts.Publishers.DTOs;
using Ee.Ebs.Web.Razor.Library.Pages.Loans;
using PagesAuthors = Ee.Ebs.Web.Razor.Library.Pages.Authors;
using PagesBooks = Ee.Ebs.Web.Razor.Library.Pages.Books;
using PagesCategories = Ee.Ebs.Web.Razor.Library.Pages.Categories;
using PagesPublishers = Ee.Ebs.Web.Razor.Library.Pages.Publishers;
using PagesMembers = Ee.Ebs.Web.Razor.Library.Pages.Members;

namespace Ee.Ebs.Web.Razor.Library;

public class LibraryModuleAutoMapperProfile : Profile
{
    public LibraryModuleAutoMapperProfile()
    {
        CreateMapForAuthor();
        CreateMapForBook();
        CreateMapForCategory();
        CreateMapForLoan();
        CreateMapForMember();
        CreateMapForMemberPage();
        CreateMapForPublisher();
    }
    
    private void CreateMapForAuthor()
    {
        CreateMap<AuthorDto, PagesAuthors.DeleteModel.AuthorVm>().ReverseMap();
        CreateMap<AuthorDto, PagesAuthors.IndexModel.AuthorVm>().ReverseMap();
        CreateMap<AuthorCreateDto, PagesAuthors.CreateModel.AuthorCreateVm>().ReverseMap();
        CreateMap<AuthorEditDto, PagesAuthors.EditModel.AuthorEditVm>().ReverseMap();
        CreateMap<AuthorDto,  PagesAuthors.EditModel.AuthorEditVm>().ReverseMap();
    }
    
    private void CreateMapForBook()
    {
        CreateMap<BookDto, PagesBooks.DeleteModel.BookDeleteVm>().ReverseMap();  
        CreateMap<BookDto, PagesBooks.IndexModel.BookVm>().ReverseMap();  
        CreateMap<BookEditDto, PagesBooks.EditModel.BookEditVm>().ReverseMap();
        CreateMap<BookCreateDto, PagesBooks.CreateModel.BookCreateVm>().ReverseMap();
        CreateMap<BookDto, PagesBooks.EditModel.BookEditVm>().ReverseMap();
    }
    
    private void CreateMapForCategory()
    {
        CreateMap<CategoryDto, PagesCategories.CreateModel.CategoryVm>().ReverseMap();
        CreateMap<CategoryDto, PagesCategories.IndexModel.CategoryVm>().ReverseMap();
        CreateMap<CategoryDto, PagesCategories.EditModel.CategoryVm>().ReverseMap();
        CreateMap<CategoryDto, PagesCategories.DeleteModel.CategoryVm>().ReverseMap();
    }
    
    private void CreateMapForLoan()
    {
        CreateMap<LoanDto, Pages.Loans.IndexModel.LoanVm>().ReverseMap();
        CreateMap<LoanCreateDto, CreateModel.LoanCreateVm>().ReverseMap();
        
    }
    
    private void CreateMapForMember()
    {
        CreateMap<MemberDto, PagesMembers.DeleteModel.MemberVm>().ReverseMap();
        CreateMap<MemberCreateDto, PagesMembers.CreateModel.MemberCreateVm>().ReverseMap();
        CreateMap<MemberBorrowedBooksDto, PagesMembers.BorrowedBooksModel.MemberBorrowedBooksVm>().ReverseMap();
        CreateMap<MemberDto, Pages.Members.IndexModel.MemberVm>().ReverseMap();
        CreateMap<MemberDto, Pages.Members.EditModel.MemberVm>().ReverseMap();
    }
    
    private void CreateMapForMemberPage()
    {
        CreateMap<LoanDto, PagesMembers.BorrowedBooksModel.BorrowedBookItem>().ReverseMap();
    }
    
    private void CreateMapForPublisher()
    {
        CreateMap<PublisherDto, PagesPublishers.CreateModel.PublisherVm>().ReverseMap();
        CreateMap<PublisherDto, Pages.Publishers.IndexModel.PublisherVm>().ReverseMap();
        CreateMap<PublisherDto, Pages.Publishers.EditModel.PublisherVm>().ReverseMap();
        CreateMap<PublisherDto, Pages.Publishers.DeleteModel.PublisherVm>().ReverseMap();


    }
}
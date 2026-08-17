using AutoMapper;
using Ee.Ebs.Application.Contracts.Authors.DTOs;
using Ee.Ebs.Application.Contracts.Books.DTOs;
using Ee.Ebs.Application.Contracts.Categories.DTOs;
using Ee.Ebs.Application.Contracts.Loans.DTOs;
using Ee.Ebs.Application.Contracts.Members.DTOs;
using Ee.Ebs.Application.Contracts.Publishers.DTOs;
using PagesAuthors = Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Authors;
using PagesBooks = Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Books;
using PagesCategories = Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Categories;
using PagesPublishers = Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Publishers;
using PagesMembers = Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Members;

namespace Ee.Ebs.Web.Razor.Library;

using Authors_EditModel = Areas.Library.Pages.Authors.EditModel;
using Books_CreateModel = Areas.Library.Pages.Books;
// using Books_DeleteModel = Areas.Library.Pages.Books.DeleteModel;
using Books_EditModel = Areas.Library.Pages.Books;
using Books_IndexModel = Areas.Library.Pages.Books.IndexModel;
using BorrowedBooksModel = Areas.Library.Pages.Members.BorrowedBooksModel;
using Categories_CreateModel = Areas.Library.Pages.Categories.CreateModel;
// using Categories_DeleteModel = Areas.Library.Pages.Categories.DeleteModel;
using Categories_EditModel = Areas.Library.Pages.Categories.EditModel;
using Categories_IndexModel = Areas.Library.Pages.Categories.IndexModel;
using CreateModel = Areas.Library.Pages.Authors.CreateModel;
using IndexModel = Areas.Library.Pages.Authors.IndexModel;
using Members_CreateModel = Areas.Library.Pages.Members.CreateModel;
// using Members_DeleteModel = Areas.Library.Pages.Members.DeleteModel;
using Members_CreateOrEditModel = Areas.Library.Pages.Members;
using Categories_CreateOrEditModel = Areas.Library.Pages.Categories;

using Publishers_Model = Areas.Library.Pages.Publishers;

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
        // CreateMap<AuthorDto, PagesAuthors.DeleteModel.AuthorVm>().ReverseMap();
        CreateMap<AuthorDto, IndexModel.AuthorVm>().ReverseMap();
        CreateMap<AuthorCreateDto, PagesAuthors.AuthorCreateOrEditVm>().ReverseMap();
        CreateMap<AuthorEditDto,  PagesAuthors.AuthorCreateOrEditVm>().ReverseMap();
        CreateMap<AuthorDto, PagesAuthors.AuthorCreateOrEditVm>().ReverseMap();
    }
    
    private void CreateMapForBook()
    {
        // CreateMap<BookDto, Books_DeleteModel.BookDeleteVm>().ReverseMap();  
        CreateMap<BookDto, Books_IndexModel.BookVm>().ReverseMap();  
        CreateMap<BookEditDto, Books_EditModel.BookCreateOrEditVm>().ReverseMap();
        CreateMap<BookCreateDto, Books_CreateModel.BookCreateOrEditVm>().ReverseMap();
        CreateMap<BookDto, Books_EditModel.BookCreateOrEditVm>().ReverseMap(); 
    }
    
    private void CreateMapForCategory()
    {
        CreateMap<CategoryDto, Categories_CreateOrEditModel.CategoryCreateOrEditVm>().ReverseMap();
        CreateMap<CategoryDto, Categories_IndexModel.CategoryVm>().ReverseMap();
        // CreateMap<CategoryDto, Categories_DeleteModel.CategoryVm>().ReverseMap();
    }
    
    private void CreateMapForLoan()
    {
        CreateMap<LoanDto, Areas.Library.Pages.Loans.IndexModel.LoanVm>().ReverseMap();
        CreateMap<LoanCreateDto, Areas.Library.Pages.Loans.CreateModel.LoanCreateVm>().ReverseMap();
        
    }
    
    private void CreateMapForMember()
    {
        // CreateMap<MemberDto, Members_DeleteModel.MemberVm>().ReverseMap();
        CreateMap<MemberCreateDto, Members_CreateOrEditModel.MemberCreateOrEditVm>().ReverseMap();
        CreateMap<MemberBorrowedBooksDto, BorrowedBooksModel.MemberBorrowedBooksVm>().ReverseMap();
        CreateMap<MemberDto, Areas.Library.Pages.Members.IndexModel.MemberVm>().ReverseMap();
        CreateMap<MemberDto,Members_CreateOrEditModel.MemberCreateOrEditVm>().ReverseMap();
    }
    
    private void CreateMapForMemberPage()
    {
        CreateMap<LoanDto, BorrowedBooksModel.BorrowedBookItem>().ReverseMap();
    }
    
    private void CreateMapForPublisher()
    {
        CreateMap<PublisherDto, Publishers_Model.PublisherCreateOrEditVm>().ReverseMap();
        CreateMap<PublisherDto, Areas.Library.Pages.Publishers.IndexModel.PublisherVm>().ReverseMap();
        // CreateMap<PublisherDto, Areas.Library.Pages.Publishers.DeleteModel.PublisherVm>().ReverseMap();


    }
}
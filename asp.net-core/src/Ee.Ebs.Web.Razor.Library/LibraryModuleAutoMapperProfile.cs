using AutoMapper;
using Ee.Ebs.Application.Contracts.Authors.DTOs;
using Ee.Ebs.Application.Contracts.Books.DTOs;
using Ee.Ebs.Application.Contracts.Categories.DTOs;
using Ee.Ebs.Application.Contracts.Loans.DTOs;
using Ee.Ebs.Application.Contracts.Members.DTOs;
using Ee.Ebs.Application.Contracts.Publishers.DTOs;
using PagesAuthors = Ee.Ebs.Web.Razor.Library.Areas.Library.Pages.Authors;

namespace Ee.Ebs.Web.Razor.Library;

using Books_CreateModel = Areas.Library.Pages.Books;
using Books_EditModel = Areas.Library.Pages.Books;
using Books_IndexModel = Areas.Library.Pages.Books.IndexModel;
using BorrowedBooksModel = Areas.Library.Pages.Members.BorrowedBooksModel;
using Categories_IndexModel = Areas.Library.Pages.Categories.IndexModel;
using IndexModel = Areas.Library.Pages.Authors.IndexModel;
using Members_IndexModel = Areas.Library.Pages.Members.IndexModel;
using Members_CreateOrEditModel = Areas.Library.Pages.Members;
using Categories_CreateOrEditModel = Areas.Library.Pages.Categories;
using Publishers_Model = Areas.Library.Pages.Publishers;
using Loan_Model = Areas.Library.Pages.Loans;

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
        CreateMap<AuthorDto, IndexModel.AuthorVm>().ReverseMap();
        CreateMap<AuthorCreateDto, PagesAuthors.AuthorCreateOrEditVm>().ReverseMap();
        CreateMap<AuthorEditDto,  PagesAuthors.AuthorCreateOrEditVm>().ReverseMap();
        CreateMap<AuthorDto, PagesAuthors.AuthorCreateOrEditVm>().ReverseMap();
    }
    
    private void CreateMapForBook()
    {
        CreateMap<BookDto, Books_IndexModel.BookVm>().ReverseMap();  
        CreateMap<BookEditDto, Books_EditModel.BookCreateOrEditVm>().ReverseMap();
        CreateMap<BookCreateDto, Books_CreateModel.BookCreateOrEditVm>().ReverseMap();
        CreateMap<BookDto, Books_EditModel.BookCreateOrEditVm>().ReverseMap(); 
    }
    
    private void CreateMapForCategory()
    {
        CreateMap<CategoryDto, Categories_CreateOrEditModel.CategoryCreateOrEditVm>().ReverseMap();
        CreateMap<CategoryDto, Categories_IndexModel.CategoryVm>().ReverseMap();
    }
    
    private void CreateMapForLoan()
    {
        CreateMap<LoanDto, Loan_Model.IndexModel.LoanVm>().ReverseMap();
        CreateMap<LoanCreateDto, Loan_Model.CreateModel.LoanCreateVm>().ReverseMap();
        
    }
    
    private void CreateMapForMember()
    {
        CreateMap<MemberCreateDto, Members_CreateOrEditModel.MemberCreateOrEditVm>().ReverseMap();
        CreateMap<MemberBorrowedBooksDto, BorrowedBooksModel.MemberBorrowedBooksVm>().ReverseMap();
        CreateMap<MemberDto, Members_IndexModel.MemberVm>().ReverseMap();
        CreateMap<MemberDto,Members_CreateOrEditModel.MemberCreateOrEditVm>().ReverseMap();
    }
    
    private void CreateMapForMemberPage()
    {
        CreateMap<LoanDto, BorrowedBooksModel.BorrowedBookItem>().ReverseMap();
    }
    
    private void CreateMapForPublisher()
    {
        CreateMap<PublisherDto, Publishers_Model.PublisherCreateOrEditVm>().ReverseMap();
        CreateMap<PublisherDto, Publishers_Model.IndexModel.PublisherVm>().ReverseMap();
    }
}
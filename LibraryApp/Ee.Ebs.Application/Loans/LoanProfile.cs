using AutoMapper;
using Ee.Ebs.Domain.Loans;
using Ee.Ebs.Application.Loans.DTOs;

namespace Ee.Ebs.Application.Loans; 

public class LoanProfile : Profile
{
    public LoanProfile()
    {
        // Ekleme işleminde kullanılan View Model -> Entity dönüşümü
        CreateMap<LoanCreateDto, Loan>().ReverseMap();

        // Listeleme işleminde kullanılan Entity -> View Model dönüşümü
        CreateMap<Loan, LoanDto>()
            // LoanVm içerisindeki BookName için Book nesnesinden Name/Title çekilir
            .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Book.Title)) // veya src.Book.Title
            
            // LoanVm içerisindeki FullName için Member nesnesinden ilgili proplar birleştirilir
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Member.FirstName + " " + src.Member.LastName)).ReverseMap();
        
    }
}
using AutoMapper;
using Data.Entities;
using LibraryApp.Models.Loan;
using LibraryApp.Models.Members;

namespace LibraryApp.Mapping;

public class LoanMappingProfile : Profile
{
    public LoanMappingProfile()
    {
        CreateMap<Loan, LoanVm>()
            .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Book.Title));
            
        CreateMap<LoanVm, Loan>();
        
        CreateMap<Loan, LoanCreateVm>();
        CreateMap<LoanCreateVm, Loan>();
    }
}
using AutoMapper;
using Domain;
using Domain.Entities;
using LibraryApp.Models.Loan;

namespace LibraryApp.MappingProfiles;

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
using Application.Loans.DTOs;
using AutoMapper;
using LibraryApp.Models.Loan;

namespace LibraryApp.MappingProfiles;

public class LoanMappingProfile : Profile
{
    public LoanMappingProfile()
    {
        CreateMap<LoanDto, LoanVm>().ReverseMap();
        
        CreateMap<LoanCreateDto, LoanCreateVm>().ReverseMap();
    }
}
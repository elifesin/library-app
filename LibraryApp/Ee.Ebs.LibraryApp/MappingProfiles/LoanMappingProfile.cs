using AutoMapper;
using Ee.Ebs.Application.Loans.DTOs;
using Ee.Ebs.LibraryApp.Models.Loan;

namespace Ee.Ebs.LibraryApp.MappingProfiles;

public class LoanMappingProfile : Profile
{
    public LoanMappingProfile()
    {
        CreateMap<LoanDto, LoanVm>().ReverseMap();
        
        CreateMap<LoanCreateDto, LoanCreateVm>().ReverseMap();
    }
}
using AutoMapper;
using Ee.Ebs.Application.Loans.DTOs;
using Ee.Ebs.Web.Library.ViewModels.Loan;

namespace Ee.Ebs.Web.Library.MappingProfiles;

public class LoanMappingProfile : Profile
{
    public LoanMappingProfile()
    {
        CreateMap<LoanDto, LoanVm>().ReverseMap();
        
        CreateMap<LoanCreateDto, LoanCreateVm>().ReverseMap();
    }
}
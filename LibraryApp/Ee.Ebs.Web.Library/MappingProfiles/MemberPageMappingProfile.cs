using AutoMapper;
using Ee.Ebs.Application.Loans.DTOs;
using Ee.Ebs.Web.Library.Models.Members;

namespace Ee.Ebs.Web.Library.MappingProfiles;

public class MemberPageMappingProfile : Profile
{
    public MemberPageMappingProfile()
    {
        CreateMap<LoanDto, BorrowedBookItem>().ReverseMap();
    }
}
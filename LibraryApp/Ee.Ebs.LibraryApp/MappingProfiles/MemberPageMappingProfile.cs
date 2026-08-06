using AutoMapper;
using Ee.Ebs.Application.Loans.DTOs;
using Ee.Ebs.LibraryApp.Models.Members;

namespace Ee.Ebs.LibraryApp.MappingProfiles;

public class MemberPageMappingProfile : Profile
{
    public MemberPageMappingProfile()
    {
        CreateMap<LoanDto, BorrowedBookItem>().ReverseMap();
    }
}
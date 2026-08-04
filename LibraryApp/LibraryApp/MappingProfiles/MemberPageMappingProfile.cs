using Application.Loans.DTOs;
using AutoMapper;
using LibraryApp.Models.Members;

namespace LibraryApp.MappingProfiles;

public class MemberPageMappingProfile : Profile
{
    public MemberPageMappingProfile()
    {
        CreateMap<LoanDto, BorrowedBookItem>().ReverseMap();
    }
}
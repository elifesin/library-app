using AutoMapper;
using Domain;
using LibraryApp.Models.Members;

namespace LibraryApp.MappingProfiles;

public class MemberPageMappingProfile : Profile
{
    public MemberPageMappingProfile()
    {
        CreateMap<Loan, BorrowedBookItem>();
    }
}
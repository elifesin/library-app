using AutoMapper;
using Domain;
using LibraryApp.Models.Members;

namespace LibraryApp.Mapping;

public class MemberPageMappingProfile : Profile
{
    public MemberPageMappingProfile()
    {
        CreateMap<Loan, BorrowedBookItem>();
    }
}
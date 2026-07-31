using AutoMapper;
using Data.Entities;
using LibraryApp.Models.Members;

namespace LibraryApp.Mapping;

public class MemberPageMappingProfile : Profile
{
    public MemberPageMappingProfile()
    {
        CreateMap<Loan, BorrowedBookItem>();
    }
}
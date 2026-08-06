using AutoMapper;
using Ee.Ebs.Application.Categories.DTOs;
using Ee.Ebs.LibraryApp.Models.Category;

namespace Ee.Ebs.LibraryApp.MappingProfiles;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CategoryDto, CategoryVm>().ReverseMap();
    }
}
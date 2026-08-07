using AutoMapper;
using Ee.Ebs.Application.Categories.DTOs;
using Ee.Ebs.Web.Library.Models.Category;

namespace Ee.Ebs.Web.Library.MappingProfiles;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CategoryDto, CategoryVm>().ReverseMap();
    }
}
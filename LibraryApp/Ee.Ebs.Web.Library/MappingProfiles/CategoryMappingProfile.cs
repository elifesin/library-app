using AutoMapper;
using Ee.Ebs.Application.Categories.DTOs;
using Ee.Ebs.Web.Library.ViewModels.Category;

namespace Ee.Ebs.Web.Library.MappingProfiles;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CategoryDto, CategoryVm>().ReverseMap();
    }
}
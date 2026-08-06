using AutoMapper;
using Ee.Ebs.Domain.Categories;
using Ee.Ebs.Application.Categories.DTOs;

namespace Ee.Ebs.Application.Categories;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();
    }
}
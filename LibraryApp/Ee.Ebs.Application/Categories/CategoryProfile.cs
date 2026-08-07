using AutoMapper;
using Ee.Ebs.Application.Contracts.Categories.DTOs;
using Ee.Ebs.Domain.Categories;

namespace Ee.Ebs.Application.Categories;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();
    }
}
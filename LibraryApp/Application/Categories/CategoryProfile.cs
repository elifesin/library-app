using Application.Categories.DTOs;
using AutoMapper;
using Domain.Categories;

namespace Application.Categories;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();
    }
}
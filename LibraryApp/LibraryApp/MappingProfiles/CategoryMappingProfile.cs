using Application.Categories.DTOs;
using AutoMapper;
using LibraryApp.Models.Category;

namespace LibraryApp.MappingProfiles;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CategoryDto, CategoryVm>().ReverseMap();
    }
}
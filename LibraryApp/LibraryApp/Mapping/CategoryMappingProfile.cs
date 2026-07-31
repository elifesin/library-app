using AutoMapper;
using Data.Entities;
using LibraryApp.Models.Category;


namespace LibraryApp.Mapping;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryVm>();
        CreateMap<CategoryVm, Category>();
    }
}
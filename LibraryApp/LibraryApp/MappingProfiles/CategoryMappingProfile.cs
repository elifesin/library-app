using AutoMapper;
using Domain;
using LibraryApp.Models.Category;

namespace LibraryApp.MappingProfiles;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryVm>();
        CreateMap<CategoryVm, Category>();
    }
}
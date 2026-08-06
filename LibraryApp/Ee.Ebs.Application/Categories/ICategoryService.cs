using Ee.Ebs.Application.Categories.DTOs;

namespace Ee.Ebs.Application.Categories;

public interface ICategoryService
{
    List<CategoryDto> GetAll();
    CategoryDto GetById(int id);
    void Insert(CategoryDto category);
    void Update(CategoryDto category);
    void Delete(int id);
}
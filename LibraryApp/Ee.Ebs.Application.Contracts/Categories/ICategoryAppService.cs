using Ee.Ebs.Application.Contracts.Categories.DTOs;

namespace Ee.Ebs.Application.Contracts.Categories;

public interface ICategoryAppService
{
    List<CategoryDto> GetAll();
    CategoryDto GetById(int id);
    void Insert(CategoryDto category);
    void Update(int id, CategoryDto category);
    void Delete(int id);
}
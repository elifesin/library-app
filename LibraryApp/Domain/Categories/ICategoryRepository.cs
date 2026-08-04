namespace Domain.Categories;

public interface ICategoryRepository
{
    public List<Category> GetAll();
    public Category GetById(int id);
    public void Insert(Category category);
    public void Update(Category category);
    public void Delete(int id);
}
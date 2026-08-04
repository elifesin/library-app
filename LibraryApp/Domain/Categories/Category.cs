namespace Domain.Categories;

public class Category
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public bool IsActive { get; set; } = true;
}
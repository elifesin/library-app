using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataEF
{
    public class CategoryRepository
    {
        private readonly AppDbContext _context;

        // DbConnectionFactory yerine EF Core'un DbContext'ini enjekte ediyoruz
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            // SELECT * FROM Categories WHERE IsActive = 1 
            return _context.Categories.Where(c => c.IsActive).ToList();
        }

        public Category GetById(int id)
        {
            // SELECT * FROM Categories WHERE Id = @Id 
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }
        
        public void Insert(Category category)
        {
            // INSERT INTO Categories(...) SET (@...)
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        
        public void Update(Category category)
        {
            // UPDATE 
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // UPDATE Categories SET IsActive = 0 WHERE Id = @Id (Soft Delete) 
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            
            if (category != null)
            {
                category.IsActive = false; // Veritabanında IsActive int ise burayı 0 yapmalısın.
                _context.SaveChanges();
            }
        }
    }
}
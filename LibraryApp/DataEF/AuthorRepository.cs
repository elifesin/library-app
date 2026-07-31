using Domain; 
using Microsoft.EntityFrameworkCore;

namespace DataEF
{
    public class AuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Author> GetAllAuthors()
        {
            return _context.Authors.Where(a => a.IsActive).ToList();
        }

        public Author GetAuthorById(int id)
        {
            return _context.Authors.FirstOrDefault(a => a.Id == id);
        }

        public void Insert(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public void Update(Author author)
        {
            _context.Authors.Update(author);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);
            
            if (author != null)
            {
                author.IsActive = false; 
                _context.SaveChanges();
            }
        }
    }
}
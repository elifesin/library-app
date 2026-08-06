using Ee.Ebs.Domain.Authors;
using Ee.Ebs.Data.EfCore.Contexts;

namespace Ee.Ebs.Data.EfCore.Authors
{
    public class AuthorRepository : IAuthorRepository
    {
    // DbConnectionFactory yerine EF Core'un AppDbContext'ini enjekte ediyoruz
    private readonly AppDbContext _context;

    public AuthorRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Author> GetAll()
    {
        // SELECT WHERE IsActive = 1
        return _context.Authors.Where(a => a.IsActive).ToList();
    }

    public Author GetById(int id)
    {
        // SELECT WHERE @Id = Id
        return _context.Authors.FirstOrDefault(a => a.Id == id);
    }

    public void Insert(Author author)
    {
        // INSERT INTO Authors(...) VALUES (@...)
        _context.Authors.Add(author);
        _context.SaveChanges();
    }

    public void Update(Author author)
    {
        // UPDATE Authors SET FirstName = @FirstName ...
        _context.Authors.Update(author);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        // UPDATE Authors SET IsActive = 0
        var author = _context.Authors.FirstOrDefault(a => a.Id == id);

        if (author != null)
        {
            author.IsActive = false;
            _context.SaveChanges();
        }
    }
    }
}
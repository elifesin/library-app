using Domain;

namespace DataEF;

public class PublisherRepository
{
    public readonly AppDbContext _context;

    public PublisherRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Publisher> GetAll()
    {
        // SELECT WHERE IsActive = 1 
        return _context.Publishers.Where(p => p.IsActive).ToList();
    }

    public Publisher GetById(int id)
    {
        // SELECT WHERE @Id = Id
        return _context.Publishers.FirstOrDefault(p => p.Id == id);
    }

    public void Insert(Publisher publisher)
    {
        // INSERT INTO Publishers(...) VALUES (@...)
        _context.Add(publisher);
        _context.SaveChanges();
    }

    public void Update(Publisher publisher)
    {
        // UPDATE Publishers SET Name = @Name WHERE Id = @Id
        _context.Update(publisher);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        // UPDATE Publishers SET IsActive = 0 WHERE Id = @Id (Soft Delete) işleminin karşılığı[cite: 15]
        var publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);
            
        if (publisher != null)
        {
            publisher.IsActive = false; // Veritabanında IsActive int ise burayı 0 olarak güncellemelisin
            _context.SaveChanges();
        }
    }
}
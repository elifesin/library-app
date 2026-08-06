using Ee.Ebs.Domain.Books;
using Ee.Ebs.Data.EfCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ee.Ebs.Data.EfCore.Books
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAll()
        {
            // JOIN'ler için relational table'ları çekiyoruz
            var books = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .Where(b => b.IsActive) // IsActive = 1 filtresi
                .ToList();

            // CASE WHEN sorgu kısmı
            var activeLoanBookIds = _context.Loans
                .Where(l => l.ReturnDate == null)
                .Select(l => l.BookID)
                .ToHashSet();

            foreach (var book in books)
            {
                book.IsBorrowed = activeLoanBookIds.Contains(book.Id);
            }

            return books;
        }

        public Book GetById(int id)
        {
            var book = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .FirstOrDefault(b => b.Id == id && b.IsActive);

            if (book != null)
            {
                book.IsBorrowed = _context.Loans.Any(l => l.BookID == book.Id && l.ReturnDate == null);
            }

            return book;
        }

        public void Insert(Book book)
        {
            // INSERT INTO Books(...) VALUES (@...)
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Update(Book book)
        {
            // UPDATE Books SET ... = @...
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void Delete(Book book)
        {
            // UPDATE Books SET IsActive = 0 WHERE Id = @Id (Soft Delete) 
            var entity = _context.Books.FirstOrDefault(b => b.Id == book.Id);
            
            if (entity != null)
            {
                entity.IsActive = false;
                _context.SaveChanges();
            }
        }
        
        public List<Book> GetAvailableBooks()
        {
            // Ödünç alınmayan kitapları dropdowna getiren SQL sorgusu karşılığı
            return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .Where(b => b.IsActive && !_context.Loans.Any(l => l.BookID == b.Id && l.ReturnDate == null))
                .ToList();
        }
    }
}
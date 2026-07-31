using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataEF
{
    public class BookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks()
        {
            var books = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .Where(b => b.IsActive) // IsActive = 1 filtresi
                .ToList();

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

        public Book GetBookById(int id)
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
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void Delete(Book book)
        {
            var entity = _context.Books.FirstOrDefault(b => b.Id == book.Id);
            
            if (entity != null)
            {
                entity.IsActive = false;
                _context.SaveChanges();
            }
        }
        
        public List<Book> GetAvailableBooks()
        {
            return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .Where(b => b.IsActive && !_context.Loans.Any(l => l.BookID == b.Id && l.ReturnDate == null))
                .ToList();
        }
    }
}
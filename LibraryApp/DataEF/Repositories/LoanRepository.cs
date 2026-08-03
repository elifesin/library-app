using DataEF.Contexts;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataEF.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AppDbContext _context;

        public LoanRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Loan> GetAll()
        {
            // INNER JOIN Books ve INNER JOIN Authors işlemlerinin LINQ karşılığı
            return _context.Loans
                .Include(l => l.Book)             // Loan nesnesi üzerinden Book'u dahil et
                .ThenInclude(b => b.Author)   // Book nesnesi üzerinden de Author'u dahil et
                .Where(l => l.ReturnDate == null) // WHERE ReturnDate IS NULL
                .ToList();
        }

        public void Insert(Loan loan)
        {
            // INSERT INTO 
            _context.Loans.Add(loan);
            _context.SaveChanges();
        }

        public List<Loan> GetLoansByMemberId(int memberId)
        {
            // INNER JOIN ve WHERE l.MemberID = @MemberID 
            return _context.Loans
                .Include(l => l.Book)
                .ThenInclude(b => b.Author)
                .Where(l => l.MemberID == memberId)
                .ToList();
        }

        public void ReturnBook(int loanId)
        {
            // UPDATE Loans SET ReturnDate = GETDATE() WHERE Id = @Id 
            var loan = _context.Loans.FirstOrDefault(l => l.Id == loanId);
            
            if (loan != null)
            {
                loan.ReturnDate = DateTime.Now; // SQL'deki GETDATE() fonksiyonu
                _context.SaveChanges();
            }
        }
    }
}
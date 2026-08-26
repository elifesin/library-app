using System;
using System.Collections.Generic;
using System.Linq;
using Ee.Ebs.Domain.Loans;
using Ee.Ebs.Data.EfCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ee.Ebs.Data.EfCore.Loans
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
                .Include(l => l.Book) 
                    .ThenInclude(b => b.Author)   // Book nesnesi üzerinden de Author'u dahil et
                .Include (m => m.Member)
                .Where(l => l.ReturnDate == null) // WHERE ReturnDate IS NULL
                .ToList();
        }

        public Loan GetById(int id)
        {
            return _context.Loans
                .Include(b => b.Book)
                .Include(b => b.Member)
                .FirstOrDefault(l => l.Id == id);;
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
                .Include(l => l.Member)
                .Where(l => l.MemberID == memberId)
                .ToList();
        }

        public void ReturnBook(int loanId)
        {
            var loan = _context.Loans.FirstOrDefault(l => l.Id == loanId);

            if (loan == null)
                throw new KeyNotFoundException("Ödünç kaydı bulunamadı.");

            loan.Return(DateTime.Now);   // artık kural burada değil, entity'de kontrol ediliyor
            _context.SaveChanges();
        }
    }
}
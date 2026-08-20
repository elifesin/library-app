using Ee.Ebs.Domain.Authors;
using Ee.Ebs.Domain.Books;
using Ee.Ebs.Domain.Categories;
using Ee.Ebs.Domain.Loans;
using Ee.Ebs.Domain.Members;
using Ee.Ebs.Domain.Publishers;
using Microsoft.EntityFrameworkCore;

namespace Ee.Ebs.Data.EfCore.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using LibraryApp.Models; 

namespace LibaryApp.Data
{
    public class LibraryDbContext : DbContext
    {
        // Constructor üzerinden bağlantı ayarlarını alıyoruz
        public LibraryDbContext(DbContextOptions options) : base(options)
        {
        }

        // Veritabanındaki tabloları temsil eden DbSet'ler
        // public DbSet Books { get; set; }
        // public DbSet Authors { get; set; }
        // public DbSet Members { get; set; }
        // public DbSet Loans { get; set; }
        // public DbSet Categories { get; set; }
    }
}
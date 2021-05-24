using Microsoft.EntityFrameworkCore;
using RemApi.Models;

namespace RemApi.Models
{
    public class RemaDbContext : DbContext
    {
        public RemaDbContext(DbContextOptions<RemaDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Supplier>().ToTable("Suppliers");
        }
    }
}
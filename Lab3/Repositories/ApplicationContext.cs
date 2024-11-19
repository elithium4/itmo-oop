using Microsoft.EntityFrameworkCore;
using Lab3.Model;

namespace Lab3.Reposiories
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Store> Stores { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<StoreProduct> StoreProducts { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=storeapp.db");
        }
    }
}

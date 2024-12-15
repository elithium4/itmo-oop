using Microsoft.EntityFrameworkCore;
using FamilyTree.DAL.Model;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection.Emit;
using static System.Formats.Asn1.AsnWriter;

namespace FamilyTree.DAL.Repository.SQL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> People { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<StoreProduct>()
        //        .HasKey(sp => new { sp.StoreId, sp.ProductName });
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=familytree.db");
        }
    }
}
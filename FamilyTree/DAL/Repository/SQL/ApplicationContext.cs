using Microsoft.EntityFrameworkCore;
using FamilyTree.DAL.Model;

namespace FamilyTree.DAL.Repository.SQL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> People { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=familytree.db");
        }
    }
}
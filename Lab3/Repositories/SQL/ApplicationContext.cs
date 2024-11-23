﻿using Microsoft.EntityFrameworkCore;
using Lab3.Repositories.Model;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection.Emit;

namespace Lab3.Repositories.SQL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Store> Stores { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<StoreProduct> ProductStoreDetails { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreProduct>()
                .HasKey(sp => new { sp.StoreId, sp.ProductName });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=storeapp.db");
        }
    }
}

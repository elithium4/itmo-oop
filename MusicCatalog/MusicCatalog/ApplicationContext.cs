using Microsoft.EntityFrameworkCore;
using MusicCatalog.Entities;

namespace MusicCatalog
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Track> Tracks { get; set; } = null!;
        public DbSet<Album> Albums { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Musician> Musicians { get; set; } = null!;
        public DbSet<TracksCollection> TracksCollection { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=musiccatalog.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("Populate");
            modelBuilder.Entity<Genre>().HasData(
                [new Genre { Id = 1, Name = "Pop Punk" },
                new Genre { Id = 2, Name = "Rock" }]);

            modelBuilder.Entity<Musician>().HasData(
                    [new Musician { Id = 1, Name = "Waterparks" },
                    new Musician { Id = 2, Name = "Five FInger Death Punch" },
                    new Musician { Id = 3, Name = "Theory of a Deadman" }]
            );

            modelBuilder.Entity<Track>().HasMany(e => e.Musicians).WithMany(e => e.Tracks);
            //    .UsingEntity(t => t.HasData(
            //        new Track { Id = 1, Name = "Little Violence", Genre = "Pop Punk", Duration = 203 },
            //        new Track { Id = 2, Name = "Royal", Genre = "Pop Punk", Duration = 211 },
            //        new Track { Id = 3, Name = "Entertainment 2019", Duration = 463 }
            //));
        }
    }
}

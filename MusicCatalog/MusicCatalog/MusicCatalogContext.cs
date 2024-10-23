using Microsoft.EntityFrameworkCore;
using MusicCatalog.Entities;

namespace MusicCatalog
{
    internal class MusicCatalogContext : DbContext
    {
        public DbSet<Track> Tracks { get; set; } = null!;
        public DbSet<Album> Albums { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Musician> Musicians { get; set; } = null!;
        public DbSet<TracksCollection> TracksCollection { get; set; } = null!;
        public MusicCatalogContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=musiccatalog.db");
        }
    }
}

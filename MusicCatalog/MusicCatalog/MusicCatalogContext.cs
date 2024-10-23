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
            // Database.EnsureDeleted();
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
            var genres = new[]
            {
                new Genre { Id = 1, Name = "Pop Punk" },
            };

            var musicians = new[]
            {
                new Musician { Id = 1, Name = "Waterparks" },
                    new Musician { Id = 2, Name = "Five Finger Death Punch" },
                    new Musician { Id = 3, Name = "Theory of a Deadman" }
            };

            var albums = new[]
            {
                new Album {Id = 1, Name = "Double Dare"}
            };

            var tracks = new[]
            {
                    new Track { Id = 1, Name = "Little Violence", Genre = genres[0], Album = albums[0], Duration = 203 },
                    new Track { Id = 2, Name = "Royal", Genre = genres[0], Album = albums[0], Duration = 211 },
                    new Track { Id = 3, Name = "Entertainment 2019", Genre = genres[0], Duration = 463 }
            };

            //modelBuilder.Entity<Genre>().HasData(genres);

            //modelBuilder.Entity<Musician>().HasMany(e => e.Albums)
            //    .WithMany(e => e.Musicians)
            //    .UsingEntity(j => j.ToTable("MusicianAlbum")); ;
            //modelBuilder.Entity<Musician>().HasMany(e => e.Tracks)
            //    .WithMany(e => e.Musicians)
            //    .UsingEntity(j => j.ToTable("MusicianTrack")); ;
            //modelBuilder.Entity<Musician>().HasData(musicians);


            modelBuilder.Entity<Album>().HasMany(e => e.Tracks).WithOne(e => e.Album);

            // Заполнение альбомов
            //modelBuilder.Entity<Album>().HasMany(e => e.Musicians)
            //    .WithMany(e => e.Albums)
            //     .UsingEntity(
            //    "MusicianAlbum",
            //    r => r.HasOne(typeof(Album)).WithMany().HasForeignKey("AlbumsId").HasPrincipalKey(nameof(Album.Id)),
            //    l => l.HasOne(typeof(Musician)).WithMany().HasForeignKey("MusiciansId").HasPrincipalKey(nameof(Musician.Id)),
            //    j => j.HasKey("AlbumsId", "MusiciansId"));
            //    //{
            //    //    j.HasKey("AlbumsId", "MusiciansId");
            //    //    //j.HasData(
            //    //    //    new { AlbumId = 1, MusicianId = 1 }
            //    //    //);
            //    //});
            //modelBuilder.Entity<Album>().HasMany(e => e.Tracks)
            //    .WithOne(e => e.Album);


            //modelBuilder.Entity<Album>().HasData(albums);


            //modelBuilder.Entity<Track>().HasMany(e => e.Musicians)
            //    .WithMany(e => e.Tracks)
            //    .UsingEntity(j => j.ToTable("MusicianTrack")); ;
        }
    }
}

using MusicCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCatalog.Repositories
{
    internal class UnitOfWork: IDisposable
    {
        private readonly MusicCatalogContext _context;
        public Repository<Track> Tracks { get; private set; }
        public Repository<Musician> Musicians { get; private set; }
        public Repository<Album> Albums { get; private set; }
        public Repository<Genre> Genres { get; private set; }
        public Repository<TracksCollection> TracksCollections { get; private set; }
        public UnitOfWork(MusicCatalogContext context)
        {
            _context = context;
            Musicians = new Repository<Musician>(context);
            Albums = new Repository<Album>(context);
            Tracks = new Repository<Track>(context);
            Genres = new Repository<Genre>(context);
            TracksCollections = new Repository<TracksCollection>(context);
        }

        public void Save() => _context.SaveChanges();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

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
        public TrackRepository Tracks { get; private set; }
        public MusiciansRepository Musicians { get; private set; }
        public AlbumRepository Albums { get; private set; }
        public GenreRepository Genres { get; private set; }
        public TracksCollectionRepository TracksCollections { get; private set; }
        public UnitOfWork(MusicCatalogContext context)
        {
            _context = context;
            Musicians = new MusiciansRepository(context);
            Albums = new AlbumRepository(context);
            Tracks = new TrackRepository(context);
            Genres = new GenreRepository(context);
            TracksCollections = new TracksCollectionRepository(context);
        }

        public void Save() => _context.SaveChanges();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

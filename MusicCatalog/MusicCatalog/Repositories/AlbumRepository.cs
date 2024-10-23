using MusicCatalog.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MusicCatalog.Repositories
{
    internal class AlbumRepository: Repository<Album>
    {
        public AlbumRepository(MusicCatalogContext context): base(context) {}
        public override Album Get(int id)
        {
            return _table.Include(e => e.Musicians).Include(e => e.Tracks).FirstOrDefault(x => x.Id == id);
        }

        public override List<Album> GetAll()
        {
            return _table.Include(e => e.Musicians).Include(e => e.Tracks).ToList();
        }
    }
}
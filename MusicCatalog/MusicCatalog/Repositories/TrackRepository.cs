using Microsoft.EntityFrameworkCore;
using MusicCatalog.Entities;
using System.Linq;

namespace MusicCatalog.Repositories
{
    internal class TrackRepository: Repository<Track>
    {
        public TrackRepository(MusicCatalogContext context): base(context) {}
        public override Track Get(int id)
        {
            return _table.Include(e => e.Musicians).Include(e => e.Album).Include(e => e.Genre).FirstOrDefault(x => x.Id == id);
        }

        public override List<Track> GetAll()
        {
            return _table.Include(e => e.Musicians).Include(e => e.Album).Include(e => e.Genre).ToList();
        }
    }
}

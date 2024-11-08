using Microsoft.EntityFrameworkCore;
using MusicCatalog.Entities;
using System.Linq;

namespace MusicCatalog.Repositories
{
    internal class TracksCollectionRepository: Repository<TracksCollection>
    {
        public TracksCollectionRepository(MusicCatalogContext context): base(context) {}
        public override TracksCollection Get(int id)
        {
            return _table.Include(e => e.Tracks).FirstOrDefault(x => x.Id == id);
        }

        public override List<TracksCollection> GetAll()
        {
            return _table.Include(e => e.Tracks).ToList();
        }
    }
}

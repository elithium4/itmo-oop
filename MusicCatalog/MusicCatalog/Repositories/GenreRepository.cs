using Microsoft.EntityFrameworkCore;
using MusicCatalog.Entities;

namespace MusicCatalog.Repositories
{
    internal class GenreRepository: Repository<Genre>
    {
        public GenreRepository(MusicCatalogContext context): base(context) {}
        public override Genre Get(int id)
        {
            return _table.Find(id);
        }

        public override List<Genre> GetAll()
        {
            return _table.ToList();
        }
    }
}

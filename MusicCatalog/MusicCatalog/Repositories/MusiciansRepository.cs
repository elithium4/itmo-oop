using MusicCatalog.Entities;

namespace MusicCatalog.Repositories
{
    internal class MusiciansRepository: Repository<Musician>
    {

        public MusiciansRepository(MusicCatalogContext context): base(context) {}
        public override Musician Get(int id)
        {
            return _table.Find(id);
        }

        public override List<Musician> GetAll()
        {
            return _table.ToList();
        }
    }
}
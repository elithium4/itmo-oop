using Microsoft.EntityFrameworkCore;

namespace MusicCatalog.Repositories
{
    internal abstract class Repository<T> where T : class
    {
        protected MusicCatalogContext _context;
        protected DbSet<T> _table;
        public Repository(MusicCatalogContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public abstract T Get(int id);

        public abstract List<T> GetAll();
        public void Add(T entity)
        {
            _table.Add(entity);
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MusicCatalog.Entities;

namespace MusicCatalog.Repositories
{
    internal class Repository<T> where T : class 
    {
        private MusicCatalogContext _context;
        private DbSet<T> _table;

        public Repository(MusicCatalogContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public T Get(int id)
        {
            return _table.Find(id);
        }

        public List<T> GetAll()
        {
            return _table.ToList();
        }

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

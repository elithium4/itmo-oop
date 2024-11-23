using Lab3.Repositories.Model;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Repositories.SQL
{
    public class SQLStoreRepository : IStoreRepository
    {
        private readonly ApplicationContext _context;


        public SQLStoreRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task CreateStoreAsync(Store store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
        }

        public async Task<Store> GetStoreByIdAsync(int id)
        {
            return await _context.Stores.FirstAsync(s => s.Id == id);
        }

        public async Task<List<Store>> GetAllStoresAsync()
        {
            return await _context.Stores.ToListAsync();
        }

    }
}

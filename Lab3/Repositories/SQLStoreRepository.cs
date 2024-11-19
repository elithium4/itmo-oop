using Lab3.Model;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Reposiories
{
    public class SQLStoreRepository : IStoreRepository
    {
        private readonly ApplicationContext _context;


        public SQLStoreRepository(ApplicationContext context) {
            _context = context;
        }
        public async Task CreateStoreAsync(Store store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
        }

        public async Task<Store> GetStoreByIdAsync(int id)
        {
            return await _context.Stores.FindAsync(id);
        }

        public async Task<List<Store>> GetAllStoresAsync()
        {
            return await _context.Stores.ToListAsync();
        }
    }
}

using Lab3.Model;
using Lab3.Reposiories;

namespace Lab3.Services
{
    public class StoreService
    {
        private readonly IStoreRepository _repository;

        public StoreService(IStoreRepository repository) { _repository = repository; }

        public async Task<List<Store>> GetAllStores()
        {
            return await _repository.GetAllStoresAsync();
        }

        public async Task CreateStore(Store store)
        {
            await _repository.CreateStoreAsync(store);
        }
    }
}

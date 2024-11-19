using Lab3.Repositories.Model;

namespace Lab3.Repositories
{
    public interface IStoreRepository
    {
        Task<List<Store>> GetAllStoresAsync();
        Task<Store> GetStoreByIdAsync(int id);
        Task CreateStoreAsync(Store store);
    }
}

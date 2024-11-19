using Lab3.Model;

namespace Lab3.Reposiories
{
    public interface IStoreRepository
    {
        Task<List<Store>> GetAllStoresAsync();
        Task<Store> GetStoreByIdAsync(int id);
        Task CreateStoreAsync(Store store);
    }
}

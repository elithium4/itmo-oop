using DAL.Repositories.Model;

namespace DAL.Repositories
{
    public interface IStoreRepository
    {
        Task<List<Store>> GetAllStoresAsync();
        Task<Store> GetStoreByIdAsync(int id);
        Task CreateStoreAsync(Store store);
    }
}

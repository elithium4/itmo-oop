using AutoMapper;
using Lab3.Repositories;
using Lab3.Repositories.Model;
using Lab3.Services.DTO;
using Lab3.Services.Exceptions;

namespace Lab3.Services
{
    public class StoreService
    {
        private readonly IStoreRepository _repository;
        private readonly IMapper _mapper;

        public StoreService(IStoreRepository repository, IMapper mapper) { _repository = repository; _mapper = mapper; }

        public async Task<List<StoreDTO>> GetAllStores()
        {
            var stores = await _repository.GetAllStoresAsync();
            return _mapper.Map<List<StoreDTO>>(stores);
        }

        public async Task CreateStore(StoreDTO store)
        {
            await _repository.CreateStoreAsync(new Store { Name = store.Name, Address = store.Address });
        }

        public async Task<FullStoreInfoDTO> GetStoreById(int storeId)
        {
            var storeInfo = await _repository.GetStoreByIdAsync(storeId);
            if (storeInfo == null)
            {
                throw new StoreDoesNotExistException(storeId);
            }
            return _mapper.Map<FullStoreInfoDTO>(storeInfo);
        }
    }
}

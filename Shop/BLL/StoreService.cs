using AutoMapper;
using DAL.Repositories;
using DAL.Repositories.Model;
using BLL.Services.DTO;
using BLL.Services.Exceptions;

namespace BLL.Services
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

        public async Task CreateStore(CreateStoreDTO store)
        {
            await _repository.CreateStoreAsync(_mapper.Map<Store>(store));
        }

        public async Task<StoreDTO> GetStoreById(int storeId)
        {
            var storeInfo = await _repository.GetStoreByIdAsync(storeId);
            if (storeInfo == null)
            {
                throw new StoreDoesNotExistException(storeId);
            }
            return _mapper.Map<StoreDTO>(storeInfo);
        }
    }
}

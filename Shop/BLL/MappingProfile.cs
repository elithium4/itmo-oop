using AutoMapper;
using DAL.Repositories.Model;
using BLL.Services.DTO;

namespace BLL.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Store, StoreDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<StoreProduct, StoreProductDTO>();
            CreateMap<StoreProduct, ProductInStoreDTO>();
            CreateMap<StoreDTO, Store>();
            CreateMap<CreateStoreDTO, Store>();
            CreateMap<ProductDTO, Product>();
            CreateMap<StoreProductDTO, StoreProduct>();
        }
    }
}

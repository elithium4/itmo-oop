﻿using AutoMapper;
using Lab3.Repositories.Model;
using Lab3.Services.DTO;

namespace Lab3.Services
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
using AutoMapper;
using NorthwindModels.DTOs;
using NorthwindModels.Models;

namespace NorthwindModels
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();
            CreateMap<CustomerCreateDTO, Customer>();

            CreateMap<Order, OrderDTO>();
            CreateMap<OrderCreateDTO, Order>();
            CreateMap<OrderDTO, Order>();

            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
            CreateMap<ProductCreateDTO, Product>();
        }
    }
}

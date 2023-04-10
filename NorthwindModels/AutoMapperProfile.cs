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
            CreateMap<CustomerCreateDTO, Customer>();
            CreateMap<CustomerUpdateDTO, Customer>();

            CreateMap<Order, OrderDTO>();
            CreateMap<OrderCreateDTO, Order>();

            CreateMap<Product, ProductDTO>();
            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>();
        }
    }
}

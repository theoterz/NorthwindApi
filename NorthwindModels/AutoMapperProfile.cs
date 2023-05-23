using AutoMapper;
using NorthwindModels.DTOs;
using NorthwindModels.Models;

namespace NorthwindModels
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerDTO>().ForMember(dest => dest.CustomerID, opt => opt.MapFrom(src => src.Id));
            CreateMap<CustomerDTO, Customer>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerID));
            CreateMap<CustomerCreateDTO, Customer>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerID));

            CreateMap<Order, OrderDTO>().ForMember(dest => dest.OrderID, opt => opt.MapFrom(src => src.Id));
            CreateMap<OrderCreateDTO, Order>();
            CreateMap<OrderDTO, Order>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderID));

            CreateMap<Product, ProductDTO>().ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));
            CreateMap<ProductDTO, Product>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId));
            CreateMap<ProductCreateDTO, Product>();
        }
    }
}

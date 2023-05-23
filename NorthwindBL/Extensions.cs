using Microsoft.Extensions.DependencyInjection;
using NorthwindBL.Interfaces;
using NorthwindBL.Services;

namespace NorthwindBL
{
    public static class Extensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<ICustomerServices, CustomerServices>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<IOrderServices, OrderServices>();

            return services;
        }
    }
}

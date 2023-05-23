using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthwindDAL.DataContext;
using NorthwindDAL.Interfaces;
using NorthwindDAL.Repositories;
using NorthwindModels.Models;

namespace NorthwindDAL
{
    public static class Extensions
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, string connectionStringName)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();

            return services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration!.GetConnectionString(connectionStringName)));
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEntityRepository<Customer, string>, EntityRepository<Customer, string>>();
            services.AddScoped<IEntityRepository<Product, int>, EntityRepository<Product, int>>();
            services.AddScoped<IEntityRepository<Order, int>, EntityRepository<Order, int>>();

            return services;
        }
    }
}

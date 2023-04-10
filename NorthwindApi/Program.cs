using Microsoft.EntityFrameworkCore;
using NorthwindBL;
using NorthwindDAL;
using NorthwindDAL.Repositories;
using NorthwindDAL.Services;
using NorthwindModels;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

        builder.Services.AddScoped<ICustomerRepository, CustomerServices>();
        builder.Services.AddScoped<IProductRepository, ProductServices>();
        builder.Services.AddScoped<IOrderRepository, OrderServices>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
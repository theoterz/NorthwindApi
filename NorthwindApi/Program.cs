using Microsoft.EntityFrameworkCore;
using NorthwindBL;
using NorthwindDAL;
using NorthwindDAL.Repositories;
using NorthwindDAL.Interfaces;
using NorthwindModels;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOrigin", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });

        //Inject Repositories
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();

        //Inject Bussiness logic
        builder.Services.AddScoped<CustomerServices>();
        builder.Services.AddScoped<ProductServices>();
        builder.Services.AddScoped<OrderServices>();

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

        app.UseCors("AllowAnyOrigin");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
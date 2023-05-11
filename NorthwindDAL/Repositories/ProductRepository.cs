using Microsoft.EntityFrameworkCore;
using NorthwindDAL.DataContext;
using NorthwindDAL.Interfaces;
using NorthwindModels.Models;

namespace NorthwindDAL.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddProductAsync(Product product)
        {
            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _appDbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetByProductIdAsync(int id)
        {
            return await _appDbContext.Products.FindAsync(id);
        }

        public bool ProductExists(int id)
        {
            return _appDbContext.Products.Any(p => p.ProductId == id);
        }

        public async Task UpdateProductAsync(Product product)
        {
            _appDbContext.Products.Update(product);
            await _appDbContext.SaveChangesAsync();
        }
    }
}

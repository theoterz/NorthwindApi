using NorthwindModels.Models;

namespace NorthwindDAL.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllProductsAsync();
        public Task<Product?> GetByProductIdAsync(int id);
        public Task AddProductAsync(Product product);
        public Task UpdateProductAsync(Product product);
        public Task DeleteProductAsync(Product product);
        public bool ProductExists(int id);
    }
}

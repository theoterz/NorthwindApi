using NorthwindModels.Models;

namespace NorthwindDAL.Interfaces
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts();
        public Product? GetByProductId(int id);
        public void AddProduct(Product product);
        public void UpdateProduct(Product product);
        public void DeleteProduct(Product product);
        public bool ProductExists(int id);
    }
}

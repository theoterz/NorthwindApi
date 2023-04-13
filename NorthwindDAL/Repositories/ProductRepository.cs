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

        public void AddProduct(Product product)
        {
            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _appDbContext.Products.Remove(product);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _appDbContext.Products;
        }

        public Product? GetByProductId(int id)
        {
            return _appDbContext.Products.Find(id);
        }

        public bool ProductExists(int id)
        {
            return _appDbContext.Products.Any(p => p.ProductId == id);
        }

        public void UpdateProduct(Product product)
        {
            _appDbContext.Products.Update(product);
            _appDbContext.SaveChanges();
        }
    }
}

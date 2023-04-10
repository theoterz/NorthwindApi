using NorthwindModels.DTOs;

namespace NorthwindDAL.Repositories
{
    public interface IProductRepository
    {
        public IEnumerable<ProductDTO> GetAllProducts();
        public ProductDTO? GetByProductId(int id);
        public ProductDTO? AddProduct(ProductCreateDTO productDTO);
        public bool UpdateProduct(ProductUpdateDTO productDTO);
        public bool DeleteProduct(int id);
    }
}

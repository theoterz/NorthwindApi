using NorthwindModels.DTOs;

namespace NorthwindBL.Interfaces
{
    public interface IProductServices
    {
        Task<ProductDTO?> AddProductAsync(ProductCreateDTO productDTO);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetByProductIdAsync(int id);
        Task<bool> UpdateProductAsync(ProductDTO productDTO);
    }
}
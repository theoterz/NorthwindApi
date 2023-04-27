using NorthwindModels.DTOs;

namespace NorthwindWebUI.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>?> GetAll();
        public Task<ProductDTO?> GetById(int id);
        public Task<string> Create(ProductCreateDTO productDTO);
        public Task<string> Update(ProductDTO productDTO);
        public Task<bool> Delete(int id);
    }
}

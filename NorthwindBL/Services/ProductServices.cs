using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthwindBL.Interfaces;
using NorthwindDAL.Interfaces;
using NorthwindModels.DTOs;
using NorthwindModels.Models;

namespace NorthwindBL.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IEntityRepository<Product, int> _productRepository;
        private readonly IMapper _mapper;
        public ProductServices(IEntityRepository<Product, int> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }


        public async Task<ProductDTO?> AddProductAsync(ProductCreateDTO productDTO)
        {

            try
            {
                Product product = _mapper.Map<Product>(productDTO);

                product.Discontinued = false;

                await _productRepository.CreateEntityAsync(product);

                return _mapper.Map<ProductDTO>(product);
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            Product? product = await _productRepository.GetEntityByIdAsync(id);

            if (product is null) return false;

            await _productRepository.DeleteEntityAsync(product);

            return true;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            IEnumerable<Product> products = await _productRepository.GetAllEntitiesAsync();
            return products.Select(p => _mapper.Map<ProductDTO>(p));
        }

        public async Task<ProductDTO?> GetByProductIdAsync(int id)
        {
            Product? product = await _productRepository.GetEntityByIdAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<bool> UpdateProductAsync(ProductDTO productDTO)
        {
            try
            {
                if (!_productRepository.EntityExists(productDTO.ProductId)) return false;

                Product product = _mapper.Map<Product>(productDTO);

                await _productRepository.UpdateEntityAsync(product);

                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}

using AutoMapper;
using NorthwindDAL.Interfaces;
using NorthwindModels.DTOs;
using NorthwindModels.Models;

namespace NorthwindBL
{
    public class ProductServices
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductServices(IProductRepository productRepository, IMapper mapper) 
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }


        public ProductDTO? AddProduct(ProductCreateDTO productDTO)
        {

            Product product = _mapper.Map<Product>(productDTO);

            product.Discontinued = false;

            _productRepository.AddProduct(product);

            return _mapper.Map<ProductDTO>(product);
        }

        public bool DeleteProduct(int id)
        {
            Product? product = _productRepository.GetByProductId(id);

            if (product is null) return false;

            _productRepository.DeleteProduct(product);

            return true;
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            IEnumerable<Product> products = _productRepository.GetAllProducts();
            return products.Select(p => _mapper.Map<ProductDTO>(p));
        }

        public ProductDTO? GetByProductId(int id)
        {
            Product? product = _productRepository.GetByProductId(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public bool UpdateProduct(ProductDTO productDTO)
        {
            if (!_productRepository.ProductExists(productDTO.ProductId)) return false;

            Product product = _mapper.Map<Product>(productDTO);

            _productRepository.UpdateProduct(product);

            return true;

        }
    }
}

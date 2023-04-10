using AutoMapper;
using NorthwindDAL.Repositories;
using NorthwindModels.DTOs;
using NorthwindModels.Models;

namespace NorthwindDAL.Services
{
    public class ProductServices : IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public ProductServices(AppDbContext appDbContext, IMapper mapper) 
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }


        public ProductDTO? AddProduct(ProductCreateDTO productDTO)
        {
            if (_appDbContext.Products.Any(p => p.ProductId.Equals(productDTO))) return null;

            Product product = _mapper.Map<Product>(productDTO);

            product.Discontinued = false;

            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();

            return _mapper.Map<ProductDTO>(product);
        }

        public bool DeleteProduct(int id)
        {
            Product? product = _appDbContext.Products.Find(id);

            if (product is null) return false;

            _appDbContext.Products.Remove(product);
            _appDbContext.SaveChanges();

            return true;
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            return _appDbContext.Products.Select(p => _mapper.Map<ProductDTO>(p));
        }

        public ProductDTO? GetByProductId(int id)
        {
            return _mapper.Map<ProductDTO>(_appDbContext.Products.Find(id));
        }

        public bool UpdateProduct(ProductUpdateDTO productDTO)
        {
            if(_appDbContext.Products.Any(p => p.ProductId.Equals(productDTO.ProductId)))
            {
                Product product = _mapper.Map<Product>(productDTO);

                _appDbContext.Products.Update(product);
                _appDbContext.SaveChanges();

                return true;
            }

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Products
{
    [AutoRegister]
    public class GetProductService : IGetProductService
    {
        private readonly IProductRepository _productRepository;

        public GetProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product GetProduct(Guid id)
        {
            return _productRepository.Get(id);
        }

        public IEnumerable<Product> GetProducts(string name = null, string sku = null, bool? isActive = null)
        {
            return _productRepository.Get(name, sku, isActive);
        }

        public IEnumerable<Product> GetProductsById(System.Collections.Generic.List<Guid> productIds)
        {
            return _productRepository.GetProductsByIds(productIds);
        }

        public IEnumerable<Product> GetProductsByTag(string tag)
        {
            return GetProducts().Where(product => product.Tags != null && product.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase))
                                 .ToList();
        }
    }
}

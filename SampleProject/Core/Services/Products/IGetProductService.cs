using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Products
{
    
    public interface IGetProductService
    {
        Product GetProduct(Guid id);
        IEnumerable<Product> GetProducts(string name = null, string sku = null, bool? isActive = null);
        IEnumerable<Product> GetProductsByTag(string tag);
        IEnumerable<Product> GetProductsById(List<Guid> productIds);
    }

}

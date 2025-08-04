using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Products
{
   
    public interface IUpdateProductService
    {
        void Update(Product product, string name, string sku, decimal? price, bool isActive
            , IEnumerable<string> tags, int stockQuantity, DateTime createdDate);
    }

}

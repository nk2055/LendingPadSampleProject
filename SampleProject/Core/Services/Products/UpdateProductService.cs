using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;

namespace Core.Services.Products
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateProductService : IUpdateProductService
    {
        public void Update(Product product, string name, string sku, decimal? price, bool isActive
            , IEnumerable<string> tags, int stockQuantity, DateTime createdDate)
        {
            product.SetName(name);
            product.SetSKU(sku);
            product.SetPrice(price);
            product.SetIsActive(isActive);
            product.SetTags(tags);
            product.SetStockQuantity(stockQuantity);
            product.SetCreatedDate(createdDate);
        }
    }
}

using BusinessEntities;
using System.Collections.Generic;

namespace WebApi.Models.Products
{
    public class ProductData : IdObjectData
    {
        public ProductData(Product product) : base(product)
        {
            Name = product.Name;
            Sku = product.SKU;
            Price = product.Price;
            IsActive = product.IsActive;
            Tags = product.Tags;
            StockQuantity = product.StockQuantity;
            CreatedDate = product.CreatedDate;
        }

        public string Name { get; set; }
        public string Sku { get; set; }
        public decimal? Price { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public int? StockQuantity { get; set; }
        public System.DateTime? CreatedDate { get; set; }
    }
}

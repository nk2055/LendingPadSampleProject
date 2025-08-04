using System;
using System.Collections.Generic;

namespace WebApi.Models.Products
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public decimal? Price { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

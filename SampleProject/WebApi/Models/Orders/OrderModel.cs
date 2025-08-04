using BusinessEntities;
using System;
using System.Collections.Generic;
using WebApi.Models.Products;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal? TotalAmount { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}

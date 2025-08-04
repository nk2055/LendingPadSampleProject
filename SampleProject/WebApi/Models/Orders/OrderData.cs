using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models.Products;

namespace WebApi.Models.Orders
{
    public class OrderData : IdObjectData
    {
        public OrderData(Order order, IEnumerable<Product> products) : base(order)
        {
            CustomerId = order.CustomerId;
            OrderId = order.OrderId;
            Status = new EnumData(order.Status);
            TotalAmount = order.TotalAmount;
            Products = products.Select(p => new ProductData(p)).ToList();
        }

        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }
        public EnumData Status { get; set; }
        public decimal? TotalAmount { get; set; }
        public List<ProductData> Products { get; set; }
    }
}

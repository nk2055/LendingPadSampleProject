using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    
    public interface IGetOrderService
    {
        Order GetOrder(Guid orderId);
        IEnumerable<Order> GetOrders(Guid? customerId = null, Guid? orderId = null, OrderStatus? status = null);
        IEnumerable<Order> GetOrdersByTag(string tag);
    }

}

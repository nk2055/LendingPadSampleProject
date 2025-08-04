using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    
    public interface IUpdateOrderService
    {
        void Update(Order order, Guid customerId, Guid orderId, OrderStatus status, decimal? totalAmount,List<Guid> productIds);
    }

}

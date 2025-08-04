using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface ICreateOrderService
    {
        Order Create(Guid customerId, Guid orderId, OrderStatus status, decimal? totalAmount, List<Guid> productIds);
    }

    
}

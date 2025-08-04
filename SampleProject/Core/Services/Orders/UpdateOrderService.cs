using BusinessEntities;
using Common;
using System;
using Collections = System.Collections.Generic;

namespace Core.Services.Orders
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateOrderService : IUpdateOrderService
    {
        public void Update(Order order, Guid customerId, Guid orderId, OrderStatus status, decimal? totalAmount, Collections.List<Guid> productIds)
        {
            order.SetCustomerId(customerId);
            order.SetOrderId(orderId);
            order.SetStatus(status);
            order.SetTotalAmount(totalAmount);
            order.SetProductIds(productIds);
        }
    }
}

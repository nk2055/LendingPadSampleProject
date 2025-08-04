using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class GetOrderService : IGetOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order GetOrder(Guid orderId)
        {
            return _orderRepository.Get(orderId);
        }

        public IEnumerable<Order> GetOrders(Guid? customerId = null, Guid? orderId = null, OrderStatus? status = null)
        {
            return _orderRepository.GetOrders(customerId, orderId, status);
        }

        public IEnumerable<Order> GetOrdersByTag(string tag)
        {
            return GetOrders().Where(order => order.Tags != null && order.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase))
                              .ToList();
        }
    }
}

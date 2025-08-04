using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IIdObjectFactory<Order> _orderFactory;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderService(IIdObjectFactory<Order> orderFactory, IOrderRepository orderRepository, IUpdateOrderService updateOrderService)
        {
            _orderFactory = orderFactory;
            _orderRepository = orderRepository;
            _updateOrderService = updateOrderService;
        }

        public Order Create(Guid customerId, Guid orderId, OrderStatus status, decimal? totalAmount, System.Collections.Generic.List<Guid> productIds)
        {
            var order = _orderFactory.Create(orderId);
            _updateOrderService.Update(order, customerId, orderId, status, totalAmount, productIds);
            _orderRepository.Save(order);
            return order;
        }
    }
}

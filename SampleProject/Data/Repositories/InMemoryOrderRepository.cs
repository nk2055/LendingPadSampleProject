using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Data.InMemoryRepositories
{
    [AutoRegister]
    public class InMemoryOrderRepository : IOrderRepository
    {
        private static readonly ConcurrentDictionary<Guid, Order> _store = new ConcurrentDictionary<Guid, Order>();

        public Order Get(Guid id)
        {
            _store.TryGetValue(id, out var order);
            return order;
        }

        public IEnumerable<Order> GetOrders(Guid? customerId = null, Guid? orderId = null, OrderStatus? status = null)
        {
            var query = _store.Values.AsEnumerable();

            if (customerId.HasValue)
                query = query.Where(o => o.CustomerId == customerId.Value);

            if (orderId.HasValue)
                query = query.Where(o => o.OrderId == orderId.Value);

            if (status.HasValue)
                query = query.Where(o => o.Status == status.Value);

            return query;
        }

        public void Save(Order order) => _store[order.Id] = order;

        public void Delete(Order order) => _store.TryRemove(order.Id, out _);

        public void DeleteAll() => _store.Clear();

        public IEnumerable<Order> GetOrders(Guid? customerId = null, string orderNumber = null, OrderStatus? status = null)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IDocumentSession _documentSession;

        public OrderRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<Order> GetOrders(Guid? customerId = null, Guid? orderId = null, OrderStatus? status = null)
        {
            var query = _documentSession.Advanced.DocumentQuery<Order, OrdersListIndex>();
            var hasFirstParameter = false;

            if (customerId != null && customerId != Guid.Empty)
            {
                query = query.WhereEquals("CustomerId", customerId.Value);
                hasFirstParameter = true;
            }

            if (orderId != null && orderId != Guid.Empty)
            {
                if (hasFirstParameter) query = query.AndAlso();
                query = query.WhereEquals("OrderId", orderId);
                hasFirstParameter = true;
            }

            if (status != null)
            {
                if (hasFirstParameter) query = query.AndAlso();
                query = query.WhereEquals("Status", (int)status);
            }

            return query.ToList();
        }

        public void DeleteAll()
        {
            base.DeleteAll<OrdersListIndex>();
        }
    }
}

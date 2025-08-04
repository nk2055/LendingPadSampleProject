using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        /// <summary>
        /// Retrieves orders with optional filters.
        /// </summary>
        /// <param name="customerId">Filter by Customer ID (GUID).</param>
        /// <param name="orderNumber">Filter by exact order number.</param>
        /// <param name="status">Filter by order status.</param>
        /// <returns>List of matching orders.</returns>
        IEnumerable<Order> GetOrders(Guid? customerId = null, Guid? orderId = null, OrderStatus? status = null);

        /// <summary>
        /// Deletes all orders from the index.
        /// </summary>
        void DeleteAll();
    }
}

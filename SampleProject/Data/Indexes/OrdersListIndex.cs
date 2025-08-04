using System.Linq;
using BusinessEntities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Data.Indexes
{
    public class OrdersListIndex : AbstractIndexCreationTask<Order>
    {
        public OrdersListIndex()
        {
            Map = orders => from order in orders
                            select new
                            {
                                order.OrderId,
                                order.CustomerId,
                                order.OrderDate,
                                order.TotalAmount,
                                order.Status
                            };

            // OrderNumber & Status should be stored for quick lookups
            Index(x => x.OrderId, FieldIndexing.NotAnalyzed);
            Index(x => x.Status, FieldIndexing.NotAnalyzed);
        }
    }
}

using System;
using System.Collections.Generic;
using Common.Extensions;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        private readonly List<string> _tags = new List<string>();
        private Guid _orderId;
        private Guid _customerId;
        private DateTime _orderDate;
        private decimal _totalAmount;
        private OrderStatus _status = OrderStatus.Pending;
        private List<Guid> _productIds = new List<Guid>();

        public Guid OrderId
        {
            get => _orderId;
            private set => _orderId = value;
        }

        public Guid CustomerId
        {
            get => _customerId;
            private set => _customerId = value;
        }

        public DateTime OrderDate
        {
            get => _orderDate;
            private set => _orderDate = value;
        }

        public decimal TotalAmount
        {
            get => _totalAmount;
            private set => _totalAmount = value;
        }

        public OrderStatus Status
        {
            get => _status;
            private set => _status = value;
        }

        public IEnumerable<string> Tags
        {
            get => _tags;
            private set => _tags.Initialize(value);
        }
        public List<Guid> ProductIds { get => _productIds; private set => _productIds.Initialize(value); }

        // method to add product
        public void AddProduct(Guid productId)
        {
            if (!ProductIds.Contains(productId))
                ProductIds.Add(productId);
        }
        // ==== Setters with Validation ====

        public void SetOrderId(Guid orderId)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException("Order ID cannot be empty.", nameof(orderId));

            _orderId = orderId;
        }

        public void SetProductIds(List<Guid> productIds)
        {
            if (productIds == null || productIds.Count == 0)
                throw new ArgumentException("Product IDs cannot be null or empty.", nameof(productIds));

            _productIds.Clear();
            _productIds.AddRange(productIds);
        }

        public void SetCustomerId(Guid customerId)
        {
            if (customerId == Guid.Empty)
                throw new ArgumentException("Customer ID cannot be empty.", nameof(customerId));

            _customerId = customerId;
        }

        public void SetOrderDate(DateTime orderDate)
        {
            if (orderDate == default)
                throw new ArgumentException("Order date is invalid.", nameof(orderDate));

            _orderDate = orderDate;
        }

        public void SetTotalAmount(decimal? totalAmount)
        {
            if (totalAmount < 0)
                throw new ArgumentOutOfRangeException(nameof(totalAmount), "Total amount cannot be negative.");

            _totalAmount = (decimal)totalAmount;
        }

        public void SetStatus(OrderStatus status)
        {
            _status = status;
        }

        public void SetTags(IEnumerable<string> tags)
        {
            _tags.Initialize(tags);
        }
    }

    // Enum for Order Status
    public enum OrderStatus
    {
        Pending,
        Processing,
        Completed,
        Cancelled
    }
}

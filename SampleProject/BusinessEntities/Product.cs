using System;
using System.Collections.Generic;
using Common.Extensions;

namespace BusinessEntities
{
    public class Product : IdObject
    {
        private readonly List<string> _tags = new List<string>();
        private string _name;
        private string _sku;
        private decimal _price;
        private int _stockQuantity;
        private DateTime _createdDate;
        private bool _isActive = true;

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public string SKU
        {
            get => _sku;
            private set => _sku = value;
        }

        public decimal Price
        {
            get => _price;
            private set => _price = value;
        }

        public int StockQuantity
        {
            get => _stockQuantity;
            private set => _stockQuantity = value;
        }

        public DateTime CreatedDate
        {
            get => _createdDate;
            private set => _createdDate = value;
        }

        public bool IsActive
        {
            get => _isActive;
            private set => _isActive = value;
        }

        public IEnumerable<string> Tags
        {
            get => _tags;
            private set => _tags.Initialize(value);
        }

        public void SetId(Guid Id)
        {
            if (Id == null && Id == Guid.Empty)
                throw new ArgumentNullException(nameof(Id), "Product Id must be provided.");

            this.Id = Id;
        }
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Product name must be provided.");

            _name = name;
        }

        public void SetSKU(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentNullException(nameof(sku), "SKU must be provided.");

            _sku = sku;
        }

        public void SetPrice(decimal? price)
        {
            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");

            _price = (decimal)price;
        }

        public void SetStockQuantity(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Stock quantity cannot be negative.");

            _stockQuantity = quantity;
        }

        public void SetCreatedDate(DateTime createdDate)
        {
            _createdDate = createdDate;
        }

        public void SetIsActive(bool isActive)
        {
            _isActive = isActive;
        }

        public void SetTags(IEnumerable<string> tags)
        {
            _tags.Initialize(tags);
        }
    }
}

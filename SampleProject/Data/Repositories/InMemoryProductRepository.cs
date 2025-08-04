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
    public class InMemoryProductRepository : IProductRepository
    {
        private static readonly ConcurrentDictionary<Guid, Product> _store = new ConcurrentDictionary<Guid, Product>();

        public Product Get(Guid id)
        {
            _store.TryGetValue(id, out var product);
            return product;
        }

        public IEnumerable<Product> Get(string name = null, string sku = null, bool? isActive = null)
        {
            var query = _store.Values.AsEnumerable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name?.Contains(name) == true);

            if (!string.IsNullOrEmpty(sku))
                query = query.Where(p => p.SKU?.Contains(sku) == true);

            if (isActive.HasValue)
                query = query.Where(p => p.IsActive == isActive.Value);

            return query;
        }

        public void Save(Product product) => _store[product.Id] = product;

        public void Delete(Product product) => _store.TryRemove(product.Id, out _);

        public void DeleteAll() => _store.Clear();

        public IEnumerable<Product> GetProductsByIds(System.Collections.Generic.List<Guid> productIds)
        {
            if (productIds == null || productIds.Count == 0)
                return Enumerable.Empty<Product>();
            return _store.Values.Where(p => productIds.Contains(p.Id)).ToList();
        }
    }
}

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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IDocumentSession _documentSession;

        public ProductRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<Product> Get(string name = null, string sku = null, bool? isActive = null)
        {
            var query = _documentSession.Advanced.DocumentQuery<Product, ProductsListIndex>();
            var hasFirstParameter = false;

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where($"Name:*{name}*");
                hasFirstParameter = true;
            }

            if (!string.IsNullOrWhiteSpace(sku))
            {
                if (hasFirstParameter) query = query.AndAlso();
                query = query.WhereEquals("SKU", sku);
                hasFirstParameter = true;
            }

            if (isActive != null)
            {
                if (hasFirstParameter) query = query.AndAlso();
                query = query.WhereEquals("IsActive", isActive.Value);
            }

            return query.ToList();
        }

        public IEnumerable<Product> GetProductsByIds(System.Collections.Generic.List<Guid> productIds)
        {
            var query = _documentSession.Advanced.DocumentQuery<Product, ProductsListIndex>();

            if (productIds != null && productIds.Count > 0)
            {
                query = query.WhereIn("Id", productIds.Cast<object>().ToList());
            }

            return query.ToList();
        }

        public void DeleteAll()
        {
            base.DeleteAll<ProductsListIndex>();
        }
    }
}

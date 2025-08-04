using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Retrieves products with optional filters.
        /// </summary>
        /// <param name="name">Filter by product name (supports partial match).</param>
        /// <param name="sku">Filter by exact SKU.</param>
        /// <param name="isActive">Filter by active status.</param>
        /// <returns>List of matching products.</returns>
        IEnumerable<Product> Get(string name = null, string sku = null, bool? isActive = null);

        /// <summary>
        /// Deletes all products from the index.
        /// </summary>
        void DeleteAll();
        IEnumerable<Product> GetProductsByIds(List<Guid> productIds);
    }
}

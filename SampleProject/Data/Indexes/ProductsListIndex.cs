using System.Linq;
using BusinessEntities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Data.Indexes
{
    public class ProductsListIndex : AbstractIndexCreationTask<Product>
    {
        public ProductsListIndex()
        {
            Map = products => from product in products
                              select new
                              {
                                  product.Name,
                                  product.SKU,
                                  product.Price,
                                  product.IsActive
                              };

            // SKU should not be analyzed to allow exact matches
            Index(x => x.SKU, FieldIndexing.NotAnalyzed);
            Index(x => x.IsActive, FieldIndexing.NotAnalyzed);
        }
    }
}

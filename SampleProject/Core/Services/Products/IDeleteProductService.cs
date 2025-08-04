using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Products
{
    
    public interface IDeleteProductService
    {
        void Delete(Product product);
        void DeleteAll();
    }
}

using BusinessEntities;
using Core.Services.Products;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly ICreateProductService _createProductService;
        private readonly IDeleteProductService _deleteProductService;
        private readonly IGetProductService _getProductService;
        private readonly IUpdateProductService _updateProductService;

        public ProductController(
            ICreateProductService createProductService,
            IDeleteProductService deleteProductService,
            IGetProductService getProductService,
            IUpdateProductService updateProductService)
        {
            _createProductService = createProductService;
            _deleteProductService = deleteProductService;
            _getProductService = getProductService;
            _updateProductService = updateProductService;
        }

        [Route("{productId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateProduct(Guid productId, [FromBody] ProductModel model)
        {
            var product = _getProductService.GetProduct(productId);
            if (product != null)
            {
                return Found("Product already exists.");
            }
            product = _createProductService.Create(productId, model.Name, model.Sku, model.Price, model.IsActive
                , model.Tags, model.StockQuantity, model.CreatedDate);
            return Found(new ProductData(product));
        }

        [Route("{productId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateProduct(Guid productId, [FromBody] ProductModel model)
        {
            try
            {
                var product = _getProductService.GetProduct(productId);
                if (product != null)
                {
                    _updateProductService.Update(product, model.Name, model.Sku, model.Price, model.IsActive, model.Tags
                        , model.StockQuantity, model.CreatedDate);
                    return Found(new ProductData(product));
                }
                else return Found("There is no product with given Id.");
            }
            catch (ArgumentNullException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{productId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteProduct(Guid productId)
        {
            var product = _getProductService.GetProduct(productId);
            if (product != null)
                _deleteProductService.Delete(product);
            return Found();
        }

        [Route("{productId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetProduct(Guid productId)
        {
            var product = _getProductService.GetProduct(productId);
            if (product != null)
                return Found(new ProductData(product));
            else return Found("There is no product with given Id.");
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetProducts(int skip, int take, string name = null, string sku = null, bool? isActive = null)
        {
            var products = _getProductService.GetProducts(name, sku, isActive)
                                             .Skip(skip).Take(take)
                                             .Select(q => new ProductData(q))
                                             .ToList();
            return Found(products);
        }

        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllProducts()
        {
            _deleteProductService.DeleteAll();
            return Found();
        }
    }
}

using BusinessEntities;
using Core.Services.Orders;
using Core.Services.Products;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using WebApi.Controllers;
using WebApi.Models.Orders;
using WebApi.Models.Products;

[RoutePrefix("orders")]
public class OrderController : BaseApiController
{
    private readonly ICreateOrderService _createOrderService;
    private readonly IDeleteOrderService _deleteOrderService;
    private readonly IGetOrderService _getOrderService;
    private readonly IUpdateOrderService _updateOrderService;
    private readonly IGetProductService _getProductService;
    private readonly ICreateProductService _createProductService;
    private readonly IUpdateProductService _updateProductService;
    private readonly IDeleteProductService _deleteProductService;

    public OrderController(
        ICreateOrderService createOrderService,
        IDeleteOrderService deleteOrderService,
        IGetOrderService getOrderService,
        IUpdateOrderService updateOrderService,
        ICreateProductService createProductService,
        IDeleteProductService deleteProductService,
        IGetProductService getProductService,
        IUpdateProductService updateProductService)
    {
        _createOrderService = createOrderService;
        _deleteOrderService = deleteOrderService;
        _getOrderService = getOrderService;
        _updateOrderService = updateOrderService;
        _createProductService = createProductService;
        _deleteProductService = deleteProductService;
        _getProductService = getProductService;
        _updateProductService = updateProductService;
    }

    [Route("{orderId:guid}/create")]
    [HttpPost]
    public HttpResponseMessage CreateOrder(Guid orderId, [FromBody] OrderModel model)
    {
        var existing = _getOrderService.GetOrder(orderId);
        if (existing != null)
            return Found("Order already exists.");

        var products = model.Products.Select(p => _createProductService.Create(
            p.Id, p.Name, p.Sku, p.Price, p.IsActive, p.Tags, p.StockQuantity, p.CreatedDate)).ToList();

        var order = _createOrderService.Create(model.CustomerId, orderId, model.Status, model.TotalAmount, products.Select(p => p.Id).ToList());

        return Found(new OrderData(order, products));
    }

    [Route("{orderId:guid}/update")]
    [HttpPost]
    public HttpResponseMessage UpdateOrder(Guid orderId, [FromBody] OrderModel model)
    {
        var order = _getOrderService.GetOrder(orderId);
        if (order == null)
            return DoesNotExist();

        foreach (var productModel in model.Products)
        {
            var product = _getProductService.GetProduct(productModel.Id) ??
                          _createProductService.Create(productModel.Id, productModel.Name, productModel.Sku, productModel.Price,
                                                       productModel.IsActive, productModel.Tags, productModel.StockQuantity, productModel.CreatedDate);
            _updateProductService.Update(product, productModel.Name, productModel.Sku, productModel.Price,
                                         productModel.IsActive, productModel.Tags, productModel.StockQuantity, productModel.CreatedDate);
        }

        _updateOrderService.Update(order, model.CustomerId, orderId, model.Status, model.TotalAmount, model.Products.Select(p => p.Id).ToList());
        var products = _getProductService.GetProducts().Where(p => order.ProductIds.Contains(p.Id)).ToList();

        return Found(new OrderData(order, products));
    }

    [Route("{orderId:guid}/delete")]
    [HttpDelete]
    public HttpResponseMessage DeleteOrder(Guid orderId)
    {
        var order = _getOrderService.GetOrder(orderId);
        if (order == null)
            return DoesNotExist();

        foreach (var productId in order.ProductIds)
        {
            var product = _getProductService.GetProduct(productId);
            if (product != null) _deleteProductService.Delete(product);
        }
        _deleteOrderService.Delete(order);
        return Found();
    }

    [Route("{orderId:guid}")]
    [HttpGet]
    public HttpResponseMessage GetOrder(Guid orderId)
    {
        var order = _getOrderService.GetOrder(orderId);
        if (order == null)
            return DoesNotExist();

        var products = _getProductService.GetProducts().Where(p => order.ProductIds.Contains(p.Id)).ToList();
        return Found(new OrderData(order, products));
    }

    [Route("list")]
    [HttpGet]
    public HttpResponseMessage GetOrders(int skip, int take, Guid? customerId = null, Guid? orderId = null, OrderStatus? status = null)
    {
        var orders = _getOrderService.GetOrders(customerId, orderId, status).ToList()
                                     .Skip(skip).Take(take).ToList();

        var result = orders.Select(o => new OrderData(o,
            _getProductService.GetProducts().Where(p => o.ProductIds.Contains(p.Id)).ToList())).ToList();

        return Found(result);
    }

    [Route("clear")]
    [HttpDelete]
    public HttpResponseMessage DeleteAllOrders()
    {
        _deleteProductService.DeleteAll();
        _deleteOrderService.DeleteAll();
        return Found();
    }

    [Route("{orderId:guid}/products")]
    [HttpGet]
    public HttpResponseMessage GetOrderProducts(Guid orderId)
    {
        var order = _getOrderService.GetOrder(orderId);
        if (order == null) return DoesNotExist();

        var products = _getProductService.GetProducts().Where(p => order.ProductIds.Contains(p.Id)).ToList();
        return Found(products.Select(p => new ProductData(p)));
    }
}

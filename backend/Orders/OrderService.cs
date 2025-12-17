using backend.Common;
using backend.Employees;
using backend.Exceptions;
using backend.Mappings;
using backend.Products;
using backend.Variations;
using Microsoft.EntityFrameworkCore;

namespace backend.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IProductRepository _productRepository;
    private readonly IVariationRepository _variationRepository;

    public OrderService(IOrderRepository repository, IEmployeeRepository employeeRepository, IProductRepository productRepository, IVariationRepository variationRepository)
    {
        _orderRepository = repository;
        _employeeRepository = employeeRepository;
        _productRepository = productRepository;
        _variationRepository = variationRepository;
    }

    public async Task<ItemResponse> AddItemAsync(int orderId, ItemCreateRequest request)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new NotFoundException();

        if (order.Status != OrderStatus.Opened) throw new OrderNotOpenException();

        if (await _productRepository.GetProductByIdAsync(request.ProductId) == null) throw new NotFoundException();

        //var variations = await Task.WhenAll(request.VariationIds.Select(_variationRepository.GetVariationByIdAsync));
        //
        //if (variations.Contains(null)) throw new NotFoundException();

        var item = await _orderRepository.AddItemAsync(new Item()
        {
            OrderId = orderId,
            ProductId = request.ProductId,
            Currency = request.Currency,
            Quantity = request.Quantity,
            Discount = request.Discount,
            VATPercentage = request.VATPercentage
            //Variations = variations!,
        });

        return new ItemResponse()
        {
            ItemId = item.ItemId,
            OrderId = item.OrderId,
            ProductId = item.ProductId,
            Currency = item.Currency,
            Quantity = item.Quantity,
            Discount = item.Discount,
            VATPercentage = item.VATPercentage
        };
    }

    public async Task CancelOrderAsync(int orderId)
    {
        Order order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new NotFoundException();

        if (order.Status != OrderStatus.Opened) throw new OrderNotOpenException();

        order.Status = OrderStatus.Cancelled;

        await _orderRepository.UpdateOrderAsync(order);
    }

    public async Task CloseOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new NotFoundException();

        if (order.Status != OrderStatus.Opened) throw new OrderNotOpenException();

        order.Status = OrderStatus.Closed;

        await _orderRepository.UpdateOrderAsync(order);
    }

    public async Task<OrderResponse> GetOrderByIdAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new NotFoundException();

        var items = await _orderRepository.GetItemsByOrderIdAsync(orderId) ?? throw new NotFoundException();

        if (items.Contains(null)) throw new NotFoundException();

        var itemResponses = items.Select(item => new ItemResponse()
        {
            ItemId = item.ItemId,
            OrderId = item.OrderId,
            ProductId = item.ProductId,
            Currency = item.Currency,
            Quantity = item.Quantity,
            Discount = item.Discount,
            VATPercentage = item.VATPercentage
        });

        return new OrderResponse()
        {
            OrderId = order.OrderId,
            OperatorId = order.OperatorId,
            Status = order.Status,
            Tip = order.Tip,
            ServiceCharge = order.ServiceCharge,
            Currency = order.Currency,
            Discount = order.Discount,
            Items = itemResponses
        };
    }

    public async Task<IEnumerable<ItemResponse>> GetItemsByOrderAsync(int orderId)
    {
        if (await _orderRepository.GetOrderByIdAsync(orderId) == null) throw new NotFoundException();

        var items = await _orderRepository.GetItemsByOrderIdAsync(orderId);

        if (items == null || items.Any(item => item == null)) throw new NotFoundException();

        //foreach (var item in items)
        //{
        //    var variations = await _variationRepository.GetVariationsByItemIdAsync(item.ItemId);
        //
        //    if (variations == null || variations.Contains(null)) throw new NotFoundException();
        //
        //    item.Variations = variations!;
        //}

        var itemResponses = items.Select(item => new ItemResponse()
        {
            ItemId = item.ItemId,
            OrderId = item.OrderId,
            ProductId = item.ProductId,
            Currency = item.Currency,
            Quantity = item.Quantity,
            Discount = item.Discount,
            VATPercentage = item.VATPercentage
        });

        return itemResponses;
    }

    public async Task<OrderTaxesResponse> GetTaxesForOrderById(int orderId)
    {
        if (await _orderRepository.GetOrderByIdAsync(orderId) == null) throw new NotFoundException();

        var items = await _orderRepository.GetItemsByOrderIdAsync(orderId) ?? throw new NotFoundException();

        if (items.Contains(null)) throw new NotFoundException();

        var products = _productRepository.GetProductsByItems(items);

        if (products.Contains(null)) throw new NotFoundException();

        var response = new OrderTaxesResponse()
        {
            Subtotal = decimal.Zero,
            Tax = decimal.Zero,
            Total = decimal.Zero
        };

        using (IEnumerator<Item> item = items.GetEnumerator())
        using (IEnumerator<Product> product = products.AsEnumerable().GetEnumerator()!)
            while (item.MoveNext() && product.MoveNext())
            {
                response.Subtotal += product.Current.UnitPrice * item.Current.Quantity;
                response.Tax += product.Current.UnitPrice * item.Current.Quantity * product.Current.VatPercent / 100;
                response.Total += product.Current.UnitPrice * item.Current.Quantity * (100 + product.Current.VatPercent) / 100;
            }

        return response;
    }

    public async Task<string> GetReceiptAsync(int orderId)
    {
        if (await _orderRepository.GetOrderByIdAsync(orderId) == null) throw new NotFoundException();

        throw new NotImplementedException();
    }

    public async Task<OrderResponse> OpenOrderAsync(OrderRequest request)
    {
        if (await _employeeRepository.GetEmployeeByIdAsync(request.OperatorId) == null) throw new NotFoundException();

        var order = await _orderRepository.AddOrderAsync(new Order()
        {
            OperatorId = request.OperatorId,
            Currency = request.Currency,
            ServiceCharge = request.ServiceCharge,
            Discount = request.Discount
        });

        return new OrderResponse
        {
            OrderId = order.OrderId,
            OperatorId = order.OperatorId,
            Status = order.Status,
            Tip = order.Tip,
            ServiceCharge = order.ServiceCharge,
            Currency = order.Currency,
            Discount = order.Discount
        };
    }

    public async Task RemoveItemAsync(int orderId, int itemId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new NotFoundException();

        if (order.Status != OrderStatus.Opened) throw new OrderNotOpenException();

        var item = await _orderRepository.GetItemByIdAsync(itemId) ?? throw new NotFoundException();

        await _orderRepository.DeleteItemAsync(item);
    }

    public async Task UpdateItemAsync(int orderId, int itemId, ItemUpdateRequest request)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new NotFoundException();

        if (order.Status != OrderStatus.Opened) throw new OrderNotOpenException();

        var item = await _orderRepository.GetItemByIdAsync(itemId) ?? throw new NotFoundException();

        //var variations = await Task.WhenAll(request.VariationIds.Select(_variationRepository.GetVariationByIdAsync));
        //
        //if (variations.Contains(null)) throw new NotFoundException();

        //item.Variations = variations!;
        item.Currency = request.Currency;
        item.Quantity = request.Quantity;
        item.Discount = request.Discount;
        item.VATPercentage = request.VATPercentage;

        await _orderRepository.UpdateItemAsync(item);
    }

    public async Task UpdateOrderAsync(int orderId, OrderRequest request)
    //idk if order status should be updateable by UpdateOrder, because there are independant methods for each different state and logic associated with order status
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new NotFoundException();

        if (order.Status != OrderStatus.Opened) throw new OrderNotOpenException();

        order.Status = request.Status;
        order.Tip = request.Tip;
        order.ServiceCharge = request.ServiceCharge;
        order.Discount = request.Discount;
        order.Currency = request.Currency;

        await _orderRepository.UpdateOrderAsync(order);
    }

    public async Task<IEnumerable<OrderResponse>> GetOrdersByLocationAsync(int locationId)
    {
        var employees = await _employeeRepository.GetEmployeesAsync(new EmployeeQuery() { LocationId = locationId }) ?? throw new NotFoundException();

        if (employees.Count == 0) throw new NotFoundException();

        var employeeIds = employees.Select(employee => employee.EmployeeId);

        return (await _orderRepository.GetOrdersByEmployeeIdsAsync(employeeIds)).Select(order => new OrderResponse()
        {
            OrderId = order.OrderId,
            OperatorId = order.OperatorId,
            Status = order.Status,
            Tip = order.Tip,
            ServiceCharge = order.ServiceCharge,
            Currency = order.Currency,
            Discount = order.Discount
        });
    }
}
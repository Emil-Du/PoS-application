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

    public async Task<ItemResponse> AddItemAsync(int orderId, ItemRequest request)
    {
        if (await _orderRepository.GetOrderByIdAsync(orderId) == null) throw new NotFoundException();
        if (await _productRepository.GetProductByIdAsync(request.ProductId) == null) throw new NotFoundException();

        var variations = await Task.WhenAll(request.VariationIds.Select(_variationRepository.GetVariationByIdAsync));

        if (variations.Contains(null)) throw new NotFoundException();

        var item = await _orderRepository.AddOrUpdateItemAsync(new Item()
        {
            Variations = variations!,
            Currency = request.Currency,
            Quantity = request.Quantity,
            Discount = request.Discount,
            VATPercentage = request.VATPercentage
        });

        //var selections = variations
        //    .Select(variation => new ItemVariationSelection()
        //    {
        //        ItemId = item.ItemId,
        //        Item = item,
        //        VariationId = variation!.VariationId,
        //        Variation = variation
        //    });

        //foreach (var selection in selections) await _orderRepository.AddOrUpdateItemVariationSelection(selection);

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

        if (order.Status != OrderStatus.Opened) throw new NotFoundException();

        order.Status = OrderStatus.Closed;

        await _orderRepository.AddOrUpdateOrderAsync(order);
    }

    public async Task CloseOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new NotFoundException();

        if (order.Status != OrderStatus.Opened) throw new NotFoundException();

        order.Status = OrderStatus.Closed;

        await _orderRepository.AddOrUpdateOrderAsync(order);
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

    public async Task<string> GetReceiptAsync(int orderId)
    {
        if (await _orderRepository.GetOrderByIdAsync(orderId) == null) throw new NotFoundException();

        throw new NotImplementedException();
    }

    public async Task<OrderResponse> OpenOrderAsync(OrderRequest request)
    {
        if (await _employeeRepository.GetEmployeeByIdAsync((int)request.OperatorId) == null) throw new NotFoundException();
        
        var order = await _orderRepository.AddOrUpdateOrderAsync(new Order()
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
        if (await _orderRepository.GetOrderByIdAsync(orderId) == null) throw new NotFoundException();

        var item = await _orderRepository.GetItemByIdAsync(itemId) ?? throw new NotFoundException();

        await _orderRepository.AddOrUpdateItemAsync(item);
    }

    public async Task UpdateItemAsync(int orderId, int itemId, ItemRequest request)
    {
        if (await _orderRepository.GetOrderByIdAsync(orderId) == null) throw new NotFoundException();
        if (await _productRepository.GetProductByIdAsync(request.ProductId) == null) throw new NotFoundException();

        var variations = await Task.WhenAll(request.VariationIds.Select(_variationRepository.GetVariationByIdAsync));

        if (variations.Contains(null)) throw new NotFoundException();

        var item = new Item()
        {
            ItemId = itemId,
            Variations = variations!,
            Currency = (Currency)request.Currency,
            Quantity = (int)request.Quantity,
            Discount = (decimal)request.Discount,
            VATPercentage = (decimal)request.VATPercentage
        };

        //await _orderRepository.AddOrUpdateItemAsync(item);
        //
        //var selections = variations
        //    .Select(variation => new ItemVariationSelection()
        //    {
        //        ItemId = item.ItemId,
        //        Item = item,
        //        VariationId = variation!.VariationId,
        //        Variation = variation
        //    });
        //
        //foreach (var selection in selections) await _orderRepository.AddOrUpdateItemVariationSelection(selection);
    }

    public async Task UpdateOrderAsync(int orderId, OrderRequest request)
    //idk if order status should be updateable by UpdateOrder, because there are independant methods for each different state and logic associated with order status
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new NotFoundException();

        if (order.Status != OrderStatus.Opened) throw new NotFoundException();

        await _orderRepository.AddOrUpdateOrderAsync(new Order()
        {
            OrderId = orderId,
            OperatorId = request.OperatorId,
            Status = request.Status,
            Tip = request.Tip,
            ServiceCharge = request.ServiceCharge,
            Discount = request.Discount,
            Currency = request.Currency
        });
    }

    public async Task<IEnumerable<OrderResponse>> GetOrdersByLocation(int locationId)
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
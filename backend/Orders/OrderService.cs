using System.Linq;
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

    public async Task<Item> AddItemAsync(int orderId, ItemRequest request)
    {

        if (await _orderRepository.GetOrderByIdAsync(orderId) == null) throw new NotFoundException();

        var variations = await Task.WhenAll(request.VariationIds.Select(_variationRepository.GetVariationByIdAsync));

        if (variations.Contains(null)) throw new NotFoundException();

        var item = new Item()
        {
            Variations = variations!,
            Currency = (Currency)request.Currency,
            Quantity = (int)request.Quantity,
            Discount = (decimal)request.Discount,
            VATPercentage = (decimal)request.VATPercentage
        };

        await _orderRepository.AddOrUpdateItemAsync(item);

        var selections = variations
            .Select(variation => new ItemVariationSelection()
            {
                ItemId = item.ItemId,
                Item = item,
                VariationId = variation!.ProductId,
                Variation = variation
            });

        foreach (var selection in selections) await _orderRepository.AddOrUpdateItemProductSelection(selection);

        return item;
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

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        return await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new NotFoundException();
    }

    public async Task<IEnumerable<Item>> GetItemsByOrderAsync(int orderId)
    {
        if (await _orderRepository.GetOrderByIdAsync(orderId) == null) throw new NotFoundException();

        var items = await _orderRepository.GetItemsByOrderIdAsync(orderId);

        if (items == null || items.Any(item => item == null)) throw new NotFoundException();

        foreach (var item in items)
        {
            var products = await _variationRepository.GetVariationsByItemIdAsync(item.ItemId);

            if (products == null || products.Any(product => product == null)) throw new NotFoundException();

            item.Variations = products!;
        }

        return items;
    }

    public async Task<string> GetReceiptAsync(int orderId)
    {
        if (await _orderRepository.GetOrderByIdAsync(orderId) == null) throw new NotFoundException();

        throw new NotImplementedException();
    }

    public async Task<Order> OpenOrderAsync(OrderRequest request)
    {

        if (await _employeeRepository.GetEmployeeByIdAsync((int)request.OperatorId) == null) throw new NotFoundException();

        var order = new Order((int)request.OperatorId, (Currency)request.Currency, (decimal)request.ServiceCharge, (decimal)request.Discount);

        await _orderRepository.AddOrUpdateOrderAsync(order);

        return order;
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

        //add check for whether product with productId from ItemRequest exists

        var item = await _orderRepository.GetItemByIdAsync(itemId) ?? throw new NotFoundException();


        await _orderRepository.AddOrUpdateItemAsync(item);
    }

    public async Task UpdateOrderAsync(int orderId, OrderRequest request)
    //idk if order status should be updateable by UpdateOrder, because there are independant methods for each different state and logic associated with order status
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new NotFoundException();

        if (order.Status != OrderStatus.Opened) throw new NotFoundException();

        await _orderRepository.AddOrUpdateOrderAsync(order);
    }

    public async Task<IEnumerable<Order>> GetOrdersByLocation(int locationId)
    {
        var employees = await _employeeRepository.GetEmployeesAsync(new EmployeeQuery() { LocationId = locationId });

        if (employees.Count == 0) throw new NotFoundException();

        var employeeIds = employees.Select(employee => employee.EmployeeId);

        return await _orderRepository.GetOrdersByEmployeeIdsAsync(employeeIds);
    }
}
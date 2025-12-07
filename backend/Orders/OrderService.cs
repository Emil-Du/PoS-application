using backend.Common;
using backend.Employees;
using backend.Exceptions;
using backend.Locations;

namespace backend.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public OrderService(IOrderRepository repository, IEmployeeRepository employeeRepository)
    {
        _orderRepository = repository;
        _employeeRepository = employeeRepository;
    }

    public async Task<Item> AddItemAsync(int orderId, Item item)
    {
        return await _orderRepository.AddOrUpdateItemAsync(item) ?? throw new NotFoundException();
    }

    public async Task CancelOrderAsync(int orderId)
    {
        Order order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new Exception();

        if (order.Status != OrderStatus.Opened) throw new Exception();

        order.Status = OrderStatus.Closed;
        order.Time = DateTime.Now.Ticks;

        await _orderRepository.AddOrUpdateOrderAsync(order);
    }

    public async Task CloseOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new Exception();

        if (order.Status != OrderStatus.Opened) throw new Exception();

        order.Status = OrderStatus.Closed;
        order.Time = DateTime.Now.Ticks;

        await _orderRepository.AddOrUpdateOrderAsync(order);
    }

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        return await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new Exception();
    }

    public async Task<IEnumerable<Item>> GetItemsByOrderAsync(int orderId)
    {
        if (await _orderRepository.GetOrderByIdAsync(orderId) == null) throw new Exception();

        var items = await _orderRepository.GetItemsByOrderIdAsync(orderId);

        if (!items.Any()) throw new Exception();

        return items;
    }

    public async Task<string> GetReceiptAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new Exception();

        throw new NotImplementedException();
    }

    public async Task<Order> OpenOrderAsync(int operatorId, decimal serviceCharge, decimal discount, Currency currency)
    {
        if (await _employeeRepository.GetEmployeeByIdAsync(operatorId) == null) throw new Exception();

        var order = new Order(operatorId, serviceCharge, discount, currency);

        await _orderRepository.AddOrUpdateOrderAsync(order);

        return order;
    }

    public Task RemoveItemAsync(int orderId, int itemId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateItemAsync(Item item)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateOrderAsync(int orderId, decimal tip, decimal serviceCharge, decimal discount, OrderStatus status)
    //idk if order status should be updateable by UpdateOrder, because there are independant methods for each different state and logic associated with order status
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new Exception();

        if (order.Status != OrderStatus.Opened) throw new Exception();

        order.Tip = tip;
        order.ServiceCharge = serviceCharge;
        order.Discount = discount;
        order.Status = status;
        order.Time = DateTime.Now.Ticks;

        await _orderRepository.AddOrUpdateOrderAsync(order);
    }

    public async Task<IEnumerable<Order>> GetOrdersByLocation(int locationId)
    {
        var employees = await _employeeRepository.GetEmployeesAsync(new EmployeeQuery() { LocationId = locationId });

        if (employees.Count == 0) throw new Exception();

        var employeeIds = employees.Select(employee => employee.EmployeeId);

        return await _orderRepository.GetOrdersByEmployeeIdsAsync(employeeIds);
    }
}
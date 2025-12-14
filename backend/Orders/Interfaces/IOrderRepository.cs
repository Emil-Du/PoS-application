using backend.Common;
using backend.Mappings;

namespace backend.Orders;

public interface IOrderRepository
{
    public Task<Order?> GetOrderByIdAsync(int orderId);
    public Task<IEnumerable<Order>> GetOrdersByEmployeeIdsAsync(IEnumerable<int> employeeIds);
    public Task<Order> AddOrderAsync(Order order);
    public Task UpdateOrderAsync(Order order);
    public Task<Item?> GetItemByIdAsync(int itemId);
    public Task<IEnumerable<Item>> GetItemsByOrderIdAsync(int orderId);
    public Task<Item> AddItemAsync(Item item);
    public Task UpdateItemAsync(Item item);
    public Task DeleteItemAsync(Item item);
    public Task AddItemVariationSelection(ItemVariationSelection selection);
}
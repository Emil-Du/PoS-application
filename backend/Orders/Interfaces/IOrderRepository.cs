using backend.Common;
using backend.Mappings;

namespace backend.Orders;

public interface IOrderRepository
{
    public Task<Order?> GetOrderByIdAsync(int orderId);
    public Task<IEnumerable<Order>> GetOrdersByEmployeeIdsAsync(IEnumerable<int> employeeIds);
    public Task AddOrUpdateOrderAsync(Order order);
    public Task<Item?> GetItemByIdAsync(int itemId);
    public Task<IEnumerable<Item>> GetItemsByOrderIdAsync(int orderId);
    public Task AddOrUpdateItemAsync(Item item);
    public Task AddOrUpdateItemProductSelection(ItemVariationSelection selection);
}
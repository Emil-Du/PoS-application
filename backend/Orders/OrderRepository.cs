using backend.Database;
using backend.Mappings;
using Microsoft.EntityFrameworkCore;

namespace backend.Orders;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders.FindAsync(orderId);
    }
    
    public async Task<IEnumerable<Order>> GetOrdersByEmployeeIdsAsync(IEnumerable<int> employeeIds)
    {
        return await _context.Orders.Where(order => employeeIds.Contains(order.OperatorId)).ToListAsync();
    }

    public async Task<Order> AddOrUpdateOrderAsync(Order order)
    {
        var uploadedOrder = await _context.Orders.AddAsync(order);
        
        await _context.SaveChangesAsync();

        return uploadedOrder.Entity;
    }

    public async Task<Item?> GetItemByIdAsync(int itemId)
    {
        return await _context.Items.FindAsync(itemId);
    }

    public async Task<IEnumerable<Item>> GetItemsByOrderIdAsync(int orderId)
    {
        return await _context.Items.Where(item => item.OrderId == orderId).ToListAsync();
    }

    public async Task<Item> AddOrUpdateItemAsync(Item item)
    {
        var uploadedItem = await _context.Items.AddAsync(item);
        
        await _context.SaveChangesAsync();

        return uploadedItem.Entity;
    }

    public async Task AddOrUpdateItemVariationSelection(ItemVariationSelection selection)
    {
        await  _context.ItemVariationSelections.AddAsync(selection);
        
        await _context.SaveChangesAsync();
    }
}
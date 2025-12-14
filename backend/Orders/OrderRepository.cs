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

    public async Task<Order> AddOrderAsync(Order order)
    {
        var uploadedOrder = await _context.Orders.AddAsync(order);
        
        await _context.SaveChangesAsync();

        return uploadedOrder.Entity;
    }

    public async Task UpdateOrderAsync(Order order)
    {
        _context.Orders.Update(order);
        
        await _context.SaveChangesAsync();
    }

    public async Task<Item?> GetItemByIdAsync(int itemId)
    {
        return await _context.Items.FindAsync(itemId);
    }

    public async Task<IEnumerable<Item>> GetItemsByOrderIdAsync(int orderId)
    {
        return await _context.Items.Where(item => item.OrderId == orderId).ToListAsync();
    }

    public async Task<Item> AddItemAsync(Item item)
    {
        var uploadedItem = await _context.Items.AddAsync(item);
        
        await _context.SaveChangesAsync();

        return uploadedItem.Entity;
    }

    public async Task UpdateItemAsync(Item item)
    {
        _context.Update(item);
        
        await _context.SaveChangesAsync();
    }

    public async Task DeleteItemAsync(Item item)
    {
        _context.Items.Remove(item);
        
        await _context.SaveChangesAsync();
    }

    public async Task AddItemVariationSelection(ItemVariationSelection selection)
    {
        await _context.ItemVariationSelections.AddAsync(selection);
        
        await _context.SaveChangesAsync();
    }
}
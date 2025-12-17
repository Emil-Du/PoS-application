
using backend.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.Inventory;

public class InventoryRepository : IInventoryRepository
{
    private readonly AppDbContext _context;

    public InventoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Stock?> GetStockByIdAsync(int stockId)
    {
        return await _context.StockList.FindAsync(stockId);
    }

    public async Task<IEnumerable<Stock?>> GetStockListByLocationIdAsync(int locationId)
    {
        return await _context.StockList.Where(stock => stock.LocationId == locationId).ToListAsync();
    }

    public async Task<Stock> AddStockAsync(Stock stock)
    {
        var returnedStock = await _context.StockList.AddAsync(stock);
        
        await _context.SaveChangesAsync();
        
        return returnedStock.Entity;
    }

    public async Task UpdateStockByIdAsync(Stock stock)
    {
        _context.Update(stock);
        
        await _context.SaveChangesAsync();
    }

    public async Task RemoveStockByIdAsync(Stock stock)
    {
        _context.Remove(stock);
        
        await _context.SaveChangesAsync();
    }
}
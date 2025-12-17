namespace backend.Inventory;

public interface IInventoryRepository
{
    public Task<Stock?> GetStockByIdAsync(int stockId);
    public Task<IEnumerable<Stock?>> GetStockListByLocationIdAsync(int locationId); 
    public Task<Stock> AddStockAsync(Stock stock);
    public Task UpdateStockByIdAsync(Stock stock);
    public Task RemoveStockByIdAsync(Stock stock);
}
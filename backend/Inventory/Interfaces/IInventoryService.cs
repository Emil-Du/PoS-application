namespace backend.Inventory;

public interface IInventoryService
{
    public Task<IEnumerable<StockResponse>> GetStockListByLocationIdAsync(int locationId);
    public Task<StockResponse> AddStockToListAsync(int locationId, StockRequest request);
    public Task UpdateStockByIdAsync(int stockId, StockRequest request);
    public Task RemoveStockByIdAsync(int stockId);
}
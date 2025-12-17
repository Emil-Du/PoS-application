
using backend.Exceptions;
using backend.Locations;

namespace backend.Inventory;

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly ILocationRepository _locationRepository;

    public InventoryService(IInventoryRepository repository, ILocationRepository locationRepository)
    {
        _inventoryRepository = repository;
        _locationRepository = locationRepository;
    }

    public async Task<StockResponse> AddStockToListAsync(int locationId, StockRequest request)
    {
        if (await _locationRepository.GetLocationByIdAsync(locationId) == null) throw new NotFoundException();

        var stock = await _inventoryRepository.AddStockAsync(new Stock()
        {
            LocationId = locationId,
            Name = request.Name,
            Count = request.Count
        });

        return new StockResponse()
        {
            StockId = stock.StockId,
            Name = stock.Name,
            Count = stock.Count  
        };
    }

    public async Task<IEnumerable<StockResponse>> GetStockListByLocationIdAsync(int locationId)
    {
        if (await _locationRepository.GetLocationByIdAsync(locationId) == null) throw new NotFoundException();

        var locationStockList = await _inventoryRepository.GetStockListByLocationIdAsync(locationId) ?? throw new NotFoundException();

        if (locationStockList.Contains(null)) throw new NotFoundException();

        return locationStockList.Select(stock => new StockResponse()
        {
            StockId = stock!.StockId,
            Name = stock!.Name,
            Count = stock!.Count  
        });
    }
    public async Task UpdateStockByIdAsync(int stockId, StockRequest request)
    {
        if (await _inventoryRepository.GetStockByIdAsync(stockId) == null) throw new NotFoundException();

        var stock = await _inventoryRepository.GetStockByIdAsync(stockId) ?? throw new NotFoundException();

        stock.Name = request.Name;
        stock.Count = request.Count;

        await _inventoryRepository.UpdateStockByIdAsync(stock);
    }

    public async Task RemoveStockByIdAsync(int stockId)
    {
        var stock = await _inventoryRepository.GetStockByIdAsync(stockId) ?? throw new NotFoundException();

        await _inventoryRepository.RemoveStockByIdAsync(stock);
    }

}
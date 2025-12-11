using backend.Common;

namespace backend.Orders;

public interface IOrderService
{
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<IEnumerable<Order>> GetOrdersByLocation(int locationId);
    // Added GetOrdersByLocationAsync, because there was no api contract for getting multiple orders at once
    public Task<Order> OpenOrderAsync(OrderRequest request);
    // Removed reservationId, as orders don't have such field in the data model
    public Task UpdateOrderAsync(int orderId, OrderRequest request);
    // Update order response altered to have no return value, because the client already has the contents
    // of the updated order and the method will either throw an exception or succeed
    public Task CloseOrderAsync(int orderId);
    // Close order response altered to have no return value, because the client already has the contents
    // of the closed order and the method will either throw an exception or succeed
    public Task CancelOrderAsync(int orderId);
    // Cancel order response altered to have no return value, because the client already has the contents
    // of the cancelled order and the method will either throw an exception or succeed
    public Task<string> GetReceiptAsync(int orderId);
    // Get receipt response altered to a string, because the receipt will be printed in text anyways,
    // so there is no point in returning a full object

    // I strongly believe this request is entirely pointless, as to have a receipt printed the client
    // should already have an order selected and returned before, which means all of the business logic
    // can be completed clientside without any further requests related to the order
    public Task<IEnumerable<Item>> GetItemsByOrderAsync(int orderId);
    // Get receipt response altered to a list of ItemResponses, because there is no need to send details
    // of the entire order since another method already fullfils that purpose

    // I strongly believe this request is entirely pointless, as to have the items of the order listed
    // the client should already have an order selected and and its items returned before, which means
    // all of the business logic can be completed clientside without any further requests related to
    // the order
    public Task<Item> AddItemAsync(int orderId, ItemRequest item);
    //Add item to order response altered to have no return value, because the client already has the contents
    //of the added order and the method will either throw an exception or succeed
    public Task UpdateItemAsync(int orderId, int itemId, ItemRequest item);
    //Update item by id response altered to have no return value, because the client already has the contents
    //of the added order and the method will either throw an exception or succeed
    public Task RemoveItemAsync(int orderId, int itemId);
    // Remove item by response altered to have no return value, because the client will delete contents after
    // item is succesfully removed from backend and the method will either throw an exception or succeed
}
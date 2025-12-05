using System.Collections.Concurrent;

namespace backend.Orders;

public interface IOrderRepository
{
    Task OpenOrderAsync(int operatorId, int serviceCharge, int discountTotal, Currency currency, int reservationId);
    // Open order response altered to have no return value, because the client already has the contents
    // of the opened order and the method will either throw an exception or succeed 
    Task<OrderResponse> GetOrderByIdAsync(int orderId);
    Task<OrderResponse> UpdateOrderAsync(int orderId, int tip, int serviceCharge, int discountTotal, int status);
    // Update order response altered to have no return value, because the client already has the contents
    // of the updated order and the method will either throw an exception or succeed
    Task CloseOrderAsync(int orderId);
    // Close order response altered to have no return value, because the client already has the contents
    // of the closed order and the method will either throw an exception or succeed
    Task CancelOrderAsync(int orderId);
    // Cancel order response altered to have no return value, because the client already has the contents
    // of the cancelled order and the method will either throw an exception or succeed
    Task<string> GetReceiptAsync(int orderId);
    // Get receipt response altered to a string, because the receipt will be printed in text anyways,
    // so there is no point in returning a full object

    // I strongly believe this request is entirely pointless, as to have a receipt printed the client
    // should already have an order selected and returned before, which means all of the business logic
    // can be completed clientside without any further requests related to the order
    Task<List<ItemResponse>> GetOrderItemsAsync(int orderId);
    // Get receipt response altered to a list of ItemResponses, because there is no need to send details
    // of the entire order since another method already fullfils that purpose

    // I strongly believe this request is entirely pointless, as to have the items of the order listed
    // the client should already have an order selected and and its items returned before, which means
    // all of the business logic can be completed clientside without any further requests related to
    // the order
    Task AddItemToOrderAsync(int orderId, ItemRequest item);
    //Add item to order response altered to have no return value, because the client already has the contents
    //of the added order and the method will either throw an exception or succeed

}
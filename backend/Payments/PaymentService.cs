namespace backend.Payments;

using backend.Orders;
using backend.Exceptions;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderService _orderService;

    public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository, IOrderService orderService)
    {
        _paymentRepository = paymentRepository;
        _orderRepository = orderRepository;
        _orderService = orderService;
    }

    public async Task<PaymentResponse?> GetPaymentByOrderIdAsync(int orderId)
    {
        return await _paymentRepository.GetPaymentByOrderIdAsync(orderId);
    }

    public async Task<PaymentResponse> CreatePaymentAsync(PaymentRequest request)
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);
        if (order == null)
        {
            throw new NotFoundException();
        }

        if(order.Status != OrderStatus.Opened)
        {
            throw new OrderNotOpenException();
        }

        var taxedTotals = await _orderService.GetTaxesForOrderById(order.OrderId);

        decimal discountedTotal = taxedTotals.Total * (1 - order.Discount / 100);

        decimal orderTotal = Math.Round(discountedTotal + order.Tip + order.ServiceCharge,2);

        if(orderTotal != request.Amount)
        {
            throw new ArgumentException("Payment amount does not match order total amount.");
        }

        return await _paymentRepository.CreatePaymentAsync(request);
    }

}

namespace backend.Orders
{
    public class OrderQuery
    {
        public int? OrderId { get; set; }
        public int? LocationId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public string? Search { get; set; }
    }
}
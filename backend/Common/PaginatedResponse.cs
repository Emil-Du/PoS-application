namespace backend.Common;

public class PaginatedResponse<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
}
namespace Shopping.Web.Models.Ordering;

public record PaginatedResult<T>(
    int PageIndex,
    int PageSize,
    int TotalCount,
    IEnumerable<T> Items) where T : class;

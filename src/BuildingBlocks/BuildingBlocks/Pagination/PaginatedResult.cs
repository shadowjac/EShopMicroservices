namespace BuildingBlocks.Pagination;

public class PaginatedResult<TEntity>
    (int pageNumber, int pageSize, int totalCount, IEnumerable<TEntity> items)
    where TEntity : class
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public int TotalCount { get; } = totalCount;
    public IEnumerable<TEntity> Items { get; } = items;
}
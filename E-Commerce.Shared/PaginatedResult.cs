namespace E_Commerce.Shared
{
    public record PaginatedResult<TResult>(int pageIndex, int count, int totalCount, IEnumerable<TResult> data);
}

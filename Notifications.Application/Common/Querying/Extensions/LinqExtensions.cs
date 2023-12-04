using Notifications.Application.Common.Models.Querying;

namespace Notifications.Application.Common.Querying.Extensions;

public static class LinqExtensions
{
    public static IQueryable<TSource> ApplyPagination<TSource>(this IQueryable<TSource> source,
        FilterPagination filterPagination)
    {
        return source.Skip(((int)filterPagination.PageToken - 1) * filterPagination.PageSize)
            .Take(filterPagination.PageSize);
    }
    
    public static IEnumerable<TSource> ApplyPagination<TSource>(this IEnumerable<TSource> source,
        FilterPagination filterPagination)
    {
        return source.Skip(((int)filterPagination.PageToken - 1) * filterPagination.PageSize)
            .Take(filterPagination.PageSize);
    }
}
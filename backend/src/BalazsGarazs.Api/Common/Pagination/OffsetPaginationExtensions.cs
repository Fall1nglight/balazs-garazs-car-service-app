using Microsoft.EntityFrameworkCore;

namespace BalazsGarazs.Api.Common.Pagination;

public static class OffsetPaginationExtensions
{
    public static async Task<PagedResponse<TResponseDto>> ToPagedResponse<TResponseDto>(
        IQueryable<TResponseDto> query,
        PagedRequest request,
        CancellationToken cancellationToken,
        SortDirection sortType = SortDirection.Descending
    )
        where TResponseDto : class, IOffsetItem
    {
        if (sortType == SortDirection.Descending)
        {
            query = query
                .OrderByDescending(item => item.CreatedAt)
                .ThenByDescending(item => item.Id);
        }
        else
        {
            query = query.OrderBy(item => item.CreatedAt).ThenBy(item => item.Id);
        }

        List<TResponseDto> items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize + 1)
            .ToListAsync(cancellationToken);

        int totalCount = await query.CountAsync(cancellationToken);
        int totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);
        bool hasNextPage = items.Count > request.PageSize;

        if (hasNextPage)
            items.RemoveAt(items.Count - 1);

        return new PagedResponse<TResponseDto>(items, totalCount, totalPages, hasNextPage);
    }
}

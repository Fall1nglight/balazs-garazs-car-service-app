namespace BalazsGarazs.Api.Common.Pagination;

public record PagedRequest
{
    public int Page
    {
        get;
        init => field = value < 1 ? 1 : value;
    } = 1;

    public int PageSize
    {
        get;
        init
        {
            if (value < 10)
                field = 10;
            else if (value > 150)
                field = 150;
            else
                field = value;
        }
    } = 10;
}

public record PagedResponse<T>(List<T> Items, int TotalCount, int TotalPages, bool HasNextPage);

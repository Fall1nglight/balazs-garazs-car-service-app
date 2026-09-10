using BalazsGarazs.Api.Common.Pagination;

namespace BalazsGarazs.Api.Data.WorkOrders;

public class WorkOrderItem : IOffsetItem
{
    public Guid Id { get; init; }
    public Guid WorkOrderId { get; set; }
    public WorkOrder WorkOrder { get; set; } = null!;
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int SortOrder { get; set; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
    public uint Version { get; private set; }
}

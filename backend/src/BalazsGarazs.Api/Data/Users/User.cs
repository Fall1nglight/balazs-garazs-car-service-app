using BalazsGarazs.Api.Common.Pagination;
using BalazsGarazs.Api.Data.WorkOrders;
using Microsoft.AspNetCore.Identity;

namespace BalazsGarazs.Api.Data.Users;

public class User : IdentityUser<Guid>, IOffsetItem
{
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public List<WorkOrder> CreatedWorkOrders { get; } = [];
    public required bool IsActive { get; set; } = true;
    public DateTime? DeactivatedAt { get; set; }
    public uint Version { get; private set; }
}

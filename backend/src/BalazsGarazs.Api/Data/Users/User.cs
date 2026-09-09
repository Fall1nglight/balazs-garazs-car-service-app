using BalazsGarazs.Api.Common.Pagination;
using BalazsGarazs.Api.Data.Shared.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BalazsGarazs.Api.Data.Users;

public class User : IdentityUser<Guid>, ISoftDeleteItem, IOffsetItem
{
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}

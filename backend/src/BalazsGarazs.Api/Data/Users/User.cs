using BalazsGarazs.Api.Common.Pagination;
using BalazsGarazs.Api.Data.Shared.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BalazsGarazs.Api.Data.Employees;

public class Employee : IdentityUser<Guid>, ISoftDeletable, IOffsetItem
{
    public string? DisplayName { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public bool IsDeleted { get; set; }
}

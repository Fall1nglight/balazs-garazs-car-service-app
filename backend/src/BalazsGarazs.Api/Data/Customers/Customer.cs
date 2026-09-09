using BalazsGarazs.Api.Common.Pagination;
using BalazsGarazs.Api.Data.Cars;
using BalazsGarazs.Api.Data.Shared.Interfaces;

namespace BalazsGarazs.Api.Data.Customers;

public class Customer : IOffsetItem
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
    public string NormalizedPhoneNumber { get; private set; } = null!;
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Note { get; set; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
    public uint Version { get; private set; }

    // nav proprs
    public List<Car> Cars { get; init; } = [];
    // WorkOrders
}

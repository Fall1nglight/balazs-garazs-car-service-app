using BalazsGarazs.Api.Common.Pagination;
using BalazsGarazs.Api.Data.Appointments;
using BalazsGarazs.Api.Data.Customers;
using BalazsGarazs.Api.Data.Shared.Interfaces;

namespace BalazsGarazs.Api.Data.Cars;

public class Car : IOwnableEntity, IOffsetItem
{
    public Guid Id { get; init; }
    public Guid? CustomerId { get; set; }
    public Customer? Owner { get; set; }
    public required string Plate { get; set; }
    public string NormalizedPlate { get; private set; } = null!;
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public string? Vin { get; set; }
    public string? EngineCode { get; set; }
    public FuelType? Fuel { get; set; }
    public int? ManufactureYear { get; set; }
    public string? Note { get; set; }
    public required DateTime CreatedAt { get; init; }
    public uint Version { get; private set; }

    public List<Appointment> Appointments { get; } = [];
    // public List<WorkOrder> WorkOrders { get; } = [];
}

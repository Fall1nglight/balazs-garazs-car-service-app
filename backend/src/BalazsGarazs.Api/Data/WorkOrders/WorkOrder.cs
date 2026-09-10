using BalazsGarazs.Api.Common.Pagination;
using BalazsGarazs.Api.Data.Appointments;
using BalazsGarazs.Api.Data.Cars;
using BalazsGarazs.Api.Data.Customers;
using BalazsGarazs.Api.Data.Users;

namespace BalazsGarazs.Api.Data.WorkOrders;

public class WorkOrder : IOffsetItem
{
    public Guid Id { get; init; }
    public long SequenceNumber { get; private set; }

    public string Number => $"ML-{SequenceNumber:D6}";

    public Guid CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; } = null!;
    public required string CreatedByUserNameSnapshot { get; init; }

    public Guid AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = null!;
    public required string ComplaintSnapshot { get; init; }
    public required DateTime AppointmentCreatedAtSnapshot { get; init; }
    public required DateTime AppointmentStartsAtSnapshot { get; init; }

    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public required string CustomerNameSnapshot { get; init; }
    public required string CustomerPhoneNumberSnapshot { get; init; }
    public string CustomerNormalizedPhoneNumberSnapshot { get; private set; } = null!;

    public Guid? CarId { get; set; }
    public Car? Car { get; set; }
    public required string CarPlateSnapshot { get; init; }
    public string CarNormalizedPlateSnapshot { get; private set; } = null!;
    public required string CarBrandSnapshot { get; init; }
    public required string CarModelSnapshot { get; init; }
    public string? CarVinSnapshot { get; init; }
    public string? CarEngineCodeSnapshot { get; init; }
    public FuelType? CarFuelSnapshot { get; init; }
    public int? CarManufactureYearSnapshot { get; init; }

    public required DateTime CreatedAt { get; init; }
    public required DateTime FinishedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public List<WorkOrderItem> WorkItems { get; } = [];
    public uint Version { get; private set; }
}

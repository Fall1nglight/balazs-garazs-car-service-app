using BalazsGarazs.Api.Data.Cars;

namespace BalazsGarazs.Api.Data.Appointments;

public class Appointment
{
    public Guid Id { get; init; }
    public required string ContactName { get; set; }
    public required string ContactPhoneNumber { get; set; }
    public string NormalizedContactPhoneNumber { get; private set; } = null!;
    public required string VehicleDescription { get; set; }
    public required string Complaint { get; set; }
    public required DateTime StartsAt { get; set; }
    public required int EstimatedDurationMinutes { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    public Guid? CarId { get; set; }
    public Car? Car { get; set; }
    public DateTime? ConvertedToWorkOrderAt { get; set; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
    public uint Version { get; private set; }

    // public List<AppointmentWorkItem> WorkItems { get; } = [];
    // public WorkOrder? WorkOrder { get; set; }
}

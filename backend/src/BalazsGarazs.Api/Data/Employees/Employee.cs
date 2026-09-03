namespace BalazsGarazs.Api.Data.Employees;

public class Employee
{
    public Guid Id { get; init; }
    public string DisplayName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}

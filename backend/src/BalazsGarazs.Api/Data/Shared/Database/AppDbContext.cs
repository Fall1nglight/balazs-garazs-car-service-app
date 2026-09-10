using BalazsGarazs.Api.Data.Appointments;
using BalazsGarazs.Api.Data.Cars;
using BalazsGarazs.Api.Data.Customers;
using BalazsGarazs.Api.Data.Users;
using BalazsGarazs.Api.Data.WorkOrders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BalazsGarazs.Api.Data.Shared.Database;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<WorkOrderItem> WorkOrderItems => Set<WorkOrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(AppDbContextSchemas.Default);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.ConfigureIdentityTables();
    }
}

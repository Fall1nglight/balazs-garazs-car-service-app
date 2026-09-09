using BalazsGarazs.Api.Data.Cars;
using BalazsGarazs.Api.Data.Customers;
using BalazsGarazs.Api.Data.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BalazsGarazs.Api.Data.Shared.Database;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    // dbsets
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.HasDefaultSchema(AppDbContextSchemas.Default);
        modelBuilder.ConfigureIdentityTables();
    }
}

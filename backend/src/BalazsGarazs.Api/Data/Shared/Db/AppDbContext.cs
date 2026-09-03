using BalazsGarazs.Api.Data.Employees;
using Microsoft.EntityFrameworkCore;

namespace BalazsGarazs.Api.Data.Shared.Db;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    // dbsets

    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.HasDefaultSchema(DbContextSchemas.Default);
    }
}

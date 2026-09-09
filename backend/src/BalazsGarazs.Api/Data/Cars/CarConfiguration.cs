using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalazsGarazs.Api.Data.Cars;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable(table =>
        {
            table.HasCheckConstraint(
                "CK_Cars_NormalizedPlate_NotEmpty",
                """length("NormalizedPlate") > 0"""
            );

            table.HasCheckConstraint(
                "CK_Cars_ManufactureYear",
                """
                "ManufactureYear" IS NULL
                OR "ManufactureYear" BETWEEN 1900 AND 9999
                """
            );
        });

        builder.HasKey(car => car.Id);
        builder.HasIndex(car => car.NormalizedPlate).IsUnique();
        builder.HasIndex(car => car.Vin).IsUnique();
        builder.HasIndex(car => car.Brand);
        builder.HasIndex(car => car.Model);
        builder.HasIndex(car => car.Fuel);
        builder.HasIndex(car => car.EngineCode);
        builder.HasIndex(car => new { car.CreatedAt, car.Id });

        builder.Property(car => car.Plate).IsRequired().HasMaxLength(20);
        builder
            .Property(x => x.NormalizedPlate)
            .HasMaxLength(20)
            .HasComputedColumnSql(
                """regexp_replace(upper("Plate"), '[^A-Z0-9]', '', 'g')""",
                stored: true
            )
            .IsRequired();

        builder.Property(car => car.Brand).HasMaxLength(100).IsRequired();
        builder.Property(car => car.Model).HasMaxLength(100).IsRequired();
        builder.Property(car => car.Vin).HasMaxLength(30);
        builder.Property(car => car.EngineCode).HasMaxLength(30);
        builder.Property(car => car.Fuel).HasConversion<string>().HasMaxLength(20);
        builder.Property(car => car.Note).HasMaxLength(2000);
        builder.Property(car => car.CreatedAt).IsRequired();
        builder.Property(car => car.Version).IsRowVersion();

        builder
            .HasOne(x => x.Owner)
            .WithMany(x => x.Cars)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

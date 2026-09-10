using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalazsGarazs.Api.Data.Cars;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable(x =>
        {
            x.HasCheckConstraint(
                "CK_Cars_NormalizedPlate_NotEmpty",
                """length("NormalizedPlate") > 0"""
            );

            x.HasCheckConstraint(
                "CK_Cars_ManufactureYear",
                """
                "ManufactureYear" IS NULL
                OR "ManufactureYear" BETWEEN 1900 AND 9999
                """
            );
        });

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.NormalizedPlate).IsUnique();
        builder.HasIndex(x => x.Vin);
        builder.HasIndex(x => x.Brand);
        builder.HasIndex(x => x.Model);
        builder.HasIndex(x => x.Fuel);
        builder.HasIndex(x => x.EngineCode);
        builder.HasIndex(x => new { x.CreatedAt, x.Id });

        builder.Property(x => x.Plate).HasMaxLength(20).IsRequired();
        builder
            .Property(x => x.NormalizedPlate)
            .HasMaxLength(20)
            .HasComputedColumnSql(
                """regexp_replace(upper("Plate"), '[^A-Z0-9]', '', 'g')""",
                stored: true
            )
            .IsRequired();

        builder.Property(x => x.Brand).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Model).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Vin).HasMaxLength(30);
        builder.Property(x => x.EngineCode).HasMaxLength(30);
        builder.Property(x => x.Fuel).HasConversion<string>().HasMaxLength(20);
        builder.Property(x => x.Note).HasMaxLength(2000);
        builder.Property(x => x.Version).IsRowVersion();

        builder
            .HasOne(x => x.Customer)
            .WithMany(x => x.Cars)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

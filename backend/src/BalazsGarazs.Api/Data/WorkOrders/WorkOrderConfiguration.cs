using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalazsGarazs.Api.Data.WorkOrders;

public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
{
    public void Configure(EntityTypeBuilder<WorkOrder> builder)
    {
        builder.ToTable(x =>
            x.HasCheckConstraint(
                "CK_WorkOrders_SequenceNumber",
                """
                "SequenceNumber" > 0
                """
            )
        );

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.AppointmentId).IsUnique();
        builder.HasIndex(x => x.SequenceNumber).IsUnique();
        builder.HasIndex(x => x.CreatedByUserNameSnapshot);
        builder.HasIndex(x => x.AppointmentCreatedAtSnapshot);
        builder.HasIndex(x => x.CustomerNameSnapshot);
        builder.HasIndex(x => x.CustomerNormalizedPhoneNumberSnapshot);
        builder.HasIndex(x => x.CarNormalizedPlateSnapshot);
        builder.HasIndex(x => x.CarBrandSnapshot);
        builder.HasIndex(x => x.CarModelSnapshot);
        builder.HasIndex(x => x.CarVinSnapshot);
        builder.HasIndex(x => x.CarFuelSnapshot);
        builder.HasIndex(x => x.CarEngineCodeSnapshot);
        builder.HasIndex(x => new { x.CreatedAt, x.Id });

        builder.Property(x => x.SequenceNumber).UseIdentityAlwaysColumn();
        builder.Ignore(x => x.Number);
        builder.Property(x => x.CreatedByUserNameSnapshot).HasMaxLength(100).IsRequired();
        builder.Property(x => x.ComplaintSnapshot).HasMaxLength(4000).IsRequired();
        builder.Property(x => x.CustomerNameSnapshot).HasMaxLength(100).IsRequired();
        builder.Property(x => x.CustomerPhoneNumberSnapshot).HasMaxLength(40).IsRequired();
        builder
            .Property(x => x.CustomerNormalizedPhoneNumberSnapshot)
            .HasMaxLength(40)
            .HasComputedColumnSql(
                """regexp_replace("CustomerPhoneNumberSnapshot", '[^0-9]', '', 'g')""",
                stored: true
            )
            .IsRequired();
        builder.Property(x => x.CarPlateSnapshot).HasMaxLength(20).IsRequired();
        builder
            .Property(x => x.CarNormalizedPlateSnapshot)
            .HasMaxLength(20)
            .HasComputedColumnSql(
                """regexp_replace(upper("CarPlateSnapshot"), '[^A-Z0-9]', '', 'g')""",
                stored: true
            )
            .IsRequired();

        builder.Property(x => x.CarBrandSnapshot).HasMaxLength(100).IsRequired();
        builder.Property(x => x.CarModelSnapshot).HasMaxLength(100).IsRequired();
        builder.Property(x => x.CarVinSnapshot).HasMaxLength(30);
        builder.Property(x => x.CarEngineCodeSnapshot).HasMaxLength(30);
        builder.Property(x => x.CarFuelSnapshot).HasConversion<string>().HasMaxLength(20);
        builder.Property(x => x.Version).IsRowVersion();

        builder
            .HasOne(x => x.Appointment)
            .WithOne(x => x.WorkOrder)
            .HasForeignKey<WorkOrder>(x => x.AppointmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Customer)
            .WithMany(x => x.WorkOrders)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(x => x.Car)
            .WithMany(x => x.WorkOrders)
            .HasForeignKey(x => x.CarId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(x => x.CreatedByUser)
            .WithMany(x => x.CreatedWorkOrders)
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

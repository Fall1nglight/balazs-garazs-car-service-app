using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalazsGarazs.Api.Data.Appointments;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable(x =>
        {
            x.HasCheckConstraint(
                "CK_Appointments_PositiveDuration",
                """
                "EstimatedDurationMinutes" > 0
                """
            );

            x.HasCheckConstraint(
                "CK_Appointments_Status",
                """
                "Status" IN ('Scheduled', 'Cancelled', 'ConvertedToWorkOrder')
                """
            );

            x.HasCheckConstraint(
                "CK_Appointments_ConversionState",
                """
                ("Status" = 'ConvertedToWorkOrder' AND "ConvertedToWorkOrderAt" IS NOT NULL)
                OR ("Status" <> 'ConvertedToWorkOrder' AND "ConvertedToWorkOrderAt" IS NULL)
                """
            );
        });

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.ContactName);
        builder.HasIndex(x => x.NormalizedContactPhoneNumber);
        builder.HasIndex(x => x.VehicleDescription);
        builder.HasIndex(x => x.StartsAt);
        builder.HasIndex(x => new { x.CarId, x.Status });

        builder.Property(x => x.ContactName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.ContactPhoneNumber).HasMaxLength(40).IsRequired();
        builder
            .Property(x => x.NormalizedContactPhoneNumber)
            .HasMaxLength(40)
            .HasComputedColumnSql(
                """regexp_replace("ContactPhoneNumber", '[^0-9]', '', 'g')""",
                stored: true
            )
            .IsRequired();

        builder.Property(x => x.VehicleDescription).HasMaxLength(300).IsRequired();
        builder.Property(x => x.Complaint).HasMaxLength(4000).IsRequired();
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(x => x.Version).IsRowVersion();

        builder
            .HasOne(x => x.Car)
            .WithMany(x => x.Appointments)
            .HasForeignKey(x => x.CarId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

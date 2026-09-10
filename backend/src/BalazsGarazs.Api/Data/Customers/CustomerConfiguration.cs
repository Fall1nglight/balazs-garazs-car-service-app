using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalazsGarazs.Api.Data.Customers;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.NormalizedPhoneNumber);
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Email);
        builder.HasIndex(x => x.Address);
        builder.HasIndex(x => new { x.CreatedAt, x.Id });

        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.PhoneNumber).HasMaxLength(40).IsRequired();
        builder
            .Property(x => x.NormalizedPhoneNumber)
            .HasMaxLength(40)
            .HasComputedColumnSql(
                """regexp_replace("PhoneNumber", '[^0-9]', '', 'g')""",
                stored: true
            )
            .IsRequired();

        builder.Property(x => x.Address).HasMaxLength(300);
        builder.Property(x => x.Email).HasMaxLength(254);
        builder.Property(x => x.Note).HasMaxLength(2000);
        builder.Property(x => x.Version).IsRowVersion();
    }
}

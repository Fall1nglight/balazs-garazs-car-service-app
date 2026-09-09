using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalazsGarazs.Api.Data.Customers;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(customer => customer.Id);
        builder.HasIndex(customer => customer.NormalizedPhoneNumber);
        builder.HasIndex(customer => customer.Name);
        builder.HasIndex(customer => customer.Email);
        builder.HasIndex(customer => customer.Address);
        builder.HasIndex(customer => new { customer.CreatedAt, customer.Id });

        builder.Property(customer => customer.Name).HasMaxLength(100).IsRequired();
        builder.Property(customer => customer.PhoneNumber).HasMaxLength(40).IsRequired();
        builder
            .Property(x => x.NormalizedPhoneNumber)
            .HasMaxLength(40)
            .HasComputedColumnSql(
                """regexp_replace("PhoneNumber", '[^0-9]', '', 'g')""",
                stored: true
            )
            .IsRequired();

        builder.Property(customer => customer.Address).HasMaxLength(300);
        builder.Property(customer => customer.Email).HasMaxLength(100);
        builder.Property(customer => customer.Note).HasMaxLength(2000);
        builder.Property(customer => customer.CreatedAt).IsRequired();
        builder.Property(customer => customer.Version).IsRowVersion();
    }
}

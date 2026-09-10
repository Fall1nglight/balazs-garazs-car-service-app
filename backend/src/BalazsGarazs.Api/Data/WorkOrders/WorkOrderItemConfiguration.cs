using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalazsGarazs.Api.Data.WorkOrders;

public class WorkOrderItemConfiguration : IEntityTypeConfiguration<WorkOrderItem>
{
    public void Configure(EntityTypeBuilder<WorkOrderItem> builder)
    {
        builder.ToTable(x =>
            x.HasCheckConstraint(
                "CK_WorkOrderItems_SortOrder",
                """
                "SortOrder" >= 0
                """
            )
        );

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => new { x.WorkOrderId, x.SortOrder });
        builder.HasIndex(x => new { x.CreatedAt, x.Id });

        builder.Property(x => x.Name).HasMaxLength(500).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(3000);
        builder.Property(x => x.Version).IsRowVersion();

        builder
            .HasOne(x => x.WorkOrder)
            .WithMany(x => x.WorkItems)
            .HasForeignKey(x => x.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

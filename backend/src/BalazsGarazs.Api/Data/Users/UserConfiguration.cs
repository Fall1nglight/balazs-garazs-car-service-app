using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalazsGarazs.Api.Data.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.CreatedAt).IsRequired();
        builder.Property(user => user.IsDeleted).HasDefaultValue(false);
    }
}

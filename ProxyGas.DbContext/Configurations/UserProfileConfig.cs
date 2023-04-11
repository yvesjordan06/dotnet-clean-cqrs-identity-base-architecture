using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProxyGas.Domain.Aggregates.UserProfiles;

namespace ProxyGas.DbContext.Configurations;

internal class UserProfileConfig: IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.OwnsOne(up => up.BasicInfo);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProxyGas.Domain.Aggregates.UserProfiles;

namespace ProxyGas.DbContext.Configurations;

internal class BasicInfoConfig : IEntityTypeConfiguration<BasicInfo>
{
    public void Configure(EntityTypeBuilder<BasicInfo> builder)
    {
       
    }
}
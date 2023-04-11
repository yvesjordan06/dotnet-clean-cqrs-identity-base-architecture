using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProxyGas.DbContext.Configurations;

internal class IdentityUserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
    {
        //builder.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
        builder.HasNoKey();
    }
}
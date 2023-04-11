using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProxyGas.DbContext.Configurations;
using ProxyGas.Domain.Aggregates.Products;
using ProxyGas.Domain.Aggregates.UserProfiles;

namespace ProxyGas.DbContext;
using Microsoft.EntityFrameworkCore;

public class ProxyGasDbContext : IdentityDbContext
{


    public ProxyGasDbContext(DbContextOptions<ProxyGasDbContext> options) : base(options)
    {
    }

    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Product> Products { get; set; }
    
    
    //On Model Creating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserProfileConfig());
        //modelBuilder.ApplyConfiguration(new IdentityUserLoginConfig());
        //modelBuilder.ApplyConfiguration(new IdentityUserRoleConfig());
        //modelBuilder.ApplyConfiguration(new IdentityUserTokenConfig());

    }
    
}
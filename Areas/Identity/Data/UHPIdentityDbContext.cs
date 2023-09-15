using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserHelpPageTemplate.Areas.Identity.Data;

public class UHPIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public UHPIdentityDbContext(DbContextOptions<UHPIdentityDbContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}

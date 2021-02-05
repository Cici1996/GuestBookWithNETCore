using GuestBook.BusinessObjects.Identities;
using GuestBook.DAL.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace GuestBook.DAL
{
    public class CoreDbContext : IdentityDbContext<AppUser, AppRole, Guid, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {
        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new AppRoleConfiguration());
            builder.ApplyConfiguration(new AppUserRoleConfiguration());
            builder.ApplyConfiguration(new AppUserClaimConfiguration());
            builder.ApplyConfiguration(new AppUserLoginConfiguration());
            builder.ApplyConfiguration(new AppUserTokenConfiguration());
            builder.ApplyConfiguration(new AppRoleClaimConfiguration());
        }
    }
}
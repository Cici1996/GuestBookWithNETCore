using GuestBook.BusinessObjects.Identities;
using GuestBook.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GuestBook.WebApp.Configurations
{
    public static class IdentityConfigurations
    {
        public static void ConfigureAuthenticationAuthorizations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.Lockout.AllowedForNewUsers = true;

                int.TryParse(configuration["Lockout:MaxFailedAccessAttempts"], out int maxFailedAccessAttempts);

                int.TryParse(configuration["Lockout:DurationInMinutes"], out int lockoutInMinutes);

                options.Lockout.MaxFailedAccessAttempts = maxFailedAccessAttempts;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(lockoutInMinutes);
            })
                .AddUserManager<UserManager<AppUser>>()
                .AddEntityFrameworkStores<CoreDbContext>()
                .AddDefaultTokenProviders()
                .AddRoles<AppRole>()
                .AddRoleManager<RoleManager<AppRole>>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.AccessDeniedPath = "/Account/AccessDenied";
            });
        }
    }
}
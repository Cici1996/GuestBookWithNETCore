using GuestBook.WebApp.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace GuestBook.WebApp.Configurations
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<GlobalDynamicAuthorizeAttribute>();
        }
    }
}
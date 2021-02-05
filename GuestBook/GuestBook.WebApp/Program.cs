using DbUp;
using GuestBook.BusinessObjects.Identities;
using GuestBook.WebApp.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.IO;

namespace GuestBook.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            try
            {
                var config = host.Services.GetRequiredService<IConfiguration>();
                var env = host.Services.GetRequiredService<IWebHostEnvironment>();
                if (env.IsDevelopment())
                {
                    var connectionString = config.GetConnectionString("DefaultConnection");
                    string scriptFolder = @"..\..\Scripts\";
                    if (Directory.Exists(scriptFolder))
                    {
                        EnsureDatabase.For.SqlDatabase(connectionString);
                        var upgrader =
                        DeployChanges.To
                            .SqlDatabase(connectionString)
                            .WithScriptsFromFileSystem(@"../../Scripts/")
                            .WithTransactionPerScript()
                            .LogToConsole()
                            .Build();
                        var result = upgrader.PerformUpgrade();

                        if (!result.Successful)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(result.Error);
                            Console.ResetColor();
                        }
                    }
                }

                using (var scope = host.Services.CreateScope())
                {
                    try
                    {
                        var serviceProvider = scope.ServiceProvider;
                        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                        var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();

                        SeedHelper.UseSeed(userManager, roleManager);
                    }
                    catch (Exception ex) { Trace.WriteLine(ex); }
                }
                Console.WriteLine("Starting host...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Host terminated unexpectedly.\n{ex}");
            }
            finally
            {
                host.Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
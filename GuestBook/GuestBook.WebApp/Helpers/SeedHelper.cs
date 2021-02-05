using GuestBook.BusinessObjects.Identities;
using GuestBook.Core.Constants;
using GuestBook.DAL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace GuestBook.WebApp.Helpers
{
    public class SeedHelper
    {
        public static void UseSeed(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedRoles(RoleManager<AppRole> roleManager)
        {
            try
            {
                List<AppRole> appRoles = new List<AppRole>
                {
                    new AppRole
                    {
                        Name = RoleListConstants.Administrator,
                        Description = "Highest user application role",
                        IsActive = true,
                        CreatedBy = "System",
                        UpdatedBy = "System",
                    },
                    new AppRole
                    {
                        Name = RoleListConstants.CommonUser,
                        Description = "Normal user application role",
                        IsActive = true,
                        CreatedBy = "System",
                        UpdatedBy = "System",
                    }
                };
                foreach (var role in appRoles)
                {
                    if (!roleManager.RoleExistsAsync(role.Name).Result)
                    {
                        try
                        {
                            roleManager.CreateAsync(role).Wait();
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        private static void SeedUsers(UserManager<AppUser> userManager)
        {
            List<Tuple<string, string, string, string, string>> users = new List<Tuple<string, string, string, string, string>>()
            {
                new Tuple<string, string, string, string, string>(
                        "admin@admin.com",                    //Email
                        "admin",                            //Admin
                        AuthenticationConstants.DefaultPassword, //Password
                        "Administrator",                    //Full name
                        RoleListConstants.Administrator     //Role name
                    )
            };
            foreach (var user in users)
            {
                try
                {
                    if (userManager.FindByNameAsync(user.Item1).Result == null)
                    {
                        AppUser appUser = new AppUser
                        {
                            UserName = user.Item2,
                            Email = user.Item1,
                            FullName = user.Item4,
                            IsActive = true,
                            CreatedDate = DateTime.Now,
                            CreatedBy = "System",
                            UpdatedDate = DateTime.Now,
                            UpdatedBy = "System"
                        };
                        var createUserResult = userManager.CreateAsync(appUser, user.Item3).Result;
                        if (createUserResult.Succeeded)
                        {
                            userManager.AddToRoleAsync(appUser, user.Item5).Wait();
                        }
                        var claimResult = userManager.AddClaimAsync(appUser, new Claim(CustomClaimNames.Id, appUser.Id.ToString())).Result;
                        claimResult = userManager.AddClaimAsync(appUser, new Claim(CustomClaimNames.Name, appUser.FullName)).Result;
                        claimResult = userManager.AddClaimAsync(appUser, new Claim(CustomClaimNames.Email, appUser.Email)).Result;
                        claimResult = userManager.AddClaimAsync(appUser, new Claim(CustomClaimNames.UserName, appUser.UserName)).Result;
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                }
            }
        }
    }
}
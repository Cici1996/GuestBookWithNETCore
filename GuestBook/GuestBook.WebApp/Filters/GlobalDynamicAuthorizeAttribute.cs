using GuestBook.BusinessObjects.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace GuestBook.WebApp.Filters
{
    public class GlobalDynamicAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly UserManager<AppUser> _userManager;

        public GlobalDynamicAuthorizeAttribute(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                bool isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                if (isAjax)
                {
                    context.Result = new StatusCodeResult(403);
                }
                else
                {
                    string url = $"{context.HttpContext.Request.Path}{(context.HttpContext.Request.QueryString.HasValue ? context.HttpContext.Request.QueryString.Value : "")}";
                    context.Result = new RedirectResult($"/Account/Login?returnUrl={url}");
                }
            }
        }
    }
}
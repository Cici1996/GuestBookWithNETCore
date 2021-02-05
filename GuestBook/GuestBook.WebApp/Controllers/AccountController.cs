using GuestBook.BusinessObjects.Identities;
using GuestBook.Core.Constants;
using GuestBook.WebApp.Models.Accounts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GuestBook.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            if (returnUrl == "/")
                return RedirectToAction("Login");

            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        ModelState.AddModelError("", ModelMessageConstants.UserInactive);
                    }
                    else
                    {
                        var signInResult = await _signInManager
                            .PasswordSignInAsync(user: user, password: model.Password, isPersistent: true, lockoutOnFailure: true);
                        if (signInResult.Succeeded)
                        {
                            if (Url.IsLocalUrl(model.ReturnUrl))
                            {
                                return Redirect(model.ReturnUrl);
                            }

                            return RedirectToAction("Index", "Home");
                        }
                        else if (signInResult.IsLockedOut)
                        {
                            ModelState.AddModelError("", string.Format(ModelMessageConstants.AccountLockoutMessage, _configuration["Lockout:DurationInMinutes"]));
                        }
                        else
                        {
                            ModelState.AddModelError("", ModelMessageConstants.UserNameOrPasswordIsInvalid);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", ModelMessageConstants.UserNameOrPasswordIsInvalid);
                }
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
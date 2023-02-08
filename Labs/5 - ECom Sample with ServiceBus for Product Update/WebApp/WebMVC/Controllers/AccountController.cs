using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Controllers
{
    public class AccountController : Controller
    {

        string identityServerUrl;
        public AccountController(IConfiguration config)
        {
            identityServerUrl = config["IdentityServerUrl"];
        }
        public IActionResult Login()
        {
            return Redirect(identityServerUrl + "/identity/account/login");
        }
        [Authorize()]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
            var homeUrl = Url.Action("Index", "Home");
            //return Redirect(homeUrl);
            return new SignOutResult("oidc", new AuthenticationProperties { RedirectUri = homeUrl });
        }

    }
}

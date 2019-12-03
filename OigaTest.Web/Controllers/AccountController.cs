using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OigaTest.Models;
using OigaTest.Models.Interfaces;

namespace OigaTest.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAppService appService;

        private Tenant tenant;

        public AccountController(Tenant tenant, IAppService appService)
        {
            this.tenant = tenant;
            this.appService = appService;
        }
        public IActionResult Login()
        {
            ViewBag.TenantName = tenant.Name;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            ViewBag.TenantName = tenant.Name;
            if (ModelState.IsValid)
            {
                var user = await appService.IsUserValid(login.Username, login.Password, tenant);
                if(user!=null)
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }          
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            ViewBag.TenantName = tenant.Name;
            return View("Login");
        }
    }
}
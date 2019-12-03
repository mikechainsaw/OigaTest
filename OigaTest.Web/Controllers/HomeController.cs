using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OigaTest.Models;
using OigaTest.Models.Interfaces;
using OigaTest.Web.Models;

namespace OigaTest.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IAppService appService;

        private Tenant tenant;
        public HomeController(Tenant tenant, IAppService appService)
        {
            this.tenant = tenant;
            this.appService = appService;
        }

        public IActionResult Index()
        {
            ViewBag.TenantName = tenant.Name;
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Users()
        {
            var list = await appService.GetUsers(tenant);
            ViewBag.TenantName = tenant.Name;
            ViewBag.IsAdmin = UserIsAdmin();

            return View(list);
        }

        public async Task<IActionResult> CreateUser(AppUser appUSer)
        {
            if (ModelState.IsValid)
            {
                appUSer.Tenant = tenant;
                var asd = await appService.CreateUser(appUSer);
                return RedirectToAction("Users");
            }
            return View("Create", appUSer);

        }
        public async Task<IActionResult> EditUser(AppUser appUSer)
        {
            appUSer.Tenant = tenant;
            var asd = await appService.UpdateUser(appUSer);
            return RedirectToAction("Users");
        }

        //[Authorize]
        public IActionResult Create()
        {
            ViewBag.TenantName = tenant.Name;
            ViewBag.CanChangeRole = true;

            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.TenantName = tenant.Name;
            var user = await appService.GetUser(id);
            ViewBag.IsAdmin = UserIsAdmin();
            return View("Create", user);
        }

        private bool UserIsAdmin()
        {
            EUserRole role;
            var claim = User?.Claims?.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value;
            if (claim != null && Enum.TryParse(claim, out role))           
                return role == EUserRole.Admin;
            return false;
        }

        [HttpPost, ActionName("Delete")]       
        public async Task<IActionResult> Delete(int id)
        {
            var removed=await appService.DeleteUser(id);
            return RedirectToAction("Users");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

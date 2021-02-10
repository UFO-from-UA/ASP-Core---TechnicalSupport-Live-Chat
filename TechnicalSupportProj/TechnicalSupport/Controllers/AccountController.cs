using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TechnicalSupport.Data;
using TechnicalSupport.Models;

namespace TechnicalSupport.Controllers
{

    public class AccountController : Controller
    {
        private UserContext _db;

        public AccountController(UserContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //return RedirectToAction( nameof(Login) );
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login( AuthenticationModel model)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Phone == model.UserString || x.Email == model.UserString);


            if(user!= null)
            {
                await Authenticate(user);

                //Redirect to Index method of HomeController
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ModelState.AddModelError("Data", "Wrong data");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType , user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType , user.GetRoleName())
            };

            var identity = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }
    }
}

using ClaimBasedAuthentication.Models;
using ClaimBasedAuthentication.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClaimBasedAuthentication.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var user = new FakeUsers().IsValid(userLogin.UserName, userLogin.Password);
            if (user != null)
            {
                List<Claim> claims = new List<Claim>
               {
                   new Claim(ClaimTypes.Name, user.UserName),
                   new Claim(ClaimTypes.Role, user.Role)
               };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return Redirect("/");
            }

            return BadRequest(new { message = "Hatalı kullanıcı adı veya şifre" });

        }

        public IActionResult Erisim()
        {
            return View();
        }
    }
}

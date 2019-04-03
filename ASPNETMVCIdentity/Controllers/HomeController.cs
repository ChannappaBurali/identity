using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPNETMVCIdentity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using ASPNETMVCIdentity.AppStart;
using Microsoft.Owin.Security;
using ASPNETMVCIdentity.App_Start;

namespace ASPNETMVCIdentity.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginModel)
        {
            var store = new IdentityUserStore(new LoginUserDbContext());
            var userManager = new IdentityUserManager(store);
            var user = userManager.Find(loginModel.UserName, loginModel.Password);
            if (user != null)
            {
                var authenticationManager = Request.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user,
                DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);
                ViewBag.Message = string.Format("User {0} Login Successfully", user.UserName);
                return View();

            }
            else
            {
                ViewBag.Message = "Invalid login or password";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel registerModel)
        {

            var store = new IdentityUserStore(new LoginUserDbContext());
            var userManager = new IdentityUserManager(store);

            string result = string.Empty;
            var user = new LoginUser()
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email,
                PhoneNumber = registerModel.PhoneNumber
            };
            IdentityResult identityResult = userManager.Create(user, registerModel.Password);
            if (identityResult.Succeeded)
            {
                ViewBag.Message = string.Format("User {0} Successfully Created", user.UserName);
                var authenticationManager = Request.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user,
                DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);
                return View("Login");
            }
            else
            {
                ViewBag.Message = identityResult.Errors.FirstOrDefault();
            }
            return View();
        }
        public ActionResult LogOut()
        {
            var authenticationManager = Request.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            ViewBag.Message = "Logout Successfully";
            return View("Login");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Concrete;
using System.Net;
using System.Web.Security;

namespace SportsStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider authProvider;
        // GET: Account

        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                using (EFDbContext dbc = new EFDbContext())
                {
                    dbc.UserAccount.Add(userAccount);
                    dbc.SaveChanges();
                    TempData["message"] = string.Format("{0}  {1} is successfully registered! Please Sign in", userAccount.FirstName, userAccount.LastName);
                }
                ModelState.Clear();
            }
            return RedirectToAction("List","Product");
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password) && model.UserName == "admin")
                {
                    return Redirect(Url.Action("Index", "Admin"));
                }
                    
                using (EFDbContext dbc = new EFDbContext())
                {
                    var user = dbc.UserAccount.Where(u => u.Username == model.UserName && u.Password == model.Password).FirstOrDefault();
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.Username, false);
                        return RedirectToAction("List","Product");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect username or password");
                        return View();
                    }
                }
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("List", "Product");
        }


    }
}
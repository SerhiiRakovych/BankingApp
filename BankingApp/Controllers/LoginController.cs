using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankingApp.Models;
using BankingApp.Methods;

namespace BankingApp.Controllers
{
    public class LoginController : Controller
    {

        IRepository repo;

        public LoginController(IRepository r)
        {
            repo = r;
        }

        public LoginController()
        {
            repo = new ClientRepository();
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Client client = repo.GetClientByLogin(model.UserName);
            if (client == null)
            {
                ViewData["LoginResult"] = "Неверное имя пользователя или пароль";
                return View("Index");
            }

            string CurrentPassword = CustomMethods.CalculateMD5Hash(model.Password, client.Salt);
            if(client.Password != CurrentPassword)
            {
                ViewData["LoginResult"] = "Неверное имя пользователя или пароль";
                return View("Index");
            }

            if (client.isAdmin)
            {
                HttpCookie AuthCookie = new HttpCookie("AuthCookie");
                string AuthStr = CustomMethods.CalculateMD5Hash(HttpContext.Session.SessionID, client.UserName);
                AuthCookie.Value = AuthStr;
                AuthCookie.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(AuthCookie);
                return Redirect("/UsersManagement/Index");
            }
            else
            {
                return Redirect("/UsersManagement/Index");
            }
            
        }
    }
}
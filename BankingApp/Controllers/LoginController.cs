using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankingApp.Models;

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
        public ActionResult UserLogin(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Client client = repo.GetClientByLogin(model.UserName);
            if (client == null)
            {
                return View();
            }


            return View();
        }
    }
}
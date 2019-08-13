using System;
using System.Net;
using System.Web.Mvc;
using BankingApp.Models;
using BankingApp.Methods;

namespace BankingApp.Controllers
{
    public class UsersManagementController : Controller
    {
        IRepository repo;

        public UsersManagementController(IRepository r)
        {
            repo = r;
        }

        public UsersManagementController()
        {
            repo = new ClientRepository();
        }


        public ActionResult Index()
        {
            return View(repo.GetClientsList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = repo.GetClient(id ?? 0);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,MiddleName,DOB, UserName, Password, ConfirmPassword")] Client client)
        {
            client.Salt = Guid.NewGuid().ToString("N");
            if (ModelState.IsValid)
            {
                string HashedPassword = CustomMethods.CalculateMD5Hash(client.Password, client.Salt);
                client.Password = HashedPassword;
                client.ConfirmPassword = HashedPassword;

                repo.Create(client);
                repo.Save();

                return RedirectToAction("Index");
            }

            return View(client);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = repo.GetClient(id ?? 0);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,MiddleName,DOB")] Client client)
        {
            if (ModelState.IsValid)
            {
                repo.Update(client);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = repo.GetClient(id ?? 0);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = repo.GetClient(id);
            repo.Delete(id);
            repo.Save();
            return RedirectToAction("Index");
        }

    }
}

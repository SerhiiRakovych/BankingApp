using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankingApp.Models;
using System.Security.Cryptography;
using System.Text;

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
                string HashedPassword = CalculateMD5Hash(client.Password, client.Salt);
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

        public string CalculateMD5Hash(string password, string salt)
        {
            string input = salt + password;

            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

    }
}

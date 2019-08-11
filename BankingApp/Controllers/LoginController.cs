using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            return View(repo.GetClientsList());
        }

        // GET: Login/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = repo.GetClient(id??0);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,MiddleName,DOB")] Client client)
        {
            if (ModelState.IsValid)
            {
                repo.Create(client);
                repo.Save();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Login/Edit/5
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

        // POST: Login/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Login/Delete/5
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

        // POST: Login/Delete/5
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

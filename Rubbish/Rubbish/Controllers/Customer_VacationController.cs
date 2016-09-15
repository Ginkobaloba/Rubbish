using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rubbish.Models;

namespace Rubbish.Controllers
{
    public class Customer_VacationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customer_Vacation
        public ActionResult Index()
        {
            var customer_Vacation = db.Customer_Vacation.Include(c => c.Customer).Include(c => c.Vacation);
            return View(customer_Vacation.ToList());
        }

        // GET: Customer_Vacation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Vacation customer_Vacation = db.Customer_Vacation.Find(id);
            if (customer_Vacation == null)
            {
                return HttpNotFound();
            }
            return View(customer_Vacation);
        }

        // GET: Customer_Vacation/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "UserID");
            ViewBag.VacationID = new SelectList(db.Vacations, "ID", "ID");
            return View();
        }

        // POST: Customer_Vacation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,VacationID,CustomerID")] Customer_Vacation customer_Vacation)
        {
            if (ModelState.IsValid)
            {
                db.Customer_Vacation.Add(customer_Vacation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "UserID", customer_Vacation.CustomerID);
            ViewBag.VacationID = new SelectList(db.Vacations, "ID", "ID", customer_Vacation.VacationID);
            return View(customer_Vacation);
        }

        // GET: Customer_Vacation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Vacation customer_Vacation = db.Customer_Vacation.Find(id);
            if (customer_Vacation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "UserID", customer_Vacation.CustomerID);
            ViewBag.VacationID = new SelectList(db.Vacations, "ID", "ID", customer_Vacation.VacationID);
            return View(customer_Vacation);
        }

        // POST: Customer_Vacation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,VacationID,CustomerID")] Customer_Vacation customer_Vacation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_Vacation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "UserID", customer_Vacation.CustomerID);
            ViewBag.VacationID = new SelectList(db.Vacations, "ID", "ID", customer_Vacation.VacationID);
            return View(customer_Vacation);
        }

        // GET: Customer_Vacation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Vacation customer_Vacation = db.Customer_Vacation.Find(id);
            if (customer_Vacation == null)
            {
                return HttpNotFound();
            }
            return View(customer_Vacation);
        }

        // POST: Customer_Vacation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer_Vacation customer_Vacation = db.Customer_Vacation.Find(id);
            db.Customer_Vacation.Remove(customer_Vacation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

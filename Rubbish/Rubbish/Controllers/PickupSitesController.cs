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
    public class PickupSitesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PickupSites
        public ActionResult Index()
        {
            var pickupSites = db.PickupSites.Include(p => p.Address).Include(p => p.Customer);
            return View(pickupSites.ToList());
        }

        // GET: PickupSites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupSite pickupSite = db.PickupSites.Find(id);
            if (pickupSite == null)
            {
                return HttpNotFound();
            }
            return View(pickupSite);
        }

        // GET: PickupSites/Create
        public ActionResult Create()
        {
            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "StreetNumber");
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "UserID");
            return View();
        }

        // POST: PickupSites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DayOfWeekID,IsActive,CustomerID,RouteNumber,AddressID")] PickupSite pickupSite)
        {
            if (ModelState.IsValid)
            {
                db.PickupSites.Add(pickupSite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "StreetNumber", pickupSite.AddressID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "UserID", pickupSite.CustomerID);
            return View(pickupSite);
        }

        // GET: PickupSites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupSite pickupSite = db.PickupSites.Find(id);
            if (pickupSite == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "StreetNumber", pickupSite.AddressID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "UserID", pickupSite.CustomerID);
            return View(pickupSite);
        }

        // POST: PickupSites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DayOfWeekID,IsActive,CustomerID,RouteNumber,AddressID")] PickupSite pickupSite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickupSite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "StreetNumber", pickupSite.AddressID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "UserID", pickupSite.CustomerID);
            return View(pickupSite);
        }

        // GET: PickupSites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupSite pickupSite = db.PickupSites.Find(id);
            if (pickupSite == null)
            {
                return HttpNotFound();
            }
            return View(pickupSite);
        }

        // POST: PickupSites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PickupSite pickupSite = db.PickupSites.Find(id);
            db.PickupSites.Remove(pickupSite);
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

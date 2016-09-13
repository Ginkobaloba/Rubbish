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
    public class Vacation_PickupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vacation_Pickup
        public ActionResult Index()
        {
            var vacation_Pickup = db.Vacations_Pickups.Include(v => v.PickupSite).Include(v => v.Vacation);
            return View(vacation_Pickup.ToList());
        }

        // GET: Vacation_Pickup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacation_Pickup vacation_Pickup = db.Vacations_Pickups.Find(id);
            if (vacation_Pickup == null)
            {
                return HttpNotFound();
            }
            return View(vacation_Pickup);
        }

        // GET: Vacation_Pickup/Create
        public ActionResult Create()
        {
            ViewBag.PickupID = new SelectList(db.PickupSites, "ID", "ID");
            ViewBag.VacationID = new SelectList(db.Vacations, "ID", "ID");
            return View();
        }

        // POST: Vacation_Pickup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,VacationID,PickupID")] Vacation_Pickup vacation_Pickup)
        {
            if (ModelState.IsValid)
            {
                db.Vacations_Pickups.Add(vacation_Pickup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PickupID = new SelectList(db.PickupSites, "ID", "ID", vacation_Pickup.PickupID);
            ViewBag.VacationID = new SelectList(db.Vacations, "ID", "ID", vacation_Pickup.VacationID);
            return View(vacation_Pickup);
        }

        // GET: Vacation_Pickup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacation_Pickup vacation_Pickup = db.Vacations_Pickups.Find(id);
            if (vacation_Pickup == null)
            {
                return HttpNotFound();
            }
            ViewBag.PickupID = new SelectList(db.PickupSites, "ID", "ID", vacation_Pickup.PickupID);
            ViewBag.VacationID = new SelectList(db.Vacations, "ID", "ID", vacation_Pickup.VacationID);
            return View(vacation_Pickup);
        }

        // POST: Vacation_Pickup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,VacationID,PickupID")] Vacation_Pickup vacation_Pickup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vacation_Pickup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PickupID = new SelectList(db.PickupSites, "ID", "ID", vacation_Pickup.PickupID);
            ViewBag.VacationID = new SelectList(db.Vacations, "ID", "ID", vacation_Pickup.VacationID);
            return View(vacation_Pickup);
        }

        // GET: Vacation_Pickup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacation_Pickup vacation_Pickup = db.Vacations_Pickups.Find(id);
            if (vacation_Pickup == null)
            {
                return HttpNotFound();
            }
            return View(vacation_Pickup);
        }

        // POST: Vacation_Pickup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vacation_Pickup vacation_Pickup = db.Vacations_Pickups.Find(id);
            db.Vacations_Pickups.Remove(vacation_Pickup);
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

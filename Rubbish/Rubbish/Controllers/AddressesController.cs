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
    public class AddressesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Addresses
        public ActionResult Index()
        {
            var addresses = db.Addresses.Include(a => a.Customer);
            return View(addresses.ToList());
        }

        // GET: Addresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: Addresses/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "UserID");
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        hilaryberigan[9:22 AM]
        [HttpPost]
               [ValidateAntiForgeryToken]
       public ActionResult Create([Bind(Include = "ID,StreetNumber,StreetName,State,ZipCode,City,CustomerID,RouteNumber")] Address address) //need to make sure address id is on pickup site
        {
            if (ModelState.IsValid)
            {
                db.Addresses.Add(address);

                db.SaveChanges();

                int number;
                int routenumber;


                int.TryParse(address.ZipCode, out number); //should this be in a different method on it's own? SetRouteNumber()..

                if (number < 20000)
                    routenumber = 1;
                else if (number < 30000)
                    routenumber = 2;
                else if (number < 40000)
                    routenumber = 3;
                else if (number < 50000)
                    routenumber = 4;
                else
                    routenumber = 5;

                //not sure if this is right... address.id? should address id be on customer or user?

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(address);
        }

        private int GetCustomerId()
        {
            string id = User.Identity.GetUserId();

            int query = (from C in db.Customers where C.UserID == id select C.ID).FirstOrDefault();

            return query;
        }

        // GET: Addresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "UserID", address.CustomerID);
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StreetNumber,StreetName,State,ZipCode,City,CustomerID")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "UserID", address.CustomerID);
            return View(address);
        }

        // GET: Addresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
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

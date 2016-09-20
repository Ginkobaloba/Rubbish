using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rubbish.Models;

using Microsoft.AspNet.Identity;


namespace Rubbish.Controllers
{
    public class AddressesController : Controller
    {
        string day = DateTime.Now.DayOfWeek.ToString().ToLower();


        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Addresses
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var employee = (from e in db.Employees where userId == e.UserID select e).FirstOrDefault();
            
            int routeNumber = employee.RouteNumber;
            FindVacations(routeNumber);
            var customers = db.Customers.Include(c => c.Address).Where(a => a.DayOfWeek.ToLower() == day)
                     .Where(d => d.Address.RouteNumber == routeNumber).Where(b => b.IsActive == true).ToList();

            List<Address> addresses = new List<Address>();
            foreach (var item in customers)
            {
                addresses.Add(item.Address);
            }
            
            return View(addresses);

        }
        private void FindVacations(int routeNumber)
        {
            List<Customer> allCustomers = db.Customers.Include(c => c.Address).Where(a => a.DayOfWeek == day)
                   .Where(d => d.Address.RouteNumber == routeNumber).ToList();
            foreach (var c in allCustomers)
            {
                c.IsActive = true;
                db.SaveChanges();
            }
            var nowDate = DateTime.Now.Date;
            List<Customer> customers = db.Customers.Include(a => a.Vacation).Where(b => b.Vacation.StartDate <= nowDate)
                .Where(c => c.Vacation.EndDate >= nowDate).ToList();
            foreach (var c in customers)
            { c.IsActive = false;
                db.SaveChanges();
            }

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
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StreetNumber,StreetName,State,ZipCode,RouteNumber,City,Lat,Lng")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Addresses.Add(address);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(address);
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
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StreetNumber,StreetName,State,ZipCode,RouteNumber,City,Lat,Lng")] Address address)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rubbish.Models;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Xml.Linq;


namespace Rubbish.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.ApplicationUser);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,MoneyOwed")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName", customer.UserID);
            return View(customer);
        }

        [AllowAnonymous]
        public ActionResult RegisterCustomer()
        {
            ViewBag.Name = new SelectList(db.Roles.Where(t => !t.Name.Contains("Admin")).ToList(), "Name", "Name");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterCustomer(RegisterCustomerViewModel model)
        {
            //AddErrors(result);
            //break out into individual functions


            Address address = new Address { StreetNumber = model.StreetNumber, StreetName = model.StreetName, City = model.City, State = model.State, ZipCode = model.ZipCode };

            if (ModelState.IsValid)
            {

                var userid = User.Identity.GetUserId();
                
                var query = (from c in db.Customers where c.UserID == userid select c).FirstOrDefault();

                query.AddressID = address.ID;



                var stringAddress = address.StreetNumber + " " + address.StreetName + " " + address.City + " " + address.State + " " + address.ZipCode;

                var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(stringAddress));

                var request = WebRequest.Create(requestUri);
                var response = request.GetResponse();
                var xdoc = XDocument.Load(response.GetResponseStream());

                var result = xdoc.Element("GeocodeResponse").Element("result");
                var locationElement = result.Element("geometry").Element("location");
                var lat = locationElement.Element("lat");
                var lng = locationElement.Element("lng");
                string stringlat = lat.ToString();
                string stringlng = lng.ToString();
                stringlat = stringlat.Substring(5, stringlat.IndexOf("</lat>") - 5);
                stringlng = stringlng.Substring(5, stringlng.IndexOf("</lng>") - 5);


                float longitude;
                float latitude;
                float.TryParse(stringlat, out latitude);
                float.TryParse(stringlng, out longitude);

                address.Lat = latitude;
                address.Lng = longitude;
    
                int number;
                int routenumber = 0;

                int.TryParse(address.ZipCode, out number);

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

                address.RouteNumber = routenumber;

                try
                {
                    db.Addresses.Add(address);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return RedirectToAction("Index", "Manage");
            }
            return View(model);
        }

        private int? GetCustomerId()
        {
            try
            {
                string id = User.Identity.GetUserId();

                int? query = (from C in db.Customers where C.UserID == id select C.ID).FirstOrDefault();

                return query;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            } } 

        



        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName", customer.UserID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,MoneyOwed")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName", customer.UserID);
            return View(customer);

        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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

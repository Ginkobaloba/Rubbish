using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Rubbish.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;

namespace Rubbish.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: Empty
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";
                    if (IsAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            
            return View();
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }
            return View();       
	}

        
        public bool IsAdminUser()
        {
            bool trigger = false;

            if (User.Identity.IsAuthenticated)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var user = User.Identity;
                var roles = UserManager.GetRoles(user.GetUserId());
                foreach (var role in roles)
                {
                    if (role.ToString() == "Admin")
                    {
                        trigger = true;
                    }
                }
            }
            return trigger;
        }
        // GET: Empty/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Empty/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empty/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Empty/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Empty/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Empty/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Empty/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

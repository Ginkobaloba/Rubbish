using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rubbish.Models;

using Microsoft.AspNet.Identity.EntityFramework;
namespace Rubbish.Controllers
{
    public class RoleController : Controller
    {
        ApplicationDbContext db;
        public RoleController()
        {
            db = new ApplicationDbContext();
        }
        // GET:All Roles
        public ActionResult Index()
        {
            var Roles = db.Roles.ToList();
            return View(Roles);
        }

        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            db.Roles.Add(Role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
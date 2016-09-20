using System.Web.Mvc;
using Rubbish.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace Rubbish.Controllers
{
    public class HomeController : Controller
    { /* var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationContext()));*/
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {

            //var rolesForUser = await UserManager.GetRolesAsync(userId);

            // rolesForUser now has a list role classes.
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
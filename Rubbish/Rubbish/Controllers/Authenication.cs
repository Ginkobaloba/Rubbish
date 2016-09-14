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
using Microsoft.Owin;
using Owin;

namespace Rubbish.Controllers
{
    public class Authenication
    {
        bool trigger;
        public bool IsAdminUser(Controller controller, string roleType)
        {

            if (controller.User.Identity.IsAuthenticated)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = controller.User.Identity;
                var roles = UserManager.GetRoles(user.GetUserId());
                foreach (var role in roles)
                {
                    if (role.ToString() == roleType)
                    {
                        trigger = true;
                    }
                }
            }
            return trigger;
        }
    }  
    }

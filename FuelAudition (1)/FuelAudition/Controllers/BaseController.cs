using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using FuelAudition.Helpers;
using Microsoft.AspNet.Identity.Owin;
using FuelAudition.Models;

namespace FuelAudition.Controllers
{
    public class BaseController : Controller
    {

        public string CultureName;

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            
            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                CultureName = cultureCookie.Value;
            else
                CultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages


            // Validate culture name
            CultureName = CultureHelper.GetImplementedCulture(CultureName); // This is safe

            if (CultureName.Contains("fr"))
            {
                CultureName = "fr-ca";
            }

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(CultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            if (Request.IsAuthenticated && Session["Utilisateur"] == null)
            {

                ApplicationUserManager manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                 ApplicationUser user = manager.Users.Single(x => x.UserName == User.Identity.Name);
                Session["Utilisateur"] = user;
               
            }

            return base.BeginExecuteCore(callback, state);
        }

        
    }
}
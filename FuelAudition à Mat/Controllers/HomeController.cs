using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FuelAudition.Helpers;
using FuelAudition.Models;

namespace FuelAudition.Controllers
{
    public class HomeController : BaseController

    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Client");
            return View();
        }
      
       
        //[Authorize(Roles = "Admin")]
        public ActionResult IndexAdmin()
        {
            return View();
        }
        //[Authorize(Roles = "Client")]
        public ActionResult IndexClient()
        {
            return View();
        }
        public ActionResult SetCulture(string culture, string action, string controller, string id)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction(action, controller, new { id = id });
        }
    }
}
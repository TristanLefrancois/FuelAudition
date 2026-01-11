using FuelAudition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FuelAudition.Filtre
{
    public class VerifierClientIDFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string action = filterContext.ActionDescriptor.ActionName;
            string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            if (HttpContext.Current.Request.IsAuthenticated && ((ApplicationUser)HttpContext.Current.Session["Utilisateur"]).ClientId == null && action != "LogOff")
            {
                if (controller != "Account" && action != "ConfirmEmail")
                {
                    if (controller != "Client" || (action != "Index" && action != "Create" && action != "Details"))
                    {


                        filterContext.Result = new RedirectToRouteResult(
                                new RouteValueDictionary{{ "controller", "Client" },
                                          { "action", "Index" }

                                             });
                    }
                }
                
            }

            

            base.OnActionExecuting(filterContext);

        }
    }
}
using FuelAudition.Filtre;
using System.Web;
using System.Web.Mvc;

namespace FuelAudition
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new VerifierClientIDFilter());
        }
    }
}

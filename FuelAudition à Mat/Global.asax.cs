using FuelAudition.Models;
using FuelAudition.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FuelAudition
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<ClientDetailVM, Client>();
                cfg.CreateMap<Client, ClientDetailVM>();
                cfg.CreateMap<FournisseurDetailVM, ClientFournisseur>();
                cfg.CreateMap<ClientFournisseur, FournisseurDetailVM>();
                cfg.CreateMap<Fournisseur, FournisseurDetailVM>();
                cfg.CreateMap<FournisseurDetailVM, Fournisseur>();
            });

            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
        }
    }
}

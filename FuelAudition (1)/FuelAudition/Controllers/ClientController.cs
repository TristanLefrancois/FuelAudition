using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using FuelAudition.Models;
using FuelAudition.Models.ViewModel;

namespace FuelAudition.Controllers
{
    [Authorize]
    public class ClientController : BaseController
    {
        private np38965_FAEntities db = new np38965_FAEntities();

        private ApplicationUserManager _userManager;

        public ClientController()
        {
        }

        public ClientController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: Client
        public ActionResult Index()
        {

            return View(((ApplicationUser)Session["Utilisateur"]).ClientId != null);
        }

     
        // GET: Client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Create");
            }
            Client client = db.Clients.Find(id);
            
            if (client == null)
            {
                return HttpNotFound();
            }

            ClientDetailVM clientVM = AutoMapper.Mapper.Map<ClientDetailVM>(client);
            clientVM.Langues = new SelectList(db.Langues, "LangueId", "LangueCode", clientVM.LangueId);
            clientVM.Villes = new SelectList(db.Villes, "VilleId", "Nom", clientVM.VilleId);
            return View(clientVM);
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            ClientDetailVM clientVM = new ClientDetailVM
            {
                Langues = new SelectList(db.Langues, "LangueId", "LangueCode"),
                Villes = new SelectList(db.Villes, "VilleId", "Nom")
            };

            return View("Details", clientVM);
        }


        // POST: Clients/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(ClientDetailVM clientVM)
        {
            if (ModelState.IsValid)
            {

                Client client = AutoMapper.Mapper.Map<Client>(clientVM);

                if (clientVM.ClientId > 0)
                {
                    db.Entry(client).State = EntityState.Modified;
                    db.SaveChanges();

                    clientVM.EstEnregistrerAvecSucces = true;
                }
                else
                {
                    
                    db.Clients.Add(client);
                    db.SaveChanges();

                    AspNetUsers AspNetUser = db.AspNetUsers.Find(User.Identity.GetUserId());
                    if (AspNetUser != null)
                    {
                        AspNetUser.ClientId = client.ClientId; // Extra Property
                        db.SaveChanges();

                        ApplicationUserManager manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                        ApplicationUser user = manager.Users.Single(x => x.UserName == User.Identity.Name);
                        Session["Utilisateur"] = user;
                    }
                    return RedirectToAction("Index");
                }

               
            }

            if (clientVM.ClientId > 0)
            {
                clientVM.Langues = new SelectList(db.Langues, "LangueId", "LangueCode", clientVM.LangueId);
                clientVM.Villes = new SelectList(db.Villes, "VilleId", "Nom", clientVM.VilleId);
            }
            else
            {
                clientVM.Langues = new SelectList(db.Langues, "LangueId", "LangueCode");
                clientVM.Villes = new SelectList(db.Villes, "VilleId", "Nom");
            }

            
            return View(clientVM);
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

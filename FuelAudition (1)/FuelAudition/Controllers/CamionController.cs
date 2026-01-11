using FuelAudition.Models;
using FuelAudition.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace FuelAudition.Controllers
{
    public class CamionController : BaseController
    {

        private np38965_FAEntities db = new np38965_FAEntities();

        private ApplicationUser Utilisateur
        {
            get
            {
                return (ApplicationUser)Session["Utilisateur"];
            }

        }


        public ActionResult Details()
        {
            Client client = db.Clients.Find(Utilisateur.ClientId);
            CamionDetailVM CamionVM = new CamionDetailVM
            {
                NombreCamion = client.NombreCamion,
                NombreHeureOperation = client.NombreHeureOperation
            };

            if (this.CultureName.ToUpper().Contains("FR"))
            {
                CamionVM.ClientCamions = db.ClientCamions.Where(x => x.ClientId == Utilisateur.ClientId).OrderBy(x => x.Camion.NomFr).ToList();
                CamionVM.Camions = new SelectList(db.Camions.OrderBy(x => x.NomFr), "CamionId", "NomFr");
            }
            else
            {
                CamionVM.ClientCamions = db.ClientCamions.Where(x => x.ClientId == Utilisateur.ClientId).OrderBy(x => x.Camion.NomAn).ToList();
                CamionVM.Camions = new SelectList(db.Camions.OrderBy(x => x.NomAn), "CamionId", "NomAn");
            }

            return View(CamionVM);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(CamionDetailVM camionVM)
        {
            if (ModelState.IsValid)
            {

                using (TransactionScope tran = new TransactionScope())
                {

                    Client client = db.Clients.Find(Utilisateur.ClientId);

                    client.NombreCamion = camionVM.NombreCamion;
                    client.NombreHeureOperation = camionVM.NombreHeureOperation;

                    db.Entry(client).State = EntityState.Modified;
                    db.SaveChanges();

                    foreach (ClientCamion clientCamion in db.ClientCamions.Where(x => x.ClientId == Utilisateur.ClientId))
                    {
                        db.ClientCamions.Remove(clientCamion);

                    }
                    db.SaveChanges();

                    if (!String.IsNullOrEmpty(camionVM.CamionsId))
                    {
                        foreach (string camionId in camionVM.CamionsId.Split(','))
                        {
                            db.ClientCamions.Add(new ClientCamion
                            {
                                CamionId = int.Parse(camionId),
                                ClientId = (int)Utilisateur.ClientId
                            });
                            
                        }

                    }
                    db.SaveChanges();

                    tran.Complete();
                }


                camionVM.EstEnregistrerAvecSucces = true;
            }

            if (this.CultureName.ToUpper().Contains("FR"))
            {
                camionVM.Camions = new SelectList(db.Camions.OrderBy(x => x.NomFr), "CamionId", "NomFr");
            }
            else
            {
                camionVM.Camions = new SelectList(db.Camions.OrderBy(x => x.NomFr), "CamionId", "NomAn");
            }

            camionVM.ClientCamions = db.ClientCamions.Where(x => x.ClientId == Utilisateur.ClientId).ToList();

            return View(camionVM);
        }
    }
}
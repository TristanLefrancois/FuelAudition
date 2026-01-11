using FuelAudition.Models;
using FuelAudition.Models.Enum;
using FuelAudition.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace FuelAudition.Controllers
{
    public class FournisseurController : BaseController
    {

        private np38965_FAEntities db = new np38965_FAEntities();

        private ApplicationUser Utilisateur
        {
            get
            {
                return (ApplicationUser)Session["Utilisateur"];
            }

        }


        public ActionResult Rechercher()
        {

            IQueryable<ClientFournisseur> fournisseurs = db.ClientFournisseurs.Where(x => x.ClientId == Utilisateur.ClientId);

            RechercheFournisseurVM critereRecherche = new RechercheFournisseurVM()
            {
                ResultatRecherche = new ResultatRechercheFournisseurVM
                {
                    Fournisseurs = fournisseurs.OrderBy(x => x.Fournisseur.Nom).Take(Constantes.NbElementParPage).ToList(),
                    PageCourante = 1,
                    Total = fournisseurs.Count(),
                    NbElementParPage = Constantes.NbElementParPage,
                    Trie = "Nom",
                    SensTrie = "asc"
                }
            };

            if (TempData["Suppression"] != null && (bool)TempData["Suppression"])
            {
                
                critereRecherche.EstSuppressionAvecSucces = true;
            }

            return View(critereRecherche);
        }

        [HttpPost]
        [OutputCache(Duration = 0)]
        public PartialViewResult ResultatRecherche(RechercheFournisseurVM critereRecherche, int? PageCourante, string Trie, string SensTrie)
        {

            IQueryable<ClientFournisseur> fournisseurs = db.ClientFournisseurs.Where(x => x.ClientId == Utilisateur.ClientId);

            int page = 1;
            if (PageCourante > 0)
            {
                page = (int)PageCourante;
            }

            fournisseurs = Trier(fournisseurs, Trie, SensTrie);

            string sens = "desc";
            if (!string.IsNullOrEmpty(SensTrie))
            {
                sens = SensTrie;
            }

            ResultatRechercheFournisseurVM resultat = new ResultatRechercheFournisseurVM
            {
                Fournisseurs = fournisseurs.Skip((page - 1) * Constantes.NbElementParPage).Take(Constantes.NbElementParPage).ToList(),
                PageCourante = page,
                Total = fournisseurs.Count(),
                NbElementParPage = Constantes.NbElementParPage,
                Trie = Trie,
                SensTrie = sens
            };

            return PartialView("_ResultatRecherche", resultat);
        }

        private IQueryable<ClientFournisseur> Trier(IQueryable<ClientFournisseur> fournisseurs, string trie, string sensTrie)
        {
            switch (trie)
            {
                case "Nom":
                    if (sensTrie == "asc")
                    {
                        fournisseurs = fournisseurs.OrderBy(x => x.Fournisseur.Nom);
                    }
                    else
                    {
                        fournisseurs = fournisseurs.OrderByDescending(x => x.Fournisseur.Nom);
                    }
                    break;
                case "Carburant":
                    if(sensTrie == "asc")
                    {
                        if (this.CultureName.ToUpper().Contains("FR"))
                        {
                            fournisseurs = fournisseurs.OrderBy(x => x.Carburant.NomFr);
                        }
                        else
                        {
                            fournisseurs = fournisseurs.OrderBy(x => x.Carburant.NomAn);
                        }
                    }
                    else
                    {
                        if (this.CultureName.ToUpper().Contains("FR"))
                        {
                            fournisseurs = fournisseurs.OrderByDescending(x => x.Carburant.NomFr);
                        }
                        else
                        {
                            fournisseurs = fournisseurs.OrderByDescending(x => x.Carburant.NomAn);
                        }
                    }
                    break;
                case "Volume":
                    if (sensTrie == "asc")
                    {
                        if (this.CultureName.ToUpper().Contains("FR"))
                        {
                            fournisseurs = fournisseurs.OrderBy(x => x.Volume.NomFr);
                        }
                        else
                        {
                            fournisseurs = fournisseurs.OrderBy(x => x.Volume.NomAn);
                        }
                    }
                    else
                    {
                        if (this.CultureName.ToUpper().Contains("FR"))
                        {
                            fournisseurs = fournisseurs.OrderByDescending(x => x.Volume.NomFr);
                        }
                        else
                        {
                            fournisseurs = fournisseurs.OrderByDescending(x => x.Volume.NomAn);
                        }
                    }
                    break;
                case "Marge":
                    if (sensTrie == "asc")
                    {
                        fournisseurs = fournisseurs.OrderBy(x => x.Marge);
                    }
                    else
                    {
                        fournisseurs = fournisseurs.OrderByDescending(x => x.Marge);
                    }
                    break;
                case "Statut":
                    if (sensTrie == "asc")
                    {
                        fournisseurs = fournisseurs.OrderBy(x => x.Statut);
                    }
                    else
                    {
                        fournisseurs = fournisseurs.OrderByDescending(x => x.Statut);
                    }
                    break;
            }

            return fournisseurs;
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Create");
            }
            ClientFournisseur clientFournisseur = db.ClientFournisseurs.Find(id);

            if (clientFournisseur == null)
            {
                return HttpNotFound();
            }

            FournisseurDetailVM fournisseurVM = AutoMapper.Mapper.Map<FournisseurDetailVM>(clientFournisseur);

            fournisseurVM.Nom = clientFournisseur.Fournisseur.Nom;
            fournisseurVM.Adresse1 = clientFournisseur.Fournisseur.Adresse1;
            fournisseurVM.CodePostal = clientFournisseur.Fournisseur.CodePostal;
            fournisseurVM.VilleId = clientFournisseur.Fournisseur.VilleId;
            fournisseurVM.ProvinceId = clientFournisseur.Fournisseur.ProvinceId;
          

            RemplirListeModele(fournisseurVM);



            return View(fournisseurVM);
        }

        public PartialViewResult ObtenirVilles(int provinceId)
        {
            FournisseurDetailVM fournisseurVM = new FournisseurDetailVM();

            if (this.CultureName.ToUpper().Contains("FR"))
            {
                
                fournisseurVM.Villes = new SelectList(db.Villes.Where(x => x.ProvinceId == provinceId).OrderBy(x => x.Nom), "VilleId", "Nom");
 
            }
            else
            {
                
                fournisseurVM.Villes = new SelectList(db.Villes.Where(x => x.ProvinceId == provinceId).OrderBy(x => x.Nom), "VilleId", "Nom");
 
            }

            return PartialView("_Villes", fournisseurVM);
        }

        private void RemplirListeModele(FournisseurDetailVM fournisseurVM)
        {
            
            if (fournisseurVM.ClientFournisseurId > 0)
            {
                if (this.CultureName.ToUpper().Contains("FR"))
                {

                    fournisseurVM.Villes = new SelectList(db.Villes.Where(x => x.ProvinceId == fournisseurVM.ProvinceId).OrderBy(x => x.Nom), "VilleId", "Nom", fournisseurVM.VilleId);
                    fournisseurVM.Provinces = new SelectList(db.Provinces.OrderBy(x => x.NomFr), "ProvinceId", "NomFr", fournisseurVM.ProvinceId);
                    fournisseurVM.Carburants = new SelectList(db.Carburants.Where(x => x.Actif).OrderBy(x => x.NomFr), "CarburantId", "NomFr", fournisseurVM.CarburantId);
                    fournisseurVM.Activites = new SelectList(db.Activites.OrderBy(x => x.NomFr), "ActiviteId", "NomFr", fournisseurVM.ActiviteId);
                    fournisseurVM.Volumes = new SelectList(db.Volumes, "VolumeId", "NomFr", fournisseurVM.VolumeId);

                }
                else
                {

                    
                    fournisseurVM.Provinces = new SelectList(db.Provinces.OrderBy(x => x.NomAn), "ProvinceId", "NomAn", fournisseurVM.ProvinceId);
                    fournisseurVM.Villes = new SelectList(db.Villes.Where(x => x.ProvinceId == fournisseurVM.ProvinceId).OrderBy(x => x.Nom), "VilleId", "Nom", fournisseurVM.VilleId);
                    fournisseurVM.Carburants = new SelectList(db.Carburants.Where(x => x.Actif).OrderBy(x => x.NomAn), "CarburantId", "NomAn", fournisseurVM.CarburantId);
                    fournisseurVM.Activites = new SelectList(db.Activites.OrderBy(x => x.NomAn), "ActiviteId", "NomAn", fournisseurVM.ActiviteId);
                    fournisseurVM.Volumes = new SelectList(db.Volumes, "VolumeId", "NomAn", fournisseurVM.VolumeId);

                }
            }
            else
            {
                if (this.CultureName.ToUpper().Contains("FR"))
                {
                    var provinceId = db.Provinces.OrderBy(x => x.NomFr).FirstOrDefault().ProvinceId;
                    fournisseurVM.Villes = new SelectList(db.Villes.Where(x => x.ProvinceId == provinceId).OrderBy(x => x.Nom), "VilleId", "Nom");
                    fournisseurVM.Provinces = new SelectList(db.Provinces.OrderBy(x => x.NomFr), "ProvinceId", "NomFr");
                    fournisseurVM.Carburants = new SelectList(db.Carburants.Where(x => x.Actif).OrderBy(x => x.NomFr), "CarburantId", "NomFr");
                    fournisseurVM.Activites = new SelectList(db.Activites.OrderBy(x => x.NomFr), "ActiviteId", "NomFr");
                    fournisseurVM.Volumes = new SelectList(db.Volumes, "VolumeId", "NomFr");

                }
                else
                {
                    var provinceId = db.Provinces.OrderBy(x => x.NomAn).FirstOrDefault().ProvinceId;
                    fournisseurVM.Villes = new SelectList(db.Villes.Where(x => x.ProvinceId == provinceId).OrderBy(x => x.Nom), "VilleId", "Nom");
                    fournisseurVM.Provinces = new SelectList(db.Provinces.OrderBy(x => x.NomAn), "ProvinceId", "NomAn");
                    fournisseurVM.Carburants = new SelectList(db.Carburants.Where(x => x.Actif).OrderBy(x => x.NomAn), "CarburantId", "NomAn");
                    fournisseurVM.Activites = new SelectList(db.Activites.OrderBy(x => x.NomAn), "ActiviteId", "NomAn");
                    fournisseurVM.Volumes = new SelectList(db.Volumes, "VolumeId", "NomAn");

                }
            }
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            FournisseurDetailVM fournisseurVM = new FournisseurDetailVM
            {
               
                ClientId = (int)Utilisateur.ClientId

            };

            RemplirListeModele(fournisseurVM);


            return View("Details", fournisseurVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(FournisseurDetailVM fournisseurVM)
        {
            if (ModelState.IsValid)
            {

                ClientFournisseur clientFournisseur = AutoMapper.Mapper.Map<ClientFournisseur>(fournisseurVM);
                Fournisseur fournisseur = AutoMapper.Mapper.Map<Fournisseur>(fournisseurVM);

                clientFournisseur.Marge = Convert.ToDecimal(clientFournisseur.Marge.ToString().Replace(".", ","), new CultureInfo("fr"));
                clientFournisseur.Statut = (int)Statut.EnAttenteApprobation;

                if (fournisseurVM.ClientFournisseurId > 0)
                {
                    
                    using (TransactionScope tran = new TransactionScope())
                    {
                        db.Entry(clientFournisseur).State = EntityState.Modified;
                        db.Entry(fournisseur).State = EntityState.Modified;
                        db.SaveChanges();

                        tran.Complete();
                    }
                       

                    fournisseurVM.EstEnregistrerAvecSucces = true;
                }
                else
                {
                    
                    
                    using (TransactionScope tran = new TransactionScope())
                    {
                        db.Fournisseurs.Add(fournisseur);
                        clientFournisseur.FournisseurId = fournisseur.FournisseurId;
                        db.ClientFournisseurs.Add(clientFournisseur);
                        db.SaveChanges();

                        tran.Complete();
                    }


                    fournisseurVM = AutoMapper.Mapper.Map<FournisseurDetailVM>(clientFournisseur);
                    fournisseurVM.Nom = clientFournisseur.Fournisseur.Nom;
                    fournisseurVM.Adresse1 = clientFournisseur.Fournisseur.Adresse1;
                    fournisseurVM.CodePostal = clientFournisseur.Fournisseur.CodePostal;
                    fournisseurVM.VilleId = clientFournisseur.Fournisseur.VilleId;
                    fournisseurVM.ProvinceId = clientFournisseur.Fournisseur.ProvinceId;
                }

                fournisseurVM.EstEnregistrerAvecSucces = true;
            }

            RemplirListeModele(fournisseurVM);


            


            return View(fournisseurVM);
        }

        [HttpPost]
        public string Supprimer(int id)
        {
            ClientFournisseur clientFournisseur = db.ClientFournisseurs.Find(id);
            db.Entry(clientFournisseur).State = EntityState.Deleted;
            db.SaveChanges();
            TempData["Suppression"] = true;
            return Url.Action("Rechercher", "Fournisseur");
        }
    }
}
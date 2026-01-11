using FuelAudition.Models;
using FuelAudition.Models.Enum;
using FuelAudition.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FuelAudition.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
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

            IQueryable<ClientFournisseur> clientfournisseurs = db.ClientFournisseurs.Where(x => (Statut)x.Statut == Statut.EnAttenteApprobation && x.ClientFournisseurId != 48);

            RechercheClientAdminVM critereRecherche = new RechercheClientAdminVM()
            {

                Statut = (int)Statut.EnAttenteApprobation,
                ResultatRecherche = new ResultatRechercheClientAdminVM
                {
                    ClientFournisseurs = clientfournisseurs.OrderBy(x => x.Fournisseur.Nom).Take(Constantes.NbElementParPage).ToList(),
                    PageCourante = 1,
                    Total = clientfournisseurs.Count(),
                    NbElementParPage = Constantes.NbElementParPage,
                    SensTrie = "asc",
                    Trie = "Nom"
                }
            };

            return View(critereRecherche);
        }


        [HttpPost]
        [OutputCache(Duration = 0)]
        public PartialViewResult ResultatRecherche(RechercheClientAdminVM critereRecherche, int? PageCourante, string Trie, string SensTrie)
        {

            IQueryable<ClientFournisseur> clientFournisseurs = db.ClientFournisseurs.Where(x => x.ClientFournisseurId != 48).AsQueryable();


            if (critereRecherche.Statut != StatutAvecTous.Tous)
            {
                clientFournisseurs = clientFournisseurs.Where(x => x.Statut == (int)critereRecherche.Statut);
            }
            int page = 1;
            if (PageCourante > 0)
            {
                page = (int)PageCourante;
            }

            clientFournisseurs = Trier(clientFournisseurs, Trie, SensTrie);

            string sens = "desc";
            if (!string.IsNullOrEmpty(SensTrie))
            {
                sens = SensTrie;
            }

            ResultatRechercheClientAdminVM resultat = new ResultatRechercheClientAdminVM
            {
                ClientFournisseurs = clientFournisseurs.Skip((page - 1) * Constantes.NbElementParPage).Take(Constantes.NbElementParPage).ToList(),
                PageCourante = page,
                Total = clientFournisseurs.Count(),
                NbElementParPage = Constantes.NbElementParPage,
                Trie = Trie,
                SensTrie = sens
            };

            return PartialView("_ResultatRecherche", resultat);
        }

        private IQueryable<ClientFournisseur> Trier(IQueryable<ClientFournisseur> clientFournisseurs, string trie, string sensTrie)
        {
            switch (trie)
            {
                case "Nom":
                    if (sensTrie == "asc")
                    {
                        clientFournisseurs = clientFournisseurs.OrderBy(x => x.Client.Nom);
                    }
                    else
                    {
                        clientFournisseurs = clientFournisseurs.OrderByDescending(x => x.Client.Nom);
                    }
                    break;
                case "Statut":
                    if (sensTrie == "asc")
                    {

                        clientFournisseurs = clientFournisseurs.OrderBy(x => x.Statut);


                    }
                    else
                    {

                        clientFournisseurs = clientFournisseurs.OrderByDescending(x => x.Statut);

                    }
                    break;
                case "Volume":
                    if (sensTrie == "asc")
                    {
                        if (this.CultureName.ToUpper().Contains("FR"))
                        {
                            clientFournisseurs = clientFournisseurs.OrderBy(x => x.Volume.NomFr);
                        }
                        else
                        {
                            clientFournisseurs = clientFournisseurs.OrderBy(x => x.Volume.NomAn);
                        }
                    }
                    else
                    {
                        if (this.CultureName.ToUpper().Contains("FR"))
                        {
                            clientFournisseurs = clientFournisseurs.OrderByDescending(x => x.Volume.NomFr);
                        }
                        else
                        {
                            clientFournisseurs = clientFournisseurs.OrderByDescending(x => x.Volume.NomAn);
                        }
                    }
                    break;
                case "Marge":
                    if (sensTrie == "asc")
                    {
                        clientFournisseurs = clientFournisseurs.OrderBy(x => x.Marge);
                    }
                    else
                    {
                        clientFournisseurs = clientFournisseurs.OrderByDescending(x => x.Marge);
                    }
                    break;
            }

            return clientFournisseurs;
        }

        public ActionResult Details(int id)
        {

            ClientFournisseur clientFournisseur = db.ClientFournisseurs.Find(id);

            if (clientFournisseur == null)
            {
                return HttpNotFound();
            }

            if(TempData["Enregistrer"] != null)
            {
                ViewBag.Enregistrer = true;
            }

            return View(clientFournisseur);
        }


        [HttpPost]
        public ActionResult Approuver(int id, string commentaire)
        {
            ClientFournisseur clientFournisseur = db.ClientFournisseurs.Find(id);
            clientFournisseur.Commentaire = commentaire;
            clientFournisseur.Statut = (int)Statut.Approuver;
            db.Entry(clientFournisseur).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();


            TempData["Enregistrer"] = true;
            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        public ActionResult Refuser(int id, string commentaire)
        {
            ClientFournisseur clientFournisseur = db.ClientFournisseurs.Find(id);
            clientFournisseur.Commentaire = commentaire;
            clientFournisseur.Statut = (int)Statut.Refuser;
            db.Entry(clientFournisseur).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            TempData["Enregistrer"] = true;
           
            return RedirectToAction("Details", new { id = id });
        }
    }
}
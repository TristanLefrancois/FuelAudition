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
    public class StatistiqueController : BaseController
    {

        private np38965_FAEntities db = new np38965_FAEntities();

        private ApplicationUser Utilisateur
        {
            get
            {
                return (ApplicationUser)Session["Utilisateur"];
            }

        }

        public ActionResult Graphique()
        {
            GraphiqueVM model = new GraphiqueVM
            {
                AFournisseurAjoute = db.ClientFournisseurs.Any(x => x.ClientId == Utilisateur.ClientId)
            };
            return View(model);
        }

        //[HttpPost]
        //public JsonResult ObtenirGraphique()
        //{
        //    var resultat = new List<Graphique>();

        //    var prov = db.Provinces.ToList();
        //    var volumes = db.ClientFournisseurs.Where(x => x.ClientId == Utilisateur.ClientId).GroupBy(x => x.VolumeId).Select(x => x.Key);
        //    var activites = db.ClientFournisseurs.Where(x => x.ClientId == Utilisateur.ClientId).GroupBy(x => x.ActiviteId).Select(x => x.Key);
        //    var provinces = db.ClientFournisseurs.Where(x => x.ClientId == Utilisateur.ClientId).GroupBy(x => x.Fournisseur.ProvinceId).Select(x => x.Key);
        //    foreach (int volume in volumes)
        //    {
        //        foreach (int activite in activites)
        //        {
        //            foreach (int province in provinces)
        //            {
        //                var cfAvecVolume = db.ClientFournisseurs.Where(x => x.VolumeId == volume && x.ActiviteId == activite && x.Fournisseur.ProvinceId == province && x.Statut == (int)Statut.Approuver);

        //                if (cfAvecVolume.Any(x => x.ClientId == Utilisateur.ClientId))
        //                {
        //                    int index = (int)(decimal.Round(cfAvecVolume.Count() / 2));


        //                    var min = Math.Floor(cfAvecVolume.OrderBy(x => x.Marge).First().Marge);
        //                    var max = cfAvecVolume.OrderByDescending(x => x.Marge).First().Marge;

        //                    List<decimal> ranges = new List<decimal>();

        //                    List<Point> points = new List<Point>();

        //                    decimal step = (max - min) / 40;

        //                    if (step < (decimal)0.0025)
        //                    {
        //                        step = new decimal(0.0025);

        //                    }

        //                    for (decimal i = min; i <= max; i = i + step)
        //                    {
        //                        ranges.Add(i);
        //                    }



        //                    points = cfAvecVolume.GroupBy(x => ranges.FirstOrDefault(r => r >= x.Marge)).Select(x => new Point { x = x.Key, y = x.Count().ToString() }).ToList();

        //                    resultat.Add(new Graphique
        //                    {
        //                        Fournisseurs = cfAvecVolume.Where(x => x.ClientId == Utilisateur.ClientId).Select(x => new FournisseurGraphique { Nom = x.Fournisseur.Nom, Marge = x.Marge }).ToList(),
        //                        Volume = db.Volumes.Single(x => x.VolumeId == volume).NomFr,
        //                        Activite = db.Activites.Single(x => x.ActiviteId == activite).NomFr,
        //                        Moyenne = cfAvecVolume.Select(x => x.Marge).Average(),
        //                        Mediane = cfAvecVolume.OrderBy(x => x.Marge).Select(x => x.Marge).ToList()[index],
        //                        Point = points,
        //                        Step = step,
        //                        NbClient = cfAvecVolume.Count().ToString(),
        //                        Province = prov.Single(x => x.ProvinceId == province).NomFr

        //                    });
        //                }

        //            }




        //        }


        //    }


        //    return Json(resultat);
        //}

        [HttpPost]
        public JsonResult ObtenirGraphiquePoint()
        {
            var resultat = new List<Graphique>();
            Dictionary<string, string> dictVolumes = new Dictionary<string, string>();

            dictVolumes.Add(1.ToString(), "5000");
            dictVolumes.Add(2.ToString(), "10000");
            dictVolumes.Add(3.ToString(), "20000");
            dictVolumes.Add(4.ToString(), "30000");
            dictVolumes.Add(5.ToString(), "50000");
            dictVolumes.Add(6.ToString(), "75000");
            dictVolumes.Add(7.ToString(), "100000");

            var prov = db.Provinces.ToList();
            var volumes = db.ClientFournisseurs.Where(x => x.ClientId == Utilisateur.ClientId).GroupBy(x => x.VolumeId).Select(x => x.Key);
            var activites = db.ClientFournisseurs.Where(x => x.ClientId == Utilisateur.ClientId).GroupBy(x => x.ActiviteId).Select(x => x.Key);
            var provinces = db.ClientFournisseurs.Where(x => x.ClientId == Utilisateur.ClientId).GroupBy(x => x.Fournisseur.ProvinceId).Select(x => x.Key);

            foreach (int activite in activites)
            {
                foreach (int province in provinces)
                {
                    var cfAvecVolume = db.ClientFournisseurs.Where(x => x.ActiviteId == activite && x.Fournisseur.ProvinceId == province && x.Statut == (int)Statut.Approuver);

                    if (cfAvecVolume.Any(x => x.ClientId == Utilisateur.ClientId))
                    {
                        int index = (int)(decimal.Round(cfAvecVolume.Count() / 2));


                        var min = Math.Floor(cfAvecVolume.OrderBy(x => x.Marge).First().Marge);
                        var max = cfAvecVolume.OrderByDescending(x => x.Marge).First().Marge;

                        List<Point> points = new List<Point>();


                        points = cfAvecVolume.Select(x => new Point { x = x.Marge, y = x.VolumeId.ToString() }).ToList();
                        points.ForEach(x => x.y = dictVolumes[x.y]);
                        //points = cfAvecVolume.GroupBy(x => ranges.FirstOrDefault(r => r >= x.Marge)).Select(x => new Point { x = x.Key, y = x.Count().ToString() }).ToList();

                        resultat.Add(new Graphique
                        {
                            Fournisseurs = cfAvecVolume.Where(x => x.ClientId == Utilisateur.ClientId).Select(x => new FournisseurGraphique { Nom = x.Fournisseur.Nom, Marge = x.Marge, Volume = x.VolumeId.ToString() }).ToList(),
                            Activite = db.Activites.Single(x => x.ActiviteId == activite).NomFr,
                            Moyenne = cfAvecVolume.Select(x => x.Marge).Average(),
                            Mediane = cfAvecVolume.OrderBy(x => x.Marge).Select(x => x.Marge).ToList()[index],
                            Point = points,
                            NbClient = cfAvecVolume.Count().ToString(),
                            Province = prov.Single(x => x.ProvinceId == province).NomFr

                        });

                        resultat.Last().Fournisseurs.ForEach(x => x.Volume = dictVolumes[x.Volume]);
                    }

                }


            }


            return Json(resultat);
        }


    }
}
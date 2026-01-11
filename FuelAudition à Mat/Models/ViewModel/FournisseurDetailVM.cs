using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FuelAudition.Models.ViewModel
{
    public class FournisseurDetailVM
    {

        public int ClientFournisseurId { get; set; }

        public int FournisseurId { get; set; }
        public int ClientId { get; set; }

        [Required]
        [Display(Name = "FournisseurDetailsMarge", ResourceType = typeof(Resources.Resources))]
        [Range(0.00000001, int.MaxValue, ErrorMessageResourceName = "FournisseurDetailsErreurMarge", ErrorMessageResourceType = typeof(Resources.Resources))]
        public decimal Marge { get; set; }


        [Required]
        [Display(Name = "FournisseurDetailsCarburant", ResourceType = typeof(Resources.Resources))]
        public Nullable<int> CarburantId { get; set; }

        public SelectList Carburants { get; set; }

        [Required]
        [Display(Name = "FournisseurDetailsVolume", ResourceType = typeof(Resources.Resources))]
        public Nullable<int> VolumeId { get; set; }

        public SelectList Volumes { get; set; }

        [Required]
        [Display(Name = "FournisseurDetailsService", ResourceType = typeof(Resources.Resources))]
        public Nullable<int> ActiviteId { get; set; }

        public SelectList Activites { get; set; }

        [Display(Name = "FournisseurDetailsDateFinContrat", ResourceType = typeof(Resources.Resources))]
        public Nullable<System.DateTime> DateFinContrat { get; set; }

        [Display(Name = "FournisseurDetailsDateDebutContrat", ResourceType = typeof(Resources.Resources))]
        public Nullable<System.DateTime> DateDebutContrat { get; set; }

        [Required]
        [Display(Name = "FournisseurDetailsNomFournisseur", Prompt ="Esso", ResourceType = typeof(Resources.Resources))]
        public string Nom { get; set; }

        
        [Display(Name = "FournisseurDetailsVille", ResourceType = typeof(Resources.Resources))]
        public Nullable<int> VilleId { get; set; }

        public SelectList Villes { get; set; }

        [Required]
        [Display(Name = "FournisseurDetailsProvince", ResourceType = typeof(Resources.Resources))]
        public Nullable<int> ProvinceId { get; set; }

        public SelectList Provinces { get; set; }


        [Display(Name = "FournisseurDetailsAdresse", ResourceType = typeof(Resources.Resources))]
        public string Adresse1 { get; set; }

        
        [Display(Name = "FournisseurDetailsCodePostal", ResourceType = typeof(Resources.Resources))]
        public string CodePostal { get; set; }


        public int Statut { get; set; }

        public string Commentaire { get; set; }

        public bool EstEnregistrerAvecSucces { get; set; }
    }
}
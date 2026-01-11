using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FuelAudition.Models.ViewModel
{
    public class ClientDetailVM
    {
        
        public int ClientId { get; set; }

        [Required]
        [Display(Name = "ClientDetailNom", ResourceType = typeof(Resources.Resources))]
        public string Nom { get; set; }

        [Display(Name = "ClientDetailTelephone", ResourceType = typeof(Resources.Resources))]
        public string Telephone { get; set; }

        [Display(Name = "ClientDetailInter", ResourceType = typeof(Resources.Resources))]
        public string Inter { get; set; }

        [Display(Name = "ClientDetailFax", ResourceType = typeof(Resources.Resources))]
        public string FAX { get; set; }

        [EmailAddress]
        [Display(Name = "ClientDetailCourriel", ResourceType = typeof(Resources.Resources))]
        public string Email { get; set; }

        [Display(Name = "ClientDetailAdresse1", ResourceType = typeof(Resources.Resources))]
        public string Adresse1 { get; set; }

        [Display(Name = "ClientDetailAdresse2", ResourceType = typeof(Resources.Resources))]
        public string Adresse2 { get; set; }

        [Display(Name = "ClientDetailVille", ResourceType = typeof(Resources.Resources))]
        public Nullable<int> VilleId { get; set; }

        [Display(Name = "ClientDetailCodePostal", ResourceType = typeof(Resources.Resources))]
        public string CodePostal { get; set; }

        [Display(Name = "ClientDetailContactNom", ResourceType = typeof(Resources.Resources))]
        public string ContactNom { get; set; }

        [Display(Name = "ClientDetailContactTelephone", ResourceType = typeof(Resources.Resources))]
        public string ContactTelephone { get; set; }

        [Display(Name = "ClientDetailContactInter", ResourceType = typeof(Resources.Resources))]
        public string ContactInter { get; set; }

        [EmailAddress]
        [Display(Name = "ClientDetailContactEmail", ResourceType = typeof(Resources.Resources))]
        public string ContactEmail { get; set; }

        
        [Display(Name = "ClientDetailLangue", ResourceType = typeof(Resources.Resources))]
        public Nullable<int> LangueId { get; set; }

        public SelectList Langues { get; set; }

        public SelectList Villes { get; set; }

        public bool EstEnregistrerAvecSucces { get; set; }
    }
}
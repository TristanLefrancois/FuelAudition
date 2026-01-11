using FuelAudition.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FuelAudition.Models.ViewModel
{
    public class RechercheClientAdminVM
    {
        public string Texte { get; set; }

        public ResultatRechercheClientAdminVM ResultatRecherche { get; set; }

        
      
        public StatutAvecTous Statut { get; set; }
    }
}
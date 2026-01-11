using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuelAudition.Models.ViewModel
{
    public class RechercheFournisseurVM
    {
        public string Texte { get; set; }

        public ResultatRechercheFournisseurVM ResultatRecherche { get; set; }

        public bool EstSuppressionAvecSucces { get; set; }
    }
}
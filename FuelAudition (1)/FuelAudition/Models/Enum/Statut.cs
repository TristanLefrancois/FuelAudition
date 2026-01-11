using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FuelAudition.Models.Enum
{
    public enum Statut
    {
        [Display(Name = "En attente d'approbation")]
        EnAttenteApprobation,
        [Display(Name = "Approuver")]
        Approuver,
        [Display(Name = "Refuser")]
        Refuser
    }

    public enum StatutAvecTous
    {
        [Display(Name = "Tous")]
        Tous = -1,
        [Display(Name = "En attente d'approbation")]
        EnAttenteApprobation,
        [Display(Name = "Approuver")]
        Approuver,
        [Display(Name = "Refuser")]
        Refuser
    }
}
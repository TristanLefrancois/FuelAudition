using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FuelAudition.Models.ViewModel
{
    public class CamionDetailVM
    {
        [Display(Name = "CamionDetailNombreCamion", ResourceType = typeof(Resources.Resources))]
        public Nullable<int> NombreCamion { get; set; }

        [Display(Name = "CamionDetailNombreHeureOperation", ResourceType = typeof(Resources.Resources))]
        public Nullable<int> NombreHeureOperation { get; set; }


        public List<ClientCamion> ClientCamions { get; set; }


        public SelectList Camions { get; set; }

        [Display(Name = "CamionDetailTypeEquipement", ResourceType = typeof(Resources.Resources))]
        public string CamionsId { get; set; }

        [Display(Name = "CamionDetailTypeEquipement", ResourceType = typeof(Resources.Resources))]
        public string CamionId { get; set; }

        public bool EstEnregistrerAvecSucces { get; set; }
    }
}
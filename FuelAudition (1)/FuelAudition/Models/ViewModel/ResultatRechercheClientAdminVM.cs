using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuelAudition.Models.ViewModel
{
    public class ResultatRechercheClientAdminVM
    {
        public List<ClientFournisseur> ClientFournisseurs { get; set; }
        public int PageCourante { get; set; }
        public int Total { get; set; }
        public int NbElementParPage { get; set; }
        public string Trie { get; set; }
        public string SensTrie { get; set; }
    }
}
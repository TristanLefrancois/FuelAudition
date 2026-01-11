using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuelAudition
{

    public class Graphique
    {
        public List<FournisseurGraphique> Fournisseurs;
        public string Volume;
        public string Activite;
        public decimal Moyenne;
        public decimal Mediane;
        public decimal Step;
        public List<Point> Point;
        public string NbClient;
        public string Province;
    }

    public class Point
    {
        
        public decimal x;
        public string y;
    }

    public class FournisseurGraphique
    {
        public decimal Marge;
        public string Nom;
        public string Volume;
    }
}
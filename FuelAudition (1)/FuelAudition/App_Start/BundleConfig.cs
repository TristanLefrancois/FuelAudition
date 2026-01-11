using System.Web;
using System.Web.Optimization;

namespace FuelAudition
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération (bluid) sur http://modernizr.com pour choisir uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery.icheck.js",
                      "~/dist/js/app.js",
                      "~/Scripts/moment-with-locales.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/fournisseur").Include(
                      "~/Scripts/fournisseur.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                      "~/Scripts/admin.js"));

            bundles.Add(new ScriptBundle("~/bundles/camion").Include(
                      "~/Scripts/camion.js"));

            bundles.Add(new ScriptBundle("~/bundles/statistique").Include(
                      "~/Scripts/statistique.js"));

            bundles.Add(new ScriptBundle("~/bundles/parallax").Include(
                      "~/Scripts/parallax.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/dist/css/AdminLTE.css",
                      "~/dist/css/skins/skin-red.css",
                      "~/Content/font-awesome.css",
                      "~/Content/iCheck/flat/red.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/parallax").Include(
                      "~/Content/parallax.css"));

            bundles.Add(new StyleBundle("~/Content/iCheck/flat/iCheck").Include(
                      "~/Content/iCheck/flat/red.css"));

            // Définissez EnableOptimizations sur False pour le débogage. Pour plus d'informations,
            // visitez http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}

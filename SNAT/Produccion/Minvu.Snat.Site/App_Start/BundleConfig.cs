using System.Web;
using System.Web.Optimization;

namespace Minvu.Snat.Site
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/Scripts/datatable").Include(
                    "~/Scripts/DataTables/jquery.dataTables.js",
                    "~/Scripts/DataTables/dataTables.tableTools.js",
                    "~/Scripts/DataTables/dataTables.scroller.min.js",
                    "~/Scripts/DataTables/dataTables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/Scripts/datepicker").Include(
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/locales/bootstrap-datepicker.es.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                            //"~/Scripts/jquery-1.10.2.min.js"
                            "~/NormaGrafica/js/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/NormaGrafica").Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/modernizr.js",
                    "~/Scripts/jquery.validate.js",
                    "~/Scripts/jquery.validate.unobtrusive.js",
                    "~/Scripts/globalize/globalize.js",
                    "~/Scripts/globalize/globalize.es-cl.js",
                    "~/Scripts/globalize/jquery.validate.globalize.js",
                    "~/Scripts/jquery.unobtrusive-ajax.js",
                    "~/Scripts/common.js",
                    "~/Scripts/jquery.Rut.min.js",
                    "~/Scripts/jquery.minvu.js",
                    "~/Scripts/jquery.prettynumber.js",
                    "~/Scripts/jquery.minvu.validate.js",
                    "~/Scripts/bootbox.min.js",
                    "~/Scripts/moment.min.js",
                    "~/Scripts/es.js",
                    "~/Scripts/bootstrap-datetimepicker.min.js",
                    "~/Scripts/fileinput.min.js",
                    "~/Scripts/locales/es.js",
                    //"~/Scripts/Common/nobackbutton.js",
                    "~/Scripts/lodash.js",
                    "~/Scripts/bootstrap-notify.min.js"
                    ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/NormaGrafica/bootstrap").Include(
                            "~/Content/bootstrap.css",
                            "~/Content/bootstrap-fileinput/css/fileinput.css"));

            bundles.Add(new StyleBundle("~/NormaGrafica/css/styles").Include(
                            "~/NormaGrafica/css/style.css",
                            "~/NormaGrafica/css/sidebar.css",
                            "~/Content/bootstrap-datetimepicker.css",
                            "~/Content/font-awesome.css",
                            "~/Content/animate.min.css"));

            bundles.Add(new StyleBundle("~/Content/datatables/styles").Include(
                         "~/Content/DataTables/css/dataTables.bootstrap.css",
                         "~/Content/bootstrap-datepicker3.min.css"));
        }
    }
}

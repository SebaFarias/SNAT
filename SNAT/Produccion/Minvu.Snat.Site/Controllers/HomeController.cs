using Minvu.Snat.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Minvu.Snat.Site.Controllers
{
    [PSSIMAuthenticateFilter]
    public class HomeController : Controller
    {

        //[ValidarTarea("", "", "t1")]
        public ActionResult Index()
        {
            
            return View("Index");
        }

        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }

}
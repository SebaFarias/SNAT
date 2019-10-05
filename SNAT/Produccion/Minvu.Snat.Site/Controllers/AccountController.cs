using Minvu.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minvu.Snat.Site.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExpiredSession()
        {
            if (this.HttpContext.Request.IsAjaxRequest())
            {
                return PartialView("ExpiredSessiondAjax");
            }
            else
            {
                return View();
            }
        }

        [PSSIMAuthenticateFilter]
        public ActionResult Logout()
        {

            Minvu.Security.SingleSignOn.SignOff();
            Session.Clear();
            //return new RedirectResult(SingleSignOnConfiguration.Instance.EntryPoint);

            //SingleSignOn.SignOffCurrentSession();
            return new RedirectResult(System.Configuration.ConfigurationManager.AppSettings["PortalAutenticacion"].ToString());
        }
    }
}
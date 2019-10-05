using Minvu.Security;
using Minvu.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minvu.Snat.Site
{
    public class PSSIMAuthenticateFilter : FilterAttribute, IAuthorizationFilter
    {
        public PSSIMAuthenticateFilter()
        {

        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            Ticket ticket = SingleSignOn.Authenticate();

            if (ticket == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                    filterContext.Result = new PartialViewResult { ViewName = "Account/ExpiredSessionAjax" };
                else
                    filterContext.Result = new RedirectResult("/Account/ExpiredSession");
            }
        }
    }
}
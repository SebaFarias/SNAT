using System;
using System.Linq;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Minvu.Snat.Site
{
    public sealed class ValidateJSONAntiForgeryToken : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }

            var headers = actionContext.HttpContext.Request.Headers;
            var cookies = actionContext.HttpContext.Request.Cookies;

            var tokenCookie = cookies[AntiForgeryConfig.CookieName].Value;

            var tokenHeader = headers["X-XSRF-Token"] as string;

            AntiForgery.Validate(tokenCookie ?? null, tokenHeader);

            base.OnActionExecuting(actionContext);
        }
    }
}
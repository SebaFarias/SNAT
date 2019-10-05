//using Minvu.Rukan2016.Helper.Enums.DS19;
//using Minvu.Rukan2016.HybridSite.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Minvu.Snat.Site
{
    // Este filtro de acciones comprueba el rol de un usuario para permitir el acceso al método
    public class PSSIMActionFilter : ActionFilterAttribute
    {
        private string[] Roles { get; set; }
        private Type EnumRoles { get; set; }

        public PSSIMActionFilter() { }

        public PSSIMActionFilter(params string[] roles)
        {
            this.Roles = roles;
        }

        public PSSIMActionFilter(Type enumRoles)
        {
            this.EnumRoles = enumRoles;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //bool autorizado = false;

            //if (roles != null && roles.Length > 0)
            //{
            //    foreach (var rol in roles)
            //    {
            //        if (Usuario.tieneRol(rol))
            //            autorizado = true;
            //    }
            //}
            //else
            //{
            //    var flags = BindingFlags.Static | BindingFlags.Public;
            //    var fields = enumRoles.GetFields(flags).Where(f => f.IsLiteral);
            //    foreach (var item in fields)
            //    {
            //        var rol = item.GetValue(null) as string;
            //        if (Usuario.tieneRol(rol))
            //            autorizado = true;
            //    }
            //}

            //if (!autorizado)
            //    filterContext.Result = new ViewResult { ViewName = "Error/AccesoDenegado" };
        }
    }
}

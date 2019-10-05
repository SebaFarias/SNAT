using Minvu.Security;
using Minvu.Snat.Helper;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minvu.Snat.Site
{
    public class ValidarTarea : FilterAttribute, IAuthorizationFilter
    {
        private bool _validar = false;
        
        public ValidarTarea(string tarea1, string tarea2, string unaTareaDeListado)
        {

            //Verifica tareas entre listado recibido, con cumplir una, está correcto
            if (unaTareaDeListado != "") {
                foreach (var tarea in unaTareaDeListado.Split(';'))
                {
                    _validar = SingleSignOn.CurrentPrincipal.HasTarea(tarea.Trim());
                    if (_validar)
                        break;
                }                
            }

            if (tarea2 != "")
            {
                _validar = Minvu.Security.SingleSignOn.CurrentPrincipal.HasTarea(tarea1);                
                if (!_validar)
                {
                    _validar = Minvu.Security.SingleSignOn.CurrentPrincipal.HasTarea(tarea2);

                }
            }
            else if (tarea1 != "")
            {
                _validar = Minvu.Security.SingleSignOn.CurrentPrincipal.HasTarea(tarea1);
               
            }
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //HttpContext context = HttpContext.Current;
            if (!_validar)
            {
            //    filterContext.RequestContext.HttpContext.Response.StatusCode = 401;
            //    filterContext.RequestContext.HttpContext.Response.SubStatusCode = 4;
                filterContext.Result = new RedirectResult("/Home");
            }
        }
    }
}
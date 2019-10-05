//using Minvu.Rukan2016.Domain.Entities.Common;
//using Minvu.Rukan2016.Helper;
//using Minvu.Rukan2016.HybridSite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minvu.Snat.Site
{
    public class LogErrorSitioFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
//#if !DEBUG
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            else
            {
                // Permite filtrar las excepciones y guardarlas en base de datos.


                //Exception errorEntrada = filterContext.Exception;
                //try
                //{
                //    Error errorData = new Error();

                //    string actionName = filterContext.RouteData.Values["action"].ToString();
                //    string ControllerName = filterContext.RouteData.Values["Controller"].ToString();
                //    Type controllerType = filterContext.Controller.GetType();
                //    var method = controllerType.GetMethods().Where(x => x.Name.ToLower() == actionName.ToLower()).FirstOrDefault();

                //    var returnType = method.ReturnType;
                //    Exception exception = null;

                //    Type exceptionType = null;
                //    if (filterContext.Exception.InnerException != null)
                //    {
                //        exceptionType = filterContext.Exception.InnerException.GetType();
                //        exception = filterContext.Exception.InnerException;
                //    }
                //    else
                //    {
                //        exceptionType = filterContext.Exception.GetType();
                //        exception = filterContext.Exception;
                //    }

                //    // Excepciones HTTP
                //    if (exceptionType != null && exceptionType.Equals(typeof(HttpException)))
                //    {
                //        HttpException httpEx = filterContext.Exception as HttpException;
                //        if (httpEx.GetHttpCode() == 403)
                //        {
                //            if (filterContext.HttpContext.Request.IsAjaxRequest())
                //                filterContext.Result = new PartialViewResult { ViewName = "Error/AccesoProhibido" };
                //            else
                //                filterContext.Result = new ViewResult { ViewName = "Error/AccesoProhibido" };

                //            filterContext.ExceptionHandled = true;
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        errorData = ErrorFactory.GuardarErrorEnDB(filterContext.Exception, Usuario.UserName);
                //        errorData.area = TypeHelper.ConvertObjectToString(filterContext.RouteData.DataTokens["area"]);
                //    }

                //    if (exceptionType != null && exceptionType.Equals(typeof(SqlException)))
                //    {
                //        // Error de timeout
                //        SqlException sqlEx = exception as SqlException;
                //        if (sqlEx.Number == -2)
                //        {
                //            errorData.extra = MensajeFactory.GetSqlTimeoutMessage().textoMensaje;
                //        }
                //        else
                //        {
                //            errorData.extra = MensajeFactory.GetGeneralExceptionMessage(errorData.idError, "ExDB").textoMensaje;
                //        }
                //    }

                //    if (exceptionType != null && exceptionType.Equals(typeof(Win32Exception)))
                //    {
                //        Win32Exception w32ex = exception as Win32Exception;
                //        if (w32ex.NativeErrorCode == 258)
                //        {
                //            errorData.extra = MensajeFactory.GetSqlTimeoutMessage().textoMensaje;
                //        }
                //        else
                //        {
                //            errorData.extra = MensajeFactory.GetGeneralExceptionMessage(errorData.idError, "ExDB").textoMensaje;
                //        }
                //    }



                //    //If the action that generated the exception returns JSON
                //    if (returnType.Equals(typeof(JsonResult)))
                //    {
                //        filterContext.Result = new JsonResult()
                //        {
                //            Data = new { idError = errorData.idError },
                //            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                //        };
                //        filterContext.ExceptionHandled = true;
                //        return;
                //    }

                //    //If the action that generated the exception returns a view
                //    if (returnType.Equals(typeof(ActionResult)) || (returnType).IsSubclassOf(typeof(ActionResult)))
                //    {
                //        if (filterContext.HttpContext.Request.IsAjaxRequest())
                //        {
                //            filterContext.Result = new PartialViewResult { ViewName = "Error/AjaxError", ViewData = new ViewDataDictionary(errorData) };
                //        }
                //        else
                //        {
                //            filterContext.Result = new ViewResult { ViewName = "Error/Index", ViewData = new ViewDataDictionary(errorData) };
                //        }
                //        filterContext.ExceptionHandled = true;
                //        return;
                //    }
                //}
                //catch (Exception)
                //{
                //    ErrorFactory.GuardarErrorEnDB(errorEntrada, Usuario.UserName);
                //}
                
            }
//#endif
        }
    }
}
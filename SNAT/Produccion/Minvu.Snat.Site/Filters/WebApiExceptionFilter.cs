//using Minvu.Rukan2016.Domain.Entities.Common;
//using Minvu.Rukan2016.HybridSite.Models;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Minvu.Snat.Site
{
    public class WebApiExceptionFilter : ExceptionFilterAttribute 
    {
        public override void OnException(HttpActionExecutedContext filterContext)
        {
            // Controla excepciones en WEBAPI

            //Exception exception = filterContext.Exception;
            //Error errorData = new Error();

            //Type exceptionType = null;
            //if (filterContext.Exception.InnerException != null)
            //{
            //    exceptionType = filterContext.Exception.InnerException.GetType();
            //    exception = filterContext.Exception.InnerException;
            //}
            //else
            //{
            //    exceptionType = filterContext.Exception.GetType();
            //    exception = filterContext.Exception;
            //}

            //// Excepciones HTTP
            //if (exceptionType != null && exceptionType.Equals(typeof(HttpException)))
            //{
            //    HttpException httpEx = filterContext.Exception as HttpException;
            //    if (httpEx.GetHttpCode() == 403)
            //    {
            //        filterContext.Response = filterContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Acceso denegado");
            //        return;
            //    }
            //}
            //else
            //{
            //    errorData = ErrorFactory.GuardarErrorEnDB(filterContext.Exception, Usuario.UserName);
            //}

            //if (exceptionType != null && exceptionType.Equals(typeof(SqlException)))
            //{
            //    // Error de timeout
            //    SqlException sqlEx = exception as SqlException;
            //    if (sqlEx.Number == -2)
            //    {
            //        errorData.extra = MensajeFactory.GetSqlTimeoutMessage().textoMensaje;
            //    }
            //    else
            //    {
            //        errorData.extra = MensajeFactory.GetGeneralExceptionMessage(errorData.idError, "ExDB").textoMensaje;
            //    }
            //}

            //if (exceptionType != null && exceptionType.Equals(typeof(Win32Exception)))
            //{
            //    Win32Exception w32ex = exception as Win32Exception;
            //    if (w32ex.NativeErrorCode == 258)
            //    {
            //        errorData.extra = MensajeFactory.GetSqlTimeoutMessage().textoMensaje;
            //    }
            //    else
            //    {
            //        errorData.extra = MensajeFactory.GetGeneralExceptionMessage(errorData.idError, "ExDB").textoMensaje;
            //    }
            //}

            //string msg = "En este momento no podemos atender su solicitud, Código de error: <strong>" + errorData.idError + "</strong><br>Por favor intente realizar la operación más tarde. " + errorData.extra;

            //filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.InternalServerError, new { errorMsg = msg });
        }
    }
}
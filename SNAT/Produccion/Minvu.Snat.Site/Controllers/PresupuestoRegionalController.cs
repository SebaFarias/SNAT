using Minvu.Security;
using Minvu.Snat.Domain.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minvu.Snat.Site.Controllers
{
    public class PresupuestoRegionalController : Controller
    {
        // GET: PresupuestoRegional
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IngresoPresupuestoRegional()
        {
            object auxConsultaInformacionProyecto = null;

            int yearNow = Int16.Parse(DateTime.Now.ToString("yyyy"));
            int yearNext = yearNow + 1;
            ViewBag.yearNow = yearNow;
            ViewBag.yearNext = yearNext;
            string usuario = @SiteHelper.UserName;
            bool usuarioFull = false;
            if (SingleSignOn.CurrentPrincipal.HasTarea("Atp_ConPresupuesto"))
            {
                usuarioFull = true;
            }
            else if (SingleSignOn.CurrentPrincipal.HasTarea("Atp_ConPresupuestoReg"))
            {
                usuarioFull = false;
            }
            DateTime fechaResolucion = DateTime.Now;
            //auxConsultaInformacionProyecto = PresupuestoRegionalFactory.getRegiones(usuario);

            auxConsultaInformacionProyecto = PresupuestoRegionalFactory.obtienePresupuestoRegional(7, yearNow, 0, fechaResolucion, 0, usuario, usuarioFull);

            
            return View(auxConsultaInformacionProyecto);
        }
        //[ValidarTarea("", "", "Administrador")]
        public ActionResult ConsultaPresupuestoRegional(PresupuestoRegional _PresupuestoRegional)
        {



            object auxConsultaInformacionProyecto = null;

            int yearNow = Int16.Parse(DateTime.Now.ToString("yyyy"));
            int yearNext = yearNow + 1;
            ViewBag.yearNow = yearNow;
            ViewBag.yearNext = yearNext;
            DateTime fechaResolucion = DateTime.Now;
            var todasRegiones = Request.Form["todos"];
            var anno1 = Request.Form["annoPresupuesto"];
            string usuario = @SiteHelper.UserName;
            bool usuarioFull = false;
            auxConsultaInformacionProyecto = PresupuestoRegionalFactory.getRegiones(usuario);


            //if ((SingleSignOn.CurrentPrincipal.HasTarea("Ig_ConPresupuesto")) && SingleSignOn.CurrentPrincipal.HasTarea("Ig_ConPresupuestoReg"))
            if (SingleSignOn.CurrentPrincipal.HasTarea("Atp_ConPresupuesto"))
            {
                usuarioFull = true;
            }
            else if (SingleSignOn.CurrentPrincipal.HasTarea("Atp_ConPresupuestoReg"))
            {
                usuarioFull = false;
            }

            if (_PresupuestoRegional._presupuestoRegionalEntities != null)
            {
                int numeroResolucion = Convert.ToInt32(_PresupuestoRegional._presupuestoRegionalEntities.idPresupuestoRegionalConsulta);
                int anno = _PresupuestoRegional._presupuestoRegionalEntities.annoPresupuesto;
                fechaResolucion = _PresupuestoRegional._presupuestoRegionalEntities.fechaResolucionPresupuestoRegional;
                int codigoRegion = _PresupuestoRegional._presupuestoRegionalEntities.idRegion;

                if (numeroResolucion > 0 && codigoRegion != 0)
                {
                    auxConsultaInformacionProyecto = PresupuestoRegionalFactory.obtienePresupuestoRegional(4, anno, numeroResolucion, fechaResolucion, codigoRegion, usuario, usuarioFull);
                }
                else if (codigoRegion > 0)
                {
                    auxConsultaInformacionProyecto = PresupuestoRegionalFactory.obtienePresupuestoRegional(6, anno, numeroResolucion, fechaResolucion, codigoRegion, usuario, usuarioFull);
                }
                else if (codigoRegion == 0 && numeroResolucion > 0 && usuarioFull == true)
                {
                    auxConsultaInformacionProyecto = PresupuestoRegionalFactory.obtienePresupuestoRegional(5, anno, numeroResolucion, fechaResolucion, 0, usuario, usuarioFull);
                }
                else if (codigoRegion == 0 && usuarioFull == true && numeroResolucion == 0)
                {
                    auxConsultaInformacionProyecto = PresupuestoRegionalFactory.obtienePresupuestoRegional(7, anno, 0, fechaResolucion, 0, usuario, usuarioFull);
                }
                else
                {
                    auxConsultaInformacionProyecto = PresupuestoRegionalFactory.obtienePresupuestoRegional(5, anno, numeroResolucion, fechaResolucion, 0, usuario, usuarioFull);
                }
                PresupuestoRegional _presupuestoRegional = auxConsultaInformacionProyecto as PresupuestoRegional;
                
               
                _presupuestoRegional._presupuestoRegionalEntities.idPresupuestoRegionalConsulta = _PresupuestoRegional._presupuestoRegionalEntities.idPresupuestoRegionalConsulta;

                if(_PresupuestoRegional._presupuestoRegionalEntities.idPresupuestoRegionalConsulta == null)
                {
                    foreach (var item in _presupuestoRegional._obtienePresupuestoRegionalEntities)
                    {
                        _presupuestoRegional._presupuestoRegionalEntities.idPresupuestoRegionalConsulta = item.idResolucionPresupuestaria.ToString();
                        break;
                    }
                }
                auxConsultaInformacionProyecto = _presupuestoRegional;

                if (_presupuestoRegional._obtienePresupuestoRegionalEntities.Count > 0)
                 {
                    string nombreArchivo = _presupuestoRegional._obtienePresupuestoRegionalEntities[0].nombreArchivo;


                    Session["VerificarExistenciaArchivo"] = verificarExistenciaArchivo(nombreArchivo);
                }
            }
            else
            {

                auxConsultaInformacionProyecto = PresupuestoRegionalFactory.obtienePresupuestoRegional(7, yearNow, 0, fechaResolucion, 0, usuario, usuarioFull);

                PresupuestoRegional _presupuestoRegional = auxConsultaInformacionProyecto as PresupuestoRegional;
                foreach (var item in _presupuestoRegional._obtienePresupuestoRegionalEntities)
                {
                    _presupuestoRegional._presupuestoRegionalEntities.annoPresupuesto= item.annoPresupuesto;
                    //_presupuestoRegional._presupuestoRegionalEntities.idRegion = item.codigoRegionPresupuestoRegional;
                    _presupuestoRegional._presupuestoRegionalEntities.idPresupuestoRegionalConsulta = item.idResolucionPresupuestaria.ToString();
                    break;
                }
                auxConsultaInformacionProyecto = _presupuestoRegional;

                if (_presupuestoRegional._obtienePresupuestoRegionalEntities.Count > 0)
                {
                    string nombreArchivo = _presupuestoRegional._obtienePresupuestoRegionalEntities[0].nombreArchivo;


                    Session["VerificarExistenciaArchivo"] = verificarExistenciaArchivo(nombreArchivo);
                }

            }


            return View(auxConsultaInformacionProyecto);
        }

        //public ActionResult ObtienePresupuestoRegional(PresupuestoRegional _PresupuestoRegional)
        //{
        //    int numeroResolucion = Convert.ToInt32(_PresupuestoRegional._presupuestoRegionalEntities.idPresupuestoRegional);
        //    int anno = _PresupuestoRegional._presupuestoRegionalEntities.annoPresupuesto;
        //    DateTime fechaResolucion = _PresupuestoRegional._presupuestoRegionalEntities.fechaResolucionPresupuestoRegional;
        //    int codigoRegion = _PresupuestoRegional._presupuestoRegionalEntities.codigoRegionPresupuestoRegional;
        //    object auxConsultaInformacionProyecto = null;

        //    auxConsultaInformacionProyecto = PresupuestoRegionalFactory.obtienePresupuestoRegional(4, anno, numeroResolucion, fechaResolucion, codigoRegion);

        //    return View("ConsultaPresupuestoRegional", auxConsultaInformacionProyecto);
        //}

        [HttpPost]
        public ActionResult GuardarPresupuestoRegional(PresupuestoRegional _PresupuestoRegional, HttpPostedFileBase file)
        {

            int yearNow = Int16.Parse(DateTime.Now.ToString("yyyy"));
            int yearNext = yearNow + 1;
            ViewBag.yearNow = yearNow;
            ViewBag.yearNext = yearNext;
            object auxGuardarPresupuestoRegional = null;
            var idContratoRequest = Request.Form["dataHidden"];
            Int16 anno = 0;
            Int32 numeroResolucion = 0;
            DateTime fechaResolucion = default(DateTime);
            Int32 codigoRegion = 0;
            long montoRegional = 0;
            string observacion = string.Empty;
            string usuario = string.Empty;
            string nombreArchivo = string.Empty;
            string codigoSalida = string.Empty;

            string[] infoRequest = idContratoRequest.Split('|');

            if (file != null)
            {
                string[] nombre = file.FileName.ToLower().Split('\\');
                int largo = nombre.Length;

                if (largo > 0)
                {
                    nombreArchivo = nombre[largo - 1];
                }


                if (infoRequest.Length > 0)
                {
                    foreach (var items in infoRequest)
                    {
                        string[] datos = items.Split('¬');

                        if (datos.Length > 1)
                        {
                            anno = Convert.ToInt16(datos[0]);
                            numeroResolucion = Convert.ToInt32(datos[1]);
                            fechaResolucion = Convert.ToDateTime(datos[2]);
                            codigoRegion = Convert.ToInt32(datos[3]);
                            if (datos[4] != "undefined")
                            { montoRegional = Convert.ToInt64(datos[4]); }
                            else { montoRegional = 0; }
                            observacion = datos[5];
                            usuario = @SiteHelper.UserName;

                            auxGuardarPresupuestoRegional = PresupuestoRegionalFactory.insertaPresupuestoRegional(anno, numeroResolucion, fechaResolucion, codigoRegion, montoRegional, observacion, anno + "_RP" + numeroResolucion + "_" + nombreArchivo, usuario);

                        }
                    }

                    if (auxGuardarPresupuestoRegional.GetType().GetProperty("codSalida").GetValue(auxGuardarPresupuestoRegional, null).ToString() == "0")
                    {
                        file.SaveAs(Server.MapPath("~/Uploads/" + anno + "_RP" + numeroResolucion + "_" + nombreArchivo));
                    }
                }
            }

            return View("IngresoPresupuestoRegional", auxGuardarPresupuestoRegional);

        }

        public FileResult Download(string nombre)
        {

            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(@"~/Uploads/" + nombre));
            //byte[] fileBytes = System.IO.File.ReadAllBytes("C:/logs/" + nombre);
            string fileName = nombre;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }

        public bool verificarExistenciaArchivo(string nombre)
        {
            try
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(@"~/Uploads/" + nombre));
                return true;
            }
            catch (Exception)
            {
                return false;
            }




        }

        public JsonResult ConsultaNumeroResolucion(int numeroResolucion, int anno)
        {
            object auxConsultaNumeroResolucion = null;

            auxConsultaNumeroResolucion = PresupuestoRegionalFactory.obtieneNumeroResolucion(numeroResolucion, anno);

            return Json(auxConsultaNumeroResolucion);

        }
    }
}
using Minvu.Security;
using Minvu.Snat.Domain.Entities;
using Minvu.Snat.Domain.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minvu.Snat.Site.Controllers
{
    public class ContratoSolicitudController : Controller
    {
        // GET: PresupuestoRegional
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetProvincias(string idRegion)
        {
            List<ProvinciasEntities> Provincias = new List<ProvinciasEntities>();

            if (idRegion != null && idRegion != String.Empty)
            {
                //BusquedaSolicitudPago objBusquedaAutorizaciondePago = new BusquedaSolicitudPago();
                Provincias = ConsultaContratoFactory.getListProvinciasRegion(int.Parse(idRegion));
            }

            return Json(new SelectList(Provincias, "idProvincia", "nombreProvincia"));
        }
        public JsonResult GetComunasRegion(string idRegion)
        {
            //Primero se obtienen las provincias
            List<ProvinciasEntities> lstProvincias = new List<ProvinciasEntities>();

            if (idRegion != null && idRegion != String.Empty)
            {
                lstProvincias = ConsultaContratoFactory.getListProvinciasRegion(int.Parse(idRegion));
            }

            //Lista con todas las comunas de la Region
            List<ComunasEntities> lstComunasProvincias = new List<ComunasEntities>();

            foreach (ProvinciasEntities Provincia in lstProvincias)
            {
                //Obtengo comunas de la Provincia
                List<ComunasEntities> lstComunas = new List<ComunasEntities>();
                lstComunas = ConsultaContratoFactory.getListComunasProvincia(Convert.ToInt32(Provincia.idProvincia));

                //Se agregan las Comunas al Total de comunas
                foreach (ComunasEntities Comuna in lstComunas)
                {
                    lstComunasProvincias.Add(Comuna);
                }
            }

            return Json(new SelectList(lstComunasProvincias, "idComuna", "nombreComuna"));
        }
        public JsonResult GetComunas(string idProvincia)
        {
            List<ComunasEntities> Comunas = new List<ComunasEntities>();

            if (idProvincia != null)
            {
                if (idProvincia == String.Empty)
                {
                    Comunas = ConsultaContratoFactory.getListComunas();
                }
                else
                {

                    Comunas = ConsultaContratoFactory.getListComunasProvincia(int.Parse(idProvincia));
                }
            }

            return Json(new SelectList(Comunas, "idComuna", "nombreComuna"));
        }

        public JsonResult SetProvinciaComuna(string idComuna)
        {
            ComunasEntities objComuna = new ComunasEntities();

            if (idComuna != null)
            {
                if (idComuna != String.Empty)
                {
                    objComuna = ConsultaContratoFactory.getComuna(int.Parse(idComuna));
                }
            }

            return Json(objComuna.idProvincia);
        }

        public ActionResult IngresoContrato()
        {
            object auxConsultaInformacionProyecto = null;

            int yearNow = Int16.Parse(DateTime.Now.ToString("yyyy"));
            int yearNext = yearNow + 1;
            ViewBag.yearNow = yearNow;
            ViewBag.yearNext = yearNext;
            string usuario = @SiteHelper.UserName;

            auxConsultaInformacionProyecto = ConsultaContratoFactory.getInfoInicial(usuario, yearNow, false);

            return View(auxConsultaInformacionProyecto);
        }

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
            if (SingleSignOn.CurrentPrincipal.HasTarea("Ig_ConPresupuesto"))
            {
                usuarioFull = true;
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
                    auxConsultaInformacionProyecto = PresupuestoRegionalFactory.obtienePresupuestoRegional(5, yearNow, 0, fechaResolucion, 0, usuario, usuarioFull);
                }
            }
            else
            {
                auxConsultaInformacionProyecto = PresupuestoRegionalFactory.obtienePresupuestoRegional(7, yearNow, 0, fechaResolucion, 0, usuario, usuarioFull);
            }

            return View(auxConsultaInformacionProyecto);
        }

        [HttpPost]
        public ActionResult GuardarContrato(ConsultaContrato _ConsultaContrato, HttpPostedFileBase file)
        {
            int yearNow = Int16.Parse(DateTime.Now.ToString("yyyy"));
            int yearNext = yearNow + 1;
            ViewBag.yearNow = yearNow;
            ViewBag.yearNext = yearNext;
            object auxGuardarContrato = null;
            var textoOtro = Request.Form["txtOtro"];
            var monto = Request.Form["montoHidden"];
            Int32 numeroResolucion = 0;
            DateTime fechaResolucion = default(DateTime);
            DateTime fechaInicio = default(DateTime);
            string observacion = string.Empty;
            string usuario = @SiteHelper.UserName;
            string nombreArchivo = string.Empty;

            int rut = Int32.Parse(_ConsultaContrato._contratoEntities.rutProfesional);
            string dv = _ConsultaContrato._contratoEntities.dvProfesional;
            string nombreProv = _ConsultaContrato._contratoEntities.nombreRazonSocial;
            int tipoProv = _ConsultaContrato._contratoEntities.idTipoProveedor;
            int anno = yearNow;
            int codigoProvincia = _ConsultaContrato._contratoEntities.idProvincia;
            int codigoRegion = _ConsultaContrato._contratoEntities.idRegion;
            int codigoComuna = _ConsultaContrato._contratoEntities.idComuna;
            int plazo = _ConsultaContrato._contratoEntities.plazoEjecucion;
            string propiedad = _ConsultaContrato._contratoEntities.idPropiedadTerreno.ToString();
            numeroResolucion = _ConsultaContrato._contratoEntities.numeroResolucion;
            fechaResolucion = _ConsultaContrato._contratoEntities.fechaResolucionContrato;
            fechaInicio = _ConsultaContrato._contratoEntities.fechaInicioContrato;
            long tipoServicio = Int32.Parse(_ConsultaContrato._contratoEntities.tipoServicio);
            int nombreServicio1 = Int32.Parse(_ConsultaContrato._contratoEntities.regulaPropiedades.ToString().Replace("True", "1").Replace("False", "0"));
            int nombreServicio2 = Int32.Parse(_ConsultaContrato._contratoEntities.regulaEstudios.ToString().Replace("True", "1").Replace("False", "0"));
            int nombreServicio3 = Int32.Parse(_ConsultaContrato._contratoEntities.revisionProyectos.ToString().Replace("True", "1").Replace("False", "0"));
            int nombreServicio4 = Int32.Parse(_ConsultaContrato._contratoEntities.elaboraProyectos.ToString().Replace("True", "1").Replace("False", "0")); 
            int nombreServicio5 = Int32.Parse(_ConsultaContrato._contratoEntities.identificacionDemanda.ToString().Replace("True", "1").Replace("False", "0"));
            int nombreServicio6 = Int32.Parse(_ConsultaContrato._contratoEntities.estudiosTecnicos.ToString().Replace("True", "1").Replace("False", "0"));
            int nombreServicio7 = Int32.Parse(_ConsultaContrato._contratoEntities.elaborarDiagnosticos.ToString().Replace("True", "1").Replace("False", "0")); 
            string nombreServicio8 = textoOtro;
            string producto = _ConsultaContrato._contratoEntities.producto;
            string descripcion = _ConsultaContrato._contratoEntities.descripcionProducto;
            ///int monto = _ConsultaContrato._contratoEntities.montoContrato;
            observacion = _ConsultaContrato._contratoEntities.observacion;

            if (file != null)
            {
                string[] nombre = file.FileName.ToLower().Split('\\');
                int largo = nombre.Length;

                if (largo > 0)
                {
                    nombreArchivo = nombre[largo - 1];
                }
            }

            auxGuardarContrato = ConsultaContratoFactory.insertaContrato(rut, dv, nombreProv, tipoProv, anno, codigoProvincia, codigoRegion, codigoComuna, plazo, propiedad,
                numeroResolucion, fechaResolucion, fechaInicio, nombreArchivo, tipoServicio, nombreServicio1, nombreServicio2, nombreServicio3, nombreServicio4, nombreServicio5,
                nombreServicio6, nombreServicio7, nombreServicio8, producto, descripcion, Int32.Parse(monto), observacion, usuario);


            if (auxGuardarContrato.GetType().GetProperty("codSalida").GetValue(auxGuardarContrato, null).ToString() == "0")
            {
                file.SaveAs(Server.MapPath("~/Uploads/" + anno + "_" + numeroResolucion + "_" + nombreArchivo));
            }

            return View("IngresoContrato", auxGuardarContrato);

        }
    }
}
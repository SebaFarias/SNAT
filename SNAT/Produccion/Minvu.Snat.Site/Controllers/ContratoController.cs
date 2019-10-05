using Minvu.Snat.Helper;
using Minvu.Security;
using Minvu.Snat.Domain.Entities;
using Minvu.Snat.Domain.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using System.Configuration;

namespace Minvu.Snat.Site.Controllers
{
    public class ContratoController : Controller
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
            ViewBag.yearNow = Int16.Parse(DateTime.Now.ToString("yyyy"));
            ViewBag.yearNext = ViewBag.yearNow + 1;

            return View(ConsultaContratoFactory.getInfoInicial(@SiteHelper.UserName, ViewBag.yearNow, SingleSignOn.CurrentPrincipal.HasTarea("Atp_IngContrato")));
        }


        public ActionResult SolicitudATPrevia(string idSolicitud)
        {
            object auxSolicitudPago = null;

            if (SingleSignOn.CurrentPrincipal.HasTarea("Atp_IngresoSolicitudPago") || SingleSignOn.CurrentPrincipal.HasTarea("Atp_Ratificador"))
            {
                auxSolicitudPago = ConsultaSolicitudContratoFactory.ObtieneCabeceraNacional(Convert.ToInt64(DateTime.Now.ToString("yyyy")), @SiteHelper.UserName);
            }
            else if (SingleSignOn.CurrentPrincipal.HasTarea("Atp_IngresoSolicitudPagoReg") || SingleSignOn.CurrentPrincipal.HasTarea("Atp_RatificadorReg"))
            {
                auxSolicitudPago = ConsultaSolicitudContratoFactory.ObtieneCabeceraRegional(Convert.ToInt64(DateTime.Now.ToString("yyyy")), @SiteHelper.UserName);
            }


            if (idSolicitud != string.Empty && idSolicitud != null)
            {

                Session["SesionUsuario"] = Session;
                string solicitudCompuesto = new string(idSolicitud.ToString().Reverse().ToArray());
                Session["IdSolicitudPaginaUsuario"] = idSolicitud.ToString();
                string year = new string(solicitudCompuesto.Substring(0, 4).Reverse().ToArray());
                string region = new string(solicitudCompuesto.Substring(4, 2).Reverse().ToArray());
                string solicitud = new string(solicitudCompuesto.Substring(6, solicitudCompuesto.Length - 6).Reverse().ToArray());
                Session["idSolicitudATP"] = solicitud;

                comun _comun = new comun();
                _comun = _comun.obtenerComun(Int16.Parse(year));

                ViewBag.yearNow = _comun.yearNow;
                ViewBag.dateNow = ""; //date ;


                auxSolicitudPago = ConsultaSolicitudContratoFactory.ConsultaSolicitudPago(_comun.usuario, _comun.yearNow, Int32.Parse(region), Int32.Parse(solicitud));

                ConsultaContrato consultaContrato = auxSolicitudPago as ConsultaContrato;
                ConsultaContrato aux = new ConsultaContrato();
                aux = ConsultaContratoFactory.getMontoPresupuesto(_comun.yearNow, consultaContrato._contratoSolicitudPago.idRegion) as ConsultaContrato;
                consultaContrato._contratoSolicitudPago.montoDisponibleContratoRegion = aux._contratoSolicitudPago.montoDisponibleContratoRegion;
                auxSolicitudPago = ConsultaSolicitudContratoFactory.ObtieneContrato(Convert.ToInt64(consultaContrato._contratoSolicitudPago.numeroResolucionContrato), Convert.ToInt64(consultaContrato._contratoSolicitudPago.idRegion), consultaContrato);




                Session["ConsultaSolicitudContrato"] = auxSolicitudPago;
            }

            return View(auxSolicitudPago);
        }

        public ActionResult ConsultaSolicitudPago(long idSolicitud)
        {

            Session["SesionUsuario"] = Session;
            string solicitudCompuesto = new string(idSolicitud.ToString().Reverse().ToArray());
            Session["IdSolicitudPaginaUsuario"] = idSolicitud.ToString();

            string region = new string(solicitudCompuesto.Substring(4, 2).Reverse().ToArray());
            string solicitud = new string(solicitudCompuesto.Substring(6, solicitudCompuesto.Length - 6).Reverse().ToArray());
            Session["idSolicitudATP"] = solicitud;
            comun _comun = new comun();
            _comun = _comun.obtenerComun(Int16.Parse(solicitudCompuesto.Substring(0, 4).Reverse().ToArray().ToString()));
            ViewBag.yearNow = _comun.yearNow;
            ViewBag.dateNow = "";
            object auxSolicitudPago = null;

            auxSolicitudPago = ConsultaSolicitudContratoFactory.ConsultaSolicitudPago(_comun.usuario, _comun.yearNow, Int32.Parse(region), Int32.Parse(solicitud));
            Session["ConsultaSolicitudContrato"] = auxSolicitudPago;

            return View(auxSolicitudPago);
        }

        public ActionResult reporteAutorizacion()
        {
            try
            {
                ConsultaContrato _consultaContrato = new ConsultaContrato();
                if (Session["ConsultaSolicitudContrato"] != null && Session["idSolicitudATP"] != null && Session.Timeout > 0)
                {
                    _consultaContrato = Session["ConsultaSolicitudContrato"] as ConsultaContrato;
                    string tipoServicio = "";
                    string Region = "";

                    if (_consultaContrato._contratoSolicitudPagoList.Count > 0)
                        foreach (var item in _consultaContrato._contratoSolicitudPagoList)
                        {
                            tipoServicio = item.nombreServicio;
                            break;
                        }
                    int contadorInicial = _consultaContrato._contratoSolicitudPago.nombreRegionPresupuestoPrint.Length;

                    Region = _consultaContrato._contratoSolicitudPago.nombreRegionPresupuestoPrint.ToString().Replace("Región del ", "");

                    if (contadorInicial == Region.Length)
                        Region = _consultaContrato._contratoSolicitudPago.nombreRegionPresupuestoPrint.ToString().Replace("Región de ", "");

                    if (contadorInicial == Region.Length)
                        Region = _consultaContrato._contratoSolicitudPago.nombreRegionPresupuestoPrint.ToString().Replace("Región ", "");


                    int IdTiapoProveedor = Convert.ToInt32(Session["IdTipoProveedor"]);

                    string ServidorReporte = ConfigurationManager.AppSettings["RutaServidorReporte"];
                    string ServerUrl = ConfigurationManager.AppSettings["URL_Report_Server"];
                    string montoContratoParcial = _consultaContrato._contratoSolicitudPago.montoContrato.ToString("C").Replace('$', ' ');
                    montoContratoParcial = montoContratoParcial.Replace(".", "");
                    string montoAutorizacionParcial = _consultaContrato._contratoSolicitudPago.montoPorPagar.ToString("C").Replace('$', ' ');
                    montoAutorizacionParcial = montoAutorizacionParcial.Replace(".", "");
                    string montototalapagarparcial = _consultaContrato._contratoSolicitudPago.montoTotalaPagar.ToString("C").Replace('$', ' ');
                    montototalapagarparcial = montototalapagarparcial.Replace(".", "");


                    Report.parametro[] parametros = new Report.parametro[16];
                    parametros[0] = new Report.parametro("nIdSolicitudPago", Session["idSolicitudATP"].ToString());
                    parametros[1] = new Report.parametro("nRegion", Region);
                    parametros[2] = new Report.parametro("nResolucionContrato", _consultaContrato._contratoSolicitudPago.numeroResolucionContrato.ToString());
                    parametros[3] = new Report.parametro("nFechaResolucionContrato", _consultaContrato._contratoSolicitudPago.fechaResolucionContrato.ToShortDateString());
                    parametros[4] = new Report.parametro("nMontoContrato", montoContratoParcial.Trim()); //
                    parametros[5] = new Report.parametro("nRutProveedor", _consultaContrato._contratoSolicitudPago.rutContrato.ToString());
                    parametros[6] = new Report.parametro("nMontoAutorizacionPago", montoAutorizacionParcial.Trim()); //
                    parametros[7] = new Report.parametro("nFechaDevengo", _consultaContrato._contratoSolicitudPago.nombreMaestroEstadoContrato);
                    parametros[8] = new Report.parametro("nTipoEmpresaProveedora", _consultaContrato._contratoSolicitudPago.nombreTipoProveedor);

                    if (tipoServicio == "SAT")
                    {
                        parametros[9] = new Report.parametro("nTipoServicio", "Asistencia Técnica");
                    }
                    else
                    {
                        parametros[9] = new Report.parametro("nTipoServicio", "Servicios de Estudios Técnicos");
                    }

                    parametros[10] = new Report.parametro("FechaImpresion", DateTime.Now.ToShortDateString());
                    parametros[11] = new Report.parametro("UsuarioImpresion", @SiteHelper.NombreCompletoUsuario);
                    parametros[12] = new Report.parametro("nNombreEmpresaProveedora", _consultaContrato._contratoSolicitudPago.nombreRazonSocial);
                    parametros[13] = new Report.parametro("IdSolicitudPaginaUsuario", Session["IdSolicitudPaginaUsuario"].ToString());
                    parametros[14] = new Report.parametro("MontoTotalPagar", montototalapagarparcial.Trim());

                    if (_consultaContrato._contratoSolicitudPago.estadoSolicitud == 7)
                        parametros[15] = new Report.parametro("UsuarioRatificante", _consultaContrato._contratoSolicitudPago.responsableNombreCompletoVB);
                    else
                        parametros[15] = new Report.parametro("UsuarioRatificante", " ");


                    Report report = new Report(ServidorReporte, ServerUrl, System.Configuration.ConfigurationManager.AppSettings["PATH_RPT_CertificadoSolicitudATP"], parametros);



                    byte[] buffer = report.ObtenerBytesPDF();

                    return File(buffer, "application/octet-stream", "certificado.pdf");

                }
                else
                {
                    Session.Clear();
                    return RedirectToAction("ListadoSolicitudPago", "Contrato");
                }
            }
            catch (Exception ex)
            {
                Session.Clear();

                return RedirectToAction("Index", "Home");

            }


        }
        public ActionResult ConsultaContratoPago(long idContrato)
        {

            string contratoCompuesto = new string(idContrato.ToString().Reverse().ToArray());

            string year = new string(contratoCompuesto.Substring(0, 4).Reverse().ToArray());
            string region = new string(contratoCompuesto.Substring(4, 2).Reverse().ToArray());
            string contrato = new string(contratoCompuesto.Substring(6, contratoCompuesto.Length - 6).Reverse().ToArray());
            comun _comun = new comun();
            _comun = _comun.obtenerComun(Convert.ToInt64(year));

            ViewBag.yearNow = _comun.yearNow;
            ViewBag.dateNow = _comun.date;
            object auxSolicitudPago = null;

            auxSolicitudPago = ConsultaSolicitudContratoFactory.ObtieneContrato(Int32.Parse(contrato));

            return View(auxSolicitudPago);
        }

        public ActionResult SolicitudPago()
        {

            comun _comun = new comun();

            _comun = _comun.obtenerComun();


            ViewBag.yearNow = _comun.yearNow;
            ViewBag.dateNow = _comun.date;
            object auxSolicitudPago = null;

            if (SingleSignOn.CurrentPrincipal.HasTarea("Atp_IngresoSolicitudPago") || SingleSignOn.CurrentPrincipal.HasTarea("Atp_Ratificador"))
            {
                auxSolicitudPago = ConsultaSolicitudContratoFactory.ObtieneCabeceraNacional(_comun.yearNow, _comun.usuario);
            }
            else if (SingleSignOn.CurrentPrincipal.HasTarea("Atp_IngresoSolicitudPagoReg") || SingleSignOn.CurrentPrincipal.HasTarea("Atp_RatificadorReg"))
            {
                auxSolicitudPago = ConsultaSolicitudContratoFactory.ObtieneCabeceraRegional(_comun.yearNow, _comun.usuario);
            }

            return View(auxSolicitudPago);
        }

        public ActionResult ListadoSolicitudPago(ConsultaContrato auxConsultaSolicitud)
        {

            ConsultaContrato _consultaContrato = new ConsultaContrato();

            comun _comun = new comun();
            _comun = _comun.obtenerComun();

            ViewBag.yearNow = _comun.yearNow;
            ViewBag.dateNow = _comun.date;
            ViewBag.yearNext = _comun.yearNext;


            if (SingleSignOn.CurrentPrincipal.HasTarea("Atp_ConSolicitudPago") || SingleSignOn.CurrentPrincipal.HasTarea("Atp_IngresoSolicitudPago") || SingleSignOn.CurrentPrincipal.HasTarea("Atp_Ratificador"))
            {


                _consultaContrato = ConsultaSolicitudContratoFactory.ObtieneCabeceraNacional(_comun.yearNow, _comun.usuario);

                if (auxConsultaSolicitud._contratoSolicitudPago != null)
                {
                    if (auxConsultaSolicitud._contratoSolicitudPago.idRegion != 0)
                    {
                        ConsultaContrato STR = new ConsultaContrato();
                        STR = (ConsultaContrato)ConsultaContratoFactory.getMontoPresupuesto(Int16.Parse(auxConsultaSolicitud._contratoSolicitudPago.annoPresupuesto.ToString()), auxConsultaSolicitud._contratoSolicitudPago.idRegion);

                        //_consultaContrato._contratoSolicitudPago.montoDisponibleContratoRegion = STR._contratoSolicitudPago.montoDisponibleContratoRegion;
                        //_consultaContrato._contratoSolicitudPago.montoPresupuestoRegional = STR._contratoSolicitudPago.montoPresupuestoRegional;
                        //_consultaContrato._contratoSolicitudPago.montoTotalContratoRegion = STR._contratoSolicitudPago.montoTotalContratoRegion;
                        //_consultaContrato._contratoSolicitudPago.montoPresupuesto = STR._contratoSolicitudPago.montoPresupuesto;
                        //_consultaContrato._contratoSolicitudPago.saldoPresupuesto = STR._contratoSolicitudPago.saldoPresupuesto;
                        //_consultaContrato._contratoSolicitudPago.saldoPresupuestoRegion = STR._contratoSolicitudPago.saldoPresupuestoRegion;

                        //auxConsultaSolicitud._contratoSolicitudPago.montoDisponibleContratoRegion = STR._contratoSolicitudPago.montoDisponibleContratoRegion;
                        //auxConsultaSolicitud._contratoSolicitudPago.montoPresupuestoRegional = STR._contratoSolicitudPago.montoPresupuestoRegional;
                        //auxConsultaSolicitud._contratoSolicitudPago.montoTotalContratoRegion = STR._contratoSolicitudPago.montoTotalContratoRegion;
                        //auxConsultaSolicitud._contratoSolicitudPago.montoPresupuesto = STR._contratoSolicitudPago.montoPresupuesto;
                        //auxConsultaSolicitud._contratoSolicitudPago.saldoPresupuesto = STR._contratoSolicitudPago.saldoPresupuesto;
                        //auxConsultaSolicitud._contratoSolicitudPago.saldoPresupuestoRegion = STR._contratoSolicitudPago.saldoPresupuestoRegion;

                        _consultaContrato._contratoSolicitudPago.montoContratoPresupuestoNacional = STR._contratoSolicitudPago.montoTotalContratoRegion;
                        _consultaContrato._contratoSolicitudPago.montoPresupuestoNacional = STR._contratoSolicitudPago.montoPresupuesto;
                        _consultaContrato._contratoSolicitudPago.saldoPresupuestoNacional = STR._contratoSolicitudPago.saldoPresupuestoRegion;


                    }
                }



            }
            else if (SingleSignOn.CurrentPrincipal.HasTarea("Atp_ConSolicitudPagoReg") || SingleSignOn.CurrentPrincipal.HasTarea("Atp_IngresoSolicitudPagoReg") || SingleSignOn.CurrentPrincipal.HasTarea("Atp_RatificadorReg"))
            {
                _consultaContrato = ConsultaSolicitudContratoFactory.ObtieneCabeceraRegional(_comun.yearNow, _comun.usuario);

            }

            if (auxConsultaSolicitud != null)
            {

                //if (SingleSignOn.CurrentPrincipal.HasTarea("Atp_ConSolicitudPagoReg") || SingleSignOn.CurrentPrincipal.HasTarea("Atp_IngresoSolicitudPagoReg") || SingleSignOn.CurrentPrincipal.HasTarea("Atp_RatificadorReg"))
                //{
                //    RegionesEntities _auxReg = new RegionesEntities();
                //    var idRegion = RegionesEntitiesFactory.getRegionUserID(SiteHelper.UserName);
                //    _auxReg = RegionesEntitiesFactory.getRegion(idRegion);
                //    auxConsultaSolicitud._regionEntities = _auxReg;

                //}



                if (auxConsultaSolicitud._contratoSolicitudPago != null || auxConsultaSolicitud._regionEntities != null)
                {

                    var accion = 1;
                    if (auxConsultaSolicitud != null)
                    {
                        if (SingleSignOn.CurrentPrincipal.HasTarea("Atp_ConSolicitudPagoReg") || SingleSignOn.CurrentPrincipal.HasTarea("Atp_IngresoSolicitudPagoReg") || SingleSignOn.CurrentPrincipal.HasTarea("Atp_RatificadorReg"))
                        {
                            RegionesEntities _auxReg = new RegionesEntities();
                            var idRegion = RegionesEntitiesFactory.getRegionUserID(SiteHelper.UserName);
                            _auxReg = RegionesEntitiesFactory.getRegion(idRegion);
                            auxConsultaSolicitud._regionEntities = _auxReg;
                        }
                    }

                    if ((auxConsultaSolicitud._contratoSolicitudPago.numeroResolucionContrato.ToString() != "")
                        && (auxConsultaSolicitud._regionEntities.idRegion.ToString() == ""))
                    {
                        accion = 2;
                        auxConsultaSolicitud._regionEntities.idRegion = 0;
                    }
                    else if ((auxConsultaSolicitud._contratoSolicitudPago.numeroResolucionContrato.ToString() == "")
                        && (auxConsultaSolicitud._regionEntities.idRegion.ToString() != ""))
                    {
                        accion = 3;
                        auxConsultaSolicitud._contratoSolicitudPago.numeroResolucionContrato = 0;
                    }
                    else if ((auxConsultaSolicitud._contratoSolicitudPago.numeroResolucionContrato.ToString() == "")
                        && (auxConsultaSolicitud._regionEntities.idRegion.ToString() == ""))
                    {
                        accion = 4;
                        auxConsultaSolicitud._contratoSolicitudPago.numeroResolucionContrato = 0;
                        auxConsultaSolicitud._regionEntities.idRegion = 0;
                    }

                    _consultaContrato = ConsultaSolicitudContratoFactory.ObtieneListadoSolicitud
                    (accion, (long)auxConsultaSolicitud._contratoSolicitudPago.numeroResolucionContrato, Convert.ToInt32(auxConsultaSolicitud._regionEntities.idRegion),
                    auxConsultaSolicitud._contratoSolicitudPago.annoPresupuesto, _consultaContrato);


                }

            }



            return View(_consultaContrato as object);
        }

        public ActionResult ListadoContratoPago()
        {
            try
            {
                comun _comun = new comun();
                _comun = _comun.obtenerComun();
                ViewBag.yearNow = _comun.yearNow;
                ViewBag.dateNow = _comun.date;
                ViewBag.yearNext = _comun.yearNext;

                object auxContratoPago = null;
                var user = Minvu.Security.SingleSignOn.CurrentPrincipal.MinvuIdentity.Name.Split('\\');
                if (SingleSignOn.CurrentPrincipal.HasTarea("Atp_ConContrato") || SingleSignOn.CurrentPrincipal.HasTarea("Atp_IngContrato"))
                {
                    Log.Instance.Info("metodo-controlador: ObtieneCabeceraNacional");
                    auxContratoPago = ConsultaSolicitudContratoFactory.ObtieneCabeceraNacional(_comun.yearNow, _comun.usuario);
                }
                else if (SingleSignOn.CurrentPrincipal.HasTarea("Atp_ConContratoReg") || SingleSignOn.CurrentPrincipal.HasTarea("Atp_IngContratoReg"))
                {
                    Log.Instance.Info("metodo-controlador: ObtieneCabeceraRegional");
                    auxContratoPago = ConsultaSolicitudContratoFactory.ObtieneCabeceraRegional(_comun.yearNow, _comun.usuario);
                }

                return View(auxContratoPago);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public JsonResult ConsultaContrato(long numeroResolucion, long idRegion)
        {

            object auxSolicitudPago = ConsultaSolicitudContratoFactory.ObtieneContrato(numeroResolucion, idRegion);

            Session["auxSolicitudPago"] = auxSolicitudPago;
            return Json(auxSolicitudPago);
        }

        public JsonResult ConsultaListadoContrato(int accion, int annoPresupuesto, int idRegion, long tipoServicio)
        {
            object auxConsultaContrato = ConsultaSolicitudContratoFactory.ObtieneListadoContrato(accion, annoPresupuesto, idRegion, tipoServicio);

            return Json(auxConsultaContrato);
        }


        public JsonResult ConfirmaEliminaContrato(int idContrato)
        {
            comun _comun = new comun();
            _comun = _comun.obtenerComun();

            ViewBag.yearNow = _comun;
            ViewBag.yearNext = _comun;

            object auxConsultaContrato = ConsultaSolicitudContratoFactory.EliminaContrato(idContrato, _comun.usuario, _comun.yearNow);

            return Json(auxConsultaContrato);
        }

        public JsonResult ConfirmaEliminaSolicitud(int idSolicitud, int idRegion, int idResolucion)
        {
            comun _comun = new comun();
            _comun = _comun.obtenerComun();

            ViewBag.yearNow = _comun.yearNow;
            ViewBag.yearNext = _comun.yearNext;

            object auxConsultaSolicitud = ConsultaSolicitudContratoFactory.EliminaSolicitud(idSolicitud, _comun.usuario, _comun.yearNow, idRegion, idResolucion);

            return Json(auxConsultaSolicitud);
        }

        [HttpPost]
        public ActionResult GuardarSolicitud(ConsultaContrato _ConsultaSolicitudContrato)
        {
            ConsultaContrato auxSolicitudPago = Session["auxSolicitudPago"] as ConsultaContrato;
            ConsultaContrato aux = Session["ConsultaSolicitudContrato"] as ConsultaContrato;
            //ConsultaContrato usuario = Session[""] as ConsultaContrato; capturar la 

            comun _comun = new comun();
            _comun = _comun.obtenerComun();

            ViewBag.yearNow = _comun.yearNow;
            ViewBag.dateNow = _comun.date;
            object auxGuardarSolicitud = null;
            if(auxSolicitudPago._contratoSolicitudPago.codigoSalida == "Imprimir")
            {
                ImprimirSolicitud();
            }   



            long montoPresupuesto = _ConsultaSolicitudContrato._contratoSolicitudPago.montoPresupuesto; //Int64.Parse(Request.Form["PresupuestoDisponibleHidden"]);
            long estadoSolicitud = Int64.Parse(Request.Form["idEstadoHidden"]);
            string actividades = Request.Form["dataHidden"];
            long montoTotalAPagar = 0;
            decimal avance = 0;
            if (Request.Form["avanceTotalHidden"] != null)
            {
                var casiavance = Request.Form["avanceTotalHidden"].ToString();
                if (casiavance != "")
                {

                    avance = Convert.ToDecimal(Request.Form["avanceTotalHidden"].ToString().Replace(".", ",").Replace("%", "").Replace(" ", ""));
                }
                else
                {
                    avance = 0;
                }
            }



            if (Request.Form["montoTotalPagarHidden"] != null)
            {
                montoTotalAPagar = Convert.ToInt64(Request.Form["montoTotalPagarHidden"]);
            }
            else
            {
                montoTotalAPagar = aux._contratoSolicitudPago.montoTotalaPagar;
            }


            long numeroResolucion = 0;
            if (_ConsultaSolicitudContrato._contratoSolicitudPago.numeroResolucionContrato != null)
                numeroResolucion = Convert.ToInt64(_ConsultaSolicitudContrato._contratoSolicitudPago.numeroResolucionContrato);

            DateTime? vbFecha = _ConsultaSolicitudContrato._contratoSolicitudPago.fechaVB;
            var reguser = RegionesEntitiesFactory.getRegionUserID(SiteHelper.UserName);
            ;// @SiteHelper.getRegion;

            if (_ConsultaSolicitudContrato._regionEntities != null)
            {
                

                auxGuardarSolicitud = ConsultaSolicitudContratoFactory.insertaSolicitudATP(
                    DateTime.Now.Year,
                    numeroResolucion,
                    _ConsultaSolicitudContrato._contratoSolicitudPago.numeroSolicitudPresupuesto,
                    _ConsultaSolicitudContrato._contratoSolicitudPago.idContrato,
                    _ConsultaSolicitudContrato._contratoSolicitudPago.annoPresupuesto,
                    montoPresupuesto,
                    (int)_ConsultaSolicitudContrato._regionEntities.idRegion,
                    auxSolicitudPago._contratoSolicitudPago.montoPagados,
                    auxSolicitudPago._contratoSolicitudPago.montoComprometidos,
                    auxSolicitudPago._contratoSolicitudPago.montoPorPagar,
                    actividades,
                    _ConsultaSolicitudContrato._contratoSolicitudPago.numeroBolFact,
                    _ConsultaSolicitudContrato._contratoSolicitudPago.fechaBolFact,
                    montoTotalAPagar,
                    _ConsultaSolicitudContrato._contratoSolicitudPago.vbServicio,
                    @SiteHelper.UserName,
                    vbFecha, estadoSolicitud,
                    _ConsultaSolicitudContrato._auxmaestroTipoPagoEntities.idMaestroTipoPago, null);
            }
            else
            {
                if (_ConsultaSolicitudContrato._contratoSolicitudPago.vbServicio == true)
                {
                    auxGuardarSolicitud = ConsultaSolicitudContratoFactory.ratificaSolicitud(
                        DateTime.Now.Year,
                        (int)aux._contratoSolicitudPago.numeroResolucionContrato,
                        aux._contratoSolicitudPago.numeroSolicitudPresupuesto,
                        //!string.IsNullOrEmpty(aux._contratoSolicitudPago.numeroSolicitudPresupuesto) ? Convert.ToInt64(aux._contratoSolicitudPago.idSolicitudRegion) : 0,
                        aux._contratoSolicitudPago.annoPresupuesto,
                        aux._contratoSolicitudPago.idRegion,
                        aux._contratoSolicitudPago.montoTotalaPagar,
                        @SiteHelper.UserName);

                }
                else
                {
                    

                    auxGuardarSolicitud = ConsultaSolicitudContratoFactory.insertaSolicitudATP(
                        DateTime.Now.Year,
                        numeroResolucion,
                        _ConsultaSolicitudContrato._contratoSolicitudPago.numeroSolicitudPresupuesto,
                        _ConsultaSolicitudContrato._contratoSolicitudPago.idContrato,
                        _ConsultaSolicitudContrato._contratoSolicitudPago.annoPresupuesto,
                        montoPresupuesto,
                        reguser,
                        auxSolicitudPago._contratoSolicitudPago.montoPagados,
                        auxSolicitudPago._contratoSolicitudPago.montoComprometidos,
                        auxSolicitudPago._contratoSolicitudPago.montoPorPagar,
                        actividades,
                        _ConsultaSolicitudContrato._contratoSolicitudPago.numeroBolFact,
                        _ConsultaSolicitudContrato._contratoSolicitudPago.fechaBolFact,
                        montoTotalAPagar,
                        _ConsultaSolicitudContrato._contratoSolicitudPago.vbServicio,
                        @SiteHelper.UserName,
                        vbFecha, estadoSolicitud,
                        _ConsultaSolicitudContrato._auxmaestroTipoPagoEntities.idMaestroTipoPago, null);
                    
                }
            }






            return View("SolicitudPago", auxGuardarSolicitud);
        }


     
        public ActionResult ImprimirSolicitud()
        {

            // casilvav
            ConsultaContrato _auxConsultaSolicitudContrato = new  ConsultaContrato();

            if (_auxConsultaSolicitudContrato != null)
            {

                ConsultaContrato auxGuardarSolicitud = (ConsultaContrato)ConsultaSolicitudContratoFactory.ConsultaSolicitudPago(@SiteHelper.UserName, _auxConsultaSolicitudContrato._contratoSolicitudPago.annoPresupuesto, _auxConsultaSolicitudContrato._contratoSolicitudPago.idRegion, (int)_auxConsultaSolicitudContrato._contratoSolicitudPago.numeroSolicitudPresupuesto);

                var report = new PartialViewAsPdf("_PartialSolicitudPagoImprimir", auxGuardarSolicitud);

                return report;
            }
            else
            {
                return null;
            }

            //antes
            //object auxGuardarSolicitud = ConsultaSolicitudContratoFactory.ConsultaSolicitudPago(@SiteHelper.UserName, _ConsultaSolicitudContrato._contratoSolicitudPago.annoPresupuesto, _ConsultaSolicitudContrato._contratoSolicitudPago.idRegion, (int)_ConsultaSolicitudContrato._contratoSolicitudPago.numeroSolicitudPresupuesto);

            //var report = new PartialViewAsPdf("_PartialSolicitudPagoImprimir", auxGuardarSolicitud);
            //return report;
        }

        public JsonResult RatificaSolicitud(string numeroResolucion, string idSolicitud, string anno, string idRegion, string montoTotal, string vbResponsable)
        {
            comun _comun = new comun();
            _comun = _comun.obtenerComun();

            ViewBag.yearNow = _comun.yearNow;
            ViewBag.yearNext = _comun.yearNext;
            montoTotal = montoTotal.Replace(".", "");

            object auxConsultaSolicitud = ConsultaSolicitudContratoFactory.ratificaSolicitud(Convert.ToInt32(anno), Int32.Parse(numeroResolucion), Int64.Parse(idSolicitud), Int32.Parse(anno), Int32.Parse(idRegion), Convert.ToInt64(montoTotal), @SiteHelper.UserName);

            return Json(auxConsultaSolicitud);
        }

        public ActionResult ImprimirSolicitudModal()
        {

            var idSolicitud = Request.Form["idSolicitudHidden"];
            var idRegion = Request.Form["idRegionHidden"];
            var anno = Request.Form["idAnnoHidden"];
            object auxGuardarSolicitud = null;

            auxGuardarSolicitud = ConsultaSolicitudContratoFactory.ConsultaSolicitudPago(@SiteHelper.UserName, Int32.Parse(anno), Int32.Parse(idRegion), Int32.Parse(idSolicitud));

            var report = new PartialViewAsPdf("_PartialSolicitudPagoImprimir", auxGuardarSolicitud);
            return report;
        }

        [HttpPost]
        public ActionResult GuardarContrato(ConsultaContrato _ConsultaContrato, HttpPostedFileBase file)
        {
            comun _comun = new comun();
            _comun = _comun.obtenerComun();

            ViewBag.yearNow = _comun.yearNow;
            ViewBag.yearNext = _comun.yearNext;


            DateTime fechaResolucion = default(DateTime);
            DateTime fechaInicio = default(DateTime);


            string nombreArchivo = string.Empty;
            int nombreServicio1 = 0;
            int nombreServicio2 = 0;
            int nombreServicio3 = 0;
            int nombreServicio4 = 0;
            int nombreServicio5 = 0;
            int nombreServicio6 = 0;
            int nombreServicio7 = 0;
            string nombreServicio8 = string.Empty;


            fechaResolucion = _ConsultaContrato._contratoEntities.fechaResolucionContrato;
            fechaInicio = _ConsultaContrato._contratoEntities.fechaInicioContrato;


            if (Int64.Parse(_ConsultaContrato._contratoEntities.tipoServicio) == 1)
            {
                nombreServicio1 = Int32.Parse(_ConsultaContrato._contratoEntities.regulaPropiedades.ToString().Replace("True", "1").Replace("False", "0"));
                nombreServicio2 = Int32.Parse(_ConsultaContrato._contratoEntities.regulaEstudios.ToString().Replace("True", "1").Replace("False", "0"));
                nombreServicio3 = Int32.Parse(_ConsultaContrato._contratoEntities.revisionProyectos.ToString().Replace("True", "1").Replace("False", "0"));
                nombreServicio4 = Int32.Parse(_ConsultaContrato._contratoEntities.elaboraProyectos.ToString().Replace("True", "1").Replace("False", "0"));
                nombreServicio5 = Int32.Parse(_ConsultaContrato._contratoEntities.identificacionDemanda.ToString().Replace("True", "1").Replace("False", "0"));
            }

            if (Int64.Parse(_ConsultaContrato._contratoEntities.tipoServicio) == 2)
            {
                nombreServicio6 = Int32.Parse(_ConsultaContrato._contratoEntities.estudiosTecnicos.ToString().Replace("True", "1").Replace("False", "0"));
                nombreServicio7 = Int32.Parse(_ConsultaContrato._contratoEntities.elaborarDiagnosticos.ToString().Replace("True", "1").Replace("False", "0"));
            }

            if (Int64.Parse(_ConsultaContrato._contratoEntities.tipoServicio) == 2 && _ConsultaContrato._contratoEntities.otros)
            {
                nombreServicio8 = Request.Form["txtOtro"];
            }

            if (file != null)
            {
                string[] nombre = file.FileName.ToLower().Split('\\');
                int largo = nombre.Length;

                if (largo > 0)
                {
                    nombreArchivo = nombre[largo - 1];
                }
            }

            object auxGuardarContrato = ConsultaContratoFactory.insertaContrato(Convert.ToInt32(_ConsultaContrato._contratoEntities.rutProfesional), _ConsultaContrato._contratoEntities.dvProfesional, _ConsultaContrato._contratoEntities.nombreRazonSocial, _ConsultaContrato._contratoEntities.idTipoProveedor,
                (int)_comun.yearNow, Int32.Parse(Request.Form["idRegionHidden"]), _ConsultaContrato._contratoEntities.idProvincia, _ConsultaContrato._contratoEntities.idComuna, _ConsultaContrato._contratoEntities.plazoEjecucion, _ConsultaContrato._contratoEntities.idPropiedadTerreno.ToString(),
                _ConsultaContrato._contratoEntities.numeroResolucion, fechaResolucion, fechaInicio, (int)_comun.yearNow + "_C" + _ConsultaContrato._contratoEntities.numeroResolucion + "_" + nombreArchivo, Int64.Parse(_ConsultaContrato._contratoEntities.tipoServicio), nombreServicio1, nombreServicio2, nombreServicio3, nombreServicio4, nombreServicio5,
                nombreServicio6, nombreServicio7, nombreServicio8, _ConsultaContrato._contratoEntities.producto, _ConsultaContrato._contratoEntities.descripcionProducto, Int32.Parse(Request.Form["montoHidden"]), _ConsultaContrato._contratoEntities.observacion, @SiteHelper.UserName);


            if (auxGuardarContrato.GetType().GetProperty("codSalida").GetValue(auxGuardarContrato, null).ToString() == "0")
            {
                file.SaveAs(Server.MapPath("~/Uploads/" + (int)_comun.yearNow + "_C" + _ConsultaContrato._contratoEntities.numeroResolucion + "_" + nombreArchivo));

                if (SingleSignOn.CurrentPrincipal.HasTarea("Ig_IngresoSolicitudPagoReg"))
                {
                    ViewBag.UsuarioRegional = 1;
                }
                else
                {
                    ViewBag.UsuarioRegional = 0;
                }
            }

            return View("IngresoContrato", auxGuardarContrato);

        }

        public JsonResult ConsultaNombreRegistroCivil(int Rut, char RutDv)
        {
            object auxConsultaInformacionProfesional = ConsultaProfesionalPPPFFactory.getProfesionalPPPFPorRut(Rut, RutDv);
            if (auxConsultaInformacionProfesional == null)
            {
                auxConsultaInformacionProfesional = ConsultaProfesionalPPPFFactory.getProfesionalRegistroCivil(Rut, RutDv);
            }

            return Json(auxConsultaInformacionProfesional);

        }
        public JsonResult GetNumeroResolucion(long numeroResolucion)
        {
            object auxConsultaResolucion = ConsultaContratoFactory.getNumeroResolucion(numeroResolucion);

            return Json(auxConsultaResolucion);
        }
        public JsonResult GetMontoPresupuestoRegion(int idRegion)
        {

            ViewBag.yearNow = Int16.Parse(DateTime.Now.ToString("yyyy"));
            object auxConsultaInformacionMonto = ConsultaContratoFactory.getMontoPresupuesto(Int16.Parse(DateTime.Now.ToString("yyyy")), idRegion);

            return Json(auxConsultaInformacionMonto);
        }

        public FileResult Download(string nombre)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(@"~/Uploads/" + nombre));
            string fileName = nombre;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }



        //mover clase a DOMAIN y referenciarla 
        public class comun
        {
            public long yearNow { get; set; }
            public long yearNext { get; set; }
            public int day { get; set; }
            public int month { get; set; }
            public string date { get; set; }
            public string usuario { get; set; }

            public comun()
            {

            }
            public comun obtenerComun()
            {
                comun comun = new comun();
                comun.yearNow = Int16.Parse(DateTime.Now.ToString("yyyy"));
                comun.yearNext = comun.yearNow + 1;
                comun.day = Int32.Parse(DateTime.Now.ToString("dd"));
                comun.month = Int32.Parse(DateTime.Now.ToString("MM"));
                comun.date = day.ToString().PadLeft(2, '0') + "-" + month + "-" + yearNow;
                comun.usuario = SiteHelper.UserName;
                return comun;
            }
            public comun obtenerComun(long ano)
            {
                comun comun = new comun();
                comun.yearNow = Int16.Parse(DateTime.Now.ToString("yyyy"));
                comun.yearNext = comun.yearNow + 1;
                comun.day = Int32.Parse(DateTime.Now.ToString("dd"));
                comun.month = Int32.Parse(DateTime.Now.ToString("MM"));
                comun.date = day.ToString().PadLeft(2, '0') + "-" + month + "-" + yearNow;
                comun.usuario = SiteHelper.UserName;
                return comun;
            }

        }
    }
}
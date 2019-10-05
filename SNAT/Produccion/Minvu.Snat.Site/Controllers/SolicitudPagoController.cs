using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Minvu.Snat.Domain.Forms;
using Minvu.Snat.Domain.Entities;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Minvu.Snat.Helper;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Minvu.Snat.Site.Controllers
{
    public class listaParcialidades
    {
        public string idParcialidad { get; set; }
        public string valorParcialidad { get; set; }
        public string resultadoParcialidad { get; set; }
        public string idServicio { get; set; }
    }

    public class listaIncremento
    {
        public string idIncremento { get; set; }
        public string resultadoIncremento { get; set; }
    }

    public class listaServicios
    {
        public string idServicio { get; set; }
        public string montoServicio { get; set; }
        public string montoAsignacion { get; set; }

        public listaServicios()
        {
            idServicio = "";
            montoServicio = "";
            montoAsignacion = "";
        }
        public listaServicios(string _idServicio, string _montoServicio, string _montoAsignacion)
        {
            idServicio = _idServicio;
            montoServicio = _montoServicio;
            montoAsignacion = _montoAsignacion;
        }
    }
    public class SolicitudPagoController : Controller
    {

        [HttpPost]
        public JsonResult obtenerValorMontoTotalProyecto()
        {
            GeneracionDeSolicitudPago _aux = new GeneracionDeSolicitudPago();
            _aux = Session["auxGeneracionDeSolicitudPago"] as GeneracionDeSolicitudPago;

            decimal? montoTotalProyecto = 0;
            int montoInicio = 0;
            int montoFinal = 0;
            string montoFinalaRRAY = "0";
            string montoArmado = "";
            //MontoFinal: el if puede que este mal asignado
            foreach (var item in _aux._aux2PlantillaEntities)
            {
                if (item.montoServicio != null && item.montoAsignacionDirecta != null)
                {
                    montoTotalProyecto = (item.montoServicio + item.montoAsignacionDirecta + montoTotalProyecto);
                }
                else
                {
                    montoTotalProyecto = item.montoServicio + montoTotalProyecto;
                }

            }
            if (montoTotalProyecto.ToString().Length > 1)
            {
                string cadena = montoTotalProyecto.ToString();
                string[] separadas;


                Regex rx = new Regex(@"(\d *\.?\d{ 2 } ?){ 1}");
                separadas = cadena.Split(',');
                montoInicio = Convert.ToInt32(separadas.GetValue(0).ToString().Replace(".",""));
                montoFinal = Convert.ToInt32(separadas.GetValue(1));
                char[] montoFinalArray;
                montoFinalaRRAY = separadas.GetValue(1).ToString();

                montoFinalArray = montoFinalaRRAY.ToCharArray();

                if (montoFinal == 0)
                {
                    montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + "000";
                }
                else if (montoFinal.ToString().Length == 1)
                {
                    if (montoFinalArray.Count() != 1)
                    {
                        montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinalaRRAY;
                    }
                    else
                    {
                        montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinal + "00";
                    }


                }
                else if (montoFinal.ToString().Length == 2)
                {
                    if (montoFinalArray.Count() != 2)
                    {
                        montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinalaRRAY;
                    }
                    else
                    {
                        montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinal + "0";
                    }


                }
                else
                {
                    montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinal;
                }
                montoArmado = montoArmado.Replace("$", "");
            }
            else
            {
                montoArmado = montoTotalProyecto.ToString() + ",000";

            }

            return Json(montoArmado);
        }
        public JsonResult obtenerValorSaldoPorPagar(string _txtMontoPagado, string _txtMontoComprometido)
        {
            GeneracionDeSolicitudPago _aux = new GeneracionDeSolicitudPago();
            _aux = Session["auxGeneracionDeSolicitudPago"] as GeneracionDeSolicitudPago;


            int montoInicio = 0;
            int montoFinal = 0;
            string montoArmado = "";
            decimal? montoTotalProyecto = 0;
            decimal? txtMontoPagado = 0;
            decimal? txtMontoComprometido = 0;
            decimal? saldoPorPagar = 0;
            string montoFinalaRRAY = "0";

            if (_txtMontoComprometido == "0")
            {
                _txtMontoComprometido = "0,000";
            }
            if (_txtMontoPagado == "0")
            {
                _txtMontoPagado = "0,000";
            }

            if (_txtMontoComprometido.Length > 1)
            {

                foreach (var item in _aux._aux2PlantillaEntities)
                {
                    if (item.montoServicio != null && item.montoAsignacionDirecta != null)
                    {
                        montoTotalProyecto = (item.montoServicio + item.montoAsignacionDirecta + montoTotalProyecto);
                    }
                    else
                    {
                        montoTotalProyecto = item.montoServicio + montoTotalProyecto;
                    }

                }

                txtMontoPagado = string.IsNullOrEmpty(_txtMontoPagado) ? 0 : Convert.ToDecimal(_txtMontoPagado);
                txtMontoComprometido = string.IsNullOrEmpty(_txtMontoComprometido) ? 0 : Convert.ToDecimal(_txtMontoComprometido);



                saldoPorPagar = (montoTotalProyecto - (txtMontoPagado + txtMontoComprometido));



                string cadena = saldoPorPagar.ToString();
                string[] separadas;
                separadas = cadena.Split(',');
                montoInicio = Convert.ToInt32(separadas.GetValue(0).ToString().Replace(".", ""));
                montoFinal = Convert.ToInt32(separadas.GetValue(1));
                char[] montoFinalArray;
                montoFinalaRRAY = separadas.GetValue(1).ToString();

                montoFinalArray = montoFinalaRRAY.ToCharArray();

                if (montoFinal == 0)
                {
                    montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + "000";
                }
                else if (montoFinal.ToString().Length == 1)
                {
                    if (montoFinalArray.Count() != 1)
                    {
                        montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinalaRRAY;
                    }
                    else
                    {
                        montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinal + "00";
                    }


                }
                else if (montoFinal.ToString().Length == 2)
                {
                    if (montoFinalArray.Count() != 2)
                    {
                        montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinalaRRAY;
                    }
                    else
                    {
                        montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinal + "0";
                    }
                }
                else
                {
                    montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinal;
                }
                montoArmado = montoArmado.Replace("$", "");
            }
            else
            {
                montoArmado = _txtMontoPagado.ToString() + ",000";

            }
            Log.Instance.Info("obtenerValorSaldoPorPagar :  " + montoArmado);
            return Json(montoArmado);
        }

        public JsonResult obtenerValorMontoComprometido(string _txtMontoComprometido)
        {
            int montoInicio = 0;
            int montoFinal = 0;
            string montoFinalaRRAY = "0";
            string montoArmado = "";




            if (_txtMontoComprometido.Length > 1)
            {
                string cadena = _txtMontoComprometido.ToString();
                string[] separadas;

                separadas = cadena.Split(',');
                separadas = cadena.Split(',');
                montoInicio = Convert.ToInt32(separadas.GetValue(0).ToString().Replace(".", ""));
                montoFinal = Convert.ToInt32(separadas.GetValue(1));
                char[] montoFinalArray;
                montoFinalaRRAY = separadas.GetValue(1).ToString();

                montoFinalArray = montoFinalaRRAY.ToCharArray();

                if (montoFinal == 0)
                {
                    montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + "000";
                }
                else if (montoFinal.ToString().Length == 1)
                {
                    if (montoFinalArray.Count() != 1)
                    {
                        montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinalaRRAY;
                    }
                    else
                    {
                        montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinal + "00";
                    }


                }
                else if (montoFinal.ToString().Length == 2)
                {
                    if (montoFinalArray.Count() != 2)
                    {
                        montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinalaRRAY;
                    }
                    else
                    {
                        montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinal + "0";
                    }


                }
                else
                {
                    montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinal;
                }
                montoArmado = montoArmado.Replace("$", "");

            }
            else
            {
                montoArmado = _txtMontoComprometido.ToString() + ",000";
            }

            return Json(montoArmado);
        }

        public JsonResult obtenerValorMontoPagado(string _txtMontoPagado)
        {
            int montoInicio = 0;
            int montoFinal = 0;
            string montoArmado = "";
            string montoFinalaRRAY = "0";
            if (_txtMontoPagado.Length > 1)
            {
                string cadena = _txtMontoPagado.ToString();
                string[] separadas;
                separadas = cadena.Split(',');
                montoInicio = Convert.ToInt32(separadas.GetValue(0).ToString().Replace(".", ""));
                montoFinal = Convert.ToInt32(separadas.GetValue(1));
                char[] montoFinalArray;
                montoFinalaRRAY = separadas.GetValue(1).ToString();

                montoFinalArray = montoFinalaRRAY.ToCharArray();

                if (montoFinal == 0)
                {
                    montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + "000";
                }
                else if (montoFinal.ToString().Length == 1)
                {
                    if (montoFinalArray.Count() != 1)
                    {
                        montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinalaRRAY;
                    }
                    else
                    {
                        montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinal + "00";
                    }


                }
                else if (montoFinal.ToString().Length == 2)
                {
                    if (montoFinalArray.Count() != 2)
                    {
                        montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinalaRRAY;
                    }
                    else
                    {
                        montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinal + "0";
                    }


                }
                else
                {
                    montoArmado = montoInicio.ToString("C").Replace(",00", "") + "," + montoFinal;
                }

                montoArmado = montoArmado.Replace("$", "");
            }
            else
            {
                montoArmado = _txtMontoPagado.ToString() + ",000";

            }
            return Json(montoArmado);
        }

        // GET: SolicitudPago
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetServicioTipologia(string idTipologia, string idPrograma)
        {
            List<maestroServicioEntities> objServicio = new List<maestroServicioEntities>();

            if (idTipologia != null)
            {
                if (idTipologia != String.Empty)
                {
                    objServicio = BusquedaSolicitudPagoFactory.getListServicioTipologiaPrograma(Convert.ToInt64(idTipologia), Convert.ToInt64(idPrograma));
                }
                else
                {
                    objServicio = BusquedaSolicitudPagoFactory.getListServicios();
                }
            }

            return Json(new SelectList(objServicio, "idMaestroServicio", "nombreMaestroServicio"));
        }

        public JsonResult GetTipologiaPrograma(string idPrograma)
        {
            List<maestroTipologiaEntities> objTipologias = new List<maestroTipologiaEntities>();

            if (idPrograma != null)
            {
                if (idPrograma != String.Empty)
                {
                    objTipologias = BusquedaSolicitudPagoFactory.getListTipologiaPrograma(int.Parse(idPrograma));
                }
                else
                {
                    // objTipologias = BusquedaSolicitudPagoFactory.getListServicios();
                }
            }

            return Json(new SelectList(objTipologias, "idMaestroTipologia", "nombreMaestroTipologia"));
        }

        public JsonResult GetProvincias(string idRegion)
        {
            List<ProvinciasEntities> Provincias = new List<ProvinciasEntities>();

            if (idRegion != null && idRegion != String.Empty)
            {
                BusquedaSolicitudPago objBusquedaAutorizaciondePago = new BusquedaSolicitudPago();
                Provincias = BusquedaSolicitudPagoFactory.getListProvinciasRegion(int.Parse(idRegion));
            }

            return Json(new SelectList(Provincias, "idProvincia", "nombreProvincia"));
        }
        public JsonResult GetComunasRegion(string idRegion)
        {
            //Primero se obtienen las provincias
            List<ProvinciasEntities> lstProvincias = new List<ProvinciasEntities>();

            if (idRegion != null && idRegion != String.Empty)
            {
                lstProvincias = BusquedaSolicitudPagoFactory.getListProvinciasRegion(int.Parse(idRegion));
            }

            //Lista con todas las comunas de la Region
            List<ComunasEntities> lstComunasProvincias = new List<ComunasEntities>();

            foreach (ProvinciasEntities Provincia in lstProvincias)
            {
                //Obtengo comunas de la Provincia
                List<ComunasEntities> lstComunas = new List<ComunasEntities>();
                lstComunas = BusquedaSolicitudPagoFactory.getListComunasProvincia(Convert.ToInt32(Provincia.idProvincia));

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
                    Comunas = BusquedaSolicitudPagoFactory.getListComunas();
                }
                else
                {

                    Comunas = BusquedaSolicitudPagoFactory.getListComunasProvincia(int.Parse(idProvincia));
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
                    objComuna = BusquedaSolicitudPagoFactory.getComuna(int.Parse(idComuna));
                }
            }

            return Json(objComuna.idProvincia);
        }
        //static HttpClient client = new HttpClient();
        //MMR CDC -->public async Task<List<Document>> GetDocument(string codigoProyecto)
        //MMR CDC -->{
        //MMR CDC -->
        //MMR CDC -->    List<Document> _listaDocumento = new List<Document>();
        //MMR CDC -->    HttpClient client = new HttpClient();
        //MMR CDC -->    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["GestorDocumentalDOM"].ToString());
        //MMR CDC -->
        //MMR CDC -->    // Add an Accept header for JSON format.
        //MMR CDC -->    client.DefaultRequestHeaders.Accept.Add(
        //MMR CDC -->    new MediaTypeWithQualityHeaderValue("application/json"));
        //MMR CDC -->
        //MMR CDC -->    // List data response.
        //MMR CDC -->    HttpResponseMessage response = client.GetAsync(codigoProyecto).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
        //MMR CDC -->    if (response.IsSuccessStatusCode)
        //MMR CDC -->    {
        //MMR CDC -->        // Parse the response body.
        //MMR CDC -->        var dataObjects = response.Content.ReadAsAsync<IEnumerable<Document>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
        //MMR CDC -->        foreach (var d in dataObjects)
        //MMR CDC -->        {
        //MMR CDC -->            Document document = new Document();
        //MMR CDC -->
        //MMR CDC -->            document.Nombre_Documento = d.Nombre_Documento;
        //MMR CDC -->            document.Publicado= d.Publicado;
        //MMR CDC -->            document.Ruta = d.Ruta;
        //MMR CDC -->            document.Tipo_Documento = d.Tipo_Documento;
        //MMR CDC -->            _listaDocumento.Add(document);
        //MMR CDC -->        }
        //MMR CDC -->    }
        //MMR CDC -->    else
        //MMR CDC -->    {
        //MMR CDC -->        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
        //MMR CDC -->    }
        //MMR CDC -->
        //MMR CDC -->    return _listaDocumento;
        //MMR CDC -->
        //MMR CDC -->}

        public ActionResult GeneracionSolicitudPago(GeneracionDeSolicitudPago _GeneracionDeSolicitudPago, string msgSolicitudGuardada, long? idSolicitud, string codigoProyecto)
        {
            //Eliminar todas las variables
            //Session.Contents.RemoveAll();
            Session["listaIncrementosGuardados"] = null;
            Session["listaParcialidadesGuardadas"] = null;
            Session["listaParcialidades"] = null;
            Session["listaServiciosGuardados"] = null;

            Session["estadoAvanceObra"] = null;
            Session["porcentajeAvanceObra"] = null;

            if (msgSolicitudGuardada != string.Empty && msgSolicitudGuardada != null)
            {
                ViewBag.guardado = msgSolicitudGuardada;

            }

            string t = Convert.ToString(Session["guardadoSolicitud"]);

            decimal montoSolicitudPago = 0;
            if (TempData["montoSolicitudPago"] != null)
                montoSolicitudPago = Convert.ToDecimal(TempData["montoSolicitudPago"]);




            if (_GeneracionDeSolicitudPago._informacionProyectoEntities != null)
            {

                GeneracionDeSolicitudPago _aux = new GeneracionDeSolicitudPago();
                var idPrograma = _GeneracionDeSolicitudPago._maestroProgramaEntities.idMaestroPrograma;
                object auxGeneracionDeSolicitudPago = null;
                if (idPrograma != 0 && _GeneracionDeSolicitudPago._informacionProyectoEntities.codigoProyectoInformacionProyecto != string.Empty)
                {
                    //MMR CDC --> var listDocument = GetDocument(_GeneracionDeSolicitudPago._informacionProyectoEntities.codigoProyectoInformacionProyecto).Result;

                   string usuario = @SiteHelper.UserName;
                    FuncionarioEntities funcionarioEntities = BusquedaSolicitudPagoFactory.getRegionUsuario(usuario);

                    informacionProyectoEntities _auxInformacionProyecto = informacionProyectoEntitiesFactory.getinformacionProyectoEntities(_GeneracionDeSolicitudPago._informacionProyectoEntities.codigoProyectoInformacionProyecto, idPrograma);

                    string respuestaSnatAntiguo = verificarExistenciaProyectoSNAT(idPrograma, _GeneracionDeSolicitudPago._informacionProyectoEntities.codigoProyectoInformacionProyecto.ToString());
                    string respuestaRegion = "";
                    if (Minvu.Security.SingleSignOn.CurrentPrincipal.HasTarea("Sim_GenerarSolPagoReg"))
                    {
                        respuestaRegion = verificarProyectoRegionUsuarioSNAT(idPrograma, _GeneracionDeSolicitudPago._informacionProyectoEntities.codigoProyectoInformacionProyecto.ToString(), funcionarioEntities.idRegion);
                    }


                    if (_auxInformacionProyecto.codigoProyectoInformacionProyecto != string.Empty && (((respuestaSnatAntiguo == "0" || respuestaSnatAntiguo == string.Empty) && respuestaRegion == "") || _auxInformacionProyecto.permisoUsuarioAdministrativo == true))
                    {

                        //Re-inicializa plantilla nueva con valores Nulos
                        GeneracionDeSolicitudPagoFactory.ReIniciaPlantillaNew(_GeneracionDeSolicitudPago._informacionProyectoEntities.codigoProyectoInformacionProyecto, Convert.ToInt64(idPrograma));

                        auxGeneracionDeSolicitudPago = GeneracionDeSolicitudPagoFactory.getProyecto(_GeneracionDeSolicitudPago._informacionProyectoEntities.codigoProyectoInformacionProyecto, Convert.ToInt64(idPrograma), null);
                        _aux = auxGeneracionDeSolicitudPago as GeneracionDeSolicitudPago;
                        //MMR CDC -->if (listDocument != null)
                        //MMR CDC -->{
                        //MMR CDC -->    _aux._listDocument = listDocument;
                        //MMR CDC -->}
                        Session["estadoAvanceObra"] = _aux._informacionProyectoEntities.estadoAvanceObra;
                        Session["porcentajeAvanceObra"] = _aux._informacionProyectoEntities.porcentajeAvanceObra;

                        //Calculos de Montos
                        solicitudPagoEntities objCalculoMontos = GeneracionDeSolicitudPagoFactory.CalculoMontosSolicitud(_GeneracionDeSolicitudPago._informacionProyectoEntities.codigoProyectoInformacionProyecto, idPrograma);

                        if (_aux._solicitudPago == null)
                        {
                            _aux._solicitudPago = new solicitudPagoEntities();
                        }
                        _aux._solicitudPago.montoTotalProyecto = objCalculoMontos.montoTotalProyecto;
                        _aux._solicitudPago.montoPagado = objCalculoMontos.montoPagado;
                        _aux._solicitudPago.montoComprometido = objCalculoMontos.montoComprometido;
                        _aux._solicitudPago.SaldoPorPagar = (_aux._solicitudPago.montoTotalProyecto - (_aux._solicitudPago.montoPagado + _aux._solicitudPago.montoComprometido));
                        //Calculos de Montos
                        Session["provedorEC"] = _aux._proveedorEC;
                        List<SelectListItem> tipoProveedor = new List<SelectListItem>();
                        List<SelectListItem> tipoPago = new List<SelectListItem>();
                        List<SelectListItem> mySkills = new List<SelectListItem>();


                        bool PagoArancel = false;
                        bool FTO = false;



                        foreach (var item in _aux._auxPlantillaEntities)
                        {
                            if (item.idServicio == 5)
                                FTO = true;
                            if (item.idServicio == 6)
                                PagoArancel = true;
                        }

                        if (_aux._listMaestroTipoPagoEntities != null)
                        {
                            foreach (var item in _aux._listMaestroTipoPagoEntities)
                            {
                                tipoPago.Add(new SelectListItem
                                {
                                    Text = item.nombreMaestroTipoPago,
                                    Value = item.idMaestroTipoPago.ToString()
                                });
                            }
                        }

                        ViewBag.TipoPago = tipoPago;

                        if (!FTO)
                        {
                            var itemToRemove = _aux._listMaestroTipoProveedorEntities.Single(r => r.idMaestroTipoProveedor == 6);
                            _aux._listMaestroTipoProveedorEntities.Remove(itemToRemove);
                        }

                        if (!PagoArancel)
                        {
                            var itemToRemove = _aux._listMaestroTipoProveedorEntities.Single(r => r.idMaestroTipoProveedor == 7);
                            _aux._listMaestroTipoProveedorEntities.Remove(itemToRemove);
                        }



                        foreach (var item in _aux._listMaestroTipoProveedorEntities)
                        {
                            if (item.idMaestroTipoProveedorSistema == 1)
                            {

                                if (item.nombreMaestroTipoProveedor == "EP Servicios" && _aux._informacionProyectoEntities.idMaestroPrograma == 5)
                                {
                                    item.nombreMaestroTipoProveedor = "ED Servicios";
                                }
                                if (item.nombreMaestroTipoProveedor == "EP Pago Arancel" && _aux._informacionProyectoEntities.idMaestroPrograma == 5)
                                {
                                    item.nombreMaestroTipoProveedor = "ED Pago Arancel";
                                }
                                tipoProveedor.Add(new SelectListItem
                                {
                                    Text = item.nombreMaestroTipoProveedor,
                                    Value = item.idMaestroTipoProveedor.ToString()
                                });
                            }
                        }

                        ViewBag.TipoProveedor = tipoProveedor;

                        foreach (var item in _aux._listMaestroProgramaEntities)
                        {
                            mySkills.Add(new SelectListItem
                            {
                                Text = item.nombreMaestroPrograma,
                                Value = item.idMaestroPrograma.ToString()
                            });
                        }

                        ViewBag.MySkills = mySkills;

                        Session["codigoProyectoInformacionProyecto"] = _GeneracionDeSolicitudPago._informacionProyectoEntities.codigoProyectoInformacionProyecto;
                        Session["idPrograma"] = idPrograma;
                    }
                    else
                    {
                        object auxGeneracionDeSolicitudPago2 = GeneracionDeSolicitudPagoFactory.getInstanciasFormulario();

                        List<SelectListItem> mySkills = new List<SelectListItem>();
                        List<SelectListItem> tipoProveedor = new List<SelectListItem>();
                        List<SelectListItem> tipoPago = new List<SelectListItem>();
                        GeneracionDeSolicitudPago _aux2 = new GeneracionDeSolicitudPago();
                        _aux2 = auxGeneracionDeSolicitudPago2 as GeneracionDeSolicitudPago;

                        foreach (var item in _aux2._listMaestroTipoProveedorEntities)
                        {
                            tipoProveedor.Add(new SelectListItem
                            {
                                Text = item.nombreMaestroTipoProveedor,
                                Value = item.idMaestroTipoProveedor.ToString()
                            });
                        }

                        ViewBag.TipoProveedor = tipoProveedor;

                        foreach (var item in _aux2._listMaestroProgramaEntities)
                        {
                            mySkills.Add(new SelectListItem
                            {
                                Text = item.nombreMaestroPrograma,
                                Value = item.idMaestroPrograma.ToString()
                            });
                        }

                        ViewBag.MySkills = mySkills;

                        if (respuestaSnatAntiguo != "0" && respuestaRegion != "")
                            ViewBag.Message = respuestaSnatAntiguo;
                        else if (respuestaSnatAntiguo != "0")
                            ViewBag.Message = respuestaSnatAntiguo;
                        else if (respuestaRegion != "")
                            ViewBag.Message = respuestaRegion;

                        if (_auxInformacionProyecto != null)
                        {
                            if (_auxInformacionProyecto.codigoProyectoInformacionProyecto == string.Empty)
                                ViewBag.Message = "El proyecto no se encuentra en las bases de datos de SNAT SIMPLIFICADO";
                        }

                        Session["auxGeneracionDeSolicitudPago"] = auxGeneracionDeSolicitudPago2;
                        return View(auxGeneracionDeSolicitudPago2);

                    }
                }

                if (_aux._caracteristicasEspecialesEntities == null || _aux._aux2PlantillaEntities.Count == 0)
                {
                    object auxGeneracionDeSolicitudPago2 = GeneracionDeSolicitudPagoFactory.getInstanciasFormulario();

                    List<SelectListItem> mySkills = new List<SelectListItem>();
                    List<SelectListItem> tipoProveedor = new List<SelectListItem>();
                    List<SelectListItem> tipoPago = new List<SelectListItem>();
                    GeneracionDeSolicitudPago _aux2 = new GeneracionDeSolicitudPago();
                    _aux2 = auxGeneracionDeSolicitudPago2 as GeneracionDeSolicitudPago;

                    foreach (var item in _aux2._listMaestroTipoProveedorEntities)
                    {
                        tipoProveedor.Add(new SelectListItem
                        {
                            Text = item.nombreMaestroTipoProveedor,
                            Value = item.idMaestroTipoProveedor.ToString()
                        });
                    }

                    ViewBag.TipoProveedor = tipoProveedor;

                    foreach (var item in _aux2._listMaestroProgramaEntities)
                    {
                        mySkills.Add(new SelectListItem
                        {
                            Text = item.nombreMaestroPrograma,
                            Value = item.idMaestroPrograma.ToString()
                        });
                    }

                    ViewBag.MySkills = mySkills;

                    if (_aux._caracteristicasEspecialesEntities == null)
                    {
                        ViewBag.Message = "El proyecto no se encuentra en las bases de datos de SNAT SIMPLIFICADO";
                    }
                    else if (_aux._aux2PlantillaEntities.Count == 0)
                    {
                        ViewBag.Message = "El proyecto se encuentra con errores en la submodalidad, favor realizar incidente a rukan solicitando catalogar la submodalidad del proyecto y eliminar el proyecto en SNAT";
                    }


                    Session["auxGeneracionDeSolicitudPago"] = auxGeneracionDeSolicitudPago2;
                    return View(auxGeneracionDeSolicitudPago2);
                }
                else
                {
                    Session["auxGeneracionDeSolicitudPago"] = auxGeneracionDeSolicitudPago;
                    return View(auxGeneracionDeSolicitudPago);
                }
            }
            else if (idSolicitud != null && codigoProyecto != null)//Ingreso por grilla
            {
                GeneracionDeSolicitudPago _aux = new GeneracionDeSolicitudPago();
                object auxGeneracionDeSolicitudPago = null;
                proveedorEntities mandato = null;
                _aux = GeneracionDeSolicitudPagoFactory.getCodPro(idSolicitud);
                if (_aux._proveedorMandato != null)
                {
                    mandato = _aux._proveedorMandato;
                }

                auxGeneracionDeSolicitudPago = GeneracionDeSolicitudPagoFactory.getProyecto(codigoProyecto, _aux._caracteristicasEspecialesEntities.idMaestroPrograma, idSolicitud);
                _aux = auxGeneracionDeSolicitudPago as GeneracionDeSolicitudPago;

                Session["provedorEC"] = _aux._proveedorEC;
                var objProveedor = GeneracionDeSolicitudPagoFactory.getProveedor(Convert.ToInt64(_aux._solicitudPago.idProveedor));
                _aux._maestroTipoProveedorEntities = GeneracionDeSolicitudPagoFactory.getMaestroTipoProveedor(objProveedor.idMaestroTipoProveedor);

                List<SelectListItem> tipoProveedor = new List<SelectListItem>();
                List<SelectListItem> tipoPago = new List<SelectListItem>();
                List<SelectListItem> mySkills = new List<SelectListItem>();

                if (_aux._listMaestroTipoPagoEntities != null)
                {
                    foreach (var item in _aux._listMaestroTipoPagoEntities)
                    {
                        tipoPago.Add(new SelectListItem
                        {
                            Text = item.nombreMaestroTipoPago,
                            Value = item.idMaestroTipoPago.ToString()
                        });
                    }
                }

                ViewBag.TipoPago = tipoPago;

                foreach (var item in _aux._listMaestroTipoProveedorEntities)
                {
                    tipoProveedor.Add(new SelectListItem
                    {
                        Text = item.nombreMaestroTipoProveedor,
                        Value = item.idMaestroTipoProveedor.ToString()
                    });
                }

                ViewBag.TipoProveedor = tipoProveedor;

                foreach (var item in _aux._listMaestroProgramaEntities)
                {
                    mySkills.Add(new SelectListItem
                    {
                        Text = item.nombreMaestroPrograma,
                        Value = item.idMaestroPrograma.ToString()
                    });
                }

                ViewBag.MySkills = mySkills;

                //Session["codigoProyectoInformacionProyecto"] = _GeneracionDeSolicitudPago._informacionProyectoEntities.codigoProyectoInformacionProyecto;
                //Session["idPrograma"] = idPrograma;
                Session["auxGeneracionDeSolicitudPago"] = auxGeneracionDeSolicitudPago;
                return View(auxGeneracionDeSolicitudPago);
            }
            else
            {
                object auxGeneracionDeSolicitudPago = GeneracionDeSolicitudPagoFactory.getInstanciasFormulario();

                List<SelectListItem> mySkills = new List<SelectListItem>();
                List<SelectListItem> tipoProveedor = new List<SelectListItem>();
                List<SelectListItem> tipoPago = new List<SelectListItem>();
                GeneracionDeSolicitudPago _aux = new GeneracionDeSolicitudPago();
                _aux = auxGeneracionDeSolicitudPago as GeneracionDeSolicitudPago;

                foreach (var item in _aux._listMaestroTipoPagoEntities)
                {
                    tipoPago.Add(new SelectListItem
                    {
                        Text = item.nombreMaestroTipoPago,
                        Value = item.idMaestroTipoPago.ToString()
                    });
                }

                ViewBag.TipoPago = tipoPago;

                foreach (var item in _aux._listMaestroTipoProveedorEntities)
                {
                    tipoProveedor.Add(new SelectListItem
                    {
                        Text = item.nombreMaestroTipoProveedor,
                        Value = item.idMaestroTipoProveedor.ToString()
                    });
                }

                ViewBag.TipoProveedor = tipoProveedor;

                foreach (var item in _aux._listMaestroProgramaEntities)
                {
                    mySkills.Add(new SelectListItem
                    {
                        Text = item.nombreMaestroPrograma,
                        Value = item.idMaestroPrograma.ToString()
                    });
                }

                ViewBag.MySkills = mySkills;
                Session["auxGeneracionDeSolicitudPago"] = auxGeneracionDeSolicitudPago;
                return View(auxGeneracionDeSolicitudPago);
            }
        }

        public JsonResult ConsultaNombreProveedor(int Rut, string RutDv, string TipoProveedor)
        {
            var proveedor = GeneracionDeSolicitudPagoFactory.getProveedorxRut(Rut, RutDv);

            if (TipoProveedor == "rutEPM")
            { Session["ProveedorMandato"] = proveedor; }


            if (proveedor != null)
            {
                if (proveedor.nombreProveedor != string.Empty)
                {
                    return Json(proveedor.nombreProveedor);
                }
                else
                {
                    return Json("Sin información");
                }
            }
            else
            {
                return Json("Sin información");
            }
        }


        [HttpPost]
        public JsonResult obtenerIncrementos()
        {
            List<listaIncremento> listObject = Session["listaIncrementosGuardados"] as List<listaIncremento>;
            string nombreCheck = String.Empty;

            if (listObject != null)
                foreach (var item in listObject)
                {
                    if (Convert.ToBoolean(item.resultadoIncremento))
                    {
                        nombreCheck = String.Format("{0}{1}1-", nombreCheck, item.idIncremento);
                    }
                    else
                    {
                        nombreCheck = String.Format("{0}{1}2-", nombreCheck, item.idIncremento);
                    }
                }

            return Json(new { success = true, responseText = nombreCheck }, JsonRequestBehavior.AllowGet);
        }

        private string verificarExistenciaProyectoSNAT(long idPrograma, string codigoProyecto)
        {
            string respuesta = GeneracionDeSolicitudPagoFactory.verificarExistenciaProyectoSNAT(idPrograma, codigoProyecto);

            if (respuesta != "0" && respuesta != string.Empty)
            {
                maestroProgramaEntities _maestroProgramaEntities = maestroProgramaEntitiesFactory.getMaestroPrograma(idPrograma);

                string sistema = "";

                if (_maestroProgramaEntities.nombreMaestroPrograma == "DS174" || _maestroProgramaEntities.nombreMaestroPrograma == "DS255"
                    || _maestroProgramaEntities.nombreMaestroPrograma == "DS1")
                    sistema = "SNAT III";
                else if (_maestroProgramaEntities.nombreMaestroPrograma == "DS49")
                    sistema = "SNAT DS49";
                else if (_maestroProgramaEntities.nombreMaestroPrograma == "DS49 Reconstrucción")
                    sistema = "SNAT RECONSTRUCCIÓN";
                else if (_maestroProgramaEntities.nombreMaestroPrograma == "DS49 res 1875")
                    sistema = "SNAT 105";
                //else if (_maestroProgramaEntities.nombreMaestroPrograma == "DS19")
                // if(_maestroProgramaEntities.nombreMaestroPrograma == "DS10")




                respuesta = "Estimado, no es posible generar la solicitud de pago para el proyecto " + respuesta + " ya que se encuentra cargado en el sistema " + sistema;
            }
            return respuesta;
        }

        //En el caso de que el usuario tenga tareas relacionadas a una region en especial, este solo podra ver los proyectos de la region indicada
        private string verificarProyectoRegionUsuarioSNAT(long idPrograma, string codigoProyecto, int regionUsuario)
        {
            int respuesta = GeneracionDeSolicitudPagoFactory.verificaProyectoRegionUsuarioSNAT(idPrograma, codigoProyecto, regionUsuario);

            string retorno = "";
            if (respuesta == 0)
            {
                RegionesEntities aux = RegionesEntitiesFactory.getRegion(regionUsuario);


                retorno = "Estimado, su rol de usuario solo limita ver proyectos de la región " + aux.nombreRegion;
            }
            else
                retorno = "";
            return retorno;
        }

        [HttpPost]
        public JsonResult guardarIncrementos(List<listaIncremento> listObject)
        {
            Session["listaIncrementosGuardados"] = listObject;

            return Json(new { success = true, responseText = "" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult rescatarNombreProveedorEC(int idProveedor)
        {
            proveedorEntities provedorEC = proveedorEntitiesFactory.getProveedor(idProveedor);
            Session["provedorEC"] = provedorEC;
            return Json(new { success = true, responseText = provedorEC.nombreProveedor }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult rescatarNombreProveedorFTO(int idProveedor)
        {
            proveedorEntities provedorFTO = proveedorEntitiesFactory.getProveedor(idProveedor);

            return Json(new { success = true, responseText = provedorFTO.nombreProveedor }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult refrescarCampos(List<listaParcialidades> listObject)
        {
            List<listaServicios> listObject2 = new List<listaServicios>();

            if (listObject != null)
            {
                bool caracterInvalido = false;
                foreach (var item in listObject)
                {
                    if (item.valorParcialidad == null)
                    {
                        caracterInvalido = true;
                        break;
                    }
                    else
                    {
                        if (item.valorParcialidad.Contains("."))
                        {
                            caracterInvalido = true;
                            break;
                        }
                    }
                }

                if (verificarMontos(listObject, listObject2, 1) && !caracterInvalido)
                {
                    try
                    {
                        string codigoProyectoInformacionProyecto = Session["codigoProyectoInformacionProyecto"].ToString();
                        long idPrograma = Convert.ToInt64(Session["idPrograma"]);
                        object auxGeneracionDeSolicitudPago = GeneracionDeSolicitudPagoFactory.getProyecto(codigoProyectoInformacionProyecto, idPrograma, null);

                        GeneracionDeSolicitudPago aux = auxGeneracionDeSolicitudPago as GeneracionDeSolicitudPago;

                        decimal? montoSolicitudPago = Convert.ToDecimal("0,000");

                        Session["listaParcialidadesGuardadas"] = listObject;
                        Session["listaParcialidades"] = aux._auxPlantillaEntities;

                        foreach (var item in aux._auxPlantillaEntities)
                        {
                            item.montoParcialidad = 0;
                            foreach (var item2 in listObject)
                            {
                                if (item.idServicio == Convert.ToInt64(item2.idServicio) && item.idParcialidad == Convert.ToInt64(item2.idParcialidad))
                                {
                                    item.montoParcialidad = Convert.ToDecimal(item2.valorParcialidad);
                                }
                            }
                        }

                        decimal montoTotalParcialPorServicio = 0;
                        bool MontosSuperdados = false;
                        decimal montoServicioMasAsignacionDirecta = 0;
                        string error = string.Empty;

                        foreach (auxPlantillaEntities item in aux._aux2PlantillaEntities)
                        {
                            montoServicioMasAsignacionDirecta = Convert.ToDecimal(item.montoServicio) + Convert.ToDecimal(item.montoAsignacionDirecta);
                            montoTotalParcialPorServicio = 0;
                            foreach (auxPlantillaEntities item2 in aux._auxPlantillaEntities)
                            {
                                if (item.idServicio == item2.idServicio)
                                {
                                    montoTotalParcialPorServicio = montoTotalParcialPorServicio + Convert.ToDecimal(item2.montoParcialidad);

                                    if (montoTotalParcialPorServicio > montoServicioMasAsignacionDirecta)
                                    {
                                        MontosSuperdados = true;
                                        error = String.Format("El monto para el servicio: \"{0}\", fue superado, favor verificar parcialidades", item.nombreMaestroServicio);//Cambios DO

                                        break;
                                    }
                                }
                            }

                            if (MontosSuperdados)
                                break;
                            montoSolicitudPago = montoSolicitudPago + montoTotalParcialPorServicio;
                        }

                        if (!MontosSuperdados)
                        {
                            GeneracionDeSolicitudPagoFactory.saveParcialidades(aux);

                            return Json(new { success = true, responseText = montoSolicitudPago.ToString() }, JsonRequestBehavior.AllowGet);
                            //return Json(montoSolicitudPago);
                        }
                        else
                        {
                            return Json(new { success = false, responseText = error }, JsonRequestBehavior.AllowGet);

                            //  return Json(montoSolicitudPago);
                        }
                    }
                    catch (Exception ex)
                    {
                        //return Json(new { success = false, responseText = "Error: Genere incidente Aranda." }, JsonRequestBehavior.AllowGet);
                        return Json(new { success = false, responseText = String.Format("Error: {0}.", ex.Message) }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, responseText = "Favor verificar que ingresó los montos incluyendo decimales en 'Identificación de parcialidades'." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, responseText = "Favor verificar que ingresó los montos incluyendo decimales en 'Identificación de parcialidades'." }, JsonRequestBehavior.AllowGet);
            }
        }

        private bool verificarMontos(List<listaParcialidades> listObject, List<listaServicios> listObject2, int tipoVerificar)
        {
            decimal number;
            bool resultado = true;

            if (tipoVerificar == 1)
            {
                if (listObject.Count > 0)
                {
                    foreach (var item in listObject)
                    {
                        resultado = Decimal.TryParse(item.valorParcialidad, out number);

                        if (!resultado)
                            break;
                    }
                }
                else
                {
                    resultado = false;
                }

            }
            else if (tipoVerificar == 2)
            {
                if (listObject2.Count > 0)
                {
                    foreach (var item in listObject2)
                    {
                        resultado = Decimal.TryParse(item.montoServicio, out number);
                        if (!resultado)
                            break;

                        if (resultado && item.montoAsignacion != null)
                            resultado = Decimal.TryParse(item.montoAsignacion, out number);

                        if (!resultado)
                            break;
                    }
                }
                else
                {
                    resultado = false;
                }

            }

            return resultado;
        }

        [HttpPost]
        public JsonResult refrescarServicios(List<listaServicios> listObject)
        {
            List<listaParcialidades> listObject2 = new List<listaParcialidades>();

            if (listObject != null)
            {
                bool caracterInvalido = false;
                foreach (var item in listObject)
                {
                    if (item.montoServicio == null)
                    {
                        caracterInvalido = true;
                        break;
                    }
                    else
                    {
                        if (item.montoServicio.Contains("."))
                        {
                            caracterInvalido = true;
                            break;
                        }

                        if (item.montoAsignacion != null)
                            if (item.montoAsignacion.Contains("."))
                            {
                                caracterInvalido = true;
                                break;
                            }
                    }
                }

                if (verificarMontos(listObject2, listObject, 2) && !caracterInvalido)
                {
                    try
                    {
                        List<listaServicios> _listaServicios = new List<listaServicios>();

                        bool encontrado = false;
                        foreach (var item in listObject)
                        {
                            encontrado = false;
                            foreach (var item2 in _listaServicios)
                            {
                                if (item.idServicio == item2.idServicio)
                                {
                                    encontrado = true;
                                }
                            }

                            if (!encontrado)
                                _listaServicios.Add(item);
                        }

                        Session["listaServiciosGuardados"] = _listaServicios;

                        string codigoProyectoInformacionProyecto = Session["codigoProyectoInformacionProyecto"].ToString();
                        long idPrograma = Convert.ToInt64(Session["idPrograma"]);
                        object auxGeneracionDeSolicitudPago = GeneracionDeSolicitudPagoFactory.getProyecto(codigoProyectoInformacionProyecto, idPrograma, null);

                        GeneracionDeSolicitudPago aux = auxGeneracionDeSolicitudPago as GeneracionDeSolicitudPago;

                        decimal? montoSolicitudPago = Convert.ToDecimal("0,000");

                        foreach (var item in aux._aux2PlantillaEntities)
                        {
                            foreach (var item2 in _listaServicios)
                            {
                                if (item.idServicio == Convert.ToInt64(item2.idServicio))
                                {
                                    item.montoServicio = Convert.ToDecimal(item2.montoServicio);
                                    item.montoAsignacionDirecta = Convert.ToDecimal(item2.montoAsignacion);
                                }
                            }
                        }

                        GeneracionDeSolicitudPagoFactory.saveServicios(aux);
                        Session["auxGeneracionDeSolicitudPago"] = aux;
                        foreach (listaServicios item in _listaServicios)
                        {
                            decimal? montoAsignacion = Convert.ToDecimal(item.montoAsignacion);
                            decimal? montoServicio = Convert.ToDecimal(item.montoServicio);

                            montoSolicitudPago = montoSolicitudPago + montoServicio + montoAsignacion;
                        }

                        return Json(new { success = true, responseText = montoSolicitudPago.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        //return Json(new { success = false, responseText = "Error: Genere incidente Aranda." }, JsonRequestBehavior.AllowGet);
                        return Json(new { success = false, responseText = String.Format("Error: {0}.", ex.Message) }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, responseText = "Favor verificar que ingresó los montos incluyendo decimales en 'Identificación de servicios'." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, responseText = "Favor verificar que ingresó los montos incluyendo decimales en 'Identificación de servicios'." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult recomponerServicios(List<listaServicios> listObject)
        {
            List<listaServicios> _listaServicios = new List<listaServicios>();

            _listaServicios = (List<listaServicios>)Session["listaServiciosGuardados"];

            if (Session["listaServiciosGuardados"] != null)
            {
                return Json(new { success = true, listServicios = _listaServicios }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, listServicios = _listaServicios }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult recomponerParcialidades(List<listaParcialidades> listObject)
        {
            List<listaParcialidades> _listaParcialidades = new List<listaParcialidades>();
            List<auxPlantillaEntities> _listPlantillas = new List<auxPlantillaEntities>();

            _listaParcialidades = (List<listaParcialidades>)Session["listaParcialidadesGuardadas"];
            _listPlantillas = (List<auxPlantillaEntities>)Session["listaParcialidades"];

            if (Session["listaParcialidadesGuardadas"] != null)
            {
                return Json(new { success = true, listParcialidades = _listaParcialidades, listPlantillas = _listPlantillas }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, listParcialidades = _listaParcialidades, listPlantillas = _listPlantillas }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ObtenerBoleta(string sFolioBoleta)
        {
            try
            {
                DateTime? FechaVencimiento = null;

                if (sFolioBoleta != String.Empty)
                {
                    FechaVencimiento = BusquedaSolicitudPagoFactory.ObtenerBoleta(sFolioBoleta);

                    Session["FechaVencimientoBoleta"] = FechaVencimiento;
                }

                return Json(new { success = true, fecBoleta = (FechaVencimiento == null ? String.Empty : ((DateTime)FechaVencimiento).ToShortDateString()) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, fecBoleta = String.Empty }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult guardarSolicitud(GeneracionDeSolicitudPago _GeneracionDeSolicitudPago)

        {
            try
            {
                string msgSolicitudGuardada2 = String.Empty, CodigoSalida = String.Empty;

                //if (_GeneracionDeSolicitudPago._maestroTipoProveedorEntities.idMaestroTipoProveedor != 6)//FTO
                //{
                var objCaracEspecial = GeneracionDeSolicitudPagoFactory.getCaracteristicaEspecial(Convert.ToInt64(_GeneracionDeSolicitudPago._caracteristicasEspecialesEntities.idCaracteristicasEspeciales));
                long idTipPro = _GeneracionDeSolicitudPago._maestroTipoProveedorEntities.idMaestroTipoProveedor;
                proveedorEntities provedor = new proveedorEntities();
                proveedorEntities provedorFTO = new proveedorEntities();


                provedor = Session["provedorEC"] as proveedorEntities;


                proveedorEntities objProveedor = new proveedorEntities();


                if (_GeneracionDeSolicitudPago._proveedorEC == null)
                {
                    _GeneracionDeSolicitudPago._proveedorEC = provedor;
                }


                // condicion ObjProveedor
                if (idTipPro == 2)
                {
                    if (_GeneracionDeSolicitudPago._proveedorEC != null)
                    {
                        if (_GeneracionDeSolicitudPago._proveedorEC.nombreProveedor != String.Empty)
                        {
                            objProveedor = _GeneracionDeSolicitudPago._proveedorEC;
                        }
                        else
                        {
                            objProveedor = provedor;
                        }
                    }
                    else
                    {
                        objProveedor = GeneracionDeSolicitudPagoFactory.getProveedorIdProyectoIdTipo(Convert.ToInt64(objCaracEspecial.idInformacionProyecto), idTipPro);
                    }

                }
                else if (idTipPro == 6)
                {
                    if (_GeneracionDeSolicitudPago._proveedorFTO != null)
                    {
                        if (_GeneracionDeSolicitudPago._proveedorFTO.nombreProveedor != string.Empty)
                        {
                            objProveedor = _GeneracionDeSolicitudPago._proveedorFTO;
                        }
                        else
                        {
                            objProveedor = provedor;
                        }
                    }
                    else
                    {
                        objProveedor = GeneracionDeSolicitudPagoFactory.getProveedorIdProyectoIdTipo(Convert.ToInt64(objCaracEspecial.idInformacionProyecto), idTipPro);
                    }

                }
                else
                {
                    objProveedor = GeneracionDeSolicitudPagoFactory.getProveedorIdProyectoIdTipo(Convert.ToInt64(objCaracEspecial.idInformacionProyecto), idTipPro);
                }

                if (_GeneracionDeSolicitudPago._proveedorMandato != null)
                {
                    if (_GeneracionDeSolicitudPago._proveedorMandato.rutProveedor > 0)
                    {
                        var aux = _GeneracionDeSolicitudPago._proveedorMandato;
                        var aux2 = Session["ProveedorMandato"] as proveedorEntities;
                        aux.nombreProveedor = aux2.nombreProveedor;
                        _GeneracionDeSolicitudPago._proveedorMandato = GeneracionDeSolicitudPagoFactory.getProveedorxRut(_GeneracionDeSolicitudPago._proveedorMandato.rutProveedor, _GeneracionDeSolicitudPago._proveedorMandato.dvDigitoprovedor.ToString());
                        if (_GeneracionDeSolicitudPago._proveedorMandato.rutProveedor != aux.rutProveedor)
                        {
                            proveedorEntitiesFactory.saveProveedor(aux.rutProveedor, aux.dvDigitoprovedor, aux.nombreProveedor, 1);
                            _GeneracionDeSolicitudPago._proveedorMandato = GeneracionDeSolicitudPagoFactory.getProveedorxRut(aux.rutProveedor, aux.dvDigitoprovedor.ToString());
                        }

                        _GeneracionDeSolicitudPago._solicitudPago.idMandatoProveedor = _GeneracionDeSolicitudPago._proveedorMandato.idProveedor;
                    }
                    else
                    {
                        if (Session["ProveedorMandato"] != null)
                            _GeneracionDeSolicitudPago._proveedorMandato = Session["ProveedorMandato"] as proveedorEntities;
                    }
                }

                //Condicion elección provedoor y parcialidades

                List<auxPlantillaEntities> _axu = new List<auxPlantillaEntities>();

                foreach (var item in _GeneracionDeSolicitudPago._auxPlantillaEntities)
                {

                    if (idTipPro == 1 || idTipPro == 2)
                    {
                        if (item.idServicio == 5 || item.idServicio == 6)
                        {
                            item.parcialidadSeleccionada = false;
                            item.montoParcialidad = 0;
                        }
                    }
                    else if (idTipPro == 6)
                    {
                        if (item.idServicio != 5)
                        {
                            item.parcialidadSeleccionada = false;
                            item.montoParcialidad = 0;
                        }
                    }
                    else if (idTipPro == 7)
                    {
                        if (item.idServicio != 6)
                        {
                            item.parcialidadSeleccionada = false;
                            item.montoParcialidad = 0;
                        }
                    }

                }

                GeneracionDeSolicitudPagoFactory.saveParcialidades(_GeneracionDeSolicitudPago);

                if (objProveedor == null)
                {
                    objProveedor = _GeneracionDeSolicitudPago._proveedorEC;

                }

                proveedorEntities objProveedorInsert = new proveedorEntities();

                if (objProveedor.idProveedor == 0)//Proveedor inexistente
                {
                    if (idTipPro == 2)//EC
                    {
                        objProveedorInsert.rutProveedor = _GeneracionDeSolicitudPago._proveedorEC.rutProveedor;
                        objProveedorInsert.dvDigitoprovedor = _GeneracionDeSolicitudPago._proveedorEC.dvDigitoprovedor;
                        objProveedorInsert.nombreProveedor = _GeneracionDeSolicitudPago._proveedorEC.nombreProveedor;
                        objProveedorInsert.idMaestroTipoProveedor = 2;
                    }
                    else if (idTipPro == 1)//EP Servicios
                    {
                        objProveedorInsert.rutProveedor = _GeneracionDeSolicitudPago._proveedorEP.rutProveedor;
                        objProveedorInsert.dvDigitoprovedor = _GeneracionDeSolicitudPago._proveedorEP.dvDigitoprovedor;
                        objProveedorInsert.nombreProveedor = _GeneracionDeSolicitudPago._proveedorEP.nombreProveedor;
                        objProveedorInsert.idMaestroTipoProveedor = 1;




                    }
                    else if (idTipPro == 6)//FTO
                    {
                        objProveedorInsert.rutProveedor = _GeneracionDeSolicitudPago._proveedorFTO.rutProveedor;
                        objProveedorInsert.dvDigitoprovedor = _GeneracionDeSolicitudPago._proveedorFTO.dvDigitoprovedor;
                        objProveedorInsert.nombreProveedor = _GeneracionDeSolicitudPago._proveedorFTO.nombreProveedor;
                        objProveedorInsert.idMaestroTipoProveedor = 6;
                    }
                    else if (idTipPro == 7)//EP Pago Aranceles
                    {
                        objProveedor = GeneracionDeSolicitudPagoFactory.getProveedorIdProyectoIdTipo(Convert.ToInt64(objCaracEspecial.idInformacionProyecto), 1);

                        objProveedorInsert.idMaestroTipoProveedor = 7;

                        if (objProveedor.idProveedor != 0)
                        {
                            objProveedorInsert.rutProveedor = objProveedor.rutProveedor;
                            objProveedorInsert.dvDigitoprovedor = objProveedor.dvDigitoprovedor;
                            objProveedorInsert.nombreProveedor = objProveedor.nombreProveedor;
                            objProveedor.idProveedor = 0;
                        }
                        else
                        {
                            objProveedorInsert.rutProveedor = _GeneracionDeSolicitudPago._proveedorEP.rutProveedor;
                            objProveedorInsert.dvDigitoprovedor = _GeneracionDeSolicitudPago._proveedorEP.dvDigitoprovedor;
                            objProveedorInsert.nombreProveedor = _GeneracionDeSolicitudPago._proveedorEP.nombreProveedor;
                        }
                    }
                }

                //if (idTipPro == 6)//FTO
                //{
                //    //objProveedor = GeneracionDeSolicitudPagoFactory.getProveedor(1);//IDPROVEEDOR 1 es "No aplica"
                //    objProveedor = GeneracionDeSolicitudPagoFactory.getProveedorxNombre("No aplica");
                //}

                _GeneracionDeSolicitudPago._solicitudPago.usuariosReponsableSolicitudPago = SiteHelper.UserName;
                _GeneracionDeSolicitudPago._solicitudPago.idProveedor = objProveedor.idProveedor;
                _GeneracionDeSolicitudPago._solicitudPago.numeroViviendasSolicitudPago = _GeneracionDeSolicitudPago._informacionProyectoEntities.numeroViviendasInformacionProyecto;
                _GeneracionDeSolicitudPago._solicitudPago.numeroFamiliasPagadarSolicitudPago = _GeneracionDeSolicitudPago._informacionProyectoEntities.cantidadBeneficiariosInformacionProyecto;
                _GeneracionDeSolicitudPago._informacionProyectoEntities.estadoAvanceObra = Session["estadoAvanceObra"].ToString();

                if (Session["porcentajeAvanceObra"] != null)
                    _GeneracionDeSolicitudPago._informacionProyectoEntities.porcentajeAvanceObra = Convert.ToInt64(Session["porcentajeAvanceObra"]);
                else
                    _GeneracionDeSolicitudPago._informacionProyectoEntities.porcentajeAvanceObra = null;

                if (Session["FechaVencimientoBoleta"] != null)
                {
                    _GeneracionDeSolicitudPago._solicitudPago.fechaBoletaGarantiaSolicitudPago = Session["FechaVencimientoBoleta"].ToString();
                }
                solicitudPagoEntities oResult = GeneracionDeSolicitudPagoFactory.GeneraSolicitudDePago(_GeneracionDeSolicitudPago, _GeneracionDeSolicitudPago._solicitudPago, Convert.ToInt64(_GeneracionDeSolicitudPago._caracteristicasEspecialesEntities.idCaracteristicasEspeciales), objProveedorInsert);

                msgSolicitudGuardada2 = oResult.mensajeSalida;
                CodigoSalida = oResult.codigoSalida;
                //}

                //msgSolicitudGuardada2 = "Solicitud de pago generada correctamente.";

                return RedirectToAction("GeneracionSolicitudPago", new { msgSolicitudGuardada = msgSolicitudGuardada2 });
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public static proveedorEntities getProveedorRutTipo(int RutProveedor, long idTipoProveedor)
        {
            proveedorEntities objProveedor = new proveedorEntities();

            // objProveedor = proveedorEntitiesFactory.getProveedorRutTipo(RutProveedor, idTipoProveedor);

            return objProveedor;
        }

        public ActionResult BusquedaSolicitud(BusquedaSolicitudPago pBusquedaSolicitudPago, bool? sesiones)
        {

            string usuario = @SiteHelper.UserName;
            FuncionarioEntities funcionarioEntities = BusquedaSolicitudPagoFactory.getRegionUsuario(usuario);

            if (sesiones == null || sesiones == false)
            {
                Session["BusquedaSolicitudVolver"] = null;
            }

            if (Session["BusquedaSolicitudVolver"] != null)
            {
                pBusquedaSolicitudPago = (BusquedaSolicitudPago)Session["BusquedaSolicitudVolver"];
            }

            BusquedaSolicitudPago auxBusquedaSolicitudPago = new BusquedaSolicitudPago();

            if (pBusquedaSolicitudPago._informacionProyectoEntities != null)
            {

                //Obtiene la instancia de DDL
                auxBusquedaSolicitudPago = BusquedaSolicitudPagoFactory.getInstanciasFormulario(funcionarioEntities.idRegion);
                if (Minvu.Security.SingleSignOn.CurrentPrincipal.HasTarea("Sim_ConsultarSolPagoReg"))
                {
                    pBusquedaSolicitudPago._RegionesEntities = new RegionesEntities();
                    pBusquedaSolicitudPago._RegionesEntities.idRegion = funcionarioEntities.idRegion;
                }

                //mantiene la instancia de DDL
                auxBusquedaSolicitudPago = BusquedaSolicitudPagoFactory.getMantenerInstanciasFormulario(auxBusquedaSolicitudPago, pBusquedaSolicitudPago._maestroProgramaEntities.idMaestroPrograma, pBusquedaSolicitudPago._maestroTipologiaEntities.idMaestroTipologia, pBusquedaSolicitudPago._maestroServicioEntities.nombreMaestroServicio, pBusquedaSolicitudPago._RegionesEntities.idRegion, pBusquedaSolicitudPago._ProvinciasEntities.idProvincia, pBusquedaSolicitudPago._ComunasEntities.idComuna, funcionarioEntities.idRegion);

                //Obtiene lista de solicitud
                pBusquedaSolicitudPago._listadoSolicitudesEntities = BusquedaSolicitudPagoFactory.obtenerListadoSolicitudes(pBusquedaSolicitudPago._informacionProyectoEntities.codigoProyectoInformacionProyecto, pBusquedaSolicitudPago._maestroProgramaEntities.idMaestroPrograma, pBusquedaSolicitudPago._maestroTipologiaEntities.idMaestroTipologia, pBusquedaSolicitudPago._maestroLlamadoEntities.idMaestroLlamado, pBusquedaSolicitudPago._maestroTipoProveedorEntities.idMaestroTipoProveedor, pBusquedaSolicitudPago._proovedorEntities.nombreProveedor, pBusquedaSolicitudPago._maestroServicioEntities.nombreMaestroServicio, pBusquedaSolicitudPago._RegionesEntities.idRegion, pBusquedaSolicitudPago._ProvinciasEntities.idProvincia, pBusquedaSolicitudPago._ComunasEntities.idComuna, pBusquedaSolicitudPago._maestroModalidadEntities.idMaestroModalidad, null);

                pBusquedaSolicitudPago.lstRegiones = auxBusquedaSolicitudPago.lstRegiones;
                pBusquedaSolicitudPago.lstProvincias = auxBusquedaSolicitudPago.lstProvincias;
                pBusquedaSolicitudPago.lstComunas = auxBusquedaSolicitudPago.lstComunas;

                pBusquedaSolicitudPago.lstTipologias = auxBusquedaSolicitudPago.lstTipologias;
                pBusquedaSolicitudPago.lstLlamado = auxBusquedaSolicitudPago.lstLlamado;
                pBusquedaSolicitudPago.lstServicios = auxBusquedaSolicitudPago.lstServicios;
                pBusquedaSolicitudPago.lstTipoProveedor = auxBusquedaSolicitudPago.lstTipoProveedor;
                pBusquedaSolicitudPago.lstModalidades = auxBusquedaSolicitudPago.lstModalidades;

                pBusquedaSolicitudPago._lstPrograma = auxBusquedaSolicitudPago._lstPrograma;
                pBusquedaSolicitudPago._lstPrograma = auxBusquedaSolicitudPago._lstPrograma;

                pBusquedaSolicitudPago._regionUserEntities = auxBusquedaSolicitudPago._regionUserEntities;

                Session["BusquedaSolicitudVolver"] = pBusquedaSolicitudPago;
            }
            else
            {
                pBusquedaSolicitudPago = BusquedaSolicitudPagoFactory.getInstanciasFormulario(funcionarioEntities.idRegion);
            }

            ViewBag.Message = "El proyecto no se encuentra en las bases de datos de RUKAN";
            return View(pBusquedaSolicitudPago);
        }

        public ActionResult InformacionServicios()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ActionGrid(BusquedaSolicitudPago _BusquedaSolicitudPago)
        {
            try
            {
                string usuario = @SiteHelper.UserName;
                FuncionarioEntities funcionarioEntities = BusquedaSolicitudPagoFactory.getRegionUsuario(usuario);


                modificacionEstadoSolicitudEntities _modificacionEstadoSolicitudEntities = new modificacionEstadoSolicitudEntities();

                if (Request.Form["idSolicitudHidden"] != String.Empty)
                {
                    var idSolicitud = Request.Form["idSolicitudHidden"];
                    modificacionEstadoSolicitudEntities objModificacionEstado = new modificacionEstadoSolicitudEntities();
                    objModificacionEstado.IDSOLICITUDPAGO = Convert.ToInt64(idSolicitud);
                    objModificacionEstado.IDMAESTROESTADOSOLICITUD = 4;
                    objModificacionEstado.USUARIORESPONSABLEMODIFICACIONESTADOSOLICITUD = SiteHelper.UserName;

                    _modificacionEstadoSolicitudEntities = BusquedaAutorizaciondePagoFactory.SaveModificacionEstadoSolicitud(objModificacionEstado);
                }
                else
                {
                    var idSolicitudCadena = Request.Form["idSolicitudCadenaHidden"];
                    autorizaciondePagoEntities objResultado = BusquedaSolicitudPagoFactory.InsertaGeneracionSolicitudPago(idSolicitudCadena, SiteHelper.UserName);
                    _modificacionEstadoSolicitudEntities.codigoSalida = objResultado.codigoSalida;
                    _modificacionEstadoSolicitudEntities.mensajeSalida = objResultado.mensajeSalida == "OK" ? "Generación de Autorización de Pago Satisfactoria" : objResultado.mensajeSalida;
                }

                BusquedaSolicitudPago auxBusquedaSolicitudPago = new BusquedaSolicitudPago();

                if (_BusquedaSolicitudPago._informacionProyectoEntities != null)
                {
                    //Obtiene la instancia de DDL
                    auxBusquedaSolicitudPago = BusquedaSolicitudPagoFactory.getInstanciasFormulario(funcionarioEntities.idRegion);

                    if (Minvu.Security.SingleSignOn.CurrentPrincipal.HasTarea("Sim_ConsultarSolPagoReg"))
                    {
                        _BusquedaSolicitudPago._RegionesEntities = new RegionesEntities();

                        _BusquedaSolicitudPago._RegionesEntities.idRegion = funcionarioEntities.idRegion;
                    }

                    //mantiene la instancia de DDL
                    auxBusquedaSolicitudPago = BusquedaSolicitudPagoFactory.getMantenerInstanciasFormulario(auxBusquedaSolicitudPago, _BusquedaSolicitudPago._maestroProgramaEntities.idMaestroPrograma, _BusquedaSolicitudPago._maestroTipologiaEntities.idMaestroTipologia, _BusquedaSolicitudPago._maestroServicioEntities.nombreMaestroServicio, _BusquedaSolicitudPago._RegionesEntities.idRegion, _BusquedaSolicitudPago._ProvinciasEntities.idProvincia, _BusquedaSolicitudPago._ComunasEntities.idComuna, funcionarioEntities.idRegion);

                    //Obtiene lista de solicitud
                    _BusquedaSolicitudPago._listadoSolicitudesEntities = BusquedaSolicitudPagoFactory.obtenerListadoSolicitudes(_BusquedaSolicitudPago._informacionProyectoEntities.codigoProyectoInformacionProyecto, _BusquedaSolicitudPago._maestroProgramaEntities.idMaestroPrograma, _BusquedaSolicitudPago._maestroTipologiaEntities.idMaestroTipologia, _BusquedaSolicitudPago._maestroLlamadoEntities.idMaestroLlamado, _BusquedaSolicitudPago._maestroTipoProveedorEntities.idMaestroTipoProveedor, _BusquedaSolicitudPago._proovedorEntities.nombreProveedor, _BusquedaSolicitudPago._maestroServicioEntities.nombreMaestroServicio, _BusquedaSolicitudPago._RegionesEntities.idRegion, _BusquedaSolicitudPago._ProvinciasEntities.idProvincia, _BusquedaSolicitudPago._ComunasEntities.idComuna, _BusquedaSolicitudPago._maestroModalidadEntities.idMaestroModalidad, null);

                    _BusquedaSolicitudPago.lstRegiones = auxBusquedaSolicitudPago.lstRegiones;
                    _BusquedaSolicitudPago.lstProvincias = auxBusquedaSolicitudPago.lstProvincias;
                    _BusquedaSolicitudPago.lstComunas = auxBusquedaSolicitudPago.lstComunas;

                    _BusquedaSolicitudPago._regionUserEntities = auxBusquedaSolicitudPago._regionUserEntities;

                    _BusquedaSolicitudPago.lstTipologias = auxBusquedaSolicitudPago.lstTipologias;
                    _BusquedaSolicitudPago.lstLlamado = auxBusquedaSolicitudPago.lstLlamado;
                    _BusquedaSolicitudPago.lstServicios = auxBusquedaSolicitudPago.lstServicios;
                    _BusquedaSolicitudPago.lstTipoProveedor = auxBusquedaSolicitudPago.lstTipoProveedor;
                    _BusquedaSolicitudPago.lstModalidades = auxBusquedaSolicitudPago.lstModalidades;

                    _BusquedaSolicitudPago._lstPrograma = auxBusquedaSolicitudPago._lstPrograma;
                    _BusquedaSolicitudPago._lstPrograma = auxBusquedaSolicitudPago._lstPrograma;
                    _BusquedaSolicitudPago._modificacionEstadoSolicitudEntities = new modificacionEstadoSolicitudEntities();
                    _BusquedaSolicitudPago._modificacionEstadoSolicitudEntities = _modificacionEstadoSolicitudEntities;
                }
                else
                {
                    auxBusquedaSolicitudPago = BusquedaSolicitudPagoFactory.getInstanciasFormulario(funcionarioEntities.idRegion);
                }

                return View("BusquedaSolicitud", _BusquedaSolicitudPago);
            }
            catch (Exception ex)
            {
                return View("BusquedaSolicitud", (new BusquedaSolicitudPago()));
            }
        }
    }
}



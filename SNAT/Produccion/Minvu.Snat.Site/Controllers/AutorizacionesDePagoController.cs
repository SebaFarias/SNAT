using Minvu.Snat.Domain.Entities;
using Minvu.Snat.Domain.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelectPdf;
using System.Configuration;


namespace Minvu.Snat.Site.Controllers
{
    public class AutorizacionesDePagoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult reporteAutorizacion()
        {
            try
            {

                BusquedaDetalleAutorizaciondePago _busquedaDetalleAutorizaciondePago = new BusquedaDetalleAutorizaciondePago();
                if (Session["busquedaDetalleAutorizacion"] != null && Session["IdTipoProveedor"] != null)
                {


                    List<DetalleAutorizacionCompletaEntities> _listDetalleAutorizacionCompletaEntities = (List<DetalleAutorizacionCompletaEntities>)Session["_listDetalleAutorizacionCompletaEntities"];
                    List<solicitudPagoEntities> listSolicitudPago = new List<solicitudPagoEntities>();

                    _busquedaDetalleAutorizaciondePago = Session["busquedaDetalleAutorizacion"] as BusquedaDetalleAutorizaciondePago;
                    List<solicitudPagoEntities> _auxListSolicitud = DetalleAutorizacionCompletaEntitiesFactory.getListSolicitudPagoEntities(_busquedaDetalleAutorizaciondePago._listSolicitudes);


                    int IdTipoProveedor = Convert.ToInt32(Session["IdTipoProveedor"]);
                    int contadorInicial = _busquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoResumenEntities.NOMBREREGIONDIRECCION.Length;
                    string Region = "";

                    Region = _busquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoResumenEntities.NOMBREREGIONDIRECCION.ToString().Replace("Región del ", "");

                    if (contadorInicial == Region.Length)
                        Region = _busquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoResumenEntities.NOMBREREGIONDIRECCION.ToString().Replace("Región de ", "");

                    if (contadorInicial == Region.Length)
                        Region = _busquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoResumenEntities.NOMBREREGIONDIRECCION.ToString().Replace("Región ", "");


                    string Tipologia = string.Empty;
                    string Modalidad = string.Empty;
                    string SubModalidad = string.Empty;
                    foreach (var item in _busquedaDetalleAutorizaciondePago.lstDetalleAutorizaciondePagoDetalle)
                    {
                        if (item.NOMBREMAESTROMODALIDAD != string.Empty && item.NOMBREMAESTROMODALIDAD != null)
                            Modalidad = item.NOMBREMAESTROMODALIDAD;
                        else
                            Modalidad = "Sin información";
                        Tipologia = item.NOMBREMAESTROTIPOLOGIA;
                        if (item.NOMBREMAESTROSUBMODALIDAD != string.Empty && item.NOMBREMAESTROSUBMODALIDAD != null)
                            SubModalidad = item.NOMBREMAESTROSUBMODALIDAD;
                        else
                            SubModalidad = "Sin información";

                        break;
                    }
                    int ufAPesos = (27284 * Convert.ToInt32(_busquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoResumenEntities.MONTOTOTALAUTORIZACION));


                    string ServidorReporte = ConfigurationManager.AppSettings["RutaServidorReporte"];
                    string ServerUrl = ConfigurationManager.AppSettings["URL_Report_Server"];
                    Report.parametro[] parametros = new Report.parametro[33];
                    parametros[0] = new Report.parametro("IDAUTORIZACION", _busquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoResumenEntities.IDAUTORIZACION.ToString());
                    parametros[1] = new Report.parametro("IDTIPOPROVEEDOR", IdTipoProveedor.ToString());
                    parametros[2] = new Report.parametro("Region", Region);
                    parametros[3] = new Report.parametro("UsuarioRatificador", _busquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoResumenEntities.USUARIORESPONSABLEAUTORIZACION);
                    parametros[4] = new Report.parametro("FechaUF", "Sin información");
                    parametros[5] = new Report.parametro("FechaInyeccion", _busquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoResumenEntities.NOMBREMAESTROESTADOAUTORIZACION);
                    parametros[6] = new Report.parametro("ValorUFPesos", "Sin información");
                    parametros[7] = new Report.parametro("MontoAutorizacion", _busquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoResumenEntities.MONTOTOTALAUTORIZACION.ToString().Replace('.', ','));
                    parametros[8] = new Report.parametro("FechaImpresion", DateTime.Now.ToShortDateString());
                    parametros[9] = new Report.parametro("UsuarioImpresion", @SiteHelper.NombreCompletoUsuario);


                    foreach (var item in _listDetalleAutorizacionCompletaEntities)
                    {

                        if (item.NOMBRE_SERVICIO == "FTO")
                        {
                            parametros[20] = new Report.parametro("MONTOFTO", item.NOMBRE_SERVICIO);

                        }
                        else if ((item.PAGO_TOTAL_POR_SERVICIO > 0) && (item.NOMBRE_SERVICIO == "1" || item.NOMBRE_SERVICIO == "I" || item.NOMBRE_SERVICIO == "A1"))
                        {
                            parametros[10] = new Report.parametro("S1", item.NOMBRE_SERVICIO);
                        }
                        else if ((item.PAGO_TOTAL_POR_SERVICIO > 0) && (item.NOMBRE_SERVICIO == "2" || item.NOMBRE_SERVICIO == "II" || item.NOMBRE_SERVICIO == "A2"))
                        {
                            parametros[11] = new Report.parametro("S2", item.NOMBRE_SERVICIO);
                        }
                        else if ((item.PAGO_TOTAL_POR_SERVICIO > 0) && (item.NOMBRE_SERVICIO == "3" || item.NOMBRE_SERVICIO == "III" || item.NOMBRE_SERVICIO == "A3"))
                        {
                            parametros[12] = new Report.parametro("S3", item.NOMBRE_SERVICIO);
                        }
                        else if ((item.PAGO_TOTAL_POR_SERVICIO > 0) && (item.NOMBRE_SERVICIO == "4" || item.NOMBRE_SERVICIO == "IV" || item.NOMBRE_SERVICIO == "A4"))
                        {
                            parametros[13] = new Report.parametro("S4", item.NOMBRE_SERVICIO);
                        }
                        else if ((item.PAGO_TOTAL_POR_SERVICIO > 0) && (item.NOMBRE_SERVICIO == "5" || item.NOMBRE_SERVICIO == "V" || item.NOMBRE_SERVICIO == "A5"))
                        {
                            parametros[14] = new Report.parametro("S5", item.NOMBRE_SERVICIO);
                        }
                        else if ((item.PAGO_TOTAL_POR_SERVICIO > 0) && (item.NOMBRE_SERVICIO == "6" || item.NOMBRE_SERVICIO == "B"))
                        {
                            parametros[15] = new Report.parametro("S6", item.NOMBRE_SERVICIO);
                        }
                        else if (item.PAGO_TOTAL_POR_SERVICIO > 0 && item.NOMBRE_SERVICIO == "C1")
                        {
                            parametros[16] = new Report.parametro("S7", item.NOMBRE_SERVICIO);
                        }
                        else if (item.PAGO_TOTAL_POR_SERVICIO > 0 && item.NOMBRE_SERVICIO == "C2")
                        {
                            parametros[17] = new Report.parametro("S8", item.NOMBRE_SERVICIO);
                        }
                        else if (item.PAGO_TOTAL_POR_SERVICIO > 0 && item.NOMBRE_SERVICIO == "C3")
                        {
                            parametros[18] = new Report.parametro("S9", item.NOMBRE_SERVICIO);
                        }
                        else if (item.PAGO_TOTAL_POR_SERVICIO > 0 && (item.NOMBRE_SERVICIO == "C4" || item.NOMBRE_SERVICIO == "P.A."))
                        {
                            parametros[19] = new Report.parametro("S10", item.NOMBRE_SERVICIO);
                        }
                    }



                    if (parametros[20].Valor == null)
                    {
                        parametros[20] = new Report.parametro("MONTOFTO", "N/A");

                    }
                    if (parametros[10].Valor == null)
                    {
                        parametros[10] = new Report.parametro("S1", "N/A");
                    }
                    if (parametros[11].Valor == null)
                    {
                        parametros[11] = new Report.parametro("S2", "N/A");
                    }

                    if (parametros[12].Valor == null)
                    {
                        parametros[12] = new Report.parametro("S3", "N/A");
                    }
                    if (parametros[13].Valor == null)
                    {
                        parametros[13] = new Report.parametro("S4", "N/A");
                    }
                    if (parametros[14].Valor == null)
                    {
                        parametros[14] = new Report.parametro("S5", "N/A");
                    }
                    if (parametros[15].Valor == null)
                    {
                        parametros[15] = new Report.parametro("S6", "N/A");
                    }
                    if (parametros[16].Valor == null)
                    {
                        parametros[16] = new Report.parametro("S7", "N/A");
                    }
                    if (parametros[17].Valor == null)
                    {
                        parametros[17] = new Report.parametro("S8", "N/A");
                    }
                    if (parametros[18].Valor == null)
                    {
                        parametros[18] = new Report.parametro("S9", "N/A");
                    }
                    if (parametros[19].Valor == null)
                    {
                        parametros[19] = new Report.parametro("S10", "N/A");
                    }

                    parametros[21] = new Report.parametro("SALDOFTO", _busquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.SALDOFTO.ToString().Replace('.', ','));
                    parametros[22] = new Report.parametro("MONTOTOTALPROYECTO", _busquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.MONTOTOTALPROYECTO.ToString().Replace('.', ','));
                    parametros[23] = new Report.parametro("MontoaPagar", _busquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.MONTOAPAGAR.ToString().Replace('.', ','));
                    parametros[24] = new Report.parametro("NOMBREPROGRAMA", _busquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoResumenEntities.NOMBREPROGRAMA.ToString());
                    parametros[25] = new Report.parametro("nombreLLamado", _busquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoResumenEntities.NOMBREMAESTROLLAMADO.ToString().Replace("Llamado", ""));
                    parametros[26] = new Report.parametro("nombreModalidad", Modalidad);
                    parametros[27] = new Report.parametro("nombreTipologia", Tipologia);
                    parametros[28] = new Report.parametro("nombreSubModalidad", SubModalidad);
                    parametros[29] = new Report.parametro("rutProveedor", _busquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoResumenEntities.RUTPROVEEDOR.ToString());
                    foreach (var item in _auxListSolicitud)
                    {


                        if (item.idMandatoProveedor > 0)
                        {
                            proveedorEntities _auxProveedor = proveedorEntitiesFactory.getProveedor(Convert.ToInt64(item.idMandatoProveedor));

                            parametros[30] = new Report.parametro("RutDestinatarioPago", _auxProveedor.rutProveedor + "-" + _auxProveedor.dvDigitoprovedor.ToString());
                            parametros[31] = new Report.parametro("nombreDestinatarioPago", _auxProveedor.nombreProveedor);
                        }
                        else
                        {
                            parametros[30] = new Report.parametro("RutDestinatarioPago", "Sin Información");
                            parametros[31] = new Report.parametro("nombreDestinatarioPago", "Sin Información");
                        }

                        break;
                    }



                    //parametros[32] = new Report.parametro("montoAutorizacionPesos", "Sin Información");
                    parametros[32] = new Report.parametro("montoAutorizacionPesos", "0");

                    Report report = new Report(ServidorReporte, ServerUrl, System.Configuration.ConfigurationManager.AppSettings["PATH_RPT_Certificado"], parametros);



                    byte[] buffer = report.ObtenerBytesPDF();


                    return File(buffer, "application/octet-stream", "certificado.pdf");


                }
                else
                {
                    Session.Clear();
                    return RedirectToAction("BusquedaSolicitud", "SolicitudPago");
                }





            }
            catch (Exception ex)
            {
                Session.Clear();
                return RedirectToAction("Index", "Home");

            }

        }



        public ActionResult DetalleAutorizacionDePago(long? IdAutorizacion, long? IdTipoProveedor)
        {
            BusquedaDetalleAutorizaciondePago _BusquedaDetalleAutorizaciondePago = new BusquedaDetalleAutorizaciondePago();
            _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoResumenEntities = BusquedaDetalleAutorizaciondePagoFactory.getDetalleAutorizacionesResumenResult(IdAutorizacion);
            _BusquedaDetalleAutorizaciondePago.lstDetalleAutorizaciondePagoDetalle = BusquedaDetalleAutorizaciondePagoFactory.getListDetalleAutorizacionesDetalleResult(IdAutorizacion, IdTipoProveedor);
            _BusquedaDetalleAutorizaciondePago._listDetalleAutorizacionCompletaEntities = BusquedaDetalleAutorizaciondePagoFactory.getListDetalleAutorizacionCompleta(IdAutorizacion);

            _BusquedaDetalleAutorizaciondePago.listaNombres = new List<string>();
            _BusquedaDetalleAutorizaciondePago._listSolicitudes = new List<DetalleAutorizacionCompletaEntities>();
            long? idsolicitud = 0;


            _BusquedaDetalleAutorizaciondePago._listDetalleAutorizacionCompletaEntities = BusquedaDetalleAutorizaciondePagoFactory.getListDetalleAutorizacionCompleta(IdAutorizacion);



            foreach (var item in _BusquedaDetalleAutorizaciondePago._listDetalleAutorizacionCompletaEntities)
            {
                idsolicitud = item.IDSOLICITUDPAGO;
                if (_BusquedaDetalleAutorizaciondePago._listSolicitudes.Count == 0)
                    _BusquedaDetalleAutorizaciondePago._listSolicitudes.Add(item);
                else
                {
                    if (!_BusquedaDetalleAutorizaciondePago._listSolicitudes.Exists(c => c.IDSOLICITUDPAGO == idsolicitud))
                    {
                        _BusquedaDetalleAutorizaciondePago._listSolicitudes.Add(item);
                    }

                }

                if (!_BusquedaDetalleAutorizaciondePago.listaNombres.Exists(c => c == item.NOMBRE_SERVICIO))
                {
                    _BusquedaDetalleAutorizaciondePago.listaNombres.Add(item.NOMBRE_SERVICIO);
                }


            }



            _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities = new DetalleAutorizaciondePagoDetalleEntities();
            _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S1 = 0;
            _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S2 = 0;
            _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S3 = 0;
            _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S4 = 0;
            _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S5 = 0;
            _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S6 = 0;
            _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S7 = 0;
            _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S8 = 0;
            _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S9 = 0;
            _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S10 = 0;
            _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.SALDOFTO = 0;
            _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.MONTOFTO = 0;
            _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.MONTOTOTALPROYECTO = 0;
            _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.MONTOAPAGAR = 0;


            if (_BusquedaDetalleAutorizaciondePago.lstDetalleAutorizaciondePagoDetalle.Count > 0)
            {
                foreach (var item in _BusquedaDetalleAutorizaciondePago.lstDetalleAutorizaciondePagoDetalle)
                {

                    _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S1 = _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S1 + item.S1;
                    _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S2 = _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S2 + item.S2;
                    _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S3 = _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S3 + item.S3;
                    _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S4 = _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S4 + item.S4;
                    _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S5 = _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S5 + item.S5;
                    _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S6 = _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S6 + item.S6;
                    _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S7 = _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S6 + item.S7;
                    _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S8 = _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S8 + item.S8;
                    _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S9 = _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S9 + item.S9;
                    _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S10 = _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.S10 + item.S10;
                    _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.SALDOFTO = _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.SALDOFTO + item.SALDOFTO;
                    _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.MONTOFTO = _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.MONTOFTO + item.MONTOFTO;
                    _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.MONTOTOTALPROYECTO = _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.MONTOTOTALPROYECTO + item.MONTOTOTALPROYECTO;
                    _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.MONTOAPAGAR = _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoDetalleEntities.MONTOAPAGAR + item.MONTOAPAGAR;

                }

            }



            idsolicitud = 0;
            int contador2 = 0;
            int contador3 = 0;
            List<solicitudAutorizacionEntities> ListClass = new List<solicitudAutorizacionEntities>();


            if (_BusquedaDetalleAutorizaciondePago._listDetalleAutorizacionCompletaEntities != null && _BusquedaDetalleAutorizaciondePago._listDetalleAutorizacionCompletaEntities.Count > 0)
            {
                foreach (var porSolicitud in _BusquedaDetalleAutorizaciondePago._listSolicitudes)
                {
                    idsolicitud = porSolicitud.IDSOLICITUDPAGO;
                    contador2 = contador2 + 1;
                    contador3 = 0;


                    foreach (var porFila in _BusquedaDetalleAutorizaciondePago._listDetalleAutorizacionCompletaEntities)
                    {
                        if (porSolicitud.IDSOLICITUDPAGO == porFila.IDSOLICITUDPAGO && contador2 == 1 && contador3 == 0)
                        {
                            contador3 = contador3 + 1;

                            solicitudAutorizacionEntities auxClass = new solicitudAutorizacionEntities();

                            auxClass.numeroAutorizacionSolicitudAutorizacion = _BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoResumenEntities.IDAUTORIZACION;

                            foreach (var item in _BusquedaDetalleAutorizaciondePago._listDetalleAutorizacionCompletaEntities)
                            {
                                if (idsolicitud == item.IDSOLICITUDPAGO)
                                {

                                    auxClass.numeroSolicitudSolicitudAutorizacion = item.IDSOLICITUDPAGO;
                                    auxClass.codigoProyectoSolicitudAutorizacion = Convert.ToInt64(item.CODIGOPROYECTOINFORMACIONPROYECTO);
                                    auxClass.nombreProyectoSolicitudAutorizacion = item.NOMBREPROYECTOINFORMACIONPROYECTO;
                                    auxClass.ubicacionComunaSolicitudAutorizacion = item.NOMBRECOMUNADIRECCION;

                                    break;
                                }
                            }
                            foreach (var item in _BusquedaDetalleAutorizaciondePago._listDetalleAutorizacionCompletaEntities)
                            {


                                if (idsolicitud == item.IDSOLICITUDPAGO)
                                {

                                    if (item.NOMBRE_SERVICIO == "FTO")
                                    {
                                        auxClass.FTOSolicitudAutorizacion = item.MONTO_A_PAGO_FTO;

                                    }
                                    else if ((item.PAGO_TOTAL_POR_SERVICIO > 0) && (item.NOMBRE_SERVICIO == "1" || item.NOMBRE_SERVICIO == "I" || item.NOMBRE_SERVICIO == "A1"))
                                    {
                                        auxClass.S1 = item.PAGO_TOTAL_POR_SERVICIO;
                                    }
                                    else if ((item.PAGO_TOTAL_POR_SERVICIO > 0) && (item.NOMBRE_SERVICIO == "2" || item.NOMBRE_SERVICIO == "II" || item.NOMBRE_SERVICIO == "A2"))
                                    {
                                        auxClass.S2 = item.PAGO_TOTAL_POR_SERVICIO;
                                    }
                                    else if ((item.PAGO_TOTAL_POR_SERVICIO > 0) && (item.NOMBRE_SERVICIO == "3" || item.NOMBRE_SERVICIO == "III" || item.NOMBRE_SERVICIO == "A3"))
                                    {
                                        auxClass.S3 = item.PAGO_TOTAL_POR_SERVICIO;
                                    }
                                    else if ((item.PAGO_TOTAL_POR_SERVICIO > 0) && (item.NOMBRE_SERVICIO == "4" || item.NOMBRE_SERVICIO == "IV" || item.NOMBRE_SERVICIO == "A4"))
                                    {
                                        auxClass.S4 = item.PAGO_TOTAL_POR_SERVICIO;
                                    }
                                    else if ((item.PAGO_TOTAL_POR_SERVICIO > 0) && (item.NOMBRE_SERVICIO == "5" || item.NOMBRE_SERVICIO == "V" || item.NOMBRE_SERVICIO == "A5"))
                                    {
                                        auxClass.S5 = item.PAGO_TOTAL_POR_SERVICIO;
                                    }
                                    else if ((item.PAGO_TOTAL_POR_SERVICIO > 0) && (item.NOMBRE_SERVICIO == "6" || item.NOMBRE_SERVICIO == "B"))
                                    {
                                        auxClass.S6 = item.PAGO_TOTAL_POR_SERVICIO;
                                    }

                                    else if (item.PAGO_TOTAL_POR_SERVICIO > 0 && item.NOMBRE_SERVICIO == "C1")
                                    {
                                        auxClass.S7 = item.PAGO_TOTAL_POR_SERVICIO;
                                    }
                                    else if (item.PAGO_TOTAL_POR_SERVICIO > 0 && item.NOMBRE_SERVICIO == "C2")
                                    {
                                        auxClass.S8 = item.PAGO_TOTAL_POR_SERVICIO;
                                    }
                                    else if (item.PAGO_TOTAL_POR_SERVICIO > 0 && item.NOMBRE_SERVICIO == "C3")
                                    {
                                        auxClass.S9 = item.PAGO_TOTAL_POR_SERVICIO;
                                    }
                                    else if (item.PAGO_TOTAL_POR_SERVICIO > 0 && (item.NOMBRE_SERVICIO == "C4" || item.NOMBRE_SERVICIO == "P.A."))
                                    {
                                        auxClass.S10 = item.PAGO_TOTAL_POR_SERVICIO;
                                    }




                                }
                                else
                                {
                                }

                            }

                            foreach (var item in _BusquedaDetalleAutorizaciondePago._listDetalleAutorizacionCompletaEntities)
                            {
                                if (idsolicitud == item.IDSOLICITUDPAGO)
                                {
                                    if (item.MONTO_FTO_TOTAL_UF > 0)
                                    {
                                        auxClass.montoFTOTotalSolicitudAutorizacion = item.MONTO_FTO_TOTAL_UF;
                                    }

                                    if (item.MONTO_A_PAGO_FTO > 0)
                                    {
                                        auxClass.montoAPagoSolicitudAutorizacion = item.MONTO_A_PAGO_FTO;
                                    }

                                    if (item.MONTO_AT_UF > 0)
                                    {
                                        auxClass.montoATSolicitudAutorizacion = item.MONTO_AT_UF;
                                    }

                                    if (item.MONTO_A_PAGO > 0 && item.MONTO_A_PAGO_FTO == 0)
                                    {
                                        auxClass.montoAPagoSolicitudAutorizacion = item.MONTO_A_PAGO;
                                    }


                                    break;
                                }
                            }

                            ListClass.Add(auxClass);
                            contador2 = 0;
                        }

                    }
                }
            }

            BusquedaDetalleAutorizaciondePagoFactory.deleteSolicitudAutorizacion(_BusquedaDetalleAutorizaciondePago._DetalleAutorizaciondePagoResumenEntities.IDAUTORIZACION);
            BusquedaDetalleAutorizaciondePagoFactory.saveSolicitudAutorizacion(ListClass);


            _BusquedaDetalleAutorizaciondePago._grillaPDF = DetalleAutorizacionPDFFactory.getListDetalleAutorizacionPDF(IdAutorizacion);
            bool S1SOLICITUDAUTORIZACION = false;
            bool S2SOLICITUDAUTORIZACION = false;
            bool S3SOLICITUDAUTORIZACION = false;
            bool S4SOLICITUDAUTORIZACION = false;
            bool S5SOLICITUDAUTORIZACION = false;
            bool S6SOLICITUDAUTORIZACION = false;
            bool S7SOLICITUDAUTORIZACION = false;
            bool S8SOLICITUDAUTORIZACION = false;
            bool S9SOLICITUDAUTORIZACION = false;
            bool S10SOLICITUDAUTORIZACION = false;

            foreach (var item in _BusquedaDetalleAutorizaciondePago._grillaPDF)
            {
                if (item.S1SOLICITUDAUTORIZACION != "0,000")
                {
                    S1SOLICITUDAUTORIZACION = true;
                }
                if (item.S2SOLICITUDAUTORIZACION != "0,000")
                {
                    S2SOLICITUDAUTORIZACION = true;
                }
                if (item.S3SOLICITUDAUTORIZACION != "0,000")
                {
                    S3SOLICITUDAUTORIZACION = true;
                }
                if (item.S4SOLICITUDAUTORIZACION != "0,000")
                {
                    S4SOLICITUDAUTORIZACION = true;
                }
                if (item.S5SOLICITUDAUTORIZACION != "0,000")
                {
                    S5SOLICITUDAUTORIZACION = true;
                }
                if (item.S6SOLICITUDAUTORIZACION != "0,000")
                {
                    S6SOLICITUDAUTORIZACION = true;
                }
                if (item.S7SOLICITUDAUTORIZACION != "0,000")
                {
                    S7SOLICITUDAUTORIZACION = true;
                }
                if (item.S8SOLICITUDAUTORIZACION != "0,000")
                {
                    S8SOLICITUDAUTORIZACION = true;
                }
                if (item.S9SOLICITUDAUTORIZACION != "0,000")
                {
                    S9SOLICITUDAUTORIZACION = true;
                }
                if (item.S10SOLICITUDAUTORIZACION != "0,000")
                {
                    S10SOLICITUDAUTORIZACION = true;
                }

            }

            foreach (var item in _BusquedaDetalleAutorizaciondePago._grillaPDF)
            {
                if (!S1SOLICITUDAUTORIZACION)
                    item.S1SOLICITUDAUTORIZACION = "N";
                if (!S2SOLICITUDAUTORIZACION)
                    item.S2SOLICITUDAUTORIZACION = "N";
                if (!S3SOLICITUDAUTORIZACION)
                    item.S3SOLICITUDAUTORIZACION = "N";
                if (!S4SOLICITUDAUTORIZACION)
                    item.S4SOLICITUDAUTORIZACION = "N";
                if (!S5SOLICITUDAUTORIZACION)
                    item.S5SOLICITUDAUTORIZACION = "N";
                if (!S6SOLICITUDAUTORIZACION)
                    item.S6SOLICITUDAUTORIZACION = "N";
                if (!S7SOLICITUDAUTORIZACION)
                    item.S7SOLICITUDAUTORIZACION = "N";
                if (!S8SOLICITUDAUTORIZACION)
                    item.S8SOLICITUDAUTORIZACION = "N";
                if (!S9SOLICITUDAUTORIZACION)
                    item.S9SOLICITUDAUTORIZACION = "N";
                if (!S10SOLICITUDAUTORIZACION)
                    item.S10SOLICITUDAUTORIZACION = "N";
            }


            Session["_listDetalleAutorizacionCompletaEntities"] = _BusquedaDetalleAutorizaciondePago._listDetalleAutorizacionCompletaEntities;
            Session["IdTipoProveedor"] = IdTipoProveedor;
            Session["busquedaDetalleAutorizacion"] = _BusquedaDetalleAutorizaciondePago;



            return View(_BusquedaDetalleAutorizaciondePago);
        }

        // GET: AutorizacionesDePago
        public ActionResult IngresoAutorizacionDePago()
        {
            return View();
        }



        public ActionResult BusquedaAutorizacionDePago(BusquedaAutorizaciondePago _BusquedaAutorizaciondePago, bool? sesiones)
        {
            if (sesiones == null || sesiones == false)
            {
                Session["BusquedaAutorizacionesVolver"] = null;
            }

            if (Session["BusquedaAutorizacionesVolver"] != null)
            {
                _BusquedaAutorizaciondePago = (BusquedaAutorizaciondePago)Session["BusquedaAutorizacionesVolver"];
            }

            string usuario = @SiteHelper.UserName;
            FuncionarioEntities funcionarioEntities = BusquedaSolicitudPagoFactory.getRegionUsuario(usuario);

            object auxBusquedaAutorizacionDePago = BusquedaAutorizaciondePagoFactory.getInstanciasFormulario(funcionarioEntities.idRegion);



            string sCodPro = _BusquedaAutorizaciondePago._informacionProyectoEntities == null ? null :
                    _BusquedaAutorizaciondePago._informacionProyectoEntities.codigoProyectoInformacionProyecto;
            long lIdProg = Convert.ToInt64(_BusquedaAutorizaciondePago._maestroProgramaEntities == null ? 0 :
                (_BusquedaAutorizaciondePago._maestroProgramaEntities.idMaestroPrograma == null ? 0 :
                _BusquedaAutorizaciondePago._maestroProgramaEntities.idMaestroPrograma));
            long lIdTipo = Convert.ToInt64(_BusquedaAutorizaciondePago._maestroTipologiaEntities == null ? 0 :
                (_BusquedaAutorizaciondePago._maestroTipologiaEntities.idMaestroTipologia == null ? 0 :
                _BusquedaAutorizaciondePago._maestroTipologiaEntities.idMaestroTipologia));
            long lIdAut = Convert.ToInt64(_BusquedaAutorizaciondePago._autorizacionEntities == null ? 0 :
                (_BusquedaAutorizaciondePago._autorizacionEntities.idAutorizacion == null ? 0 :
                _BusquedaAutorizaciondePago._autorizacionEntities.idAutorizacion));
            string sNomPro = _BusquedaAutorizaciondePago._proovedorEntities == null ? String.Empty :
                (_BusquedaAutorizaciondePago._proovedorEntities.nombreProveedor == null ? String.Empty :
                _BusquedaAutorizaciondePago._proovedorEntities.nombreProveedor);
            long lIdMod = Convert.ToInt64(_BusquedaAutorizaciondePago._maestroModalidadEntities == null ? 0 :
                _BusquedaAutorizaciondePago._maestroModalidadEntities.idMaestroModalidad);
            long lIdLlam = Convert.ToInt64(_BusquedaAutorizaciondePago._maestroLlamadoEntities == null ? 0 :
                (_BusquedaAutorizaciondePago._maestroLlamadoEntities.idMaestroLlamado == null ? 0 :
                _BusquedaAutorizaciondePago._maestroLlamadoEntities.idMaestroLlamado));

            if (Minvu.Security.SingleSignOn.CurrentPrincipal.HasTarea("Sim_ConsultarSolPagoReg"))
            {
                _BusquedaAutorizaciondePago._RegionesEntities = new RegionesEntities();

                _BusquedaAutorizaciondePago._RegionesEntities.idRegion = funcionarioEntities.idRegion;
            }


            int iIdReg = Convert.ToInt32(_BusquedaAutorizaciondePago._RegionesEntities == null ? 0 :
                (_BusquedaAutorizaciondePago._RegionesEntities.idRegion == null ? 0 :
                _BusquedaAutorizaciondePago._RegionesEntities.idRegion));
            int iIdProv = Convert.ToInt32(_BusquedaAutorizaciondePago._ProvinciasEntities == null ? 0 :
                (_BusquedaAutorizaciondePago._ProvinciasEntities.idProvincia == null ? 0 :
                _BusquedaAutorizaciondePago._ProvinciasEntities.idProvincia));
            int iIdCom = Convert.ToInt32(_BusquedaAutorizaciondePago._ComunasEntities == null ? 0 :
                (_BusquedaAutorizaciondePago._ComunasEntities.idComuna == null ? 0 :
                _BusquedaAutorizaciondePago._ComunasEntities.idComuna));
            long lIdServ = Convert.ToInt64(_BusquedaAutorizaciondePago._maestroServicioEntities == null ? 0 :
                (_BusquedaAutorizaciondePago._maestroServicioEntities.idMaestroServicio == null ? 0 :
                _BusquedaAutorizaciondePago._maestroServicioEntities.idMaestroServicio));
            long lIdTipPro = Convert.ToInt64(_BusquedaAutorizaciondePago._maestroTipoProveedorEntities == null ? 0 :
                (_BusquedaAutorizaciondePago._maestroTipoProveedorEntities.idMaestroTipoProveedor == null ? 0 :
                _BusquedaAutorizaciondePago._maestroTipoProveedorEntities.idMaestroTipoProveedor));
            long lIdTit = Convert.ToInt64(_BusquedaAutorizaciondePago._maestroTituloEntities == null ? 0 :
                (_BusquedaAutorizaciondePago._maestroTituloEntities.idMaestroTitulo == null ? 0 :
                _BusquedaAutorizaciondePago._maestroTituloEntities.idMaestroTitulo));
            //long lIdEstAut = Convert.ToInt64(_BusquedaAutorizaciondePago._maestroEstadoAutorizacionEntities == null ? 0 :
            //    (_BusquedaAutorizaciondePago._maestroEstadoAutorizacionEntities.idMaestroEstadoAutorizacion == null ? 0 :
            //    _BusquedaAutorizaciondePago._maestroEstadoAutorizacionEntities.idMaestroEstadoAutorizacion));

            long lIdEstAut = Convert.ToInt64(_BusquedaAutorizaciondePago._maestroEstadoAutorizacionEntities == null ? 99 : //si el estado esta en nulo
                (_BusquedaAutorizaciondePago._maestroEstadoAutorizacionEntities.estadoMaestroEstadoAutorizacion == false ? 99 : // si el check esta des-seleccionado
                (_BusquedaAutorizaciondePago._maestroEstadoAutorizacionEntities.idMaestroEstadoAutorizacion == null ? 0 : // si esta el check esta seleccionado 
               _BusquedaAutorizaciondePago._maestroEstadoAutorizacionEntities.idMaestroEstadoAutorizacion)));



            BusquedaAutorizaciondePago _aux2 = new BusquedaAutorizaciondePago();
            _aux2 = auxBusquedaAutorizacionDePago as BusquedaAutorizaciondePago;

            _BusquedaAutorizaciondePago.lstRegiones = _aux2.lstRegiones;
            _BusquedaAutorizaciondePago._lstPrograma = _aux2._lstPrograma;
            _BusquedaAutorizaciondePago.lstProvincias = _aux2.lstProvincias;
            _BusquedaAutorizaciondePago.lstTipologias = _aux2.lstTipologias;
            _BusquedaAutorizaciondePago.lstComunas = _aux2.lstComunas;
            _BusquedaAutorizaciondePago.lstServicios = _aux2.lstServicios;
            _BusquedaAutorizaciondePago.lstLlamado = _aux2.lstLlamado;
            _BusquedaAutorizaciondePago.lstTipoProveedor = _aux2.lstTipoProveedor;
            _BusquedaAutorizaciondePago.lstTipologias = _aux2.lstTipologias;
            _BusquedaAutorizaciondePago.lstLlamado = _aux2.lstLlamado;
            _BusquedaAutorizaciondePago.lstModalidades = _aux2.lstModalidades;
            _BusquedaAutorizaciondePago._regionUserEntities = _aux2._regionUserEntities;
            _BusquedaAutorizaciondePago.lstEstadoAutorizacion = _aux2.lstEstadoAutorizacion;






            //_BusquedaAutorizaciondePago.lstServicios = lIdTipo != 0 ? BusquedaAutorizaciondePagoFactory.getListServicioTipologia(lIdTipo) :
            //    BusquedaAutorizaciondePagoFactory.getListServicios();

            //_BusquedaAutorizaciondePago.lstProvincias = iIdReg != 0 ? BusquedaAutorizaciondePagoFactory.getListProvinciasRegion(iIdReg) :
            //       BusquedaAutorizaciondePagoFactory.getListProvincias();

            //_BusquedaAutorizaciondePago.lstComunas = iIdProv != 0 ? BusquedaAutorizaciondePagoFactory.getListComunasProvincia(iIdProv) :
            //    BusquedaAutorizaciondePagoFactory.getListComunas();

            //Busqueda de Autorizaciones de Pago
            _BusquedaAutorizaciondePago._listadoAutorizacionEntities = BusquedaAutorizaciondePagoFactory.obtenerListadoAutorizacion(sCodPro, lIdProg, lIdTipo, lIdLlam, lIdTipPro,
                                                                                                                                sNomPro, lIdServ, iIdReg, iIdProv, iIdCom, lIdMod, lIdAut,
                                                                                                                                lIdTit, lIdEstAut);

            if (Session["BusquedaAutorizacionesVolver"] == null)
            {
                Session["BusquedaAutorizacionesVolver"] = _BusquedaAutorizaciondePago;
            }

            return View(_BusquedaAutorizaciondePago);

            //ViewBag.Message = "El proyecto no se encuentra en las bases de datos de SNAT";
        }

        public JsonResult RatificarAutorizacion(string idAutorizacion)
        {
            contratoSolicitudPagoEntities objResultSPS = BusquedaAutorizaciondePagoFactory.RatificaAutorizacion(Convert.ToInt64(idAutorizacion), SiteHelper.UserName);

            bool bSuccess = objResultSPS.codigoSalidaRatifica == "1" ? true : false;
            return Json(new { success = bSuccess, Estado = objResultSPS.codigoSalidaRatifica, Mensaje = objResultSPS.mensajeSalidaRatifica });
        }

        // POST: AutorizacionesDePago/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #region "Region/Provincia/Comuna"
        public JsonResult GetProvincias(string idRegion)
        {
            List<ProvinciasEntities> Provincias = new List<ProvinciasEntities>();

            if (idRegion != null && idRegion != String.Empty)
            {
                BusquedaAutorizaciondePago objBusquedaAutorizaciondePago = new BusquedaAutorizaciondePago();
                Provincias = BusquedaAutorizaciondePagoFactory.getListProvinciasRegion(int.Parse(idRegion));
            }

            return Json(new SelectList(Provincias, "idProvincia", "nombreProvincia"));
        }
        public JsonResult GetComunasRegion(string idRegion)
        {
            //Primero se obtienen las provincias
            List<ProvinciasEntities> lstProvincias = new List<ProvinciasEntities>();

            if (idRegion != null && idRegion != String.Empty)
            {
                BusquedaAutorizaciondePago objBusquedaAutorizaciondePago = new BusquedaAutorizaciondePago();
                lstProvincias = BusquedaAutorizaciondePagoFactory.getListProvinciasRegion(int.Parse(idRegion));
            }

            //Lista con todas las comunas de la Region
            List<ComunasEntities> lstComunasProvincias = new List<ComunasEntities>();

            foreach (ProvinciasEntities Provincia in lstProvincias)
            {
                //Obtengo comunas de la Provincia
                List<ComunasEntities> lstComunas = new List<ComunasEntities>();
                lstComunas = BusquedaAutorizaciondePagoFactory.getListComunasProvincia(Convert.ToInt32(Provincia.idProvincia));

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
                    BusquedaAutorizaciondePago objBusquedaAutorizaciondePago = new BusquedaAutorizaciondePago();
                    Comunas = BusquedaAutorizaciondePagoFactory.getListComunas();
                }
                else
                {
                    BusquedaAutorizaciondePago objBusquedaAutorizaciondePago = new BusquedaAutorizaciondePago();
                    Comunas = BusquedaAutorizaciondePagoFactory.getListComunasProvincia(int.Parse(idProvincia));
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
                    objComuna = BusquedaAutorizaciondePagoFactory.getComuna(int.Parse(idComuna));
                }
            }

            return Json(objComuna.idProvincia);
        }
        #endregion

        #region "Combos Programa/Tipologia/Servicio"
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

        public JsonResult GetServicioTipologia(string idTipologia, string idPrograma)//@*CambioQA*@
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
        #endregion
    }
}

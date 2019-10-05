using Minvu.Snat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.DAO;

namespace Minvu.Snat.Domain.Forms
{
    public class BusquedaAutorizaciondePago
    {
        public BusquedaAutorizaciondePago() { }
        #region Listados
        public List<RegionesEntities> lstRegiones { get; set; }
        public List<RegionesEntities> _regionUserEntities { get; set; }
        public List<ProvinciasEntities> lstProvincias { get; set; }
        public List<ComunasEntities> lstComunas { get; set; }

        public List<maestroTipologiaEntities> lstTipologias { get; set; }
        public List<listadoAutorizacionEntities> _listadoAutorizacionEntities { get; set; }
        public List<maestroLlamadoEntities> lstLlamado { get; set; }
        public List<maestroProgramaEntities> _lstPrograma { get; set; }
        public List<maestroTipoProveedorEntities> lstTipoProveedor { get; set; }
        public List<maestroServicioEntities> lstServicios { get; set; }
        public List<maestroModalidadEntities> lstModalidades { get; set; }
        public List<maestroTituloEntities> lstTitulo { get; set; }
        public List<maestroEstadoAutorizacionEntities> lstEstadoAutorizacion { get; set; }
        public List<autorizaciondePagoEntities> lstAutorizacionesResult { get; set; }
        #endregion

        #region Entidades
        public RegionesEntities _RegionesEntities { get; set; }
        public ProvinciasEntities _ProvinciasEntities { get; set; }
        public ComunasEntities _ComunasEntities { get; set; }

        public informacionProyectoEntities _informacionProyectoEntities { get; set; }
        public maestroServicioEntities _maestroServicioEntities { get; set; }
        public maestroLlamadoEntities _maestroLlamadoEntities { get; set; }
        public maestroProgramaEntities _maestroProgramaEntities { get; set; }
        public maestroTipologiaEntities _maestroTipologiaEntities { get; set; }
        public maestroTituloEntities _maestroTituloEntities { get; set; }
        public maestroTipoProveedorEntities _maestroTipoProveedorEntities { get; set; }
        public maestroModalidadEntities _maestroModalidadEntities { get; set; }
        public proovedorEntities _proovedorEntities { get; set; }
        public autorizacionEntities _autorizacionEntities { get; set; }
        public maestroEstadoAutorizacionEntities _maestroEstadoAutorizacionEntities { get; set; }
        public listadoAutorizacionEntities _AutorizaciondePagoEntities { get; set; }
        #endregion
    }

    public class BusquedaAutorizaciondePagoFactory
    {
        

        public static BusquedaAutorizaciondePago getInstanciasFormulario(int codigoRegion)
        {
            BusquedaAutorizaciondePago _BusquedaAutorizacionPago = new BusquedaAutorizaciondePago();
            _BusquedaAutorizacionPago.lstRegiones = RegionesEntitiesFactory.getListRegiones();
            _BusquedaAutorizacionPago._regionUserEntities = RegionesEntitiesFactory.GetRegion(codigoRegion);
            _BusquedaAutorizacionPago.lstProvincias = ProvinciasEntitiesFactory.getListProvinciaRegion(null);
            _BusquedaAutorizacionPago.lstComunas = ComunasEntitiesFactory.getComunasProvincia(null);

            _BusquedaAutorizacionPago.lstTipologias = maestroTipologiaEntitiesFactory.getListTipologiaPrograma(null);
            _BusquedaAutorizacionPago.lstTipologias = _BusquedaAutorizacionPago.lstTipologias.OrderBy(x => x.nombreMaestroTipologia).ToList();

            _BusquedaAutorizacionPago.lstLlamado = maestroLlamadoEntitiesFactory.getListLlamado();
            _BusquedaAutorizacionPago.lstLlamado = _BusquedaAutorizacionPago.lstLlamado.OrderBy(x => x.nombreMaestroLlamado).ToList();

            _BusquedaAutorizacionPago.lstServicios = maestroServicioEntitiesFactory.getListServicioTipologia(null);
            _BusquedaAutorizacionPago.lstServicios = _BusquedaAutorizacionPago.lstServicios.OrderBy(x => x.nombreMaestroServicio).ToList();

            _BusquedaAutorizacionPago.lstTipoProveedor = maestroTipoProveedorEntitiesFactory.getListTipoProveedor();
            _BusquedaAutorizacionPago.lstTipoProveedor = _BusquedaAutorizacionPago.lstTipoProveedor.OrderBy(x => x.nombreMaestroTipoProveedor).ToList();

            _BusquedaAutorizacionPago.lstModalidades = maestroModalidadEntitiesFactory.getListMaestroModalidad();
            _BusquedaAutorizacionPago.lstModalidades = _BusquedaAutorizacionPago.lstModalidades.OrderBy(x => x.nombreMaestroModalidad).ToList();

            _BusquedaAutorizacionPago._lstPrograma = maestroProgramaEntitiesFactory.getListMaestroPrograma();
            _BusquedaAutorizacionPago._lstPrograma = _BusquedaAutorizacionPago._lstPrograma.OrderBy(x => x.nombreMaestroPrograma).ToList();


            return _BusquedaAutorizacionPago;
        }

        public static List<maestroProgramaEntities> getListProgramas()
        {
            List<maestroProgramaEntities> _lstProgramas = new List<maestroProgramaEntities>();
            _lstProgramas = maestroProgramaEntitiesFactory.getListMaestroPrograma();

            return _lstProgramas;
        }

        public static List<maestroTipologiaEntities> getListTipologias()
        {
            List<maestroTipologiaEntities> _lstTipologias = new List<maestroTipologiaEntities>();
            _lstTipologias = maestroTipologiaEntitiesFactory.getListTipologia();

            return _lstTipologias;
        }

        public static List<RegionesEntities> getListRegiones()
        {
            RegionesEntities _Regiones = new RegionesEntities();
            _Regiones.lstRegiones = RegionesEntitiesFactory.getListRegiones();

            return _Regiones.lstRegiones;
        }

        public static List<maestroTipoProveedorEntities> getListTipoProveedor()
        {
            List<maestroTipoProveedorEntities> _lstTipoProveedor = new List<maestroTipoProveedorEntities>();
            _lstTipoProveedor = maestroTipoProveedorEntitiesFactory.getListTipoProveedor();

            return _lstTipoProveedor;
        }

        public static List<maestroModalidadEntities> getListModalidades()
        {
            List<maestroModalidadEntities> _lstModalidades = new List<maestroModalidadEntities>();
            _lstModalidades = maestroModalidadEntitiesFactory.getListMaestroModalidad();

            return _lstModalidades;
        }

        public static List<maestroTituloEntities> getListTitulo()
        {
            List<maestroTituloEntities> _lstTitulo = new List<maestroTituloEntities>();
            _lstTitulo = maestroTituloEntitiesFactory.getListMaestroTitulo();

            return _lstTitulo;
        }

        public static List<maestroEstadoAutorizacionEntities> getListEstadoAutorizacion()
        {
            List<maestroEstadoAutorizacionEntities> _lstEstadoAutorizacion = new List<maestroEstadoAutorizacionEntities>();
            _lstEstadoAutorizacion = maestroEstadoAutorizacionEntitiesFactory.getListEstadoAutorizacion();

            return _lstEstadoAutorizacion;
        }

        public static List<maestroLlamadoEntities> getListLlamado()
        {
            List<maestroLlamadoEntities> _lstLlamado = new List<maestroLlamadoEntities>();
            _lstLlamado = maestroLlamadoEntitiesFactory.getListLlamado();

            return _lstLlamado;
        }

        public static List<ProvinciasEntities> getListProvincias()
        {
            ProvinciasEntities _Provincias = new ProvinciasEntities();
            _Provincias.lstProvincias = ProvinciasEntitiesFactory.getListProvincias();

            return _Provincias.lstProvincias;
        }

        public static RegionesEntities getRegion(int idRegion)
        {
            return RegionesEntitiesFactory.getRegion(idRegion);
        }

        public static modificacionEstadoSolicitudEntities SaveModificacionEstadoSolicitud(modificacionEstadoSolicitudEntities objModificacionEstado)
        {
            return ModificacionEstadoSolicitudEntitiesFactory.SaveModificacionEstadoSolicitud(objModificacionEstado);
        }

        public static solicitudPagoEntities GetSolicitudPago(long idSolicitudPago)
        {
            return solicitudPagoEntitiesFactory.getSolicitudPago(idSolicitudPago);
        }

        #region BusquedaSeteoAutorizacion
        public static List<listadoAutorizacionEntities> obtenerListadoAutorizacion(string pCodProyecto, long? pIdMaestroPrograma, long? pIdMaestroTIpologia, long? pIdMaestroLLamado,
                                                                                    long? pIdMaestroTIpoProveedor, string pNombreProveedor, long? pIdServicio, long? pRegion,
                                                                                    long? pProvincia, long? pComuna, long? pIdModalidad, long? pIdAutorizacion,
                                                                                    long? pIdMaestroTitulo, long? pIdMaestroEstadoAutorizacion)
        {
            BusquedaAutorizaciondePago _busquedaAutorizacion = new BusquedaAutorizaciondePago();

            #region setNull
            if (pCodProyecto == string.Empty)
                pCodProyecto = null;
            if (pIdMaestroPrograma == 0)
                pIdMaestroPrograma = null;
            if (pIdMaestroTIpologia == 0)
                pIdMaestroTIpologia = null;
            if (pIdMaestroLLamado == 0)
                pIdMaestroLLamado = null;
            if (pIdMaestroTIpoProveedor == 0)
                pIdMaestroTIpoProveedor = null;
            if (pIdServicio == 0)
                pIdServicio = null;
            if (pIdModalidad == 0)
                pIdModalidad = null;
            if (pIdAutorizacion == 0)
                pIdAutorizacion = null;
            if (pIdMaestroTitulo == 0)
                pIdMaestroTitulo = null;
            if (pIdMaestroEstadoAutorizacion == 0)
                pIdMaestroEstadoAutorizacion = null;

            if (pRegion == 0)
                pRegion = null;
            if (pProvincia == 0)
                pProvincia = null;
            if (pComuna == 0)
                pComuna = null;

            #endregion

            _busquedaAutorizacion._listadoAutorizacionEntities = listadoAutorizacionsFactory.obtenerListadoAutorizacion(pCodProyecto, pIdMaestroPrograma, pIdMaestroTIpologia, pIdMaestroLLamado,
                                                                                                                        pIdMaestroTIpoProveedor, pNombreProveedor, pIdServicio, pRegion,
                                                                                                                        pProvincia, pComuna, pIdModalidad, pIdAutorizacion,
                                                                                                                        pIdMaestroTitulo, pIdMaestroEstadoAutorizacion);

            return _busquedaAutorizacion._listadoAutorizacionEntities;
        }

        public static listadoAutorizacionEntities getAutorizaciondePago(int? IdAutorizacion)
        {
            listadoAutorizacionEntities _listadoAutorizacionEntities = new listadoAutorizacionEntities();
            //_AutorizacionesdePagoResult = AutorizaciondePagoEntitiesFactory.getAutorizaciondePago(IdAutorizacion);
            _listadoAutorizacionEntities = obtenerListadoAutorizacion(string.Empty, 0, 0, 0, 0, String.Empty, 0, 0, 0, 0, 0, IdAutorizacion, 0, 0)[0];

            return _listadoAutorizacionEntities;
        }

        public static contratoSolicitudPagoEntities RatificaAutorizacion(long idAutorizacion, string UserLog)
        {
            contratoSolicitudPagoEntities objResultSPS = new contratoSolicitudPagoEntities();

            try
            {
                autorizacionEntities objAutorizacion = autorizacionEntitiesFactory.getAutorizacion(idAutorizacion);
                proveedorEntities objProveedor = proveedorEntitiesFactory.getProveedor((long)objAutorizacion.idProveedor);
                List<tipoAutorizacionEntities> _auxListAutorizacion = tipoAutorizacionEntitiesFactory.getListTipoAutorizacion(objAutorizacion.idAutorizacion);
                solicitudPagoEntities solicitud = new solicitudPagoEntities();
                proveedorEntities _proveedorMandato = new proveedorEntities();
                if (_auxListAutorizacion.Count > 0)
                {
                    foreach (var item in _auxListAutorizacion)
                    {
                        solicitud = solicitudPagoEntitiesFactory.getSolicitudPago(item.idSolicitudPago);
                        if(solicitud.idMandatoProveedor != null)
                        _proveedorMandato = proveedorEntitiesFactory.getProveedor(Convert.ToInt64(solicitud.idMandatoProveedor));
                        break;
                    }
                }



                User objUser = UserFactory.GetUser(UserLog);

                if ((long)objAutorizacion.idMaestroPrograma == 8)
                    objAutorizacion.idMaestroPrograma = 3;
                ///*Inserta Ratifica*/
                long CodigoOrdenAT = (long)objAutorizacion.NumeroAutorizacion;
                int lineaOrigen = (int)objAutorizacion.idMaestroPrograma;
                int lineaAplicacion = (int)objAutorizacion.idMaestroPrograma;
                int tipoLlamado = 1;
                decimal montoUF = (decimal)objAutorizacion.montoTotalAutorizacion;
                int montoPesos = 0;
                string nota = "Simplificado";
                int rutContrato = objProveedor.rutProveedor;
                string dvContrato = objProveedor.dvDigitoprovedor.ToString();
                string nombres = objProveedor.nombreProveedor;
                int mandatoRut = 0;
                string mandatodv = string.Empty;
                string mandatoNombres = string.Empty;

                if (solicitud.idMandatoProveedor >0)
                {
                    mandatoRut = _proveedorMandato.rutProveedor;
                    mandatodv = _proveedorMandato.dvDigitoprovedor.ToString();
                    mandatoNombres = _proveedorMandato.nombreProveedor;
                }
                else
                {
                    mandatoRut = objProveedor.rutProveedor;
                    mandatodv = objProveedor.dvDigitoprovedor.ToString();
                    mandatoNombres = objProveedor.nombreProveedor;
                }

               
                int RutFuncionario = objUser.RutUsuario;
                int organismo = (int)objAutorizacion.CodigoRegionAutorizacion;
                int indicaATPrevia = 0;

                objResultSPS = contratoSolicitudPagoEntitiesFactory.insertaRatificaSolicitud(CodigoOrdenAT, lineaOrigen, lineaAplicacion,
                                                              tipoLlamado, montoUF, montoPesos, nota, rutContrato, dvContrato, nombres, mandatoRut, mandatodv, mandatoNombres,
                                                              RutFuncionario, organismo, indicaATPrevia);

                if (objResultSPS.codigoSalidaRatifica == "1")
                {
                    long iResult = 0;
                    iResult = AutorizaciondePagoEntitiesFactory.ActualizaEstadoAutorizacion(idAutorizacion);

                    if (iResult > 0)
                    {
                        objResultSPS.codigoSalidaRatifica = "1";
                        objResultSPS.mensajeSalidaRatifica = "Ratificado de forma exitosa";
                    }
                }
            }
            catch (Exception ex)
            {
                objResultSPS.codigoSalidaRatifica = "0";
                objResultSPS.mensajeSalidaRatifica = ex.Message.ToString();
            }

            return objResultSPS;
        }
        #endregion

        #region Metodos secundarios
        //public static BusquedaAutorizaciondePago getInstanciasFormulario()
        //{
        //    BusquedaAutorizaciondePago _BusquedaAutorizaciondePago = new BusquedaAutorizaciondePago();
        //    _BusquedaAutorizaciondePago.lstRegiones = RegionesEntitiesFactory.getListRegiones();
        //    _BusquedaAutorizaciondePago.lstProvincias = ProvinciasEntitiesFactory.getListProvinciaRegion(null);
        //    _BusquedaAutorizaciondePago.lstComunas = ComunasEntitiesFactory.getComunasProvincia(null);

        //    _BusquedaAutorizaciondePago.lstTipologias = maestroTipologiaEntitiesFactory.getListTipologiaPrograma(null);
        //    _BusquedaAutorizaciondePago.lstTipologias = _BusquedaAutorizaciondePago.lstTipologias.OrderBy(x => x.nombreMaestroTipologia).ToList();

        //    _BusquedaAutorizaciondePago.lstLlamado = maestroLlamadoEntitiesFactory.getListLlamado();
        //    _BusquedaAutorizaciondePago.lstLlamado = _BusquedaAutorizaciondePago.lstLlamado.OrderBy(x => x.nombreMaestroLlamado).ToList();

        //    _BusquedaAutorizaciondePago.lstServicios = maestroServicioEntitiesFactory.getListServicioTipologia(null);
        //    _BusquedaAutorizaciondePago.lstServicios = _BusquedaAutorizaciondePago.lstServicios.OrderBy(x => x.nombreMaestroServicio).ToList();

        //    _BusquedaAutorizaciondePago.lstTipoProveedor = maestroTipoProveedorEntitiesFactory.getListTipoProveedor();
        //    _BusquedaAutorizaciondePago.lstTipoProveedor = _BusquedaAutorizaciondePago.lstTipoProveedor.OrderBy(x => x.nombreMaestroTipoProveedor).ToList();

        //    _BusquedaAutorizaciondePago.lstModalidades = maestroModalidadEntitiesFactory.getListMaestroModalidad();
        //    _BusquedaAutorizaciondePago.lstModalidades = _BusquedaAutorizaciondePago.lstModalidades.OrderBy(x => x.nombreMaestroModalidad).ToList();

        //    _BusquedaAutorizaciondePago._lstPrograma = maestroProgramaEntitiesFactory.getListMaestroPrograma();
        //    _BusquedaAutorizaciondePago._lstPrograma = _BusquedaAutorizaciondePago._lstPrograma.OrderBy(x => x.nombreMaestroPrograma).ToList();

        //    return _BusquedaAutorizaciondePago;
        //}

        public static BusquedaAutorizaciondePago getMantenerInstanciasFormulario(BusquedaAutorizaciondePago _auxP, long? pIdMaestroPrograma, long? pIdMaestroTIpologia, string pNombreServicio, long? pRegion, long? pProvincia, long? pComuna)
        {
            _auxP.lstRegiones = RegionesEntitiesFactory.getListRegiones();
            if (pRegion != null && pProvincia == null)
            {
                _auxP.lstProvincias = ProvinciasEntitiesFactory.getListProvinciaRegion(pRegion);
            }

            if (pProvincia != null && pComuna == null)
            {
                _auxP.lstComunas = ComunasEntitiesFactory.getComunasProvincia(pProvincia);
            }

            if (pIdMaestroPrograma != null && pIdMaestroTIpologia == null)
            {
                _auxP.lstTipologias = maestroTipologiaEntitiesFactory.getListTipologiaPrograma(pIdMaestroPrograma);
                _auxP.lstTipologias = _auxP.lstTipologias.OrderBy(x => x.nombreMaestroTipologia).ToList();
            }

            if (pIdMaestroTIpologia != null && pNombreServicio == string.Empty)
            {
                _auxP.lstServicios = maestroServicioEntitiesFactory.getListServicioTipologia(pIdMaestroTIpologia);
                _auxP.lstServicios = _auxP.lstServicios.OrderBy(x => x.nombreMaestroServicio).ToList();
            }

            if (pProvincia != null)
            {
                if (pProvincia != 0)
                {
                    _auxP.lstProvincias = ProvinciasEntitiesFactory.getMantenerListProvinciaRegion(pRegion, pProvincia);

                    if (pComuna != null)
                    {
                        if (pComuna != 0)
                        {
                            _auxP.lstComunas = ComunasEntitiesFactory.getMantenerListComunaProvincia(pProvincia, pComuna);
                        }
                    }
                }
            }

            if (pIdMaestroPrograma != null && pIdMaestroTIpologia != null)
            {
                if (pIdMaestroPrograma != 0 && pIdMaestroTIpologia != 0)
                {
                    _auxP.lstTipologias = maestroTipologiaEntitiesFactory.getMantenerListTipologiaPrograma(pIdMaestroPrograma, pIdMaestroTIpologia);

                    if (pNombreServicio != string.Empty)
                    {
                        _auxP.lstServicios = maestroServicioEntitiesFactory.getMantenerListServicioTipologia(pIdMaestroTIpologia, Convert.ToInt32(pNombreServicio));
                    }
                }
            }

            return _auxP;
        }
        #endregion

        #region inicializarDDL
        public static List<maestroServicioEntities> getListServicios()
        {
            List<maestroServicioEntities> _lstServicios = new List<maestroServicioEntities>();
            _lstServicios = maestroServicioEntitiesFactory.getListServicio();

            return _lstServicios;
        }
        public static List<maestroServicioEntities> getListServicioTipologia(long idMaestroTipologia)
        {
            List<maestroServicioEntities> _lstServicios = new List<maestroServicioEntities>();
            _lstServicios = maestroServicioEntitiesFactory.getListServicioTipologia(idMaestroTipologia);

            return _lstServicios;
        }

        public static List<maestroTipologiaEntities> getListTipologiaPrograma(int idMaestroTipologia)
        {
            List<maestroTipologiaEntities> _lstTipologia = new List<maestroTipologiaEntities>();
            _lstTipologia = maestroTipologiaEntitiesFactory.getListTipologiaPrograma(idMaestroTipologia);

            return _lstTipologia;
        }

        public static string getNombrePrograma(long idPrograma)
        {
            maestroProgramaEntities _maestroProgramaEntities = maestroProgramaEntitiesFactory.getMaestroPrograma(idPrograma);

            return _maestroProgramaEntities.nombreMaestroPrograma;
        }
        public static List<ProvinciasEntities> getListProvinciasRegion(long idRegion)
        {
            ProvinciasEntities _Provincias = new ProvinciasEntities();
            _Provincias.lstProvincias = ProvinciasEntitiesFactory.getListProvinciaRegion(idRegion);

            return _Provincias.lstProvincias;
        }
        public static List<ComunasEntities> getListComunasProvincia(int idProvincia)
        {
            ComunasEntities _Comunas = new ComunasEntities();
            _Comunas.lstComunas = ComunasEntitiesFactory.getComunasProvincia(idProvincia);

            return _Comunas.lstComunas;
        }
        public static List<ComunasEntities> getListComunas()
        {
            List<ComunasEntities> _auxListComunasEntities = new List<ComunasEntities>();

            var _ComunaDAO = comunaDAO.obtenerComunas();

            if (_ComunaDAO != null)
            {
                foreach (var item in _ComunaDAO)
                {
                    ComunasEntities _ComunasEntities = new ComunasEntities();

                    _ComunasEntities.idComuna = item.COM_ID;
                    _ComunasEntities.nombreComuna = item.COM_DES;
                    _ComunasEntities.idProvincia = item.PRV_ID;
                    _ComunasEntities.idRegion = item.REG_ID;
                    _ComunasEntities.zonaSaturada = item.ZONA_SATURADA;
                    _ComunasEntities.zonaTermica = item.ZONA_TERMICA;
                    _ComunasEntities.ComIDSII = item.COM_ID_SII;
                    _ComunasEntities.ComQtyHab = item.COM_QTY_HAB;
                    _ComunasEntities.ComQtyHabExcp = item.COM_QTY_HAB_EXCP;
                    _ComunasEntities.ComTra = item.COM_TRA;
                    _ComunasEntities.ComMaxCvsEspTII = item.COM_MAX_CVS_ESP_TII;
                    _ComunasEntities.ComQtyNumHab = item.COM_QTY_NUM_HAB;

                    _auxListComunasEntities.Add(_ComunasEntities);
                }
            }

            return _auxListComunasEntities;
        }
        public static ComunasEntities getComuna(int idComuna)
        {
            ComunasEntities _Comunas = new ComunasEntities();
            _Comunas = ComunasEntitiesFactory.getComuna(idComuna);
            return _Comunas;
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.Domain.Entities;
using Minvu.Snat.Domain.Controls;
using System.Web.Mvc;
using Minvu.Snat.IData.DAO;
using System.Xml;
using Minvu.Snat.Domain.IconService;

namespace Minvu.Snat.Domain.Forms
{
    public class BusquedaSolicitudPago
    {
        public BusquedaSolicitudPago() { }

        #region Listados
        public List<RegionesEntities> lstRegiones { get; set; }
        public List<RegionesEntities> _regionUserEntities { get; set; }
        public List<ProvinciasEntities> lstProvincias { get; set; }
        public List<ComunasEntities> lstComunas { get; set; }

        public List<maestroTipologiaEntities> lstTipologias { get; set; }
        public List<listadoSolicitudesEntities> _listadoSolicitudesEntities { get; set; }
        public List<maestroLlamadoEntities> lstLlamado { get; set; }
        public List<maestroProgramaEntities> _lstPrograma { get; set; }
        public List<maestroTipoProveedorEntities> lstTipoProveedor { get; set; }
        public List<maestroServicioEntities> lstServicios { get; set; }
        public List<maestroModalidadEntities> lstModalidades { get; set; }
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
        public modificacionEstadoSolicitudEntities _modificacionEstadoSolicitudEntities { get; set; }
        #endregion

    }

    public class BusquedaSolicitudPagoFactory
    {

        #region BusquedaSolicitud
        public static List<listadoSolicitudesEntities> obtenerListadoSolicitudes(string pCodProyecto, long? pIdMaestroPrograma, long? pIdMaestroTIpologia, long? pIdMaestroLLamado, long? pIdMaestroTIpoProveedor, string pNombreProveedor, string pNombreServicio, long? pRegion, long? pProvincia, long? pComuna, long? pIdModalidad, long? pIdAutorizacion)
        {
            BusquedaSolicitudPago _busquedaSOlicitudPago = new BusquedaSolicitudPago();


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
            if (pIdModalidad == 0)
                pIdModalidad = null;
            if (pIdAutorizacion == 0)
                pIdAutorizacion = null;

            if (pRegion == 0)
                pRegion = null;
            if (pProvincia == 0)
                pProvincia = null;
            if (pComuna == 0)
                pComuna = null;

            #endregion

            _busquedaSOlicitudPago._listadoSolicitudesEntities = listadoSolicitudesFactory.obtenerListadoSolicitudes(pCodProyecto, pIdMaestroPrograma, pIdMaestroTIpologia, pIdMaestroLLamado, pIdMaestroTIpoProveedor, pNombreProveedor, pNombreServicio, pRegion, pProvincia, pComuna, pIdModalidad, pIdAutorizacion);

            return _busquedaSOlicitudPago._listadoSolicitudesEntities;
        }
        #endregion

        public static autorizaciondePagoEntities InsertaGeneracionSolicitudPago(string idSolicitudCadena, string UsuarioResponsable)
        {
           return AutorizaciondePagoEntitiesFactory.InsertaGeneracionSolicitudPago(idSolicitudCadena, UsuarioResponsable);
        }

        public static DateTime? ObtenerBoleta(string sFolioBoleta)
        {
            WsGarantiasSoapClient objClient = new WsGarantiasSoapClient();

            try
            {
                var objBoleta = objClient.consulta_boletas_mm(sFolioBoleta);

                XmlDocument reader = new XmlDocument();
                reader.LoadXml(objBoleta);

                XmlNodeList table = reader.GetElementsByTagName("Table1");
                //XmlNodeList lista = ((XmlElement)table[0]).GetElementsByTagName("persona");
                DateTime? fecVencimiento = null;

                foreach (XmlElement nodo in table)
                {
                    XmlNodeList fechaVencimiento = nodo.GetElementsByTagName("FEC_VENCIMIENTO");
                    if (fechaVencimiento[0].InnerText != String.Empty)
                    {
                        fecVencimiento = Convert.ToDateTime(fechaVencimiento[0].InnerText.ToString());
                    }
                }

                return fecVencimiento;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #region Metodos secundarios
        public static BusquedaSolicitudPago getInstanciasFormulario(int codigoRegion)
        {
            BusquedaSolicitudPago _BusquedaSolicitudPago = new BusquedaSolicitudPago();

            _BusquedaSolicitudPago._regionUserEntities = RegionesEntitiesFactory.GetRegion(codigoRegion);

            _BusquedaSolicitudPago.lstRegiones = RegionesEntitiesFactory.getListRegiones();
            _BusquedaSolicitudPago.lstProvincias = ProvinciasEntitiesFactory.getListProvinciaRegion(null);
            _BusquedaSolicitudPago.lstComunas = ComunasEntitiesFactory.getComunasProvincia(null);

            _BusquedaSolicitudPago.lstTipologias = maestroTipologiaEntitiesFactory.getListTipologiaPrograma(null);
            _BusquedaSolicitudPago.lstTipologias = _BusquedaSolicitudPago.lstTipologias.OrderBy(x => x.nombreMaestroTipologia).ToList();

            _BusquedaSolicitudPago.lstLlamado = maestroLlamadoEntitiesFactory.getListLlamado();
            _BusquedaSolicitudPago.lstLlamado = _BusquedaSolicitudPago.lstLlamado.OrderBy(x => x.nombreMaestroLlamado).ToList();

            _BusquedaSolicitudPago.lstServicios = maestroServicioEntitiesFactory.getListServicioTipologia(null);
            _BusquedaSolicitudPago.lstServicios = _BusquedaSolicitudPago.lstServicios.OrderBy(x => x.nombreMaestroServicio).ToList();

            _BusquedaSolicitudPago.lstTipoProveedor = maestroTipoProveedorEntitiesFactory.getListTipoProveedor();
            _BusquedaSolicitudPago.lstTipoProveedor = _BusquedaSolicitudPago.lstTipoProveedor.OrderBy(x => x.nombreMaestroTipoProveedor).ToList();

            _BusquedaSolicitudPago.lstModalidades = maestroModalidadEntitiesFactory.getListMaestroModalidad();
            _BusquedaSolicitudPago.lstModalidades = _BusquedaSolicitudPago.lstModalidades.OrderBy(x => x.nombreMaestroModalidad).ToList();

            _BusquedaSolicitudPago._lstPrograma = maestroProgramaEntitiesFactory.getListMaestroPrograma();
            _BusquedaSolicitudPago._lstPrograma = _BusquedaSolicitudPago._lstPrograma.OrderBy(x => x.nombreMaestroPrograma).ToList();


            return _BusquedaSolicitudPago;
        }

        public static BusquedaSolicitudPago getMantenerInstanciasFormulario(BusquedaSolicitudPago _auxP, long? pIdMaestroPrograma, long? pIdMaestroTIpologia, string pNombreServicio, long? pRegion, long? pProvincia, long? pComuna, int regionUsuario)
        {

            _auxP.lstRegiones = RegionesEntitiesFactory.getListRegiones();
            _auxP._regionUserEntities = RegionesEntitiesFactory.GetRegion(regionUsuario);
            if (pRegion != null && pProvincia == null)
            {
                _auxP.lstProvincias = ProvinciasEntitiesFactory.getListProvinciaRegion(pRegion);
            }

            if (pProvincia != null && pComuna == null)
            {
                _auxP.lstComunas = ComunasEntitiesFactory.getComunasProvincia(pProvincia);
            }

            if ((pIdMaestroPrograma != null && pIdMaestroTIpologia == null) || (pIdMaestroPrograma != null && pIdMaestroTIpologia == 0))
            {
                _auxP.lstTipologias =  maestroTipologiaEntitiesFactory.getListTipologiaPrograma(pIdMaestroPrograma);
                _auxP.lstTipologias = _auxP.lstTipologias.OrderBy(x => x.nombreMaestroTipologia).ToList();
            }

            if (pIdMaestroTIpologia != null && pNombreServicio == string.Empty )
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

                    if(pNombreServicio!= string.Empty)
                    {
                        _auxP.lstServicios = maestroServicioEntitiesFactory.getMantenerListServicioTipologia(pIdMaestroTIpologia, Convert.ToInt32(pNombreServicio));
                    }
                }

            }

          
            return _auxP;
        }

        public static FuncionarioEntities getRegionUsuario(string userName)
        {
            FuncionarioEntities _funcionarioEntities = FuncionarioEntitiesFactory.getFuncionario(userName);

            return _funcionarioEntities;
        }

        #endregion

        #region inicializarDDL
        public static List<maestroServicioEntities> getListServicios()
        {
            List<maestroServicioEntities> _lstServicios = new List<maestroServicioEntities>();
            _lstServicios = maestroServicioEntitiesFactory.getListServicio();

            return _lstServicios;
        }
        public static List<maestroServicioEntities> getListServicioTipologiaPrograma(long idMaestroTipologia, long idPrograma)
        {
            List<maestroServicioEntities> _lstServicios = new List<maestroServicioEntities>();
            _lstServicios = maestroServicioEntitiesFactory.getListServicioTipologiaPrograma(idMaestroTipologia, idPrograma);

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

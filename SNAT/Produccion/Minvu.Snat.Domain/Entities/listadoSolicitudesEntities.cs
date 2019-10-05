using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.DAO;
using Minvu.Snat.IData.ORM;
using System.ComponentModel.DataAnnotations;
namespace Minvu.Snat.Domain.Entities
{
    public class listadoSolicitudesEntities
    {
        [Display(Name = "idSolicitud:")]
        public long? idCaracteristicas { get; set; }
        public long? idSolicitud { get; set; }
        public long? codigoRegion { get; set; }
        public string nombreRegion { get; set; }
        public long? codigoProyecto { get; set; }
        public string nombreTipologia { get; set; }
        public int? numeroVivienda { get; set; }
        public string serviciosDeSolicitud { get; set; }
        public string rutProveedor { get; set; }
        public string dvProveedor { get; set; }
        public string nombreProveedor { get; set; }
        public string estadoSolicitud { get; set; }
        public decimal? montoSolicitud { get; set; }
        public long? numeroAutorizacion { get; set; }
        public string estadoAutorizacion { get; set; }
        public string servicios { get; set; }
        public string nombreTipoProveedor { get; set; }
        public string nombrePrograma { get; set; }
        public string accion { get; set; }
        public bool? Apago { get; set; }
        public bool? PagoHistorico { get; set; }
        public bool? DisabledItem { get; set; }
        public string MontoEntero { get; set; }
        public string MontoDecimal { get; set; }

        public listadoSolicitudesEntities()
        {
            idSolicitud = null;
            codigoRegion = null;
            codigoProyecto = null;
            nombreTipologia = null;
            numeroVivienda = null;
            serviciosDeSolicitud = null;
            rutProveedor = null;
            nombreProveedor = null;
            estadoSolicitud = null;
            montoSolicitud = null;
            numeroAutorizacion = null;
            estadoAutorizacion = null;
            accion = null;
            Apago = null;
            servicios = null;
            nombreRegion = null;
            PagoHistorico = null;
            DisabledItem = null;
            MontoEntero = String.Empty;
            MontoDecimal = String.Empty;
        }

        public listadoSolicitudesEntities(long? _idCaracteristicas, long? _idSolicitud, long? _codigoRegion, string _nombreRegion,
                                            long? _codigoProyecto, string _nombreTipologia, int? _numeroVivienda, string _serviciosDeSolicitud,
                                            string _rutProveedor, string _dvProveedor, string _nombreProveedor, string _estadoSolicitud,
                                            decimal? _montoSolicitud, long? _numeroAutorizacion, string _estadoAutorizacion, string _servicios,
                                            string _nombreTipoProveedor, string _nombrePrograma, string _accion, bool? _Apago, bool? _PagoHistorico, bool? _DisabledItem)
        {
            idCaracteristicas = _idCaracteristicas;
            idSolicitud = _idSolicitud;
            codigoRegion = _codigoRegion;
            nombreRegion = _nombreRegion;
            codigoProyecto = _codigoProyecto;
            nombreTipologia = _nombreTipologia;
            numeroVivienda = _numeroVivienda;
            serviciosDeSolicitud = _serviciosDeSolicitud;
            rutProveedor = _rutProveedor;
            dvProveedor = _dvProveedor;
            nombreProveedor = _nombreProveedor;
            estadoSolicitud = _estadoSolicitud;
            montoSolicitud = _montoSolicitud;
            numeroAutorizacion = _numeroAutorizacion;
            estadoAutorizacion = _estadoAutorizacion;
            servicios = _servicios;
            nombreTipoProveedor = _nombreTipoProveedor;
            nombrePrograma = _nombrePrograma;
            accion = _accion;
            Apago = _Apago;
            PagoHistorico = _PagoHistorico;
            DisabledItem = _DisabledItem;
            if (montoSolicitud != null)
            {
                string[] montoSplit = montoSolicitud.ToString().Split(',');
                MontoEntero = Convert.ToInt32(montoSplit[0]).ToString("N0");
                MontoDecimal = montoSplit[1];
            }
        }
    }

    public class listadoSolicitudesFactory
    {
        public static List<listadoSolicitudesEntities> obtenerListadoSolicitudes(string pCodProyecto, long? pIdMaestroPrograma, long? pIdMaestroTIpologia, long? pIdMaestroLLamado, long? pIdMaestroTIpoProveedor, string pNombreProveedor, string pNombreServicio, long? pRegion, long? pProvincia, long? pComuna, long? pIdModalidad, long? pIdAutorizacion)
        {
            List<SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_SOLICITUDES_PAGO_Result> _mae = new List<SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_SOLICITUDES_PAGO_Result>();
            List<listadoSolicitudesEntities> _ListListadoSolicitudesEntities = new List<listadoSolicitudesEntities>();
            _mae = listadoSolicitudesDAO.obtenerListadoSolicitudes(pCodProyecto, pIdMaestroPrograma, pIdMaestroTIpologia, pIdMaestroLLamado, pIdMaestroTIpoProveedor, pNombreProveedor, pNombreServicio, pRegion, pProvincia, pComuna, pIdModalidad, pIdAutorizacion);
            //_mae = listadoSolicitudesDAO.obtenerListadoSolicitudes(null, null, null, null, null, null, null, null, null, null);

            foreach (var item in _mae)
            {
                listadoSolicitudesEntities _listadoSolicitudesEntities = new listadoSolicitudesEntities();

                _listadoSolicitudesEntities.numeroVivienda = item.CANTIDADVIVIENDASINFORMACIONPROYECTO;
                _listadoSolicitudesEntities.codigoProyecto = item.CODIGOPROYECTOINFORMACIONPROYECTO;
                _listadoSolicitudesEntities.codigoRegion = item.CODIGOREGIONDIRECCION;
                _listadoSolicitudesEntities.nombreRegion = regionDAO.obtenerNombreRegion(Convert.ToInt32(item.CODIGOREGIONDIRECCION));
                _listadoSolicitudesEntities.estadoAutorizacion = item.ESTADOAUTORIZACION;
                _listadoSolicitudesEntities.estadoSolicitud = item.ESTADOSOLICITUD;
                _listadoSolicitudesEntities.idSolicitud = item.IDSOLICITUDPAGO;
                _listadoSolicitudesEntities.montoSolicitud = item.MONTOSOLICITUDSOLICITUDPAGO;
                _listadoSolicitudesEntities.nombreTipologia = item.NOMBREMAESTROTIPOLOGIA;
                _listadoSolicitudesEntities.nombreProveedor = item.NOMBREPROVEEDOR;
                _listadoSolicitudesEntities.numeroAutorizacion = item.NUMEROAUTORIZACION;
                _listadoSolicitudesEntities.rutProveedor = item.RUTPROVEEDOR.ToString();
                _listadoSolicitudesEntities.dvProveedor = item.DV;
                _listadoSolicitudesEntities.servicios = item.SERVICIO;
                _listadoSolicitudesEntities.nombreTipoProveedor = item.TIPOPROVEEDOR;
                _listadoSolicitudesEntities.nombrePrograma = item.NOMBREPROGRAMA;
                _listadoSolicitudesEntities.idCaracteristicas = item.IDCARACTERISTICASESPECIALES;
                _listadoSolicitudesEntities.Apago = item.NUMEROAUTORIZACION == 0 ? false : true;
                _listadoSolicitudesEntities.PagoHistorico = item.PagoHistorico;
                _listadoSolicitudesEntities.DisabledItem = Convert.ToBoolean(_listadoSolicitudesEntities.PagoHistorico) ? true : _listadoSolicitudesEntities.Apago;
                if (_listadoSolicitudesEntities.montoSolicitud != null)
                {
                    string[] montoSplit = _listadoSolicitudesEntities.montoSolicitud.ToString().Split(',');
                    _listadoSolicitudesEntities.MontoEntero = Convert.ToInt32(montoSplit[0]).ToString("N0");
                    _listadoSolicitudesEntities.MontoDecimal = montoSplit[1];
                }

                _ListListadoSolicitudesEntities.Add(_listadoSolicitudesEntities);
            }

            return _ListListadoSolicitudesEntities;
        }
    }
}
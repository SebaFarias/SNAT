using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
    public class listadoSolicitudesDAO
    {
        public string nombreTipologia { get; set; }
        public string nombreSubModalidad { get; set; }
        public string nombreMaestroServicio { get; set; }
        public string nombreParcialidad { get; set; }
        public int? porcentajeParcialidad { get; set; }
        public bool? parcialidadSeleccionada { get; set; }
        public long? idServicio { get; set; }
        public long? idParcialidad { get; set; }
        public long? idServicioParcialidad { get; set; }
        public long? idTipoServicioParcialidadCaracteristica { get; set; }
        public long? idCaracteristicaEspeciales { get; set; }
        public decimal? montoServicio { get; set; }
        public decimal? montoParcialidad { get; set; }
        public decimal? asignacionDirecta { get; set; }
        public bool? estadoServicio { get; set; }
        public bool? estadoParcialidad { get; set; }

        public listadoSolicitudesDAO()
        {
            nombreTipologia = "";
            nombreSubModalidad = "";
            nombreMaestroServicio = "";
            nombreParcialidad = "";
            porcentajeParcialidad = null;
            parcialidadSeleccionada = null;
            idServicio = null;
            idParcialidad = null;
            montoServicio = null;
            montoParcialidad = null;
            asignacionDirecta = null;
            estadoServicio = null;
            estadoParcialidad = null;
            idServicioParcialidad = null;
            idTipoServicioParcialidadCaracteristica = null;
            idCaracteristicaEspeciales = null;
        }


        public static List<SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_SOLICITUDES_PAGO_Result> obtenerListadoSolicitudes(string pCodProyecto, long? pIdMaestroPrograma, long? pIdMaestroTIpologia, long? pIdMaestroLLamado, long? pIdMaestroTIpoProveedor, string pNombreProveedor, string pNombreServicio, long? pRegion, long? pProvincia, long? pComuna, long? pIdModalidad, long? pIdAutorizacion)
        {
            List<SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_SOLICITUDES_PAGO_Result> list_DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result = new List<SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_SOLICITUDES_PAGO_Result>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qListadoSolicitudesPago = contexto.SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_SOLICITUDES_PAGO(pCodProyecto, pIdMaestroPrograma, pIdMaestroTIpologia, pIdMaestroLLamado, pIdMaestroTIpoProveedor, pNombreProveedor, pNombreServicio, pRegion, pProvincia, pComuna, pIdModalidad, null);

                foreach (var item in qListadoSolicitudesPago)
                {
                    SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_SOLICITUDES_PAGO_Result _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result = new SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_SOLICITUDES_PAGO_Result();

                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.CANTIDADVIVIENDASINFORMACIONPROYECTO = item.CANTIDADVIVIENDASINFORMACIONPROYECTO;
                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.CODIGOPROYECTOINFORMACIONPROYECTO = item.CODIGOPROYECTOINFORMACIONPROYECTO;
                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.CODIGOREGIONDIRECCION = item.CODIGOREGIONDIRECCION;
                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.ESTADOAUTORIZACION = item.ESTADOAUTORIZACION;
                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.ESTADOSOLICITUD = item.ESTADOSOLICITUD;
                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.IDSOLICITUDPAGO = item.IDSOLICITUDPAGO;
                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.MONTOSOLICITUDSOLICITUDPAGO = item.MONTOSOLICITUDSOLICITUDPAGO;
                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.NOMBREMAESTROTIPOLOGIA = item.NOMBREMAESTROTIPOLOGIA;
                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.NOMBREPROVEEDOR = item.NOMBREPROVEEDOR;
                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.NUMEROAUTORIZACION = item.NUMEROAUTORIZACION;
                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.RUTPROVEEDOR = item.RUTPROVEEDOR;
                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.DV = item.DV;
                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.SERVICIO = item.SERVICIO;
                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.NOMBREPROVEEDOR = item.NOMBREPROVEEDOR;
                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.IDCARACTERISTICASESPECIALES = item.IDCARACTERISTICASESPECIALES;
                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.NOMBREPROGRAMA = item.NOMBREPROGRAMA;
                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.TIPOPROVEEDOR = item.TIPOPROVEEDOR;
                    _DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.PagoHistorico = item.PagoHistorico;

                    list_DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result.Add(_DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result);
                }

                return list_DB_SNAT_V_USP_OBTENER_LISTADO_SOLICITUD_PAGO_Result;
            }
        }




    }
}

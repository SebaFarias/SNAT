using Minvu.Snat.IData.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class DetalleAutorizacionCompletaEntities
    {
        public long? IDSOLICITUDPAGO { get; set; }
        public string CODIGOPROYECTOINFORMACIONPROYECTO { get; set; }
        public string NOMBREPROYECTOINFORMACIONPROYECTO { get; set; }
        public string NOMBREPROVINCIADIRECCION { get; set; }
        public string NOMBRECOMUNADIRECCION { get; set; }
        public string NOMBRE_SERVICIO { get; set; }
        public int? CANTIDAD_PARCIALIDAD_POR_SERVICIO { get; set; }
        public decimal? PAGO_TOTAL_POR_SERVICIO { get; set; }
        public decimal? MONTO_FTO_TOTAL_UF { get; set; }
        public decimal? MONTO_A_PAGO_FTO { get; set; }
        public decimal? PAGO_ARANCELES_UF { get; set; }
        public decimal? MONTO_A_PAGO_ARANCELES_UF { get; set; }
        public decimal? MONTO_AT_UF { get; set; }
        public decimal? MONTO_A_PAGO { get; set; }



        public DetalleAutorizacionCompletaEntities()
        {
            IDSOLICITUDPAGO = null;
            CODIGOPROYECTOINFORMACIONPROYECTO = string.Empty;
            NOMBREPROYECTOINFORMACIONPROYECTO = string.Empty;
            NOMBREPROVINCIADIRECCION = string.Empty;
            NOMBRECOMUNADIRECCION = string.Empty;
            NOMBRE_SERVICIO = string.Empty;
            CANTIDAD_PARCIALIDAD_POR_SERVICIO = null;
            PAGO_TOTAL_POR_SERVICIO = null;
            MONTO_FTO_TOTAL_UF = null;
            MONTO_A_PAGO_FTO = null;
            PAGO_ARANCELES_UF = null;
            MONTO_A_PAGO_ARANCELES_UF = null;
            MONTO_AT_UF = null;
            MONTO_A_PAGO = null;

        }
    }

    public class DetalleAutorizacionCompletaEntitiesFactory
    {
        internal static List<DetalleAutorizacionCompletaEntities> getListDetalleAutorizacionCompleta(long? IdAutorizacion)
        {
            var a = DetalleAutorizaciondePagoDAO.getListDetalleAutorizacionCompleta(IdAutorizacion);

            List<DetalleAutorizacionCompletaEntities> list = new List<DetalleAutorizacionCompletaEntities>();

            foreach (var item in a)
            {
                DetalleAutorizacionCompletaEntities aux = new DetalleAutorizacionCompletaEntities();

                aux.IDSOLICITUDPAGO = item.IDSOLICITUDPAGO;
                aux.CODIGOPROYECTOINFORMACIONPROYECTO = item.CODIGOPROYECTOINFORMACIONPROYECTO;
                aux.NOMBREPROYECTOINFORMACIONPROYECTO = item.NOMBREPROYECTOINFORMACIONPROYECTO;
                aux.NOMBRECOMUNADIRECCION = comunaDAO.ObtenerNombreComuna(Convert.ToInt32(item.CODIGOCOMUNADIRECCION));
                aux.NOMBREPROVINCIADIRECCION = ProvinciaDAO.getProvinciaDESC(Convert.ToInt32(item.CODIGOPROVINCIADIRECCION));
                aux.NOMBRE_SERVICIO = item.NOMBRE_SERVICIO;
                aux.CANTIDAD_PARCIALIDAD_POR_SERVICIO = item.CANTIDAD_PARCIALIDAD_POR_SERVICIO;
                aux.PAGO_TOTAL_POR_SERVICIO = item.PAGO_TOTAL_POR_SERVICIO;
                aux.MONTO_FTO_TOTAL_UF = item.MONTO_FTO_TOTAL_UF;
                aux.MONTO_A_PAGO_FTO = item.MONTO_A_PAGO_FTO;
                aux.PAGO_ARANCELES_UF = item.PAGO_ARANCELES_UF;
                aux.MONTO_A_PAGO_ARANCELES_UF = item.MONTO_A_PAGO_ARANCELES_UF;
                aux.MONTO_AT_UF = item.MONTO_AT_UF;
                aux.MONTO_A_PAGO = item.MONTO_A_PAGO;

                list.Add(aux);
            }

            return list;
        }

        public static List<solicitudPagoEntities> getListSolicitudPagoEntities(List<DetalleAutorizacionCompletaEntities> _listSolicitudes )
        {
            List<solicitudPagoEntities> _listSolicitudPago = new List<solicitudPagoEntities>();
            foreach (var item in _listSolicitudes)
            {

                solicitudPagoEntities _aux = new solicitudPagoEntities();
                _aux = solicitudPagoEntitiesFactory.getSolicitudPago(item.IDSOLICITUDPAGO);
                _listSolicitudPago.Add(_aux);
            }
            return _listSolicitudPago;
        }
    }

    public class DetalleAutorizacionPDF
    {
        public string NUMEROSOLICITUDSOLICITUDAUTORIZACION { get; set; }
        public string CODIGOPROYECTOSOLICITUDAUTORIZACION { get; set; }
        public string NOMBREPROYECTOSOLICITUDAUTORIZACION { get; set; }
        public string UBICACIONCOMUNASOLICITUDAUTORIZACION { get; set; }
        public string S1SOLICITUDAUTORIZACION { get; set; }
        public string S2SOLICITUDAUTORIZACION { get; set; }
        public string S3SOLICITUDAUTORIZACION { get; set; }
        public string S4SOLICITUDAUTORIZACION { get; set; }
        public string S5SOLICITUDAUTORIZACION { get; set; }
        public string S6SOLICITUDAUTORIZACION { get; set; }
        public string S7SOLICITUDAUTORIZACION { get; set; }
        public string S8SOLICITUDAUTORIZACION { get; set; }
        public string S9SOLICITUDAUTORIZACION { get; set; }
        public string S10SOLICITUDAUTORIZACION { get; set; }
        public string FTOSOLICITUDAUTORIZACION { get; set; }
        public string MONTOFTOTOTALSOLICITUDAUTORIZACION { get; set; }
        public string MONTOATSOLICITUDAUTORIZACION { get; set; }
        public string MONTOAPAGOSOLICITUDAUTORIZACION { get; set; }
        public bool eliminarSolicitud { get; set; }



        public DetalleAutorizacionPDF()
        {
            NUMEROSOLICITUDSOLICITUDAUTORIZACION = string.Empty;
            CODIGOPROYECTOSOLICITUDAUTORIZACION = string.Empty;
            NOMBREPROYECTOSOLICITUDAUTORIZACION = string.Empty;
            UBICACIONCOMUNASOLICITUDAUTORIZACION = string.Empty;
            S1SOLICITUDAUTORIZACION = string.Empty;
            S2SOLICITUDAUTORIZACION = string.Empty;
            S3SOLICITUDAUTORIZACION = string.Empty;
            S4SOLICITUDAUTORIZACION = string.Empty;
            S5SOLICITUDAUTORIZACION = string.Empty;
            S6SOLICITUDAUTORIZACION = string.Empty;
            S7SOLICITUDAUTORIZACION = string.Empty;
            S8SOLICITUDAUTORIZACION = string.Empty;
            S9SOLICITUDAUTORIZACION = string.Empty;
            S10SOLICITUDAUTORIZACION = string.Empty;
            FTOSOLICITUDAUTORIZACION = string.Empty;
            MONTOFTOTOTALSOLICITUDAUTORIZACION = string.Empty;
            MONTOATSOLICITUDAUTORIZACION = string.Empty;
            MONTOAPAGOSOLICITUDAUTORIZACION = string.Empty;
            eliminarSolicitud = false;


        }
    }

    public class DetalleAutorizacionPDFFactory
    {
        public static List<DetalleAutorizacionPDF> getListDetalleAutorizacionPDF(long? IdAutorizacion)
        {
            var p = DetalleAutorizaciondePagoDAO.getListDetalleAutorizacionPDF(IdAutorizacion);

            List<DetalleAutorizacionPDF> list = new List<DetalleAutorizacionPDF>();

            foreach (var a in p)
            {
                DetalleAutorizacionPDF aux = new DetalleAutorizacionPDF();

                aux.CODIGOPROYECTOSOLICITUDAUTORIZACION = a.CODIGOPROYECTOSOLICITUDAUTORIZACION.ToString();
                aux.FTOSOLICITUDAUTORIZACION = a.FTOSOLICITUDAUTORIZACION;
                aux.MONTOAPAGOSOLICITUDAUTORIZACION = a.MONTOAPAGOSOLICITUDAUTORIZACION;
                aux.MONTOATSOLICITUDAUTORIZACION = a.MONTOATSOLICITUDAUTORIZACION;
                aux.MONTOFTOTOTALSOLICITUDAUTORIZACION = a.MONTOFTOTOTALSOLICITUDAUTORIZACION;
                aux.NOMBREPROYECTOSOLICITUDAUTORIZACION = a.NOMBREPROYECTOSOLICITUDAUTORIZACION;
                aux.NUMEROSOLICITUDSOLICITUDAUTORIZACION = a.NUMEROSOLICITUDSOLICITUDAUTORIZACION.ToString();
                aux.S10SOLICITUDAUTORIZACION = a.S10SOLICITUDAUTORIZACION;
                aux.S1SOLICITUDAUTORIZACION = a.S1SOLICITUDAUTORIZACION;
                aux.S2SOLICITUDAUTORIZACION = a.S2SOLICITUDAUTORIZACION;
                aux.S3SOLICITUDAUTORIZACION = a.S3SOLICITUDAUTORIZACION;
                aux.S4SOLICITUDAUTORIZACION = a.S4SOLICITUDAUTORIZACION;
                aux.S5SOLICITUDAUTORIZACION = a.S5SOLICITUDAUTORIZACION;
                aux.S6SOLICITUDAUTORIZACION = a.S6SOLICITUDAUTORIZACION;
                aux.S7SOLICITUDAUTORIZACION = a.S7SOLICITUDAUTORIZACION;
                aux.S8SOLICITUDAUTORIZACION = a.S8SOLICITUDAUTORIZACION;
                aux.S9SOLICITUDAUTORIZACION = a.S9SOLICITUDAUTORIZACION;
                aux.UBICACIONCOMUNASOLICITUDAUTORIZACION = a.UBICACIONCOMUNASOLICITUDAUTORIZACION;


                list.Add(aux);
            }

            return list;
        }


    }
}

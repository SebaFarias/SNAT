using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.IData.DAO
{
    public class DetalleAutorizaciondePagoDAO
    {
        public static BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_Result GetDetalleAutorizacionesDePago(long? IdAutorizacion)
        {
            BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_Result objDetalleAutorizaciondePago = new BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_Result();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var objAutorizacionesPago = from a in contexto.BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO(IdAutorizacion)
                                            select a;
                foreach (var a in objAutorizacionesPago)
                {
                    objDetalleAutorizaciondePago.IDAUTORIZACION = a.IDAUTORIZACION;
                    objDetalleAutorizaciondePago.CODIGOREGIONDIRECCION = a.CODIGOREGIONDIRECCION;
                    objDetalleAutorizaciondePago.NROAUTORIZACION = a.NROAUTORIZACION;
                    objDetalleAutorizaciondePago.FECHAINGRESOAUTORIZACION = a.FECHAINGRESOAUTORIZACION;
                    objDetalleAutorizaciondePago.NROPROYECTOS = a.NROPROYECTOS;
                    objDetalleAutorizaciondePago.NOMBREMAESTROLLAMADO = a.NOMBREMAESTROLLAMADO;
                    objDetalleAutorizaciondePago.CANTIDADSOLICITUDES = a.CANTIDADSOLICITUDES;
                    objDetalleAutorizaciondePago.NOMBREPROVEEDOR = a.NOMBREPROVEEDOR;
                    objDetalleAutorizaciondePago.RutProveedor = a.RutProveedor;
                    objDetalleAutorizaciondePago.NOMBREMAESTROESTADOAUTORIZACION = a.NOMBREMAESTROESTADOAUTORIZACION;
                    objDetalleAutorizaciondePago.MONTOTOTALAUTORIZACION = a.MONTOTOTALAUTORIZACION;
                    objDetalleAutorizaciondePago.NOMBREMAESTROTIPOPROVEEDOR = a.NOMBREMAESTROTIPOPROVEEDOR;
                    objDetalleAutorizaciondePago.USUARIORESPONSABLESOLICITUDPAGO = a.USUARIORESPONSABLESOLICITUDPAGO;
                    objDetalleAutorizaciondePago.USUARIORESPONSABLEAUTORIZACION = a.USUARIORESPONSABLEAUTORIZACION;
                    
                    objDetalleAutorizaciondePago.NOMBREMAESTROPROGRAMA = a.NOMBREMAESTROPROGRAMA;
                    objDetalleAutorizaciondePago.NOMBREMAESTROPROGRAMA = a.NOMBREMAESTROPROGRAMA;
                }

                return objDetalleAutorizaciondePago;
            }
        }

        public static List<CABECERA_AUTORIZACION_PAGO_Result> getListDetalleAutorizacionCompleta(long? IdAutorizacion)
        {
            List<CABECERA_AUTORIZACION_PAGO_Result> objDetalleAutorizaciondePago = new List<CABECERA_AUTORIZACION_PAGO_Result>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var objAutorizacionesPago = from a in contexto.CABECERA_AUTORIZACION_PAGO(IdAutorizacion)
                                            select a;
                foreach (var a in objAutorizacionesPago)
                {
                    CABECERA_AUTORIZACION_PAGO_Result aux = new CABECERA_AUTORIZACION_PAGO_Result();


                    aux.CANTIDAD_PARCIALIDAD_POR_SERVICIO = a.CANTIDAD_PARCIALIDAD_POR_SERVICIO;
                    aux.CODIGOCOMUNADIRECCION = a.CODIGOCOMUNADIRECCION;
                    aux.CODIGOPROVINCIADIRECCION = a.CODIGOPROVINCIADIRECCION;
                    aux.CODIGOPROYECTOINFORMACIONPROYECTO = a.CODIGOPROYECTOINFORMACIONPROYECTO;
                    aux.IDSOLICITUDPAGO = a.IDSOLICITUDPAGO;
                    aux.MONTO_AT_UF = a.MONTO_AT_UF;
                    aux.MONTO_A_PAGO = a.MONTO_A_PAGO;
                    aux.MONTO_A_PAGO_ARANCELES_UF= a.MONTO_A_PAGO_ARANCELES_UF;
                    aux.MONTO_A_PAGO_FTO = a.MONTO_A_PAGO_FTO;
                    aux.MONTO_FTO_TOTAL_UF = a.MONTO_FTO_TOTAL_UF;
                    aux.NOMBREPROYECTOINFORMACIONPROYECTO = a.NOMBREPROYECTOINFORMACIONPROYECTO;
                    aux.NOMBRE_SERVICIO = a.NOMBRE_SERVICIO;
                    aux.PAGO_ARANCELES_UF = a.PAGO_ARANCELES_UF;
                    aux.PAGO_TOTAL_POR_SERVICIO= a.PAGO_TOTAL_POR_SERVICIO;

                    objDetalleAutorizaciondePago.Add(aux);

                }

                return objDetalleAutorizaciondePago;
            }
        }

        public static List<BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA_PDF_Result> getListDetalleAutorizacionPDF(long? IdAutorizacion)
        {
            List<BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA_PDF_Result> objDetalleAutorizaciondePago = new List<BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA_PDF_Result>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var objAutorizacionesPago = from a in contexto.BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA_PDF(IdAutorizacion)
                                            select a;
                foreach (var a in objAutorizacionesPago)
                {
                    BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA_PDF_Result aux = new BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA_PDF_Result();


                    aux.CODIGOPROYECTOSOLICITUDAUTORIZACION = a.CODIGOPROYECTOSOLICITUDAUTORIZACION;
                    aux.FTOSOLICITUDAUTORIZACION = a.FTOSOLICITUDAUTORIZACION;
                    aux.MONTOAPAGOSOLICITUDAUTORIZACION = a.MONTOAPAGOSOLICITUDAUTORIZACION;
                    aux.MONTOATSOLICITUDAUTORIZACION = a.MONTOATSOLICITUDAUTORIZACION;
                    aux.MONTOFTOTOTALSOLICITUDAUTORIZACION = a.MONTOFTOTOTALSOLICITUDAUTORIZACION;
                    aux.NOMBREPROYECTOSOLICITUDAUTORIZACION = a.NOMBREPROYECTOSOLICITUDAUTORIZACION;
                    aux.NUMEROSOLICITUDSOLICITUDAUTORIZACION = a.NUMEROSOLICITUDSOLICITUDAUTORIZACION;
                    aux.S10SOLICITUDAUTORIZACION= a.S10SOLICITUDAUTORIZACION;
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


                    objDetalleAutorizaciondePago.Add(aux);

                }

                return objDetalleAutorizaciondePago;
            }
        }


        public static List<BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA_Result> GetDetalleAutorizacionesDePagoGrilla(long? IdAutorizacion, long? idTipoProveedor)
        {
            List<BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA_Result> lstDetalleAutorizacionesPagoGrilla = new List<BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA_Result>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var objAutorizacionesPagoGrilla = from a in contexto.BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA(IdAutorizacion, idTipoProveedor)
                                                    select a;
                foreach (var a in objAutorizacionesPagoGrilla)
                {
                    BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA_Result objDetalleAutorizaciondePagoGrilla = new BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA_Result();

                    objDetalleAutorizaciondePagoGrilla.IDCARACTERISTICASESPECIALES = a.IDCARACTERISTICASESPECIALES;
                    objDetalleAutorizaciondePagoGrilla.IDINFORMACIONPROYECTO = a.IDINFORMACIONPROYECTO;
                    objDetalleAutorizaciondePagoGrilla.CODIGOPROYECTOINFORMACIONPROYECTO = a.CODIGOPROYECTOINFORMACIONPROYECTO;
                    objDetalleAutorizaciondePagoGrilla.NOMBREPROYECTOINFORMACIONPROYECTO = a.NOMBREPROYECTOINFORMACIONPROYECTO;
                    objDetalleAutorizaciondePagoGrilla.CODIGOREGIONDIRECCION = a.CODIGOREGIONDIRECCION;
                    objDetalleAutorizaciondePagoGrilla.NOMBREMAESTROPROGRAMA = a.NOMBREMAESTROPROGRAMA;
                    objDetalleAutorizaciondePagoGrilla.NOMBREMAESTROTIPOLOGIA = a.NOMBREMAESTROTIPOLOGIA;
                    objDetalleAutorizaciondePagoGrilla.CLASE = a.CLASE;
                    objDetalleAutorizaciondePagoGrilla.S1 = a.S1;
                    objDetalleAutorizaciondePagoGrilla.S2 = a.S2;
                    objDetalleAutorizaciondePagoGrilla.S3 = a.S3;
                    objDetalleAutorizaciondePagoGrilla.S4 = a.S4;
                    objDetalleAutorizaciondePagoGrilla.S5 = a.S5;
                    objDetalleAutorizaciondePagoGrilla.S6 = a.S6;
                    objDetalleAutorizaciondePagoGrilla.S7 = a.S7;
                    objDetalleAutorizaciondePagoGrilla.S8 = a.S8;
                    objDetalleAutorizaciondePagoGrilla.S9 = a.S9;
                    objDetalleAutorizaciondePagoGrilla.S10 = a.S10;
                    objDetalleAutorizaciondePagoGrilla.MONTOFTO = a.MONTOFTO;
                    objDetalleAutorizaciondePagoGrilla.SALDOFTO = a.SALDOFTO;
                    objDetalleAutorizaciondePagoGrilla.MONTOTOTALPROYECTO = a.MONTOTOTALPROYECTO;
                    objDetalleAutorizaciondePagoGrilla.MontoaPagar = a.MontoaPagar;
                    objDetalleAutorizaciondePagoGrilla.IDSOLICITUDPAGO = a.IDSOLICITUDPAGO;

                    lstDetalleAutorizacionesPagoGrilla.Add(objDetalleAutorizaciondePagoGrilla);
                }

                return lstDetalleAutorizacionesPagoGrilla;
            }
        }
    }
}

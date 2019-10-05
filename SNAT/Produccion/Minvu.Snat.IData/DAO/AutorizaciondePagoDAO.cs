using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.IData.DAO
{
    public class AutorizaciondePagoDAO
    {
        public static BUSQUEDA_AUTORIZACION_DE_PAGO_Result GetAutorizacionesDePagoById(int? IdAutorizacion)
        {
            BUSQUEDA_AUTORIZACION_DE_PAGO_Result objAutorizaciondePago = new BUSQUEDA_AUTORIZACION_DE_PAGO_Result();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var objAutorizacionesPago = from a in contexto.BUSQUEDA_AUTORIZACION_DE_PAGO(0, 0, 0, IdAutorizacion, String.Empty, 0, 0, 0, 0, 0, 0, 0, 0, 0)
                                            where a.IDAUTORIZACION == IdAutorizacion
                                            select a;
                foreach (var a in objAutorizacionesPago)
                {
                    objAutorizaciondePago.NROAUTORIZACION = a.NROAUTORIZACION;
                    objAutorizaciondePago.IDAUTORIZACION = a.IDAUTORIZACION;
                    objAutorizaciondePago.CODIGOREGIONDIRECCION = a.CODIGOREGIONDIRECCION;
                    objAutorizaciondePago.NROPROYECTOS = a.NROPROYECTOS;
                    objAutorizaciondePago.NOMBREMAESTROPROGRAMA = a.NOMBREMAESTROPROGRAMA;
                    objAutorizaciondePago.NOMBREMAESTROTIPOLOGIA = a.NOMBREMAESTROTIPOLOGIA;
                    objAutorizaciondePago.NOMBREMAESTROMODALIDAD = a.NOMBREMAESTROMODALIDAD;
                    objAutorizaciondePago.CLASE = a.CLASE;
                    objAutorizaciondePago.NOMBREMAESTROTIPOPROVEEDOR = a.NOMBREMAESTROTIPOPROVEEDOR;
                    objAutorizaciondePago.RUTPROVEEDOR = a.RUTPROVEEDOR;
                    objAutorizaciondePago.NOMBREPROVEEDOR = a.NOMBREPROVEEDOR;
                    objAutorizaciondePago.MONTOTOTALAUTORIZACION = a.MONTOTOTALAUTORIZACION;
                    objAutorizaciondePago.NOMBREMAESTROESTADOAUTORIZACION = a.NOMBREMAESTROESTADOAUTORIZACION;
                    objAutorizaciondePago.STRINGSOLICITUD = a.STRINGSOLICITUD;
                    objAutorizaciondePago.STRINGPROYECTO = a.STRINGPROYECTO;
                }

                return objAutorizaciondePago;
            }
        }

        public static List<BUSQUEDA_AUTORIZACION_DE_PAGO_Result> GetAutorizacionesDePago(Nullable<int> cODIGOPROYECTO, Nullable<long> iDMAESTROPROGRAMA, Nullable<long> iDMAESTROTIPOLOGIA, Nullable<long> iDAUTORIZACION, string nOMBREPROVEEDOR, Nullable<long> iDMAESTROMODALIDAD, Nullable<long> iDMAESTROLLAMADO, Nullable<int> cODIGOREGIONDIRECCION, Nullable<int> cODIGOPROVINCIADIRECCION, Nullable<int> cODIGOCOMUNADIRECCION, Nullable<long> iDMAESTROSERVICIO, Nullable<long> iDMAESTROTIPOPROVEEDOR, Nullable<long> iDMAESTROTITULO, Nullable<long> iDMAESTROESTADOAUTORIZACION)
        {
            List<BUSQUEDA_AUTORIZACION_DE_PAGO_Result> ListAutorizacionesPago = new List<BUSQUEDA_AUTORIZACION_DE_PAGO_Result>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var objAutorizacionesPago = contexto.BUSQUEDA_AUTORIZACION_DE_PAGO(cODIGOPROYECTO, iDMAESTROPROGRAMA, iDMAESTROTIPOLOGIA, iDAUTORIZACION,
                                                                                            nOMBREPROVEEDOR, iDMAESTROMODALIDAD, iDMAESTROLLAMADO, cODIGOREGIONDIRECCION,
                                                                                            cODIGOPROVINCIADIRECCION, cODIGOCOMUNADIRECCION, iDMAESTROSERVICIO,
                                                                                            iDMAESTROTIPOPROVEEDOR, iDMAESTROTITULO, iDMAESTROESTADOAUTORIZACION);

                foreach (var a in objAutorizacionesPago)
                {
                    BUSQUEDA_AUTORIZACION_DE_PAGO_Result _objResult = new BUSQUEDA_AUTORIZACION_DE_PAGO_Result();

                    _objResult.NROAUTORIZACION = a.NROAUTORIZACION;
                    _objResult.IDAUTORIZACION = a.IDAUTORIZACION;
                    _objResult.CODIGOREGIONDIRECCION = a.CODIGOREGIONDIRECCION;
                    _objResult.NROPROYECTOS = a.NROPROYECTOS;
                    _objResult.NOMBREMAESTROPROGRAMA = a.NOMBREMAESTROPROGRAMA;
                    _objResult.NOMBREMAESTROTIPOLOGIA = a.NOMBREMAESTROTIPOLOGIA;
                    _objResult.NOMBREMAESTROMODALIDAD = a.NOMBREMAESTROMODALIDAD;
                    _objResult.CLASE = a.CLASE;
                    _objResult.NOMBREMAESTROTIPOPROVEEDOR = a.NOMBREMAESTROTIPOPROVEEDOR;
                    _objResult.RUTPROVEEDOR = a.RUTPROVEEDOR;
                    _objResult.NOMBREPROVEEDOR = a.NOMBREPROVEEDOR;
                    _objResult.MONTOTOTALAUTORIZACION = a.MONTOTOTALAUTORIZACION;
                    _objResult.NOMBREMAESTROESTADOAUTORIZACION = a.NOMBREMAESTROESTADOAUTORIZACION;
                    _objResult.STRINGSOLICITUD = a.STRINGSOLICITUD;
                    _objResult.STRINGPROYECTO = a.STRINGPROYECTO;

                    ListAutorizacionesPago.Add(_objResult);
                }

                return ListAutorizacionesPago;
            }
        }

        public static INSERTA_GENERACION_AUTORIZACION_PAGO_Result InsertaGeneracionSolicitudPago(string idSolicitudCadena, string UsuarioResponsable)
        {
            INSERTA_GENERACION_AUTORIZACION_PAGO_Result objAutorizaciondePago = new INSERTA_GENERACION_AUTORIZACION_PAGO_Result();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var objAutorizacionesPago = from a in contexto.INSERTA_GENERACION_AUTORIZACION_PAGO(idSolicitudCadena, UsuarioResponsable)
                                            select a;
                foreach (var a in objAutorizacionesPago)
                {
                    objAutorizaciondePago.ERR = a.ERR;
                    objAutorizaciondePago.MSG = a.MSG.ToString();
                }

                return objAutorizaciondePago;
            }
        }

        public static long ActualizaEstadoAutorizacion(long idAutorizacion)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                try
                {
                    AUTORIZACION objAutorizacion = new AUTORIZACION();

                    objAutorizacion = contexto.AUTORIZACION.Where(a => a.IDAUTORIZACION == idAutorizacion).FirstOrDefault<AUTORIZACION>();

                    objAutorizacion.IDMAESTROESTADOAUTORIZACION = 2;//SPS

                    contexto.SaveChanges();

                    return (long)objAutorizacion.IDAUTORIZACION;
                }
                catch
                {
                    return 0;
                }
            }
        }
    }
}

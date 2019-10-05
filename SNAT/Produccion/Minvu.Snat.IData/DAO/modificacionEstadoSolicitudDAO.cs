using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.IData.DAO
{
    public class modificacionEstadoSolicitudDAO
    {
        //public static long Save(MODIFICACION_ESTADO_SOLICITUD _ModiEstSol)
        //{
        //    using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
        //    {
        //        MODIFICACION_ESTADO_SOLICITUD objModEstSol = new MODIFICACION_ESTADO_SOLICITUD();
        //        try
        //        {
        //            objModEstSol = contexto.MODIFICACION_ESTADO_SOLICITUD.Where(c => c.IDMODIFICACIONESTADOSOLICITUD == _ModiEstSol.IDMODIFICACIONESTADOSOLICITUD).FirstOrDefault<MODIFICACION_ESTADO_SOLICITUD>();

        //            if (objModEstSol == null)
        //            {
        //                objModEstSol = _ModiEstSol;
        //                contexto.MODIFICACION_ESTADO_SOLICITUD.Add(objModEstSol);
        //            }
        //            else
        //            {
        //                objModEstSol.IDMODIFICACIONESTADOSOLICITUD= _ModiEstSol.IDMODIFICACIONESTADOSOLICITUD;
        //                objModEstSol.IDSOLICITUDPAGO= _ModiEstSol.IDSOLICITUDPAGO;
        //                objModEstSol.IDMAESTROESTADOSOLICITUD = _ModiEstSol.IDMAESTROESTADOSOLICITUD;
        //                objModEstSol.FECHAMODIFICACIONESTADOSOLICITUD = _ModiEstSol.FECHAMODIFICACIONESTADOSOLICITUD;
        //                objModEstSol.USUARIORESPONSABLEMODIFICACIONESTADOSOLICITUD = _ModiEstSol.USUARIORESPONSABLEMODIFICACIONESTADOSOLICITUD;
        //            }

        //            contexto.SaveChanges();
        //            return (long)objModEstSol.IDMODIFICACIONESTADOSOLICITUD;
        //        }
        //        catch (Exception Ex)
        //        {
        //            throw Ex;
        //        }
        //    }
        //}

        public static INSERTA_MODIFICACION_ESTADO_SOLICITUD_Result SaveModificacionEstadoSolicitud(long IdSolicitudPago, long IdMaestroEstadoSolicitud, string UsuarioResponsable)
        {
            INSERTA_MODIFICACION_ESTADO_SOLICITUD_Result objInsModificacion = new INSERTA_MODIFICACION_ESTADO_SOLICITUD_Result();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var objResult = contexto.INSERTA_MODIFICACION_ESTADO_SOLICITUD(IdSolicitudPago, IdMaestroEstadoSolicitud, UsuarioResponsable);

                foreach (var a in objResult)
                {
                    objInsModificacion.ERR = a.ERR;
                    objInsModificacion.MSG = a.MSG.ToString();
                }

                return objInsModificacion;
            }
        }

        public static MODIFICACION_ESTADO_SOLICITUD Get(long? idModificacionEstado)
        {
            MODIFICACION_ESTADO_SOLICITUD objModEstSol = new MODIFICACION_ESTADO_SOLICITUD();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qSolicitudPago = from a in contexto.MODIFICACION_ESTADO_SOLICITUD
                                     where a.IDMODIFICACIONESTADOSOLICITUD == idModificacionEstado
                                     select a;
                foreach (var a in qSolicitudPago)
                {
                    objModEstSol.IDMODIFICACIONESTADOSOLICITUD = a.IDMODIFICACIONESTADOSOLICITUD;
                    objModEstSol.IDSOLICITUDPAGO = a.IDSOLICITUDPAGO;
                    objModEstSol.IDMAESTROESTADOSOLICITUD = a.IDMAESTROESTADOSOLICITUD;
                    objModEstSol.FECHAMODIFICACIONESTADOSOLICITUD = a.FECHAMODIFICACIONESTADOSOLICITUD;
                    objModEstSol.USUARIORESPONSABLEMODIFICACIONESTADOSOLICITUD = a.USUARIORESPONSABLEMODIFICACIONESTADOSOLICITUD;
                }

                return objModEstSol;
            }
        }
    }
}

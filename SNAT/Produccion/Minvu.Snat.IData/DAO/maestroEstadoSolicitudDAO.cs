using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
   public class maestroEstadoSolicitudDAO
    {
        public static int Save(MAESTRO_ESTADO_SOLICITUD _maestroEstadosolicitud)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_ESTADO_SOLICITUD _mas = new MAESTRO_ESTADO_SOLICITUD();
                try
                {
                    _mas = contexto.MAESTRO_ESTADO_SOLICITUD.Where(c => c.IDMAESTROESTADOSOLICITUD == _maestroEstadosolicitud.IDMAESTROESTADOSOLICITUD).FirstOrDefault<MAESTRO_ESTADO_SOLICITUD>();

                    if (_mas == null)
                    {
                        _mas = _maestroEstadosolicitud;
                        contexto.MAESTRO_ESTADO_SOLICITUD.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.ESTADOMAESTROESTADOSOLICITUD = _maestroEstadosolicitud.ESTADOMAESTROESTADOSOLICITUD;
                        _mas.IDMAESTROESTADOSOLICITUD  = _maestroEstadosolicitud.IDMAESTROESTADOSOLICITUD;
                        _mas.NOMBREMAESTROESTADOSOLICITUD = _maestroEstadosolicitud.NOMBREMAESTROESTADOSOLICITUD;

                        
                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDMAESTROESTADOSOLICITUD;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static MAESTRO_ESTADO_SOLICITUD Get(int idMaestroEstadoSolicityd)
        {

            MAESTRO_ESTADO_SOLICITUD _mae = new MAESTRO_ESTADO_SOLICITUD();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroEstadoSolicitud = from a in contexto.MAESTRO_ESTADO_SOLICITUD
                                                 where a.IDMAESTROESTADOSOLICITUD == idMaestroEstadoSolicityd
                                           select a;
                foreach (var a in qMaestroEstadoSolicitud)
                {

                    _mae.ESTADOMAESTROESTADOSOLICITUD = a.ESTADOMAESTROESTADOSOLICITUD;
                    _mae.IDMAESTROESTADOSOLICITUD = a.IDMAESTROESTADOSOLICITUD;
                    _mae.NOMBREMAESTROESTADOSOLICITUD = a.NOMBREMAESTROESTADOSOLICITUD;
                                    }

                return _mae;
            }

        }

        public static List<MAESTRO_ESTADO_SOLICITUD> GetList()
        {

            List<MAESTRO_ESTADO_SOLICITUD> ListaMaestroEstadoSolicitud = new List<MAESTRO_ESTADO_SOLICITUD>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroEstadoSolicitud = from a in contexto.MAESTRO_ESTADO_SOLICITUD
                                                 select a;
                foreach (var a in qMaestroEstadoSolicitud)
                {

                    MAESTRO_ESTADO_SOLICITUD _mae = new MAESTRO_ESTADO_SOLICITUD();


                    _mae.ESTADOMAESTROESTADOSOLICITUD = a.ESTADOMAESTROESTADOSOLICITUD;
                    _mae.IDMAESTROESTADOSOLICITUD = a.IDMAESTROESTADOSOLICITUD;
                    _mae.NOMBREMAESTROESTADOSOLICITUD = a.NOMBREMAESTROESTADOSOLICITUD;

                    ListaMaestroEstadoSolicitud.Add(_mae);


                }

                return ListaMaestroEstadoSolicitud;
            }

        }


        protected void Delete(MAESTRO_ESTADO_SOLICITUD _maestroEstadoSolicitud)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_ESTADO_SOLICITUD qMaestroEstadoSolicitud = (from c in contexto.MAESTRO_ESTADO_SOLICITUD
                                                                       where c.ESTADOMAESTROESTADOSOLICITUD == _maestroEstadoSolicitud.ESTADOMAESTROESTADOSOLICITUD
                                                                    select c).FirstOrDefault();

                contexto.MAESTRO_ESTADO_SOLICITUD.Remove(qMaestroEstadoSolicitud);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(MAESTRO_ESTADO_SOLICITUD _maestroEstadoSolicitud)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_ESTADO_SOLICITUD qMaestroEstadoSolicitud = (from c in contexto.MAESTRO_ESTADO_SOLICITUD
                                                                       where c.IDMAESTROESTADOSOLICITUD == _maestroEstadoSolicitud.IDMAESTROESTADOSOLICITUD
                                                                    select c).FirstOrDefault();


                qMaestroEstadoSolicitud.ESTADOMAESTROESTADOSOLICITUD = false;

                contexto.SaveChanges();
            }


        }

    }
}



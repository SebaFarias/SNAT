using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
    public class maestroEstadoAutorizacionDAO
    {
        public static int Save(MAESTRO_ESTADO_AUTORIZACION _maestroEstadoAutorizacion)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_ESTADO_AUTORIZACION _mas = new MAESTRO_ESTADO_AUTORIZACION();
                try
                {
                    _mas = contexto.MAESTRO_ESTADO_AUTORIZACION.Where(c => c.IDMAESTROESTADOAUTORIZACION == _maestroEstadoAutorizacion.IDMAESTROESTADOAUTORIZACION).FirstOrDefault<MAESTRO_ESTADO_AUTORIZACION>();

                    if (_mas == null)
                    {
                        _mas = _maestroEstadoAutorizacion;
                        contexto.MAESTRO_ESTADO_AUTORIZACION.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.ESTADOMAESTROESTADOAUTORIZACION = _maestroEstadoAutorizacion.ESTADOMAESTROESTADOAUTORIZACION;
                        _mas.IDMAESTROESTADOAUTORIZACION = _maestroEstadoAutorizacion.IDMAESTROESTADOAUTORIZACION;
                        _mas.NOMBREMAESTROESTADOAUTORIZACION = _maestroEstadoAutorizacion.NOMBREMAESTROESTADOAUTORIZACION;
                        
                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDMAESTROESTADOAUTORIZACION;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static MAESTRO_ESTADO_AUTORIZACION Get(int idMaestroEstadoAutorizacion)
        {

            MAESTRO_ESTADO_AUTORIZACION _mae = new MAESTRO_ESTADO_AUTORIZACION();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroEstadoAutorizacion = from a in contexto.MAESTRO_ESTADO_AUTORIZACION
                                          where a.IDMAESTROESTADOAUTORIZACION == idMaestroEstadoAutorizacion
                                           select a;
                foreach (var a in qMaestroEstadoAutorizacion)
                {

                    _mae.IDMAESTROESTADOAUTORIZACION = a.IDMAESTROESTADOAUTORIZACION;
                    _mae.ESTADOMAESTROESTADOAUTORIZACION = a.ESTADOMAESTROESTADOAUTORIZACION;
                    _mae.NOMBREMAESTROESTADOAUTORIZACION = a.NOMBREMAESTROESTADOAUTORIZACION;
                                    }

                return _mae;
            }

        }

        public static List<MAESTRO_ESTADO_AUTORIZACION> GetList()
        {

            List<MAESTRO_ESTADO_AUTORIZACION> ListaMaestroEstadoAutorizacion = new List<MAESTRO_ESTADO_AUTORIZACION>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroEstadoAutorizacion = from a in contexto.MAESTRO_ESTADO_AUTORIZACION
                                                     select a;
                foreach (var a in qMaestroEstadoAutorizacion)
                {

                    MAESTRO_ESTADO_AUTORIZACION _mae = new MAESTRO_ESTADO_AUTORIZACION();


                    _mae.ESTADOMAESTROESTADOAUTORIZACION = a.ESTADOMAESTROESTADOAUTORIZACION;
                    _mae.IDMAESTROESTADOAUTORIZACION = a.IDMAESTROESTADOAUTORIZACION;
                    _mae.NOMBREMAESTROESTADOAUTORIZACION = a.NOMBREMAESTROESTADOAUTORIZACION;

                    ListaMaestroEstadoAutorizacion.Add(_mae);


                }

                return ListaMaestroEstadoAutorizacion;
            }

        }


        protected void Delete(MAESTRO_ESTADO_AUTORIZACION _maestroEstadoAutorizacion)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_ESTADO_AUTORIZACION qMaestroEstadoAutorizacion = (from c in contexto.MAESTRO_ESTADO_AUTORIZACION
                                                                          where c.IDMAESTROESTADOAUTORIZACION == _maestroEstadoAutorizacion.IDMAESTROESTADOAUTORIZACION
                                                                          select c).FirstOrDefault();

                contexto.MAESTRO_ESTADO_AUTORIZACION.Remove(qMaestroEstadoAutorizacion);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(MAESTRO_ESTADO_AUTORIZACION _maestroAlternativaPostulacion)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_ESTADO_AUTORIZACION qMaestroEstadiAutorizacion = (from c in contexto.MAESTRO_ESTADO_AUTORIZACION
                                                   where c.IDMAESTROESTADOAUTORIZACION == _maestroAlternativaPostulacion.IDMAESTROESTADOAUTORIZACION
                                        select c).FirstOrDefault();


                qMaestroEstadiAutorizacion.ESTADOMAESTROESTADOAUTORIZACION = false;

                contexto.SaveChanges();
            }


        }

    }
}



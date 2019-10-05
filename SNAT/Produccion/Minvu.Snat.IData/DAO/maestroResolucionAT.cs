using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
 public   class maestroResolucionDAO
    {
        public static int Save(MAESTRO_RESOLUCION _maestroResolucion)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_RESOLUCION _mas = new MAESTRO_RESOLUCION();
                try
                {
                    _mas = contexto.MAESTRO_RESOLUCION.Where(c => c.IDMAESTRORESOLUCION == _maestroResolucion.IDMAESTRORESOLUCION).FirstOrDefault<MAESTRO_RESOLUCION>();

                    if (_mas == null)
                    {
                        _mas = _maestroResolucion;
                        contexto.MAESTRO_RESOLUCION.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.ESTADOMAESTRORESOLUCION = _maestroResolucion.ESTADOMAESTRORESOLUCION;
                        _mas.IDMAESTRORESOLUCION = _maestroResolucion.IDMAESTRORESOLUCION;
                        _mas.NOMBREMAESTRORESOLUCION = _maestroResolucion.NOMBREMAESTRORESOLUCION;
                        _mas.FECHAMAESTRORESOLUCION = _maestroResolucion.FECHAMAESTRORESOLUCION;
                        _mas.IDMAESTROPROGRAMA = _maestroResolucion.IDMAESTROPROGRAMA;


                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDMAESTRORESOLUCION;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static MAESTRO_RESOLUCION Get(long idMaestroResolucion)
        {

            MAESTRO_RESOLUCION _mae = new MAESTRO_RESOLUCION();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroResolucion = from a in contexto.MAESTRO_RESOLUCION
                                      where a.IDMAESTRORESOLUCION == idMaestroResolucion
                                           select a;
                foreach (var a in qMaestroResolucion)
                {

                    _mae.IDMAESTRORESOLUCION = a.IDMAESTRORESOLUCION;
                    _mae.ESTADOMAESTRORESOLUCION = a.ESTADOMAESTRORESOLUCION;
                    _mae.NOMBREMAESTRORESOLUCION = a.NOMBREMAESTRORESOLUCION;
                    _mae.FECHAMAESTRORESOLUCION = a.FECHAMAESTRORESOLUCION;
                    _mae.IDMAESTROPROGRAMA = a.IDMAESTROPROGRAMA;
                }

                return _mae;
            }

        }

        public static MAESTRO_RESOLUCION GetporPograma(long idPrograma)
        {

            MAESTRO_RESOLUCION _mae = new MAESTRO_RESOLUCION();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroResolucion = from a in contexto.MAESTRO_RESOLUCION
                                         where a.IDMAESTROPROGRAMA == idPrograma
                                         select a;
                foreach (var a in qMaestroResolucion)
                {

                    _mae.IDMAESTRORESOLUCION = a.IDMAESTRORESOLUCION;
                    _mae.ESTADOMAESTRORESOLUCION = a.ESTADOMAESTRORESOLUCION;
                    _mae.NOMBREMAESTRORESOLUCION = a.NOMBREMAESTRORESOLUCION;
                    _mae.FECHAMAESTRORESOLUCION = a.FECHAMAESTRORESOLUCION;
                    _mae.IDMAESTROPROGRAMA = a.IDMAESTROPROGRAMA;
                }

                return _mae;
            }

        }

        public static List<MAESTRO_RESOLUCION> GetList()
        {

            List<MAESTRO_RESOLUCION> ListaMaestroResolucion = new List<MAESTRO_RESOLUCION>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroResolucion = from a in contexto.MAESTRO_RESOLUCION
                                      select a;
                foreach (var a in qMaestroResolucion)
                {

                    MAESTRO_RESOLUCION _mae = new MAESTRO_RESOLUCION();


                    _mae.ESTADOMAESTRORESOLUCION = a.ESTADOMAESTRORESOLUCION;
                    _mae.IDMAESTRORESOLUCION = a.IDMAESTRORESOLUCION;
                    _mae.NOMBREMAESTRORESOLUCION = a.NOMBREMAESTRORESOLUCION;
                    _mae.FECHAMAESTRORESOLUCION = a.FECHAMAESTRORESOLUCION;
                    _mae.IDMAESTROPROGRAMA = a.IDMAESTROPROGRAMA;

                    ListaMaestroResolucion.Add(_mae);


                }

                return ListaMaestroResolucion;
            }

        }


        protected void Delete(MAESTRO_RESOLUCION _maestroResolucion)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_RESOLUCION qMaestroResolucion = (from c in contexto.MAESTRO_RESOLUCION
                                                           where c.IDMAESTRORESOLUCION == _maestroResolucion.IDMAESTRORESOLUCION
                                                   select c).FirstOrDefault();

                contexto.MAESTRO_RESOLUCION.Remove(qMaestroResolucion);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(MAESTRO_RESOLUCION _maestroEstadoSolicitud)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_RESOLUCION qMaestroResolucion = (from c in contexto.MAESTRO_RESOLUCION
                                                              where c.IDMAESTRORESOLUCION == _maestroEstadoSolicitud.IDMAESTRORESOLUCION
                                                         select c).FirstOrDefault();


                qMaestroResolucion.ESTADOMAESTRORESOLUCION = false;

                contexto.SaveChanges();
            }


        }

    }
}



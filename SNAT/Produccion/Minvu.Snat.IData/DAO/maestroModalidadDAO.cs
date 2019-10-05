using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
 public   class maestroModalidadDAO
    {
        public static int Save(MAESTRO_MODALIDAD _maestroEstadosolicitud)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_MODALIDAD _mas = new MAESTRO_MODALIDAD();
                try
                {
                    _mas = contexto.MAESTRO_MODALIDAD.Where(c => c.IDMAESTROMODALIDAD == _maestroEstadosolicitud.IDMAESTROMODALIDAD).FirstOrDefault<MAESTRO_MODALIDAD>();

                    if (_mas == null)
                    {
                        _mas = _maestroEstadosolicitud;
                        contexto.MAESTRO_MODALIDAD.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.ESTADOMAESTROMODALIDAD = _maestroEstadosolicitud.ESTADOMAESTROMODALIDAD;
                        _mas.IDMAESTROMODALIDAD = _maestroEstadosolicitud.IDMAESTROMODALIDAD;
                        _mas.NOMBREMAESTROMODALIDAD = _maestroEstadosolicitud.NOMBREMAESTROMODALIDAD;

                        
                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDMAESTROMODALIDAD;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }

        public static MAESTRO_MODALIDAD get(long? idMaestroModalidad)
        {

            MAESTRO_MODALIDAD _mae = new MAESTRO_MODALIDAD();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroLlamado = from a in contexto.MAESTRO_MODALIDAD
                                      where a.IDMAESTROMODALIDAD == idMaestroModalidad
                                           select a;
                foreach (var a in qMaestroLlamado)
                {

                    _mae.ESTADOMAESTROMODALIDAD = a.ESTADOMAESTROMODALIDAD;
                    _mae.IDMAESTROMODALIDAD = a.IDMAESTROMODALIDAD;
                    _mae.NOMBREMAESTROMODALIDAD= a.NOMBREMAESTROMODALIDAD;
                                    }

                return _mae;
            }
        }

        public static List<MAESTRO_MODALIDAD> GetList()
        {
            List<MAESTRO_MODALIDAD> ListaMaestroModalidad = new List<MAESTRO_MODALIDAD>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroModalidad = from a in contexto.MAESTRO_MODALIDAD
                                        orderby a.NOMBREMAESTROMODALIDAD
                                        select a;

                foreach (var a in qMaestroModalidad)
                {
                    MAESTRO_MODALIDAD _mae = new MAESTRO_MODALIDAD();

                    _mae.ESTADOMAESTROMODALIDAD = a.ESTADOMAESTROMODALIDAD;
                    _mae.IDMAESTROMODALIDAD = a.IDMAESTROMODALIDAD;
                    _mae.NOMBREMAESTROMODALIDAD = a.NOMBREMAESTROMODALIDAD;

                    ListaMaestroModalidad.Add(_mae);
                }

                return ListaMaestroModalidad;
            }
        }

        protected void Delete(MAESTRO_MODALIDAD _maestroModalidad)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_MODALIDAD qMaestroModalidad = (from c in contexto.MAESTRO_MODALIDAD
                                                     where c.IDMAESTROMODALIDAD == _maestroModalidad.IDMAESTROMODALIDAD
                                                       select c).FirstOrDefault();

                contexto.MAESTRO_MODALIDAD.Remove(qMaestroModalidad);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(MAESTRO_MODALIDAD _maestroModalidad)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_MODALIDAD qMaestroModalidad = (from c in contexto.MAESTRO_MODALIDAD
                                                             where c.IDMAESTROMODALIDAD == _maestroModalidad.IDMAESTROMODALIDAD
                                                       select c).FirstOrDefault();


                qMaestroModalidad.ESTADOMAESTROMODALIDAD = false;

                contexto.SaveChanges();
            }


        }

    }
}



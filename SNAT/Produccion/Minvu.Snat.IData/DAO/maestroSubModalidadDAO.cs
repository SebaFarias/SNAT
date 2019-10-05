using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
 public   class maestroSubModalidadDAO
    {
        public static int Save(MAESTRO_SUB_MODALIDAD _maestroSubModalidad)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_SUB_MODALIDAD _mas = new MAESTRO_SUB_MODALIDAD();
                try
                {
                    _mas = contexto.MAESTRO_SUB_MODALIDAD.Where(c => c.IDMAESTROSUBMODALIDAD == _maestroSubModalidad.IDMAESTROSUBMODALIDAD).FirstOrDefault<MAESTRO_SUB_MODALIDAD>();

                    if (_mas == null)
                    {
                        _mas = _maestroSubModalidad;
                        contexto.MAESTRO_SUB_MODALIDAD.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.ESTADOMAESTROSUBMODALIDAD = _maestroSubModalidad.ESTADOMAESTROSUBMODALIDAD;
                        _mas.IDMAESTROSUBMODALIDAD = _maestroSubModalidad.IDMAESTROSUBMODALIDAD;
                        _mas.NOMBREMAESTROSUBMODALIDAD = _maestroSubModalidad.NOMBREMAESTROSUBMODALIDAD;

                        
                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDMAESTROSUBMODALIDAD;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }

        public static MAESTRO_SUB_MODALIDAD get(long? idMaestroModalidad)
        {

            MAESTRO_SUB_MODALIDAD _mae = new MAESTRO_SUB_MODALIDAD();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroLlamado = from a in contexto.MAESTRO_SUB_MODALIDAD
                                      where a.IDMAESTROSUBMODALIDAD == idMaestroModalidad
                                           select a;
                foreach (var a in qMaestroLlamado)
                {

                    _mae.ESTADOMAESTROSUBMODALIDAD = a.ESTADOMAESTROSUBMODALIDAD;
                    _mae.IDMAESTROSUBMODALIDAD = a.IDMAESTROSUBMODALIDAD;
                    _mae.NOMBREMAESTROSUBMODALIDAD = a.NOMBREMAESTROSUBMODALIDAD;
                }

                return _mae;
            }
        }

        public static List<MAESTRO_SUB_MODALIDAD> GetList()
        {
            List<MAESTRO_SUB_MODALIDAD> ListaMaestroModalidad = new List<MAESTRO_SUB_MODALIDAD>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroModalidad = from a in contexto.MAESTRO_SUB_MODALIDAD
                                        orderby a.NOMBREMAESTROSUBMODALIDAD
                                        select a;

                foreach (var a in qMaestroModalidad)
                {
                    MAESTRO_SUB_MODALIDAD _mae = new MAESTRO_SUB_MODALIDAD();

                    _mae.ESTADOMAESTROSUBMODALIDAD = a.ESTADOMAESTROSUBMODALIDAD;
                    _mae.IDMAESTROSUBMODALIDAD = a.IDMAESTROSUBMODALIDAD;
                    _mae.NOMBREMAESTROSUBMODALIDAD = a.NOMBREMAESTROSUBMODALIDAD;

                    ListaMaestroModalidad.Add(_mae);
                }

                return ListaMaestroModalidad;
            }
        }

        protected void Delete(MAESTRO_SUB_MODALIDAD _maestroModalidad)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_SUB_MODALIDAD qMaestroModalidad = (from c in contexto.MAESTRO_SUB_MODALIDAD
                                                           where c.IDMAESTROSUBMODALIDAD == _maestroModalidad.IDMAESTROSUBMODALIDAD
                                                           select c).FirstOrDefault();

                contexto.MAESTRO_SUB_MODALIDAD.Remove(qMaestroModalidad);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(MAESTRO_SUB_MODALIDAD _maestroModalidad)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_SUB_MODALIDAD qMaestroModalidad = (from c in contexto.MAESTRO_SUB_MODALIDAD
                                                           where c.IDMAESTROSUBMODALIDAD == _maestroModalidad.IDMAESTROSUBMODALIDAD
                                                           select c).FirstOrDefault();


                qMaestroModalidad.ESTADOMAESTROSUBMODALIDAD = false;

                contexto.SaveChanges();
            }


        }

    }
}



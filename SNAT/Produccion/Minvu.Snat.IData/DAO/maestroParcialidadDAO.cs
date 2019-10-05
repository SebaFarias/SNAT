using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
    class maestroParcialidadDAO
    {
        public static int Save(MAESTRO_PARCIALIDAD _maestroParcialidad)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_PARCIALIDAD _mas = new MAESTRO_PARCIALIDAD();
                try
                {
                    _mas = contexto.MAESTRO_PARCIALIDAD.Where(c => c.IDMAESTROPARCIALIDAD == _maestroParcialidad.IDMAESTROPARCIALIDAD).FirstOrDefault<MAESTRO_PARCIALIDAD>();

                    if (_mas == null)
                    {
                        _mas = _maestroParcialidad;
                        contexto.MAESTRO_PARCIALIDAD.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.ESTADOMAESTROPARCIALIDAD = _maestroParcialidad.ESTADOMAESTROPARCIALIDAD;
                        _mas.IDMAESTROPARCIALIDAD = _maestroParcialidad.IDMAESTROPARCIALIDAD;
                        _mas.NOMBREMAESTROPARCIALIDAD = _maestroParcialidad.NOMBREMAESTROPARCIALIDAD;
                        


                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDMAESTROPARCIALIDAD;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static MAESTRO_PARCIALIDAD Get(int idMaestroModalidad)
        {

            MAESTRO_PARCIALIDAD _mae = new MAESTRO_PARCIALIDAD();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroParcialidad = from a in contexto.MAESTRO_PARCIALIDAD
                                      where a.IDMAESTROPARCIALIDAD == idMaestroModalidad
                                          select a;
                foreach (var a in qMaestroParcialidad)
                {

                    _mae.ESTADOMAESTROPARCIALIDAD = a.ESTADOMAESTROPARCIALIDAD;
                    _mae.IDMAESTROPARCIALIDAD = a.IDMAESTROPARCIALIDAD;
                    _mae.IDMAESTROPARCIALIDAD = a.IDMAESTROPARCIALIDAD;
                    _mae.NOMBREMAESTROPARCIALIDAD = a.NOMBREMAESTROPARCIALIDAD;
                    
                }

                return _mae;
            }

        }

        public static List<MAESTRO_PARCIALIDAD> GetList()
        {

            List<MAESTRO_PARCIALIDAD> ListaMaestroParcialidad = new List<MAESTRO_PARCIALIDAD>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroModalidad = from a in contexto.MAESTRO_PARCIALIDAD
                                        select a;
                foreach (var a in qMaestroModalidad)
                {

                    MAESTRO_PARCIALIDAD _mae = new MAESTRO_PARCIALIDAD();


                    _mae.ESTADOMAESTROPARCIALIDAD = a.ESTADOMAESTROPARCIALIDAD;
                    _mae.IDMAESTROPARCIALIDAD = a.IDMAESTROPARCIALIDAD;
                    _mae.NOMBREMAESTROPARCIALIDAD = a.NOMBREMAESTROPARCIALIDAD;
                    

                    ListaMaestroParcialidad.Add(_mae);


                }

                return ListaMaestroParcialidad;
            }

        }


        protected void Delete(MAESTRO_PARCIALIDAD _maestroParcialidad)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_PARCIALIDAD qMaestroParcialidad = (from c in contexto.MAESTRO_PARCIALIDAD
                                                         where c.IDMAESTROPARCIALIDAD == _maestroParcialidad.IDMAESTROPARCIALIDAD
                                                           select c).FirstOrDefault();

                contexto.MAESTRO_PARCIALIDAD.Remove(qMaestroParcialidad);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(MAESTRO_PARCIALIDAD _maestroParcialidad)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_PARCIALIDAD qMaestroParcialidad = (from c in contexto.MAESTRO_PARCIALIDAD
                                                         where c.IDMAESTROPARCIALIDAD == _maestroParcialidad.IDMAESTROPARCIALIDAD
                                                           select c).FirstOrDefault();


                qMaestroParcialidad.ESTADOMAESTROPARCIALIDAD = false;

                contexto.SaveChanges();
            }


        }

    }
}



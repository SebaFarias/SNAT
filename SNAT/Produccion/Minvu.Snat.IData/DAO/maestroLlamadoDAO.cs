using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
 public   class maestroLlamadoDAO
    {
        public static int Save(MAESTRO_LLAMADO _maestroEstadosolicitud)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_LLAMADO _mas = new MAESTRO_LLAMADO();
                try
                {
                    _mas = contexto.MAESTRO_LLAMADO.Where(c => c.IDMAESTROLLAMADO == _maestroEstadosolicitud.IDMAESTROLLAMADO).FirstOrDefault<MAESTRO_LLAMADO>();

                    if (_mas == null)
                    {
                        _mas = _maestroEstadosolicitud;
                        contexto.MAESTRO_LLAMADO.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.ESTADOMAESTROLLAMADO = _maestroEstadosolicitud.ESTADOMAESTROLLAMADO;
                        _mas.IDMAESTROLLAMADO = _maestroEstadosolicitud.IDMAESTROLLAMADO;
                        _mas.NOMBREMAESTROLLAMADO = _maestroEstadosolicitud.NOMBREMAESTROLLAMADO;

                        
                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDMAESTROLLAMADO;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }

        public static MAESTRO_LLAMADO Get(long idMaestroLlamado)
        {

            MAESTRO_LLAMADO _mae = new MAESTRO_LLAMADO();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroLlamado = from a in contexto.MAESTRO_LLAMADO
                                              where a.IDMAESTROLLAMADO == idMaestroLlamado
                                           select a;
                foreach (var a in qMaestroLlamado)
                {

                    _mae.ESTADOMAESTROLLAMADO = a.ESTADOMAESTROLLAMADO;
                    _mae.IDMAESTROLLAMADO = a.IDMAESTROLLAMADO;
                    _mae.NOMBREMAESTROLLAMADO= a.NOMBREMAESTROLLAMADO;
                                    }

                return _mae;
            }

        }

        public static List<MAESTRO_LLAMADO> GetList()
        {
            List<MAESTRO_LLAMADO> ListaMaestrLlamado = new List<MAESTRO_LLAMADO>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroLlamado = from a in contexto.MAESTRO_LLAMADO
                                      orderby a.NOMBREMAESTROLLAMADO
                                      select a;

                foreach (var a in qMaestroLlamado)
                {
                    MAESTRO_LLAMADO _mae = new MAESTRO_LLAMADO();

                    _mae.ESTADOMAESTROLLAMADO = a.ESTADOMAESTROLLAMADO;
                    _mae.IDMAESTROLLAMADO = a.IDMAESTROLLAMADO;
                    _mae.NOMBREMAESTROLLAMADO = a.NOMBREMAESTROLLAMADO;

                    ListaMaestrLlamado.Add(_mae);
                }

                return ListaMaestrLlamado;
            }
        }

        protected void Delete(MAESTRO_LLAMADO _maestroLlamado)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_LLAMADO qMaestroLlamado = (from c in contexto.MAESTRO_LLAMADO
                                                           where c.IDMAESTROLLAMADO == _maestroLlamado.IDMAESTROLLAMADO
                                                   select c).FirstOrDefault();

                contexto.MAESTRO_LLAMADO.Remove(qMaestroLlamado);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(MAESTRO_LLAMADO _maestroEstadoSolicitud)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_LLAMADO qMaestroEstadoSolicitud = (from c in contexto.MAESTRO_LLAMADO
                                                           where c.IDMAESTROLLAMADO == _maestroEstadoSolicitud.IDMAESTROLLAMADO
                                                           select c).FirstOrDefault();


                qMaestroEstadoSolicitud.ESTADOMAESTROLLAMADO = false;

                contexto.SaveChanges();
            }


        }

    }
}



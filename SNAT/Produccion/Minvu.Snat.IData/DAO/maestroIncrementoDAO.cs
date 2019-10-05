using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
   public class maestroIncrementoDAO
    {
        public static int Save(MAESTRO_INCREMENTO _maestroIncremento)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_INCREMENTO _mas = new MAESTRO_INCREMENTO();
                try
                {
                    _mas = contexto.MAESTRO_INCREMENTO.Where(c => c.IDMAESTROINCREMENTO == _maestroIncremento.IDMAESTROINCREMENTO).FirstOrDefault<MAESTRO_INCREMENTO>();

                    if (_mas == null)
                    {
                        _mas = _maestroIncremento;
                        contexto.MAESTRO_INCREMENTO.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.ESTADOMAESTROINCREMENTO = _maestroIncremento.ESTADOMAESTROINCREMENTO;
                        _mas.IDMAESTROINCREMENTO = _maestroIncremento.IDMAESTROINCREMENTO;
                        _mas.NOMBREMAESTROINCREMENTO = _maestroIncremento.NOMBREMAESTROINCREMENTO;

                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDMAESTROINCREMENTO;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static MAESTRO_INCREMENTO Get(long idMaestroIncremento)
        {

            MAESTRO_INCREMENTO _mae = new MAESTRO_INCREMENTO();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroIncremento = from a in contexto.MAESTRO_INCREMENTO
                                              where a.IDMAESTROINCREMENTO == idMaestroIncremento
                                           select a;
                foreach (var a in qMaestroIncremento)
                {

                    _mae.IDMAESTROINCREMENTO = a.IDMAESTROINCREMENTO;
                    _mae.NOMBREMAESTROINCREMENTO  = a.NOMBREMAESTROINCREMENTO;
                    _mae.ESTADOMAESTROINCREMENTO = a.ESTADOMAESTROINCREMENTO;
                    
                }

                return _mae;
            }

        }

        public static List<MAESTRO_INCREMENTO> GetList()
        {

            List<MAESTRO_INCREMENTO> ListaMaestroIncremento = new List<MAESTRO_INCREMENTO>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroIncremento = from a in contexto.MAESTRO_INCREMENTO
                                              select a;
                foreach (var a in qMaestroIncremento)
                {

                    MAESTRO_INCREMENTO _mae = new MAESTRO_INCREMENTO();


                    _mae.ESTADOMAESTROINCREMENTO = a.ESTADOMAESTROINCREMENTO;
                    _mae.IDMAESTROINCREMENTO = a.IDMAESTROINCREMENTO;
                    _mae.NOMBREMAESTROINCREMENTO = a.NOMBREMAESTROINCREMENTO;

                    ListaMaestroIncremento.Add(_mae);


                }

                return ListaMaestroIncremento;
            }

        }


        protected void Delete(MAESTRO_INCREMENTO _maestroIncremento)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_INCREMENTO qMaestroIncremento = (from c in contexto.MAESTRO_INCREMENTO
                                                              where c.ESTADOMAESTROINCREMENTO == _maestroIncremento.ESTADOMAESTROINCREMENTO
                                                         select c).FirstOrDefault();

                contexto.MAESTRO_INCREMENTO.Remove(qMaestroIncremento);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(MAESTRO_INCREMENTO _maestroIncremento)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_INCREMENTO qMaestroEstadoSolicitud = (from c in contexto.MAESTRO_INCREMENTO
                                                                    where c.IDMAESTROINCREMENTO == _maestroIncremento.IDMAESTROINCREMENTO
                                                              select c).FirstOrDefault();


                qMaestroEstadoSolicitud.ESTADOMAESTROINCREMENTO = false;

                contexto.SaveChanges();
            }


        }

    }
}



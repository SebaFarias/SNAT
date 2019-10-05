using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
 public class maestroProgramaDAO
    {
        public static int Save(MAESTRO_PROGRAMA _maestroPrograma)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_PROGRAMA _mas = new MAESTRO_PROGRAMA();
                try
                {
                    _mas = contexto.MAESTRO_PROGRAMA.Where(c => c.IDMAESTROPROGRAMA == _maestroPrograma.IDMAESTROPROGRAMA).FirstOrDefault<MAESTRO_PROGRAMA>();

                    if (_mas == null)
                    {
                        _mas = _maestroPrograma;
                        contexto.MAESTRO_PROGRAMA.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.ESTADOMAESTROPROGRAMA = _maestroPrograma.ESTADOMAESTROPROGRAMA;
                        _mas.IDMAESTROPROGRAMA = _maestroPrograma.IDMAESTROPROGRAMA;
                        _mas.NOMBREMAESTROPROGRAMA = _maestroPrograma.NOMBREMAESTROPROGRAMA;
                        _mas.CODIGOENTRAZABILIDAD = _maestroPrograma.CODIGOENTRAZABILIDAD;



                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDMAESTROPROGRAMA;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static MAESTRO_PROGRAMA get(int idMaestroModalidad)
        {

            MAESTRO_PROGRAMA _mae = new MAESTRO_PROGRAMA();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroPrograma = from a in contexto.MAESTRO_PROGRAMA
                                          where a.IDMAESTROPROGRAMA == idMaestroModalidad
                                          select a;
                foreach (var a in qMaestroPrograma)
                {

                    _mae.ESTADOMAESTROPROGRAMA = a.ESTADOMAESTROPROGRAMA;
                    _mae.IDMAESTROPROGRAMA = a.IDMAESTROPROGRAMA;
                    _mae.NOMBREMAESTROPROGRAMA  = a.NOMBREMAESTROPROGRAMA;
                    _mae.CODIGOENTRAZABILIDAD = a.CODIGOENTRAZABILIDAD;

                }

                return _mae;
            }

        }

        public static List<MAESTRO_PROGRAMA> getList()
        {
            List<MAESTRO_PROGRAMA> ListaMaestroPrograma = new List<MAESTRO_PROGRAMA>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroPrograma = from a in contexto.MAESTRO_PROGRAMA
                                       where a.ESTADOMAESTROPROGRAMA == true
                                       orderby a.NOMBREMAESTROPROGRAMA
                                       select a;

                foreach (var a in qMaestroPrograma)
                {
                    MAESTRO_PROGRAMA _mae = new MAESTRO_PROGRAMA();

                    _mae.ESTADOMAESTROPROGRAMA = a.ESTADOMAESTROPROGRAMA;
                    _mae.IDMAESTROPROGRAMA = a.IDMAESTROPROGRAMA;
                    _mae.CODIGOENTRAZABILIDAD = a.CODIGOENTRAZABILIDAD;
                    _mae.NOMBREMAESTROPROGRAMA = a.NOMBREMAESTROPROGRAMA;

                    ListaMaestroPrograma.Add(_mae);
                }

                return ListaMaestroPrograma;
            }
        }

        protected void Delete(MAESTRO_PROGRAMA _maestroPrograma)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_PROGRAMA qMaestroPrograma = (from c in contexto.MAESTRO_PROGRAMA
                                                        where c.IDMAESTROPROGRAMA == _maestroPrograma.IDMAESTROPROGRAMA
                                                     select c).FirstOrDefault();

                contexto.MAESTRO_PROGRAMA.Remove(qMaestroPrograma);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(MAESTRO_PROGRAMA _maestroPrograma)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_PROGRAMA qMaestroPrograma = (from c in contexto.MAESTRO_PROGRAMA
                                                        where c.IDMAESTROPROGRAMA == _maestroPrograma.IDMAESTROPROGRAMA
                                                        select c).FirstOrDefault();


                qMaestroPrograma.ESTADOMAESTROPROGRAMA = false;

                contexto.SaveChanges();
            }


        }

    }
}



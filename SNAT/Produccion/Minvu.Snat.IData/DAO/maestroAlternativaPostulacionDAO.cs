using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
   public class maestroAlternativaPostulacionDAO
    {
        public static int Save(MAESTRO_ALTERNATIVA_POSTULACION _maestroAlternativaPostulacion)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_ALTERNATIVA_POSTULACION _mas = new MAESTRO_ALTERNATIVA_POSTULACION();
                try
                {
                    _mas = contexto.MAESTRO_ALTERNATIVA_POSTULACION.Where(c => c.IDMAESTROALTERNATIVAPOSTULACION == _maestroAlternativaPostulacion.IDMAESTROALTERNATIVAPOSTULACION).FirstOrDefault<MAESTRO_ALTERNATIVA_POSTULACION>();

                    if (_mas == null)
                    {
                        _mas = _maestroAlternativaPostulacion;
                        contexto.MAESTRO_ALTERNATIVA_POSTULACION.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.ESTADOMAESTROALTERNATIVAPOSTULACION = _maestroAlternativaPostulacion.ESTADOMAESTROALTERNATIVAPOSTULACION;
                        _mas.IDMAESTROALTERNATIVAPOSTULACION = _maestroAlternativaPostulacion.IDMAESTROALTERNATIVAPOSTULACION;
                        _mas.NOMBREMAESTROALTERNATIVAPOSTULACION = _maestroAlternativaPostulacion.NOMBREMAESTROALTERNATIVAPOSTULACION;
                        
                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDMAESTROALTERNATIVAPOSTULACION;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static MAESTRO_ALTERNATIVA_POSTULACION Get(long idMaestroAlternativaPostulacion)
        {

            MAESTRO_ALTERNATIVA_POSTULACION _mae = new MAESTRO_ALTERNATIVA_POSTULACION();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroAlternativa = from a in contexto.MAESTRO_ALTERNATIVA_POSTULACION
                                           where a.IDMAESTROALTERNATIVAPOSTULACION == idMaestroAlternativaPostulacion
                                           select a;
                foreach (var a in qMaestroAlternativa)
                {

                    _mae.ESTADOMAESTROALTERNATIVAPOSTULACION = a.ESTADOMAESTROALTERNATIVAPOSTULACION;
                    _mae.IDMAESTROALTERNATIVAPOSTULACION = a.IDMAESTROALTERNATIVAPOSTULACION;
                    _mae.NOMBREMAESTROALTERNATIVAPOSTULACION = a.NOMBREMAESTROALTERNATIVAPOSTULACION;
                                    }

                return _mae;
            }

        }

        public static List<MAESTRO_ALTERNATIVA_POSTULACION> GetList()
        {

            List<MAESTRO_ALTERNATIVA_POSTULACION> ListaMaestroAlternativaPostulacion = new List<MAESTRO_ALTERNATIVA_POSTULACION>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroAlternativaPostulacion = from a in contexto.MAESTRO_ALTERNATIVA_POSTULACION
                                    select a;
                foreach (var a in qMaestroAlternativaPostulacion)
                {

                    MAESTRO_ALTERNATIVA_POSTULACION _mae = new MAESTRO_ALTERNATIVA_POSTULACION();


                    _mae.ESTADOMAESTROALTERNATIVAPOSTULACION = a.ESTADOMAESTROALTERNATIVAPOSTULACION;
                    _mae.IDMAESTROALTERNATIVAPOSTULACION = a.IDMAESTROALTERNATIVAPOSTULACION;
                    _mae.NOMBREMAESTROALTERNATIVAPOSTULACION = a.NOMBREMAESTROALTERNATIVAPOSTULACION;

                    ListaMaestroAlternativaPostulacion.Add(_mae);


                }

                return ListaMaestroAlternativaPostulacion;
            }

        }


        protected void Delete(MAESTRO_ALTERNATIVA_POSTULACION _maestroAlternativa)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_ALTERNATIVA_POSTULACION qMaestroAlternativaPostulacion = (from c in contexto.MAESTRO_ALTERNATIVA_POSTULACION
                                                                        where c.IDMAESTROALTERNATIVAPOSTULACION == _maestroAlternativa.IDMAESTROALTERNATIVAPOSTULACION
                                                                        select c).FirstOrDefault();

                contexto.MAESTRO_ALTERNATIVA_POSTULACION.Remove(qMaestroAlternativaPostulacion);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(MAESTRO_ALTERNATIVA_POSTULACION _maestroAlternativaPostulacion)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_ALTERNATIVA_POSTULACION qMaestroAlternativaPostulacion = (from c in contexto.MAESTRO_ALTERNATIVA_POSTULACION
                                                                        where c.IDMAESTROALTERNATIVAPOSTULACION == _maestroAlternativaPostulacion.IDMAESTROALTERNATIVAPOSTULACION
                                                                        select c).FirstOrDefault();


                qMaestroAlternativaPostulacion.ESTADOMAESTROALTERNATIVAPOSTULACION = false;

                contexto.SaveChanges();
            }


        }

    }
}



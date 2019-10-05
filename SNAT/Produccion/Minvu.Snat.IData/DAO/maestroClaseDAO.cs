using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minvu.Snat.IData.DAO
{
  public  class maestroClaseDAO
    {
        public static int Save(MAESTRO_CLASE _maestroTitulo)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_CLASE _mas = new MAESTRO_CLASE();
                try
                {
                    _mas = contexto.MAESTRO_CLASE.Where(c => c.IDMAESTROCLASE == _maestroTitulo.IDMAESTROCLASE).FirstOrDefault<MAESTRO_CLASE>();

                    if (_mas == null)
                    {
                        _mas = _maestroTitulo;
                        contexto.MAESTRO_CLASE.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.ESTADOMAESTROCLASE = _maestroTitulo.ESTADOMAESTROCLASE;
                        _mas.IDMAESTROCLASE = _maestroTitulo.IDMAESTROCLASE;
                        _mas.NOMBREMAESTROCLASE = _maestroTitulo.NOMBREMAESTROCLASE;
                        
                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDMAESTROCLASE;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static MAESTRO_CLASE Get(long idMaestroTitulo)
        {

            MAESTRO_CLASE _mae = new MAESTRO_CLASE();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroClase = from a in contexto.MAESTRO_CLASE
                                     where a.IDMAESTROCLASE == idMaestroTitulo
                                          select a;
                foreach (var a in qMaestroClase)
                {

                    _mae.ESTADOMAESTROCLASE = a.ESTADOMAESTROCLASE;
                    _mae.IDMAESTROCLASE = a.IDMAESTROCLASE;
                    _mae.NOMBREMAESTROCLASE = a.NOMBREMAESTROCLASE;



                }

                return _mae;
            }

        }

        public static List<MAESTRO_CLASE> GetList()
        {

            List<MAESTRO_CLASE> ListaMaestroTitulo = new List<MAESTRO_CLASE>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroTitulo = from a in contexto.MAESTRO_CLASE
                                     select a;
                foreach (var a in qMaestroTitulo)
                {

                    MAESTRO_CLASE _mae = new MAESTRO_CLASE();


                    _mae.ESTADOMAESTROCLASE = a.ESTADOMAESTROCLASE;
                    _mae.IDMAESTROCLASE = a.IDMAESTROCLASE;
                    _mae.NOMBREMAESTROCLASE = a.NOMBREMAESTROCLASE;
                    


                    ListaMaestroTitulo.Add(_mae);


                }

                return ListaMaestroTitulo;
            }

        }


        protected void Delete(MAESTRO_CLASE _maestroTitulo)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_CLASE qMaestroTitulo = (from c in contexto.MAESTRO_CLASE
                                                where c.IDMAESTROCLASE == _maestroTitulo.IDMAESTROCLASE
                                                 select c).FirstOrDefault();

                contexto.MAESTRO_CLASE.Remove(qMaestroTitulo);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(MAESTRO_CLASE _maestroTitulo)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_CLASE qMaestroTitulo = (from c in contexto.MAESTRO_CLASE
                                                where c.IDMAESTROCLASE == _maestroTitulo.IDMAESTROCLASE
                                                   select c).FirstOrDefault();


                qMaestroTitulo.ESTADOMAESTROCLASE = false;

                contexto.SaveChanges();
            }


        }

    }
}



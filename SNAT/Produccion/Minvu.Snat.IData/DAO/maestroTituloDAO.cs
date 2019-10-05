using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minvu.Snat.IData.DAO
{
  public  class maestroTituloDAO
    {
        public static int Save(MAESTRO_TITULO _maestroTitulo)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_TITULO _mas = new MAESTRO_TITULO();
                try
                {
                    _mas = contexto.MAESTRO_TITULO.Where(c => c.IDMAESTROTITULO == _maestroTitulo.IDMAESTROTITULO).FirstOrDefault<MAESTRO_TITULO>();

                    if (_mas == null)
                    {
                        _mas = _maestroTitulo;
                        contexto.MAESTRO_TITULO.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.ESTADOMAESTROTITULO = _maestroTitulo.ESTADOMAESTROTITULO;
                        _mas.IDMAESTROTITULO = _maestroTitulo.IDMAESTROTITULO;
                        _mas.NOMBREMAESTROTITULO = _maestroTitulo.NOMBREMAESTROTITULO;
                        
                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDMAESTROTITULO;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static MAESTRO_TITULO Get(long idMaestroTitulo)
        {

            MAESTRO_TITULO _mae = new MAESTRO_TITULO();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroTitulo = from a in contexto.MAESTRO_TITULO
                                            where a.IDMAESTROTITULO == idMaestroTitulo
                                          select a;
                foreach (var a in qMaestroTitulo)
                {

                    _mae.ESTADOMAESTROTITULO = a.ESTADOMAESTROTITULO;
                    _mae.IDMAESTROTITULO = a.IDMAESTROTITULO;
                    _mae.NOMBREMAESTROTITULO = a.NOMBREMAESTROTITULO;



                }

                return _mae;
            }

        }

        public static List<MAESTRO_TITULO> GetList()
        {

            List<MAESTRO_TITULO> ListaMaestroTitulo = new List<MAESTRO_TITULO>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroTitulo = from a in contexto.MAESTRO_TITULO
                                       select a;
                foreach (var a in qMaestroTitulo)
                {

                    MAESTRO_TITULO _mae = new MAESTRO_TITULO();


                    _mae.ESTADOMAESTROTITULO = a.ESTADOMAESTROTITULO;
                    _mae.IDMAESTROTITULO = a.IDMAESTROTITULO;
                    _mae.NOMBREMAESTROTITULO = a.NOMBREMAESTROTITULO;
                    


                    ListaMaestroTitulo.Add(_mae);


                }

                return ListaMaestroTitulo;
            }

        }


        protected void Delete(MAESTRO_TITULO _maestroTitulo)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_TITULO qMaestroTitulo = (from c in contexto.MAESTRO_TITULO
                                                        where c.IDMAESTROTITULO == _maestroTitulo.IDMAESTROTITULO
                                                 select c).FirstOrDefault();

                contexto.MAESTRO_TITULO.Remove(qMaestroTitulo);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(MAESTRO_TITULO _maestroTitulo)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_TITULO qMaestroTitulo = (from c in contexto.MAESTRO_TITULO
                                                   where c.IDMAESTROTITULO == _maestroTitulo.IDMAESTROTITULO
                                                   select c).FirstOrDefault();


                qMaestroTitulo.ESTADOMAESTROTITULO = false;

                contexto.SaveChanges();
            }


        }

    }
}



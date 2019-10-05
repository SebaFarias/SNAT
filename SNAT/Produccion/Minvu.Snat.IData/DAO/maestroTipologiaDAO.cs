using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minvu.Snat.IData.DAO
{
  public  class maestroTipologiaDAO
    {
        public static int Save(MAESTRO_TIPOLOGIA _maestroTipoProveedor)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_TIPOLOGIA _mas = new MAESTRO_TIPOLOGIA();
                try
                {
                    _mas = contexto.MAESTRO_TIPOLOGIA.Where(c => c.IDMAESTROTIPOLOGIA == _maestroTipoProveedor.IDMAESTROTIPOLOGIA).FirstOrDefault<MAESTRO_TIPOLOGIA>();

                    if (_mas == null)
                    {
                        _mas = _maestroTipoProveedor;
                        contexto.MAESTRO_TIPOLOGIA.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;

                        _mas.ESTADOMAESTROTIPOLOGIA = _maestroTipoProveedor.ESTADOMAESTROTIPOLOGIA;
                        _mas.IDMAESTROTIPOLOGIA = _maestroTipoProveedor.IDMAESTROTIPOLOGIA;
                        _mas.NOMBREMAESTROTIPOLOGIA = _maestroTipoProveedor.NOMBREMAESTROTIPOLOGIA;


                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDMAESTROTIPOLOGIA;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static MAESTRO_TIPOLOGIA Get(long idMaestroTipoProveedor)
        {

            MAESTRO_TIPOLOGIA _mae = new MAESTRO_TIPOLOGIA();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroTipologia = from a in contexto.MAESTRO_TIPOLOGIA
                                            where a.IDMAESTROTIPOLOGIA == idMaestroTipoProveedor
                                          select a;
                foreach (var a in qMaestroTipologia)
                {

                    _mae.ESTADOMAESTROTIPOLOGIA = a.ESTADOMAESTROTIPOLOGIA;
                    _mae.IDMAESTROTIPOLOGIA = a.IDMAESTROTIPOLOGIA;
                    _mae.NOMBREMAESTROTIPOLOGIA = a.NOMBREMAESTROTIPOLOGIA;
                    



                }

                return _mae;
            }

        }

        public static List<MAESTRO_TIPOLOGIA> GetList()
        {
            List<MAESTRO_TIPOLOGIA> ListaMaestroTipologia = new List<MAESTRO_TIPOLOGIA>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroTipologia = from a in contexto.MAESTRO_TIPOLOGIA
                                        orderby a.NOMBREMAESTROTIPOLOGIA
                                        select a;
                foreach (var a in qMaestroTipologia)
                {

                    MAESTRO_TIPOLOGIA _mae = new MAESTRO_TIPOLOGIA();


                    _mae.ESTADOMAESTROTIPOLOGIA = a.ESTADOMAESTROTIPOLOGIA;
                    _mae.IDMAESTROTIPOLOGIA = a.IDMAESTROTIPOLOGIA;
                    _mae.NOMBREMAESTROTIPOLOGIA = a.NOMBREMAESTROTIPOLOGIA;

                    ListaMaestroTipologia.Add(_mae);
                }

                return ListaMaestroTipologia;
            }
        }

        public static List<MAESTRO_TIPOLOGIA> GetListTipologiaPrograma(long? idMaestroPrograma)
        {
            List<MAESTRO_TIPOLOGIA> ListaMaestroTipologia = new List<MAESTRO_TIPOLOGIA>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroTipologia = from sp in contexto.SERVICIO_PARCIALIDAD
                                        join ts in contexto.TIPOLOGIA_SERVICIO on sp.IDTIPOLOGIASERVICIO equals ts.IDTIPOLOGIASERVICIO
                                        join mt in contexto.MAESTRO_TIPOLOGIA on ts.IDMAESTROTIPOLOGIA equals mt.IDMAESTROTIPOLOGIA
                                        where sp.IDMAESTROPROGRAMA == idMaestroPrograma
                                        orderby mt.NOMBREMAESTROTIPOLOGIA
                                        group mt.NOMBREMAESTROTIPOLOGIA by mt.IDMAESTROTIPOLOGIA into g
                                        select new { IDMAESTROTIPOLOGIA = g.Key, NOMBREMAESTROTIPOLOGIA = g.FirstOrDefault() };

                foreach (var a in qMaestroTipologia)
                {

                    MAESTRO_TIPOLOGIA _mae = new MAESTRO_TIPOLOGIA();

                    _mae.IDMAESTROTIPOLOGIA = a.IDMAESTROTIPOLOGIA;
                    _mae.NOMBREMAESTROTIPOLOGIA = a.NOMBREMAESTROTIPOLOGIA;

                    ListaMaestroTipologia.Add(_mae);
                }

                return ListaMaestroTipologia;
            }
        }

        protected void Delete(MAESTRO_TIPOLOGIA _maestroTipoProveedor)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_TIPOLOGIA qMaestroTipologia = (from c in contexto.MAESTRO_TIPOLOGIA
                                                           where c.IDMAESTROTIPOLOGIA == _maestroTipoProveedor.IDMAESTROTIPOLOGIA
                                                       select c).FirstOrDefault();

                contexto.MAESTRO_TIPOLOGIA.Remove(qMaestroTipologia);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(MAESTRO_TIPOLOGIA _maestroTipologia)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_TIPOLOGIA qMaestroTipologia = (from c in contexto.MAESTRO_TIPOLOGIA
                                                      where c.IDMAESTROTIPOLOGIA == _maestroTipologia.IDMAESTROTIPOLOGIA
                                                       select c).FirstOrDefault();

                

                contexto.SaveChanges();
            }


        }

    }
}



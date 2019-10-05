using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minvu.Snat.IData.DAO
{
    public class maestroServicioDAO
    {
        public static int Save(MAESTRO_SERVICIO _maestroServicio)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_SERVICIO _mas = new MAESTRO_SERVICIO();
                try
                {
                    _mas = contexto.MAESTRO_SERVICIO.Where(c => c.IDMAESTROSERVICIO == _maestroServicio.IDMAESTROSERVICIO).FirstOrDefault<MAESTRO_SERVICIO>();

                    if (_mas == null)
                    {
                        _mas = _maestroServicio;
                        contexto.MAESTRO_SERVICIO.Add(_mas);
                    }
                    else
                    {
                        _mas.ESTADOMAESTROSERVICIO = _maestroServicio.ESTADOMAESTROSERVICIO;
                        _mas.IDMAESTROSERVICIO = _maestroServicio.IDMAESTROSERVICIO;
                        _mas.NOMBREMAESTROSERVICIO = _maestroServicio.NOMBREMAESTROSERVICIO;
                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDMAESTROSERVICIO;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        }

        public static MAESTRO_SERVICIO Get(int idMaestroServicio)
        {
            MAESTRO_SERVICIO _mae = new MAESTRO_SERVICIO();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroServicio = from a in contexto.MAESTRO_SERVICIO
                                       where a.IDMAESTROSERVICIO == idMaestroServicio
                                       select a;

                foreach (var a in qMaestroServicio)
                {
                    _mae.ESTADOMAESTROSERVICIO = a.ESTADOMAESTROSERVICIO;
                    _mae.IDMAESTROSERVICIO = a.IDMAESTROSERVICIO;
                    _mae.NOMBREMAESTROSERVICIO = a.NOMBREMAESTROSERVICIO;
                }

                return _mae;
            }
        }

        public static List<MAESTRO_SERVICIO> getListServicioTipologia(long? idTipologia)
        {
            List<MAESTRO_SERVICIO> ListaMaestroServicio = new List<MAESTRO_SERVICIO>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroTipologia = from sp in contexto.SERVICIO_PARCIALIDAD
                                        join ts in contexto.TIPOLOGIA_SERVICIO on sp.IDTIPOLOGIASERVICIO equals ts.IDTIPOLOGIASERVICIO
                                        join mt in contexto.MAESTRO_SERVICIO on ts.IDMAESTROSERVICIO equals mt.IDMAESTROSERVICIO
                                        where ts.IDMAESTROTIPOLOGIA == idTipologia
                                        orderby mt.NOMBREABREVIADOMAESTROSERVICIO
                                        group mt.NOMBREABREVIADOMAESTROSERVICIO by mt.IDMAESTROSERVICIO into g
                                        select new { IDMAESTROSERVICIO = g.Key, NOMBREABREVIADOMAESTROSERVICIO = g.FirstOrDefault() };

                foreach (var a in qMaestroTipologia)
                {
                    MAESTRO_SERVICIO _mae = new MAESTRO_SERVICIO();

                    _mae.IDMAESTROSERVICIO = a.IDMAESTROSERVICIO;
                    _mae.NOMBREABREVIADOMAESTROSERVICIO = a.NOMBREABREVIADOMAESTROSERVICIO;

                    ListaMaestroServicio.Add(_mae);
                }

                return ListaMaestroServicio;
            }
        }

        public static List<MAESTRO_SERVICIO> getListServicioTipologiaPrograma(long? idTipologia, long? idPrograma)
        {
            List<MAESTRO_SERVICIO> ListaMaestroServicio = new List<MAESTRO_SERVICIO>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroTipologia = from sp in contexto.SERVICIO_PARCIALIDAD
                                        join ts in contexto.TIPOLOGIA_SERVICIO on sp.IDTIPOLOGIASERVICIO equals ts.IDTIPOLOGIASERVICIO
                                        join mt in contexto.MAESTRO_SERVICIO on ts.IDMAESTROSERVICIO equals mt.IDMAESTROSERVICIO
                                        where ts.IDMAESTROTIPOLOGIA == idTipologia
                                        && sp.IDMAESTROPROGRAMA == idPrograma
                                        orderby mt.NOMBREMAESTROSERVICIO
                                        group mt.NOMBREMAESTROSERVICIO by mt.IDMAESTROSERVICIO into g
                                        select new { IDMAESTROSERVICIO = g.Key, NOMBREMAESTROSERVICIO = g.FirstOrDefault() };

                foreach (var a in qMaestroTipologia)
                {
                    MAESTRO_SERVICIO _mae = new MAESTRO_SERVICIO();

                    _mae.IDMAESTROSERVICIO = a.IDMAESTROSERVICIO;
                    //_mae.NOMBREMAESTROSERVICIO = a.NOMBREMAESTROSERVICIO;
                    _mae.NOMBREMAESTROSERVICIO = a.NOMBREMAESTROSERVICIO;

                    ListaMaestroServicio.Add(_mae);
                }

                return ListaMaestroServicio;
            }
        }

        public static List<MAESTRO_SERVICIO> GetList()
        {
            List<MAESTRO_SERVICIO> ListaMaestroServicio = new List<MAESTRO_SERVICIO>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroServicio = from a in contexto.MAESTRO_SERVICIO
                                       orderby a.NOMBREMAESTROSERVICIO
                                       select a;
                foreach (var a in qMaestroServicio)
                {
                    MAESTRO_SERVICIO _mae = new MAESTRO_SERVICIO();

                    _mae.ESTADOMAESTROSERVICIO = a.ESTADOMAESTROSERVICIO;
                    _mae.IDMAESTROSERVICIO = a.IDMAESTROSERVICIO;
                    _mae.NOMBREMAESTROSERVICIO = a.NOMBREMAESTROSERVICIO;

                    ListaMaestroServicio.Add(_mae);
                }

                return ListaMaestroServicio;
            }
        }

        protected void Delete(MAESTRO_SERVICIO _maestroServicio)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_SERVICIO qMaestroServicio = (from c in contexto.MAESTRO_SERVICIO
                                                     where c.IDMAESTROSERVICIO == _maestroServicio.IDMAESTROSERVICIO
                                                     select c).FirstOrDefault();

                contexto.MAESTRO_SERVICIO.Remove(qMaestroServicio);
                contexto.SaveChanges();
            }
        }

        protected void ChangeStatus(MAESTRO_SERVICIO _maestroServicio)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_SERVICIO qMaestroServicio = (from c in contexto.MAESTRO_SERVICIO
                                                     where c.IDMAESTROSERVICIO == _maestroServicio.IDMAESTROSERVICIO
                                                     select c).FirstOrDefault();

                qMaestroServicio.ESTADOMAESTROSERVICIO = false;

                contexto.SaveChanges();
            }
        }
    }
}
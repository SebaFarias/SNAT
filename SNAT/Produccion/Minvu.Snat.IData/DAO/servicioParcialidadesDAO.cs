
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{



    public class servicioParcialidadesDAO
    {
        public static int Save(SERVICIO_PARCIALIDAD _servicioParcialidad)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                SERVICIO_PARCIALIDAD _inf = new SERVICIO_PARCIALIDAD();
                try
                {
                    _inf = contexto.SERVICIO_PARCIALIDAD.Where(c => c.IDSERVICIOPARCIALIDAD == _servicioParcialidad.IDSERVICIOPARCIALIDAD).FirstOrDefault<SERVICIO_PARCIALIDAD>();

                    if (_inf == null)
                    {
                        _inf = _servicioParcialidad;
                        contexto.SERVICIO_PARCIALIDAD.Add(_inf);
                    }
                    else
                    {
                        _inf.IDSUBMODALIDADPARCIALIDAD  = _servicioParcialidad.IDSUBMODALIDADPARCIALIDAD;
                        _inf.IDGRUPOSUBMODALIDADPARCIALIDAD = _servicioParcialidad.IDGRUPOSUBMODALIDADPARCIALIDAD;
                        _inf.IDTIPOLOGIASERVICIO = _servicioParcialidad.IDTIPOLOGIASERVICIO;
                        _inf.IDGRUPOTIPOLOGIASERVICIO = _servicioParcialidad.IDGRUPOTIPOLOGIASERVICIO;
                        _inf.IDSERVICIOPARCIALIDAD = _servicioParcialidad.IDSERVICIOPARCIALIDAD;
                        _inf.NUMEROPLANTILLASERVICIOPARCIALIDAD = _servicioParcialidad.NUMEROPLANTILLASERVICIOPARCIALIDAD;
                        _inf.OBSERVACIONSERVICIOPARCIALIDAD = _servicioParcialidad.OBSERVACIONSERVICIOPARCIALIDAD;
                        _inf.IDMAESTROPROGRAMA = _servicioParcialidad.IDMAESTROPROGRAMA;
                        _inf.ESTADOSERVICIOPARCIALIDAD = _servicioParcialidad.ESTADOSERVICIOPARCIALIDAD;
                    }

                    contexto.SaveChanges();
                    return (int)_inf.IDSERVICIOPARCIALIDAD;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static SERVICIO_PARCIALIDAD Get(long _IDSERVICIOPARCIALIDAD)
        {

            SERVICIO_PARCIALIDAD _inf = new SERVICIO_PARCIALIDAD();

            try
            {
                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qServicioParcialidad = from a in contexto.SERVICIO_PARCIALIDAD
                                                     where a.IDSERVICIOPARCIALIDAD == _IDSERVICIOPARCIALIDAD
                                               select a;
                    foreach (var a in qServicioParcialidad)
                    {

                        _inf.IDSUBMODALIDADPARCIALIDAD = a.IDSUBMODALIDADPARCIALIDAD;
                        _inf.IDGRUPOSUBMODALIDADPARCIALIDAD = a.IDGRUPOSUBMODALIDADPARCIALIDAD;
                        _inf.IDTIPOLOGIASERVICIO = a.IDTIPOLOGIASERVICIO;
                        _inf.IDGRUPOTIPOLOGIASERVICIO = a.IDGRUPOTIPOLOGIASERVICIO;
                        _inf.IDSERVICIOPARCIALIDAD = a.IDSERVICIOPARCIALIDAD;
                        _inf.NUMEROPLANTILLASERVICIOPARCIALIDAD = a.NUMEROPLANTILLASERVICIOPARCIALIDAD;
                        _inf.OBSERVACIONSERVICIOPARCIALIDAD = a.OBSERVACIONSERVICIOPARCIALIDAD;
                        _inf.IDMAESTROPROGRAMA = a.IDMAESTROPROGRAMA;
                        _inf.ESTADOSERVICIOPARCIALIDAD = a.ESTADOSERVICIOPARCIALIDAD;

                    }

                    return _inf;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        

        }

        public static List<SERVICIO_PARCIALIDAD> GetList()
        {

            List<SERVICIO_PARCIALIDAD> listaServicioParcialidad = new List<SERVICIO_PARCIALIDAD>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qCaracteristicasEspeciales = from a in contexto.SERVICIO_PARCIALIDAD
                                                 select a;
                foreach (var a in qCaracteristicasEspeciales)
                {

                    SERVICIO_PARCIALIDAD _inf = new SERVICIO_PARCIALIDAD();


                    _inf.IDSUBMODALIDADPARCIALIDAD = a.IDSUBMODALIDADPARCIALIDAD;
                    _inf.IDGRUPOSUBMODALIDADPARCIALIDAD = a.IDGRUPOSUBMODALIDADPARCIALIDAD;
                    _inf.IDTIPOLOGIASERVICIO = a.IDTIPOLOGIASERVICIO;
                    _inf.IDGRUPOTIPOLOGIASERVICIO = a.IDGRUPOTIPOLOGIASERVICIO;
                    _inf.IDSERVICIOPARCIALIDAD = a.IDSERVICIOPARCIALIDAD;
                    _inf.NUMEROPLANTILLASERVICIOPARCIALIDAD = a.NUMEROPLANTILLASERVICIOPARCIALIDAD;
                    _inf.OBSERVACIONSERVICIOPARCIALIDAD = a.OBSERVACIONSERVICIOPARCIALIDAD;
                    _inf.IDMAESTROPROGRAMA = a.IDMAESTROPROGRAMA;
                    _inf.ESTADOSERVICIOPARCIALIDAD = a.ESTADOSERVICIOPARCIALIDAD;

                    listaServicioParcialidad.Add(_inf);


                }

                return listaServicioParcialidad;
            }

        }


        protected void Delete(SERVICIO_PARCIALIDAD _servicioParcialidad)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                SERVICIO_PARCIALIDAD qServicioParcialidad = (from c in contexto.SERVICIO_PARCIALIDAD
                                                                         where c.IDSERVICIOPARCIALIDAD  == _servicioParcialidad.IDSERVICIOPARCIALIDAD
                                                                   select c).FirstOrDefault();

                contexto.SERVICIO_PARCIALIDAD.Remove(qServicioParcialidad);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(SERVICIO_PARCIALIDAD _servicioParcialidad)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                SERVICIO_PARCIALIDAD qCaracteristicasespeciales = (from c in contexto.SERVICIO_PARCIALIDAD
                                                                   where c.IDSERVICIOPARCIALIDAD == _servicioParcialidad.IDSERVICIOPARCIALIDAD
                                                                   select c).FirstOrDefault();


                //qCaracteristicasespeciales.= false;

                contexto.SaveChanges();
            }


        }



     

    }
}



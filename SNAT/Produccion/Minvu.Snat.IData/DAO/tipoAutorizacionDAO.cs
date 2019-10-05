using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minvu.Snat.IData.DAO
{
 public   class tipoAutorizacionDAO
    {
        public static int Save(TIPO_AUTORIZACION _tipoAutorizacion)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                TIPO_AUTORIZACION _mas = new TIPO_AUTORIZACION();
                try
                {
                    _mas = contexto.TIPO_AUTORIZACION.Where(c => c.IDTIPOAUTORIZACION == _tipoAutorizacion.IDTIPOAUTORIZACION).FirstOrDefault<TIPO_AUTORIZACION>();

                    if (_mas == null)
                    {
                        _mas = _tipoAutorizacion;
                        contexto.TIPO_AUTORIZACION.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.ESTADOTIPOAUTORIZACION = _tipoAutorizacion.ESTADOTIPOAUTORIZACION;
                        _mas.IDAUTORIZACION = _tipoAutorizacion.IDAUTORIZACION;
                        _mas.IDSOLICITUDPAGO = _tipoAutorizacion.IDSOLICITUDPAGO;
                        _mas.IDTIPOAUTORIZACION = _tipoAutorizacion.IDTIPOAUTORIZACION;
                        
                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDTIPOAUTORIZACION; 
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static TIPO_AUTORIZACION Get(int idTipoAutorizacion)
        {

            TIPO_AUTORIZACION _mae = new TIPO_AUTORIZACION();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qTipoAutorizacion = from a in contexto.TIPO_AUTORIZACION
                                     where a.IDTIPOAUTORIZACION == idTipoAutorizacion
                                          select a;
                foreach (var a in qTipoAutorizacion)
                {

                    _mae.ESTADOTIPOAUTORIZACION = a.ESTADOTIPOAUTORIZACION;
                    _mae.IDAUTORIZACION = a.IDAUTORIZACION;
                    _mae.IDSOLICITUDPAGO = a.IDSOLICITUDPAGO;
                    _mae.IDTIPOAUTORIZACION = a.IDTIPOAUTORIZACION;
                    
                }

                return _mae;
            }

        }

        public static List<TIPO_AUTORIZACION> GetList(long idAutorizacion)
        {

            List<TIPO_AUTORIZACION> ListaTipoAutorizacion = new List<TIPO_AUTORIZACION>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qTipoAutorizacion = from a in contexto.TIPO_AUTORIZACION
                                        where a.IDAUTORIZACION == idAutorizacion && a.ESTADOTIPOAUTORIZACION == true
                                        select a;
                foreach (var a in qTipoAutorizacion)
                {

                    TIPO_AUTORIZACION _mae = new TIPO_AUTORIZACION();


                    _mae.ESTADOTIPOAUTORIZACION = a.ESTADOTIPOAUTORIZACION;
                    _mae.IDAUTORIZACION = a.IDAUTORIZACION;
                    _mae.IDSOLICITUDPAGO = a.IDSOLICITUDPAGO;
                    _mae.IDTIPOAUTORIZACION = a.IDTIPOAUTORIZACION;
                    
                    
                    ListaTipoAutorizacion.Add(_mae);


                }

                return ListaTipoAutorizacion;
            }

        }


        protected void Delete(TIPO_AUTORIZACION _tipoAutorizacion)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                TIPO_AUTORIZACION qTipoAutorizacion = (from c in contexto.TIPO_AUTORIZACION
                                                   where c.IDTIPOAUTORIZACION == _tipoAutorizacion.IDTIPOAUTORIZACION
                                                       select c).FirstOrDefault();

                contexto.TIPO_AUTORIZACION.Remove(qTipoAutorizacion);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(TIPO_AUTORIZACION _tipoAutorizacion)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                TIPO_AUTORIZACION qTipoAutorizacion = (from c in contexto.TIPO_AUTORIZACION
                                                    where c.IDTIPOAUTORIZACION == _tipoAutorizacion.IDTIPOAUTORIZACION
                                                       select c).FirstOrDefault();


                qTipoAutorizacion.ESTADOTIPOAUTORIZACION = false;

                contexto.SaveChanges();
            }


        }

    }
}



using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minvu.Snat.IData.DAO
{
  public  class tipoIncrementoDAO
    {
        public static int Save(TIPO_INCREMENTO _tipoIncremento)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                TIPO_INCREMENTO _mas = new TIPO_INCREMENTO();
                try
                {
                    _mas = contexto.TIPO_INCREMENTO.Where(c => c.IDTIPOINCREMENTO == _tipoIncremento.IDTIPOINCREMENTO).FirstOrDefault<TIPO_INCREMENTO>();

                    if (_mas == null)
                    {
                        _mas = _tipoIncremento;
                        contexto.TIPO_INCREMENTO.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.ESTADOTIPOINCREMENTO = _tipoIncremento.ESTADOTIPOINCREMENTO;
                        _mas.IDMAESTROINCREMENTO = _tipoIncremento.IDMAESTROINCREMENTO;
                        _mas.IDTIPOINCREMENTO = _tipoIncremento.IDTIPOINCREMENTO;
                        _mas.IDCARACTERISTICASESPECIALES = _tipoIncremento.IDCARACTERISTICASESPECIALES;
                        _mas.SELECCIONADOTIPOINCREMENTO = _tipoIncremento.SELECCIONADOTIPOINCREMENTO;
                        _mas.IDSOLICITUDPAGO = _tipoIncremento.IDSOLICITUDPAGO;


                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDTIPOINCREMENTO; 
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static TIPO_INCREMENTO Get(int idTipoIncremento)
        {

            TIPO_INCREMENTO _mae = new TIPO_INCREMENTO();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qTipoIncremento = from a in contexto.TIPO_INCREMENTO
                                        where a.IDTIPOINCREMENTO == idTipoIncremento
                                          select a;
                foreach (var a in qTipoIncremento)
                {

                    _mae.ESTADOTIPOINCREMENTO = a.ESTADOTIPOINCREMENTO;
                    _mae.IDMAESTROINCREMENTO = a.IDMAESTROINCREMENTO;
                    _mae.IDTIPOINCREMENTO = a.IDTIPOINCREMENTO;
                    _mae.IDCARACTERISTICASESPECIALES = a.IDCARACTERISTICASESPECIALES;
                    _mae.SELECCIONADOTIPOINCREMENTO = a.SELECCIONADOTIPOINCREMENTO;


                }

                return _mae;
            }

        }

        public static List<TIPO_INCREMENTO> GetList(long idCaracteristicasEspeciales, long? idSolicitudPago)
        {

            List<TIPO_INCREMENTO> ListaTipoIncremento = new List<TIPO_INCREMENTO>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qTipoIncremento = from a in contexto.TIPO_INCREMENTO
                                      where a.IDCARACTERISTICASESPECIALES == idCaracteristicasEspeciales && a.IDSOLICITUDPAGO == idSolicitudPago
                                      select a;

                foreach (var a in qTipoIncremento)
                {

                    TIPO_INCREMENTO _mae = new TIPO_INCREMENTO();


                    _mae.ESTADOTIPOINCREMENTO = a.ESTADOTIPOINCREMENTO;
                    _mae.IDMAESTROINCREMENTO = a.IDMAESTROINCREMENTO;
                    _mae.IDTIPOINCREMENTO = a.IDTIPOINCREMENTO;
                    _mae.IDCARACTERISTICASESPECIALES = a.IDCARACTERISTICASESPECIALES;
                    _mae.SELECCIONADOTIPOINCREMENTO = a.SELECCIONADOTIPOINCREMENTO;



                    ListaTipoIncremento.Add(_mae);


                }

                return ListaTipoIncremento;
            }

        }


        protected void Delete(TIPO_INCREMENTO _tipoIncremento)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                TIPO_INCREMENTO qTipoAutorizacion = (from c in contexto.TIPO_INCREMENTO
                                                     where c.IDTIPOINCREMENTO == _tipoIncremento.IDTIPOINCREMENTO
                                                     select c).FirstOrDefault();

                contexto.TIPO_INCREMENTO.Remove(qTipoAutorizacion);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(TIPO_INCREMENTO _tipoIncremento)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                TIPO_INCREMENTO qTipoAutorizacion = (from c in contexto.TIPO_INCREMENTO
                                                     where c.IDTIPOINCREMENTO == _tipoIncremento.IDTIPOINCREMENTO
                                                     select c).FirstOrDefault();


                qTipoAutorizacion.ESTADOTIPOINCREMENTO = false;

                contexto.SaveChanges();
            }


        }

    }
}



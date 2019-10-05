using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minvu.Snat.IData.DAO
{
   public class tipoProveedorInformacionProyectoDAO
    {
        public static int Save(TIPO_PROVEEDOR_INFORMACION_PROYECTO _tipoIncremento)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                TIPO_PROVEEDOR_INFORMACION_PROYECTO _mas = new TIPO_PROVEEDOR_INFORMACION_PROYECTO();
                try
                {
                    _mas = contexto.TIPO_PROVEEDOR_INFORMACION_PROYECTO.Where(c => c.IDPROVEEDOR == _tipoIncremento.IDPROVEEDOR && c.IDTIPOPROVEEDORINFORMACIONPROYECTO == _tipoIncremento.IDTIPOPROVEEDORINFORMACIONPROYECTO && c.IDINFORMACIONPROYECTO == _tipoIncremento.IDINFORMACIONPROYECTO).FirstOrDefault<TIPO_PROVEEDOR_INFORMACION_PROYECTO>();

                    if (_mas == null)
                    {
                        _mas = _tipoIncremento;
                        contexto.TIPO_PROVEEDOR_INFORMACION_PROYECTO.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.IDTIPOPROVEEDORINFORMACIONPROYECTO = _tipoIncremento.IDTIPOPROVEEDORINFORMACIONPROYECTO;
                        _mas.IDPROVEEDOR = _tipoIncremento.IDPROVEEDOR;
                        _mas.IDINFORMACIONPROYECTO = _tipoIncremento.IDINFORMACIONPROYECTO;
                        _mas.ESTADOTIPOPROVEEDORINFORMACIONPROYECTO = _tipoIncremento.ESTADOTIPOPROVEEDORINFORMACIONPROYECTO;
                        

                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDTIPOPROVEEDORINFORMACIONPROYECTO; 
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static TIPO_PROVEEDOR_INFORMACION_PROYECTO Get(long idInformacionProyecto)
        {

            TIPO_PROVEEDOR_INFORMACION_PROYECTO _mae = new TIPO_PROVEEDOR_INFORMACION_PROYECTO();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qTipoProveedorInformacionProyecto = from a in contexto.TIPO_PROVEEDOR_INFORMACION_PROYECTO
                                      where a.IDINFORMACIONPROYECTO == idInformacionProyecto
                                                        select a;
                foreach (var a in qTipoProveedorInformacionProyecto)
                {

                    _mae.IDINFORMACIONPROYECTO = a.IDINFORMACIONPROYECTO;
                    _mae.IDPROVEEDOR = a.IDPROVEEDOR;
                    _mae.IDTIPOPROVEEDORINFORMACIONPROYECTO = a.IDTIPOPROVEEDORINFORMACIONPROYECTO;
                    _mae.ESTADOTIPOPROVEEDORINFORMACIONPROYECTO = a.ESTADOTIPOPROVEEDORINFORMACIONPROYECTO;
                    

                }

                return _mae;
            }

        }

        public static List<TIPO_PROVEEDOR_INFORMACION_PROYECTO> GetList()
        {

            List<TIPO_PROVEEDOR_INFORMACION_PROYECTO> ListaTipoIncremento = new List<TIPO_PROVEEDOR_INFORMACION_PROYECTO>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qTipoProveedorInformacionProyecto = from a in contexto.TIPO_PROVEEDOR_INFORMACION_PROYECTO
                                      select a;
                foreach (var a in qTipoProveedorInformacionProyecto)
                {

                    TIPO_PROVEEDOR_INFORMACION_PROYECTO _mae = new TIPO_PROVEEDOR_INFORMACION_PROYECTO();


                    _mae.IDINFORMACIONPROYECTO = a.IDINFORMACIONPROYECTO;
                    _mae.IDPROVEEDOR = a.IDPROVEEDOR;
                    _mae.IDTIPOPROVEEDORINFORMACIONPROYECTO = a.IDTIPOPROVEEDORINFORMACIONPROYECTO;
                    _mae.ESTADOTIPOPROVEEDORINFORMACIONPROYECTO = a.ESTADOTIPOPROVEEDORINFORMACIONPROYECTO;



                    ListaTipoIncremento.Add(_mae);


                }

                return ListaTipoIncremento;
            }

        }

        public static List<TIPO_PROVEEDOR_INFORMACION_PROYECTO> GetList(long? idInformacionProyecto)
        {

            List<TIPO_PROVEEDOR_INFORMACION_PROYECTO> ListaTipoIncremento = new List<TIPO_PROVEEDOR_INFORMACION_PROYECTO>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qTipoProveedorInformacionProyecto = from a in contexto.TIPO_PROVEEDOR_INFORMACION_PROYECTO
                                                        where a.IDINFORMACIONPROYECTO == idInformacionProyecto  && a.ESTADOTIPOPROVEEDORINFORMACIONPROYECTO ==true
                                                        select a;
                foreach (var a in qTipoProveedorInformacionProyecto)
                {

                    TIPO_PROVEEDOR_INFORMACION_PROYECTO _mae = new TIPO_PROVEEDOR_INFORMACION_PROYECTO();


                    _mae.IDINFORMACIONPROYECTO = a.IDINFORMACIONPROYECTO;
                    _mae.IDPROVEEDOR = a.IDPROVEEDOR;
                    _mae.IDTIPOPROVEEDORINFORMACIONPROYECTO = a.IDTIPOPROVEEDORINFORMACIONPROYECTO;
                    _mae.ESTADOTIPOPROVEEDORINFORMACIONPROYECTO = a.ESTADOTIPOPROVEEDORINFORMACIONPROYECTO;



                    ListaTipoIncremento.Add(_mae);


                }

                return ListaTipoIncremento;
            }

        }


        protected void Delete(TIPO_PROVEEDOR_INFORMACION_PROYECTO _tipoIncremento)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                TIPO_PROVEEDOR_INFORMACION_PROYECTO qTipoAutorizacion = (from c in contexto.TIPO_PROVEEDOR_INFORMACION_PROYECTO
                                                                         where c.IDTIPOPROVEEDORINFORMACIONPROYECTO == _tipoIncremento.IDTIPOPROVEEDORINFORMACIONPROYECTO
                                                                         select c).FirstOrDefault();

                contexto.TIPO_PROVEEDOR_INFORMACION_PROYECTO.Remove(qTipoAutorizacion);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(TIPO_PROVEEDOR_INFORMACION_PROYECTO _tipoIncremento)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                TIPO_PROVEEDOR_INFORMACION_PROYECTO qTipoAutorizacion = (from c in contexto.TIPO_PROVEEDOR_INFORMACION_PROYECTO
                                                                         where c.IDTIPOPROVEEDORINFORMACIONPROYECTO == _tipoIncremento.IDTIPOPROVEEDORINFORMACIONPROYECTO
                                                                         select c).FirstOrDefault();


                qTipoAutorizacion.ESTADOTIPOPROVEEDORINFORMACIONPROYECTO = false;

                contexto.SaveChanges();
            }


        }

    }
}



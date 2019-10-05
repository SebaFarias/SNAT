using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minvu.Snat.IData.DAO
{
    class tipoServicioDAO
    {
        //public static int Save(TIPO_SERVICIO _tipoServicio)
        //{
        //    using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
        //    {
        //        TIPO_SERVICIO _mas = new TIPO_SERVICIO();
        //        try
        //        {
        //            _mas = contexto.TIPO_SERVICIO.Where(c => c.IDTIPOSERVICIO == _tipoServicio.IDTIPOSERVICIO).FirstOrDefault<TIPO_SERVICIO>();

        //            if (_mas == null)
        //            {
        //                _mas = _tipoServicio;
        //                contexto.TIPO_SERVICIO.Add(_mas);
        //            }
        //            else
        //            {
        //                //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
        //                _mas.IDMAESTROSERVICIO = _tipoServicio.IDMAESTROSERVICIO;
        //                _mas.IDSOLICITUDPAGO = _tipoServicio.IDSOLICITUDPAGO;
        //                _mas.IDTIPOSERVICIO = _tipoServicio.IDTIPOSERVICIO;
        //                _mas.MONTOASIGNACIONDIRECTATIPOSERVICIO = _tipoServicio.MONTOASIGNACIONDIRECTATIPOSERVICIO;
        //                _mas.MONTOINCREMENTOTIPOSERVICIO = _tipoServicio.MONTOINCREMENTOTIPOSERVICIO;
        //                _mas.TOTALSERVICIOTIPOSERVICIO = _tipoServicio.TOTALSERVICIOTIPOSERVICIO;
                        



        //            }

        //            contexto.SaveChanges();
        //            return (int)_mas.IDTIPOSERVICIO; 
        //        }
        //        catch (Exception Ex)
        //        {
        //            //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
        //            throw Ex;
        //        }
        //    }
        //}



        //public static TIPO_SERVICIO get(int idTipoServicio)
        //{

        //    TIPO_SERVICIO _mae = new TIPO_SERVICIO();

        //    using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
        //    {
        //        var qTipoServicio = from a in contexto.TIPO_SERVICIO
        //                               where a.IDTIPOSERVICIO == idTipoServicio
        //                                  select a;
        //        foreach (var a in qTipoServicio)
        //        {

        //            _mae.IDTIPOSERVICIO = a.IDTIPOSERVICIO;
                    
        //            _mae.IDSOLICITUDPAGO = a.IDSOLICITUDPAGO;
        //            _mae.MONTOASIGNACIONDIRECTATIPOSERVICIO = a.MONTOASIGNACIONDIRECTATIPOSERVICIO;
        //            _mae.MONTOINCREMENTOTIPOSERVICIO = a.MONTOINCREMENTOTIPOSERVICIO;
        //            _mae.TOTALSERVICIOTIPOSERVICIO = a.TOTALSERVICIOTIPOSERVICIO;


        //        }

        //        return _mae;
        //    }

        //}

        //public static List<TIPO_SERVICIO> getList()
        //{

        //    List<TIPO_SERVICIO> ListaTipoServicio = new List<TIPO_SERVICIO>();

        //    using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
        //    {
        //        var qTipoServicio = from a in contexto.TIPO_SERVICIO
        //                               select a;
        //        foreach (var a in qTipoServicio)
        //        {

        //            TIPO_SERVICIO _mae = new TIPO_SERVICIO();


        //            _mae.IDMAESTROSERVICIO = a.IDMAESTROSERVICIO;
        //            _mae.IDSOLICITUDPAGO = a.IDSOLICITUDPAGO;
        //            _mae.IDTIPOSERVICIO = a.IDTIPOSERVICIO;
        //            _mae.MONTOASIGNACIONDIRECTATIPOSERVICIO = a.MONTOASIGNACIONDIRECTATIPOSERVICIO;
        //            _mae.MONTOINCREMENTOTIPOSERVICIO = a.MONTOINCREMENTOTIPOSERVICIO;
        //            _mae.TOTALSERVICIOTIPOSERVICIO = a.TOTALSERVICIOTIPOSERVICIO;



        //            ListaTipoServicio.Add(_mae);


        //        }

        //        return ListaTipoServicio;
        //    }

        //}


        //protected void Delete(TIPO_SERVICIO _tipoServicio)
        //{

        //    using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
        //    {
        //        TIPO_SERVICIO qTipoServicio = (from c in contexto.TIPO_SERVICIO
        //                                          where c.IDTIPOSERVICIO == _tipoServicio.IDTIPOSERVICIO
        //                                       select c).FirstOrDefault();

        //        contexto.TIPO_SERVICIO.Remove(qTipoServicio);
        //        contexto.SaveChanges();
        //    }

        //}


        //protected void ChangeStatus(TIPO_SERVICIO _tipoServicio)
        //{
        //    using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
        //    {
        //        TIPO_SERVICIO qTipoServicio = (from c in contexto.TIPO_SERVICIO
        //                                          where c.IDTIPOSERVICIO == _tipoServicio.IDTIPOSERVICIO
        //                                          select c).FirstOrDefault();


        //        //Añadir estado a tipo servicio
        //        //qTipoServicio.MONTOASIGNACIONDIRECTATIPOSERVICIO = false;

        //        contexto.SaveChanges();
        //    }


        //}

    }
}




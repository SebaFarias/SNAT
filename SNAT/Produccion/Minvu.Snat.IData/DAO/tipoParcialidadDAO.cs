using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minvu.Snat.IData.DAO
{
    class tipoParcialidadDAO
    {
        //public static int Save(TIPO_PARCIALIDAD _tipoParcialidad)
        //{
        //    using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
        //    {
        //        TIPO_PARCIALIDAD _mas = new TIPO_PARCIALIDAD();
        //        try
        //        {
        //            _mas = contexto.TIPO_PARCIALIDAD.Where(c => c.IDTIPOPARCIALIDAD == _tipoParcialidad.IDTIPOPARCIALIDAD).FirstOrDefault<TIPO_PARCIALIDAD>();

        //            if (_mas == null)
        //            {
        //                _mas = _tipoParcialidad;
        //                contexto.TIPO_PARCIALIDAD.Add(_mas);
        //            }
        //            else
        //            {
        //                //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
        //                _mas.ESTADOTIPOPARCIALIDAD = _tipoParcialidad.ESTADOTIPOPARCIALIDAD;
        //                _mas.IDMAESTROPARCIALIDAD = _tipoParcialidad.IDMAESTROPARCIALIDAD;
        //                _mas.IDTIPOPARCIALIDAD = _tipoParcialidad.IDTIPOPARCIALIDAD;
        //                _mas.IDTIPOSERVICIO = _tipoParcialidad.IDTIPOSERVICIO;
        //                _mas.PORCENTAJETIPOPARCIALIDAD = _tipoParcialidad.PORCENTAJETIPOPARCIALIDAD;
                        


        //            }

        //            contexto.SaveChanges();
        //            return (int)_mas.IDTIPOPARCIALIDAD; 
        //        }
        //        catch (Exception Ex)
        //        {
        //            //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
        //            throw Ex;
        //        }
        //    }
        //}



        //public static TIPO_PARCIALIDAD get(int idTipoParcialidad)
        //{

        //    TIPO_PARCIALIDAD _mae = new TIPO_PARCIALIDAD();

        //    using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
        //    {
        //        var qTipoParcialidad = from a in contexto.TIPO_PARCIALIDAD
        //                              where a.IDTIPOPARCIALIDAD == idTipoParcialidad
        //                                  select a;
        //        foreach (var a in qTipoParcialidad)
        //        {

        //            _mae.ESTADOTIPOPARCIALIDAD = a.ESTADOTIPOPARCIALIDAD;
        //            _mae.IDMAESTROPARCIALIDAD = a.IDMAESTROPARCIALIDAD;
        //            _mae.IDTIPOPARCIALIDAD = a.IDTIPOPARCIALIDAD;
        //            _mae.IDTIPOSERVICIO = a.IDTIPOSERVICIO;
        //            _mae.PORCENTAJETIPOPARCIALIDAD = a.PORCENTAJETIPOPARCIALIDAD;


        //        }

        //        return _mae;
        //    }

        //}

        //public static List<TIPO_PARCIALIDAD> getList()
        //{

        //    List<TIPO_PARCIALIDAD> ListaTipoParcialidad = new List<TIPO_PARCIALIDAD>();

        //    using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
        //    {
        //        var qTipoParcialidad = from a in contexto.TIPO_PARCIALIDAD
        //                              select a;
        //        foreach (var a in qTipoParcialidad)
        //        {

        //            TIPO_PARCIALIDAD _mae = new TIPO_PARCIALIDAD();


        //            _mae.ESTADOTIPOPARCIALIDAD = a.ESTADOTIPOPARCIALIDAD;
        //            _mae.IDMAESTROPARCIALIDAD = a.IDMAESTROPARCIALIDAD;
        //            _mae.IDTIPOPARCIALIDAD = a.IDTIPOPARCIALIDAD;
        //            _mae.IDTIPOSERVICIO = a.IDTIPOSERVICIO;
        //            _mae.PORCENTAJETIPOPARCIALIDAD = a.PORCENTAJETIPOPARCIALIDAD;



        //            ListaTipoParcialidad.Add(_mae);


        //        }

        //        return ListaTipoParcialidad;
        //    }

        //}


        //protected void Delete(TIPO_PARCIALIDAD _tipoParcialidad)
        //{

        //    using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
        //    {
        //        TIPO_PARCIALIDAD qTipoParcialidad = (from c in contexto.TIPO_PARCIALIDAD
        //                                              where c.IDTIPOPARCIALIDAD == _tipoParcialidad.IDTIPOPARCIALIDAD
        //                                             select c).FirstOrDefault();

        //        contexto.TIPO_PARCIALIDAD.Remove(qTipoParcialidad);
        //        contexto.SaveChanges();
        //    }

        //}


        //protected void ChangeStatus(TIPO_PARCIALIDAD _tipoParcialidad)
        //{
        //    using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
        //    {
        //        TIPO_PARCIALIDAD qTipoParcialidad = (from c in contexto.TIPO_PARCIALIDAD
        //                                              where c.IDTIPOPARCIALIDAD == _tipoParcialidad.IDTIPOPARCIALIDAD
        //                                             select c).FirstOrDefault();


        //        qTipoParcialidad.ESTADOTIPOPARCIALIDAD = false;

        //        contexto.SaveChanges();
        //    }


        //}

    }
}




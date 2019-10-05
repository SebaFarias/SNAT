using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM; 

namespace Minvu.Snat.IData.DAO
{
  public  class solicitudAutorizacionDAO  
    {
        public static int Save(SOLICITUD_AUTORIZACION _solicitudAutorizacion)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                SOLICITUD_AUTORIZACION _dir = new SOLICITUD_AUTORIZACION();
                try
                {
                    _dir = contexto.SOLICITUD_AUTORIZACION.Where(c => c.IDSOLICITUDAUTORIZACION == _solicitudAutorizacion.IDSOLICITUDAUTORIZACION).FirstOrDefault<SOLICITUD_AUTORIZACION>();

                    if (_dir == null)
                    {
                        _dir = _solicitudAutorizacion;
                        contexto.SOLICITUD_AUTORIZACION.Add(_dir);
                    }
                    else
                    {
                        
                        _dir.NUMEROAUTORIZACIONSOLICITUDAUTORIZACION = _solicitudAutorizacion.NUMEROAUTORIZACIONSOLICITUDAUTORIZACION;
                        _dir.CODIGOPROYECTOSOLICITUDAUTORIZACION = _solicitudAutorizacion.CODIGOPROYECTOSOLICITUDAUTORIZACION;
                        _dir.FTOSOLICITUDAUTORIZACION = _solicitudAutorizacion.FTOSOLICITUDAUTORIZACION;
                        _dir.IDSOLICITUDAUTORIZACION = _solicitudAutorizacion.IDSOLICITUDAUTORIZACION;
                        _dir.MONTOAPAGOSOLICITUDAUTORIZACION = _solicitudAutorizacion.MONTOAPAGOSOLICITUDAUTORIZACION;
                        _dir.MONTOATSOLICITUDAUTORIZACION = _solicitudAutorizacion.MONTOATSOLICITUDAUTORIZACION;

                        _dir.MONTOFTOTOTALSOLICITUDAUTORIZACION = _solicitudAutorizacion.MONTOFTOTOTALSOLICITUDAUTORIZACION;
                        _dir.NOMBREPROYECTOSOLICITUDAUTORIZACION = _solicitudAutorizacion.NOMBREPROYECTOSOLICITUDAUTORIZACION;
                        _dir.NUMEROSOLICITUDSOLICITUDAUTORIZACION = _solicitudAutorizacion.NUMEROSOLICITUDSOLICITUDAUTORIZACION;
                        _dir.S10SOLICITUDAUTORIZACION = _solicitudAutorizacion.S10SOLICITUDAUTORIZACION;
                        _dir.S1SOLICITUDAUTORIZACION = _solicitudAutorizacion.S1SOLICITUDAUTORIZACION;

                        _dir.S2SOLICITUDAUTORIZACION = _solicitudAutorizacion.S2SOLICITUDAUTORIZACION;
                        _dir.S3SOLICITUDAUTORIZACION = _solicitudAutorizacion.S3SOLICITUDAUTORIZACION;
                        _dir.S4SOLICITUDAUTORIZACION = _solicitudAutorizacion.S4SOLICITUDAUTORIZACION;
                        _dir.S5SOLICITUDAUTORIZACION = _solicitudAutorizacion.S5SOLICITUDAUTORIZACION;
                        _dir.S6SOLICITUDAUTORIZACION = _solicitudAutorizacion.S6SOLICITUDAUTORIZACION;

                        _dir.S7SOLICITUDAUTORIZACION = _solicitudAutorizacion.S7SOLICITUDAUTORIZACION;
                        _dir.S8SOLICITUDAUTORIZACION = _solicitudAutorizacion.S8SOLICITUDAUTORIZACION;
                        _dir.S9SOLICITUDAUTORIZACION= _solicitudAutorizacion.S9SOLICITUDAUTORIZACION;
                        _dir.UBICACIONCOMUNASOLICITUDAUTORIZACION = _solicitudAutorizacion.UBICACIONCOMUNASOLICITUDAUTORIZACION;
                    }

                    contexto.SaveChanges();
                    return (int)_dir.IDSOLICITUDAUTORIZACION;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static List<SOLICITUD_AUTORIZACION> GetList(long idAutorizacion)
        {

            List<SOLICITUD_AUTORIZACION> list = new List<SOLICITUD_AUTORIZACION>();
            

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var a = from b in contexto.SOLICITUD_AUTORIZACION
                                  where b.NUMEROAUTORIZACIONSOLICITUDAUTORIZACION == idAutorizacion
                                  select b;
                foreach (var _solicitudAutorizacion in a)
                {
                    SOLICITUD_AUTORIZACION _dir = new SOLICITUD_AUTORIZACION();

                    _dir.NUMEROAUTORIZACIONSOLICITUDAUTORIZACION = _solicitudAutorizacion.NUMEROAUTORIZACIONSOLICITUDAUTORIZACION;
                    _dir.CODIGOPROYECTOSOLICITUDAUTORIZACION = _solicitudAutorizacion.CODIGOPROYECTOSOLICITUDAUTORIZACION;
                    _dir.FTOSOLICITUDAUTORIZACION = _solicitudAutorizacion.FTOSOLICITUDAUTORIZACION;
                    _dir.IDSOLICITUDAUTORIZACION = _solicitudAutorizacion.IDSOLICITUDAUTORIZACION;
                    _dir.MONTOAPAGOSOLICITUDAUTORIZACION = _solicitudAutorizacion.MONTOAPAGOSOLICITUDAUTORIZACION;
                    _dir.MONTOATSOLICITUDAUTORIZACION = _solicitudAutorizacion.MONTOATSOLICITUDAUTORIZACION;

                    _dir.MONTOFTOTOTALSOLICITUDAUTORIZACION = _solicitudAutorizacion.MONTOFTOTOTALSOLICITUDAUTORIZACION;
                    _dir.NOMBREPROYECTOSOLICITUDAUTORIZACION = _solicitudAutorizacion.NOMBREPROYECTOSOLICITUDAUTORIZACION;
                    _dir.NUMEROSOLICITUDSOLICITUDAUTORIZACION = _solicitudAutorizacion.NUMEROSOLICITUDSOLICITUDAUTORIZACION;
                    _dir.S10SOLICITUDAUTORIZACION = _solicitudAutorizacion.S10SOLICITUDAUTORIZACION;
                    _dir.S1SOLICITUDAUTORIZACION = _solicitudAutorizacion.S1SOLICITUDAUTORIZACION;

                    _dir.S2SOLICITUDAUTORIZACION = _solicitudAutorizacion.S2SOLICITUDAUTORIZACION;
                    _dir.S3SOLICITUDAUTORIZACION = _solicitudAutorizacion.S3SOLICITUDAUTORIZACION;
                    _dir.S4SOLICITUDAUTORIZACION = _solicitudAutorizacion.S4SOLICITUDAUTORIZACION;
                    _dir.S5SOLICITUDAUTORIZACION = _solicitudAutorizacion.S5SOLICITUDAUTORIZACION;
                    _dir.S6SOLICITUDAUTORIZACION = _solicitudAutorizacion.S6SOLICITUDAUTORIZACION;

                    _dir.S7SOLICITUDAUTORIZACION = _solicitudAutorizacion.S7SOLICITUDAUTORIZACION;
                    _dir.S8SOLICITUDAUTORIZACION = _solicitudAutorizacion.S8SOLICITUDAUTORIZACION;
                    _dir.S9SOLICITUDAUTORIZACION = _solicitudAutorizacion.S9SOLICITUDAUTORIZACION;
                    _dir.UBICACIONCOMUNASOLICITUDAUTORIZACION = _solicitudAutorizacion.UBICACIONCOMUNASOLICITUDAUTORIZACION;

                    list.Add(_dir);
                }

                return list;
            }

        }
          
        public static void Delete(long numeroAutorizacion)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                List<SOLICITUD_AUTORIZACION> q = (from c in contexto.SOLICITUD_AUTORIZACION
                                        where c.NUMEROAUTORIZACIONSOLICITUDAUTORIZACION == numeroAutorizacion
                                        select c).ToList();

                foreach (SOLICITUD_AUTORIZACION item in q)
                {
                    contexto.SOLICITUD_AUTORIZACION.Remove(item);
                    contexto.SaveChanges();
                }

                
            }

        }

    }
}



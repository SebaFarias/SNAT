using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM; 

namespace Minvu.Snat.IData.DAO
{
  public  class direccionDAO
    {
        public static int Save(DIRECCION _direccion)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                DIRECCION _dir = new DIRECCION();
                try
                {
                    _dir = contexto.DIRECCION.Where(c => c.IDDIRECCION == _direccion.IDDIRECCION).FirstOrDefault<DIRECCION>();

                    if (_dir == null)
                    {
                        _dir = _direccion;
                        contexto.DIRECCION.Add(_dir);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _dir.CODIGOCOMUNADIRECCION = _direccion.CODIGOCOMUNADIRECCION;
                        _dir.CODIGOPROVINCIADIRECCION = _direccion.CODIGOPROVINCIADIRECCION;
                        _dir.CODIGOREGIONDIRECCION = _direccion.CODIGOREGIONDIRECCION;
                        _dir.IDDIRECCION = _direccion.IDDIRECCION;
                        _dir.NUMERODIRECCION = _direccion.NUMERODIRECCION;                        

                    }

                    contexto.SaveChanges();
                    return (int)_dir.IDDIRECCION;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static DIRECCION Get(long idDireccion)
        {

            DIRECCION _dir = new DIRECCION();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qDirecccion = from a in contexto.DIRECCION
                               where a.IDDIRECCION == idDireccion
                               select a;
                foreach (var a in qDirecccion)
                {

                    _dir.IDDIRECCION = a.IDDIRECCION;
                    _dir.CODIGOCOMUNADIRECCION = a.CODIGOCOMUNADIRECCION;
                    _dir.CODIGOPROVINCIADIRECCION = a.CODIGOPROVINCIADIRECCION;
                    _dir.CODIGOREGIONDIRECCION = a.CODIGOREGIONDIRECCION;
                    _dir.NUMERODIRECCION = a.NUMERODIRECCION;
                    
                }

                return _dir;
            }

        }

        public static List<DIRECCION> GetList()
        {

            List<DIRECCION> ListaDireccion = new List<DIRECCION>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qAutorizacion = from a in contexto.DIRECCION
                               select a;
                foreach (var a in qAutorizacion)
                {

                    DIRECCION auxDireccion = new DIRECCION();


                    auxDireccion.IDDIRECCION = a.IDDIRECCION;
                    auxDireccion.CODIGOCOMUNADIRECCION = a.CODIGOCOMUNADIRECCION;
                    auxDireccion.CODIGOPROVINCIADIRECCION = a.CODIGOPROVINCIADIRECCION;
                    auxDireccion.CODIGOREGIONDIRECCION = a.CODIGOREGIONDIRECCION;
                    auxDireccion.NUMERODIRECCION = a.NUMERODIRECCION;
;

                    ListaDireccion.Add(auxDireccion);


                }

                return ListaDireccion;
            }

        }


        protected void Delete(DIRECCION _direccion)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                DIRECCION qDireccion = (from c in contexto.DIRECCION
                                    where c.IDDIRECCION == _direccion.IDDIRECCION
                                        select c).FirstOrDefault();

                contexto.DIRECCION.Remove(qDireccion);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(DIRECCION _direccion)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                DIRECCION qDireccion = (from c in contexto.DIRECCION
                                    where c.IDDIRECCION == _direccion.IDDIRECCION
                                           select c).FirstOrDefault();


                qDireccion.IDDIRECCION = 0;

                contexto.SaveChanges();
            }


        }

    }
}



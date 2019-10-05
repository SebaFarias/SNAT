using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minvu.Snat.IData.DAO
{
    public class maestroTipoProveedorDAO
    {
        public static int Save(MAESTRO_TIPO_PROVEEDOR _maestroTipoProveedor)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_TIPO_PROVEEDOR _mtp = new MAESTRO_TIPO_PROVEEDOR();
                try
                {
                    _mtp = contexto.MAESTRO_TIPO_PROVEEDOR.Where(c => c.IDMAESTROTIPOPROVEEDOR == _maestroTipoProveedor.IDMAESTROTIPOPROVEEDOR).FirstOrDefault<MAESTRO_TIPO_PROVEEDOR>();

                    if (_mtp == null)
                    {
                        _mtp = _maestroTipoProveedor;
                        contexto.MAESTRO_TIPO_PROVEEDOR.Add(_mtp);
                    }
                    else
                    {
                        _mtp.ESTADOMAESTROTIPOPROVEEDOR = _maestroTipoProveedor.ESTADOMAESTROTIPOPROVEEDOR;
                        _mtp.IDMAESTROTIPOPROVEEDOR = _maestroTipoProveedor.IDMAESTROTIPOPROVEEDOR;
                        _mtp.NOMBREMAESTROTIPOPROVEEDOR = _maestroTipoProveedor.NOMBREMAESTROTIPOPROVEEDOR;
                        _mtp.IDMAESTROTIPOPROVEEDORSISTEMA = _maestroTipoProveedor.IDMAESTROTIPOPROVEEDORSISTEMA;
                    }

                    contexto.SaveChanges();
                    return (int)_mtp.IDMAESTROTIPOPROVEEDOR;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        }

        public static MAESTRO_TIPO_PROVEEDOR Get(long idMaestroTipoProveedor)
        {
            MAESTRO_TIPO_PROVEEDOR _mae = new MAESTRO_TIPO_PROVEEDOR();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroTipoProveedor = from a in contexto.MAESTRO_TIPO_PROVEEDOR
                                            where a.IDMAESTROTIPOPROVEEDOR == idMaestroTipoProveedor
                                            select a;

                foreach (var a in qMaestroTipoProveedor)
                {
                    _mae.ESTADOMAESTROTIPOPROVEEDOR = a.ESTADOMAESTROTIPOPROVEEDOR;
                    _mae.IDMAESTROTIPOPROVEEDOR = a.IDMAESTROTIPOPROVEEDOR;
                    _mae.NOMBREMAESTROTIPOPROVEEDOR = a.NOMBREMAESTROTIPOPROVEEDOR;
                }

                return _mae;
            }
        }

        public static List<MAESTRO_TIPO_PROVEEDOR> GetList()
        {
            List<MAESTRO_TIPO_PROVEEDOR> ListaMaestroTipoProveedor = new List<MAESTRO_TIPO_PROVEEDOR>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroServicio = from a in contexto.MAESTRO_TIPO_PROVEEDOR
                                       where a.ESTADOMAESTROTIPOPROVEEDOR == true
                                       orderby a.NOMBREMAESTROTIPOPROVEEDOR
                                       select a;
                foreach (var a in qMaestroServicio)
                {
                    MAESTRO_TIPO_PROVEEDOR _mtp = new MAESTRO_TIPO_PROVEEDOR();

                    _mtp.ESTADOMAESTROTIPOPROVEEDOR = a.ESTADOMAESTROTIPOPROVEEDOR;
                    _mtp.IDMAESTROTIPOPROVEEDOR = a.IDMAESTROTIPOPROVEEDOR;
                    _mtp.NOMBREMAESTROTIPOPROVEEDOR = a.NOMBREMAESTROTIPOPROVEEDOR;
                    _mtp.IDMAESTROTIPOPROVEEDORSISTEMA = a.IDMAESTROTIPOPROVEEDORSISTEMA;

                    ListaMaestroTipoProveedor.Add(_mtp);
                }

                return ListaMaestroTipoProveedor;
            }
        }

        protected void Delete(MAESTRO_TIPO_PROVEEDOR _maestroTipoProveedor)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_TIPO_PROVEEDOR qMaestroTipoProveedor = (from c in contexto.MAESTRO_TIPO_PROVEEDOR
                                                                where c.IDMAESTROTIPOPROVEEDOR == _maestroTipoProveedor.IDMAESTROTIPOPROVEEDOR
                                                                select c).FirstOrDefault();

                contexto.MAESTRO_TIPO_PROVEEDOR.Remove(qMaestroTipoProveedor);
                contexto.SaveChanges();
            }
        }

        protected void ChangeStatus(MAESTRO_TIPO_PROVEEDOR _maestroTipoProveedor)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_TIPO_PROVEEDOR qMaestroServicio = (from c in contexto.MAESTRO_TIPO_PROVEEDOR
                                                           where c.IDMAESTROTIPOPROVEEDOR == _maestroTipoProveedor.IDMAESTROTIPOPROVEEDOR
                                                           select c).FirstOrDefault();

                qMaestroServicio.ESTADOMAESTROTIPOPROVEEDOR = false;

                contexto.SaveChanges();
            }
        }
    }
}
using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minvu.Snat.IData.DAO
{
    public class proveedorDAO
    {
        public static int Save(PROVEEDOR _proveedor)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                PROVEEDOR _mas = new PROVEEDOR();
                try
                {
                    _mas = contexto.PROVEEDOR.Where(c => c.RUTPROVEEDOR == _proveedor.RUTPROVEEDOR && c.IDMAESTROTIPOPROVEEDOR == _proveedor.IDMAESTROTIPOPROVEEDOR).FirstOrDefault<PROVEEDOR>();

                    if (_mas == null)
                    {
                        _mas = _proveedor;
                        contexto.PROVEEDOR.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.DVPROVEDIGITOVERIFICADORPROVEEDOR = _proveedor.DVPROVEDIGITOVERIFICADORPROVEEDOR;
                        _mas.IDMAESTROTIPOPROVEEDOR = _proveedor.IDMAESTROTIPOPROVEEDOR;
                        _mas.IDPROVEEDOR = _proveedor.IDPROVEEDOR;
                        _mas.NOMBREPROVEEDOR = _proveedor.NOMBREPROVEEDOR;
                        _mas.RUTPROVEEDOR = _proveedor.RUTPROVEEDOR;
                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDPROVEEDOR;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }

        public static PROVEEDOR GetProveedorRutProveedorIdTipo(int rutProveedor, long idTipoProveedor)
        {
            PROVEEDOR objProveedor = new PROVEEDOR();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qProveedor = from a in contexto.PROVEEDOR

                                 where a.IDMAESTROTIPOPROVEEDOR == idTipoProveedor
                                 && a.RUTPROVEEDOR == rutProveedor

                                 select a;
                foreach (var a in qProveedor)
                {
                    objProveedor.DVPROVEDIGITOVERIFICADORPROVEEDOR = a.DVPROVEDIGITOVERIFICADORPROVEEDOR;
                    objProveedor.IDMAESTROTIPOPROVEEDOR = a.IDMAESTROTIPOPROVEEDOR;
                    objProveedor.IDPROVEEDOR = a.IDPROVEEDOR;
                    objProveedor.NOMBREPROVEEDOR = a.NOMBREPROVEEDOR;
                    objProveedor.RUTPROVEEDOR = a.RUTPROVEEDOR;
                }

                return objProveedor;
            }
        }



        public static PROVEEDOR GetProveedorRut(int rutProveedor)
        {
            PROVEEDOR objProveedor = new PROVEEDOR();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qProveedor = from a in contexto.PROVEEDOR

                                 where a.RUTPROVEEDOR == rutProveedor

                                 select a;
                foreach (var a in qProveedor)
                {
                    objProveedor.DVPROVEDIGITOVERIFICADORPROVEEDOR = a.DVPROVEDIGITOVERIFICADORPROVEEDOR;
                    objProveedor.IDMAESTROTIPOPROVEEDOR = a.IDMAESTROTIPOPROVEEDOR;
                    objProveedor.IDPROVEEDOR = a.IDPROVEEDOR;
                    objProveedor.NOMBREPROVEEDOR = a.NOMBREPROVEEDOR;
                    objProveedor.RUTPROVEEDOR = a.RUTPROVEEDOR;
                }

                return objProveedor;
            }
        }


        public static PROVEEDOR GetProveedorIdProyectoIdTipo(long idInfoProyecto, long idTipoProveedor)
        {
            PROVEEDOR objProveedor = new PROVEEDOR();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qProveedor = from a in contexto.PROVEEDOR
                                 join b in contexto.TIPO_PROVEEDOR_INFORMACION_PROYECTO
                                 on a.IDPROVEEDOR equals b.IDPROVEEDOR
                                 where a.IDMAESTROTIPOPROVEEDOR == idTipoProveedor
                                 && b.IDINFORMACIONPROYECTO == idInfoProyecto
                                 && b.ESTADOTIPOPROVEEDORINFORMACIONPROYECTO == true
                                 select a;
                foreach (var a in qProveedor)
                {
                    objProveedor.DVPROVEDIGITOVERIFICADORPROVEEDOR = a.DVPROVEDIGITOVERIFICADORPROVEEDOR;
                    objProveedor.IDMAESTROTIPOPROVEEDOR = a.IDMAESTROTIPOPROVEEDOR;
                    objProveedor.IDPROVEEDOR = a.IDPROVEEDOR;
                    objProveedor.NOMBREPROVEEDOR = a.NOMBREPROVEEDOR;
                    objProveedor.RUTPROVEEDOR = a.RUTPROVEEDOR;
                }

                return objProveedor;
            }
        }
        public static PROVEEDOR Get(long idProveedor, long IDTIPOPROVEEDORINFORMACIONPROYECTO)
        {
            PROVEEDOR _mae = new PROVEEDOR();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qProveedor = from a in contexto.PROVEEDOR 
                                 join b in contexto.TIPO_PROVEEDOR_INFORMACION_PROYECTO
                                 on a.IDPROVEEDOR equals b.IDPROVEEDOR
                                 where a.IDPROVEEDOR == idProveedor && b.IDTIPOPROVEEDORINFORMACIONPROYECTO == IDTIPOPROVEEDORINFORMACIONPROYECTO
                                 select a;
                foreach (var a in qProveedor)
                {
                    _mae.DVPROVEDIGITOVERIFICADORPROVEEDOR = a.DVPROVEDIGITOVERIFICADORPROVEEDOR;
                    _mae.IDMAESTROTIPOPROVEEDOR = a.IDMAESTROTIPOPROVEEDOR;
                    _mae.IDPROVEEDOR = a.IDPROVEEDOR;
                    _mae.NOMBREPROVEEDOR = a.NOMBREPROVEEDOR;
                    _mae.RUTPROVEEDOR = a.RUTPROVEEDOR;
                }

                return _mae;
            }
        }
        public static PROVEEDOR Get(long idProveedor)
        {
            PROVEEDOR _mae = new PROVEEDOR();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qProveedor = from a in contexto.PROVEEDOR
                                 where a.IDPROVEEDOR == idProveedor
                                 select a;
                foreach (var a in qProveedor)
                {
                    _mae.DVPROVEDIGITOVERIFICADORPROVEEDOR = a.DVPROVEDIGITOVERIFICADORPROVEEDOR;
                    _mae.IDMAESTROTIPOPROVEEDOR = a.IDMAESTROTIPOPROVEEDOR;
                    _mae.IDPROVEEDOR = a.IDPROVEEDOR;
                    _mae.NOMBREPROVEEDOR = a.NOMBREPROVEEDOR;
                    _mae.RUTPROVEEDOR = a.RUTPROVEEDOR;
                }

                return _mae;
            }
        }

        public static PROVEEDOR ProveedorxNombre(string NomProveedor)
        {
            PROVEEDOR _mae = new PROVEEDOR();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qProveedor = from a in contexto.PROVEEDOR
                                 where a.NOMBREPROVEEDOR.ToUpper().Trim() == NomProveedor.ToUpper().Trim()
                                 select a;

                foreach (var a in qProveedor)
                {
                    _mae.DVPROVEDIGITOVERIFICADORPROVEEDOR = a.DVPROVEDIGITOVERIFICADORPROVEEDOR;
                    _mae.IDMAESTROTIPOPROVEEDOR = a.IDMAESTROTIPOPROVEEDOR;
                    _mae.IDPROVEEDOR = a.IDPROVEEDOR;
                    _mae.NOMBREPROVEEDOR = a.NOMBREPROVEEDOR;
                    _mae.RUTPROVEEDOR = a.RUTPROVEEDOR;
                }

                return _mae;
            }
        }

        public static List<PROVEEDOR> GetList()
        {
            List<PROVEEDOR> ListaProveedor = new List<PROVEEDOR>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qProveedor = from a in contexto.PROVEEDOR
                                 select a;
                foreach (var a in qProveedor)
                {
                    PROVEEDOR _mae = new PROVEEDOR();

                    _mae.DVPROVEDIGITOVERIFICADORPROVEEDOR = a.DVPROVEDIGITOVERIFICADORPROVEEDOR;
                    _mae.IDMAESTROTIPOPROVEEDOR = a.IDMAESTROTIPOPROVEEDOR;
                    _mae.IDPROVEEDOR = a.IDPROVEEDOR;
                    _mae.NOMBREPROVEEDOR = a.NOMBREPROVEEDOR;
                    _mae.RUTPROVEEDOR = a.RUTPROVEEDOR;

                    ListaProveedor.Add(_mae);
                }

                return ListaProveedor;
            }
        }

        protected void Delete(PROVEEDOR _maestroTitulo)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                PROVEEDOR qProveedor = (from c in contexto.PROVEEDOR
                                        where c.IDPROVEEDOR == _maestroTitulo.IDPROVEEDOR
                                        select c).FirstOrDefault();

                contexto.PROVEEDOR.Remove(qProveedor);
                contexto.SaveChanges();
            }
        }

        protected void ChangeStatus(PROVEEDOR _maestroTitulo)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                PROVEEDOR qProveedor = (from c in contexto.PROVEEDOR
                                        where c.IDPROVEEDOR == _maestroTitulo.IDPROVEEDOR
                                        select c).FirstOrDefault();
                //añadir estado proovedor
                // qProveedor.RUTPROVEEDOR = false;

                contexto.SaveChanges();
            }
        }

        public static PROVEEDOR GetProveedorByRut(int rutProveedor)
        {

            PROVEEDOR _mae = new PROVEEDOR();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qProveedor = (from a in contexto.PROVEEDOR
                                  where a.RUTPROVEEDOR == rutProveedor
                                  select a).FirstOrDefault();

                if (qProveedor != null)
                {
                    _mae.DVPROVEDIGITOVERIFICADORPROVEEDOR = qProveedor.DVPROVEDIGITOVERIFICADORPROVEEDOR;
                    _mae.IDMAESTROTIPOPROVEEDOR = qProveedor.IDMAESTROTIPOPROVEEDOR;
                    _mae.IDPROVEEDOR = qProveedor.IDPROVEEDOR;
                    _mae.NOMBREPROVEEDOR = qProveedor.NOMBREPROVEEDOR;
                    _mae.RUTPROVEEDOR = qProveedor.RUTPROVEEDOR;
                }


                return _mae;
            }

        }
    }
}



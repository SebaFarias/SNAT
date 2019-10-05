using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.Domain.Entities
{
    public class proveedorEntities
    {
        public long idProveedor { get; set; }
        public long idMaestroTipoProveedor { get; set; }

        public string rutCompleto { get; set; }

        [Display(Name = "Proveedor:")]
        [Required(ErrorMessage = "El Nombre del proveedor es obligatorio")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string nombreProveedor { get; set; }

        [Display(Name = "Rut:")]
        [Required(ErrorMessage = "El rut del proveedor es obligatorio")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public int rutProveedor { get; set; }

        [Display(Name = "DV proveedor:")]
        [Required(ErrorMessage = "El DV del proveedor es obligatorio")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public char dvDigitoprovedor { get; set; }
        public proveedorEntities()
        {
            idProveedor = 0;
            idMaestroTipoProveedor = 0;
            nombreProveedor = string.Empty;
            rutProveedor = 0;
            dvDigitoprovedor = '0';
        }
        public proveedorEntities(long _idProveedor ,long _idMaestroTipoProveedor ,string _nombreProveedor,int _rutProveedor,char _dvDigitoprovedor)
        {
            idProveedor = idProveedor;
            idMaestroTipoProveedor = _idMaestroTipoProveedor;
            nombreProveedor = _nombreProveedor;
            rutProveedor = _rutProveedor;
            dvDigitoprovedor = _dvDigitoprovedor;
        }
    }

    public class proveedorEntitiesFactory
    {
        internal static proveedorEntities getProveedorIdProyectoIdTipo(long idInfoProyecto, long idTipoProveedor)
        {
            var _proveedorDAO = proveedorDAO.GetProveedorIdProyectoIdTipo(idInfoProyecto, idTipoProveedor);

            if (_proveedorDAO != null)
            {
                if (_proveedorDAO.IDPROVEEDOR != 0)
                {
                    return new proveedorEntities
                    {
                        idProveedor = _proveedorDAO.IDPROVEEDOR,
                        idMaestroTipoProveedor = Convert.ToInt64(_proveedorDAO.IDMAESTROTIPOPROVEEDOR),
                        nombreProveedor = _proveedorDAO.NOMBREPROVEEDOR,
                        rutProveedor = Convert.ToInt32(_proveedorDAO.RUTPROVEEDOR),
                        dvDigitoprovedor = Convert.ToChar(_proveedorDAO.DVPROVEDIGITOVERIFICADORPROVEEDOR)
                    };
                }
                else
                {
                    return new proveedorEntities
                    {
                        idProveedor = 0
                    };
                }
            }
            else
            {
                return null;
            }
        }

        internal static proveedorEntities getProveedorRutProveedoroIdTipo(int rutProveedor, long idTipoProveedor)
        {
            var _proveedorDAO = proveedorDAO.GetProveedorRutProveedorIdTipo(rutProveedor, idTipoProveedor);

            if (_proveedorDAO != null)
            {
                if (_proveedorDAO.IDPROVEEDOR != 0)
                {
                    return new proveedorEntities
                    {
                        idProveedor = _proveedorDAO.IDPROVEEDOR,
                        idMaestroTipoProveedor = Convert.ToInt64(_proveedorDAO.IDMAESTROTIPOPROVEEDOR),
                        nombreProveedor = _proveedorDAO.NOMBREPROVEEDOR,
                        rutProveedor = Convert.ToInt32(_proveedorDAO.RUTPROVEEDOR),
                        dvDigitoprovedor = Convert.ToChar(_proveedorDAO.DVPROVEDIGITOVERIFICADORPROVEEDOR)
                    };
                }
                else
                {
                    return new proveedorEntities
                    {
                        idProveedor = 0
                    };
                }
            }
            else
            {
                return null;
            }
        }

        internal static proveedorEntities getProveedorRut(int rutProveedor)
        {
            var _proveedorDAO = proveedorDAO.GetProveedorRut(rutProveedor);

            if (_proveedorDAO != null)
            {
                if (_proveedorDAO.IDPROVEEDOR != 0)
                {
                    return new proveedorEntities
                    {
                        idProveedor = _proveedorDAO.IDPROVEEDOR,
                        idMaestroTipoProveedor = Convert.ToInt64(_proveedorDAO.IDMAESTROTIPOPROVEEDOR),
                        nombreProveedor = _proveedorDAO.NOMBREPROVEEDOR,
                        rutProveedor = Convert.ToInt32(_proveedorDAO.RUTPROVEEDOR),
                        dvDigitoprovedor = Convert.ToChar(_proveedorDAO.DVPROVEDIGITOVERIFICADORPROVEEDOR)
                    };
                }
                else
                {
                    return new proveedorEntities
                    {
                        idProveedor = 0
                    };
                }
            }
            else
            {
                return null;
            }
        }

        public static int saveProveedor(int rutProveedor, char dvProveedor, string nombreProveedor, long idtipoProveedor)
        {
            PROVEEDOR _proveedor = new PROVEEDOR();
            _proveedor.RUTPROVEEDOR = rutProveedor;
            _proveedor.DVPROVEDIGITOVERIFICADORPROVEEDOR = dvProveedor.ToString();
            _proveedor.NOMBREPROVEEDOR = nombreProveedor;
            _proveedor.IDMAESTROTIPOPROVEEDOR = idtipoProveedor;

            return proveedorDAO.Save(_proveedor);
        }

        public static proveedorEntities getProveedor(long idProveedor,long idTIpoProveedorInformacionProyecto)
        {
            var _proveedorDAO = proveedorDAO.Get(idProveedor, idTIpoProveedorInformacionProyecto);
            if (_proveedorDAO != null)
            {
                return new proveedorEntities
                {
                    idProveedor = _proveedorDAO.IDPROVEEDOR,//Convert.ToInt64(_proveedorDAO.IDPROVEEDOR),
                    idMaestroTipoProveedor = Convert.ToInt64(_proveedorDAO.IDMAESTROTIPOPROVEEDOR),// Convert.ToInt64(_proveedorDAO.IDMAESTROTIPOPROVEEDOR),
                    nombreProveedor = _proveedorDAO.NOMBREPROVEEDOR,
                    rutProveedor = Convert.ToInt32(_proveedorDAO.RUTPROVEEDOR),
                    dvDigitoprovedor = Convert.ToChar(_proveedorDAO.DVPROVEDIGITOVERIFICADORPROVEEDOR)
                };
            }
            else
                return null;
        }
        public static proveedorEntities getProveedor(long idProveedor)
         {
            var _proveedorDAO = proveedorDAO.Get(idProveedor);
            if (_proveedorDAO != null)
            {
                return new proveedorEntities
                {
                    idProveedor = _proveedorDAO.IDPROVEEDOR,//Convert.ToInt64(_proveedorDAO.IDPROVEEDOR),
                    idMaestroTipoProveedor = Convert.ToInt64(_proveedorDAO.IDMAESTROTIPOPROVEEDOR),// Convert.ToInt64(_proveedorDAO.IDMAESTROTIPOPROVEEDOR),
                    nombreProveedor = _proveedorDAO.NOMBREPROVEEDOR,
                    rutProveedor = Convert.ToInt32(_proveedorDAO.RUTPROVEEDOR),
                    dvDigitoprovedor = Convert.ToChar(_proveedorDAO.DVPROVEDIGITOVERIFICADORPROVEEDOR)
                };
            }
            else
                return null;
        }

        public static proveedorEntities getProveedorxNombre(string NomProveedor)
        {
            var _proveedorDAO = proveedorDAO.ProveedorxNombre(NomProveedor);
            if (_proveedorDAO != null)
            {
                return new proveedorEntities
                {
                    idProveedor = _proveedorDAO.IDPROVEEDOR,
                    idMaestroTipoProveedor = Convert.ToInt64(_proveedorDAO.IDMAESTROTIPOPROVEEDOR),
                    nombreProveedor = _proveedorDAO.NOMBREPROVEEDOR,
                    rutProveedor = Convert.ToInt32(_proveedorDAO.RUTPROVEEDOR),
                    dvDigitoprovedor = Convert.ToChar(_proveedorDAO.DVPROVEDIGITOVERIFICADORPROVEEDOR)

                };
            }
            else
                return null;
        }
    }
}


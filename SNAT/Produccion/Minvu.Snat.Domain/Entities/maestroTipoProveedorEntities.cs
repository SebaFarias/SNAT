using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;

namespace Minvu.Snat.Domain.Entities
{
    public class maestroTipoProveedorEntities
    {
        public long idMaestroTipoProveedor { get; set; }


        [Display(Name = "Tipo proveedor:")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string nombreMaestroTipoProveedor { get; set; }
        public bool estadoMaestroTipoProveedor { get; set; }
        public long? idMaestroTipoProveedorSistema { get; set; }

        public maestroTipoProveedorEntities()
        {
            idMaestroTipoProveedor = 0;
            nombreMaestroTipoProveedor = string.Empty;
            estadoMaestroTipoProveedor = false;
            idMaestroTipoProveedorSistema = null;
        }

        public maestroTipoProveedorEntities(long _idMaestroTipoProveedor, string _nombreMaestroTipoProveedor, bool _estadoMaestroTipoProveedor, long? _idMaestroTipoProveedorSistema)
        {
            idMaestroTipoProveedor = _idMaestroTipoProveedor;
            nombreMaestroTipoProveedor = _nombreMaestroTipoProveedor;
            estadoMaestroTipoProveedor = _estadoMaestroTipoProveedor;
            idMaestroTipoProveedorSistema = _idMaestroTipoProveedorSistema;
        }
    }

    public class maestroTipoProveedorEntitiesFactory
    {
        internal static maestroTipoProveedorEntities getTipoProveedor(long idTipoProveedor)
        {
            var _maestroTipoProveedorDAO = maestroTipoProveedorDAO.Get(idTipoProveedor);
            if (_maestroTipoProveedorDAO != null)
            {
                return new maestroTipoProveedorEntities
                {
                    idMaestroTipoProveedor = Convert.ToInt64(_maestroTipoProveedorDAO.IDMAESTROTIPOPROVEEDOR),
                    nombreMaestroTipoProveedor = _maestroTipoProveedorDAO.NOMBREMAESTROTIPOPROVEEDOR,
                    estadoMaestroTipoProveedor = Convert.ToBoolean(_maestroTipoProveedorDAO.ESTADOMAESTROTIPOPROVEEDOR),
                    idMaestroTipoProveedorSistema= (Int64?)(_maestroTipoProveedorDAO.IDMAESTROTIPOPROVEEDORSISTEMA)
                };
            }
            else
                return null;
        }

        internal static List<maestroTipoProveedorEntities> getListTipoProveedor()
        {
            List<maestroTipoProveedorEntities> principal = new List<maestroTipoProveedorEntities>();
            var _maestroTipoProveedorDAO = maestroTipoProveedorDAO.GetList();

            if (_maestroTipoProveedorDAO != null)
            {
                foreach (var item in _maestroTipoProveedorDAO)
                {
                    maestroTipoProveedorEntities aux = new maestroTipoProveedorEntities();

                    aux.idMaestroTipoProveedor = Convert.ToInt64(item.IDMAESTROTIPOPROVEEDOR);
                    aux.nombreMaestroTipoProveedor = item.NOMBREMAESTROTIPOPROVEEDOR;
                    aux.estadoMaestroTipoProveedor = Convert.ToBoolean(item.ESTADOMAESTROTIPOPROVEEDOR);
                    aux.idMaestroTipoProveedorSistema = (Int64?)(item.IDMAESTROTIPOPROVEEDORSISTEMA);

                    principal.Add(aux);
                }

                return principal;
            }

            else
                return null;
        }
    }
}

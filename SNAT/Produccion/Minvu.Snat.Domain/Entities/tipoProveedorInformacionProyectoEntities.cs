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
    public class tipoProveedorInformacionProyectoEntities
    {
        public long? idTipoProveedorInformacionProyecto { get; set; }
        public long? idProveedor { get; set; }
        public long? idInformacionProyecto { get; set; }
        public bool? estadoTipoProveedorInformacionProyecto  { get; set; }

        public tipoProveedorInformacionProyectoEntities()
        {
            idTipoProveedorInformacionProyecto = null;
               idProveedor = null;
            idInformacionProyecto = null;
            estadoTipoProveedorInformacionProyecto = null;
        }
        public tipoProveedorInformacionProyectoEntities(long? _idTipoProveedorInformacionProyecto, long? _idProveedor, long? _idInformacionProyecto, bool? _estadoTipoProveedorInformacionProyecto)
        {
            idTipoProveedorInformacionProyecto = _idTipoProveedorInformacionProyecto;
            idProveedor = _idProveedor;
            idInformacionProyecto = _idInformacionProyecto;
            estadoTipoProveedorInformacionProyecto = _estadoTipoProveedorInformacionProyecto;
        }
    }

    public class tipoProveedorInformacionProyectoEntitiesFactory
    {

        internal static tipoProveedorInformacionProyectoEntities getTipoProveedorInformacionProyecto(long? idInformacionProyecto)
        {
            var _tipoProveedorInformacionProyecto = tipoProveedorInformacionProyectoDAO.Get(Convert.ToInt64(idInformacionProyecto));
            if (_tipoProveedorInformacionProyecto != null)
            {
                return new tipoProveedorInformacionProyectoEntities
                {

                idTipoProveedorInformacionProyecto = _tipoProveedorInformacionProyecto.IDTIPOPROVEEDORINFORMACIONPROYECTO,
                idProveedor = _tipoProveedorInformacionProyecto.IDPROVEEDOR,
                idInformacionProyecto = _tipoProveedorInformacionProyecto.IDINFORMACIONPROYECTO,
                estadoTipoProveedorInformacionProyecto = _tipoProveedorInformacionProyecto.ESTADOTIPOPROVEEDORINFORMACIONPROYECTO,

       
                };
            }
            else
                return null;


        }


        internal static bool saveTipoProveedorInformacionProyecto(tipoProveedorInformacionProyectoEntities _tipoProveedorInformacionProyectoEntities)
        {

            TIPO_PROVEEDOR_INFORMACION_PROYECTO _TIPO_PROVEEDOR_INFORMACION_PROYECTO = new TIPO_PROVEEDOR_INFORMACION_PROYECTO();

            _TIPO_PROVEEDOR_INFORMACION_PROYECTO.ESTADOTIPOPROVEEDORINFORMACIONPROYECTO = _tipoProveedorInformacionProyectoEntities.estadoTipoProveedorInformacionProyecto;
            _TIPO_PROVEEDOR_INFORMACION_PROYECTO.IDINFORMACIONPROYECTO = Convert.ToInt64(_tipoProveedorInformacionProyectoEntities.idInformacionProyecto);
            _TIPO_PROVEEDOR_INFORMACION_PROYECTO.IDPROVEEDOR = Convert.ToInt64(_tipoProveedorInformacionProyectoEntities.idProveedor);
            _TIPO_PROVEEDOR_INFORMACION_PROYECTO.IDTIPOPROVEEDORINFORMACIONPROYECTO = Convert.ToInt64(_tipoProveedorInformacionProyectoEntities.idTipoProveedorInformacionProyecto);
            


            tipoProveedorInformacionProyectoDAO.Save(_TIPO_PROVEEDOR_INFORMACION_PROYECTO);

            return true;
        }

        internal static List<tipoProveedorInformacionProyectoEntities> getListTipoProveedorInformacionProyecto(long? idInformacionProyecto)
        {
            List<tipoProveedorInformacionProyectoEntities> principal = new List<tipoProveedorInformacionProyectoEntities>();
            var _tipoProveedorInformacionProyecto = tipoProveedorInformacionProyectoDAO.GetList(Convert.ToInt64(idInformacionProyecto));
            if (_tipoProveedorInformacionProyecto != null)
            {
                foreach (var item in _tipoProveedorInformacionProyecto)
                {
                    tipoProveedorInformacionProyectoEntities _auxTipoProveedorInformacionProyectoEntities = new tipoProveedorInformacionProyectoEntities();

                     _auxTipoProveedorInformacionProyectoEntities.idTipoProveedorInformacionProyecto = item.IDTIPOPROVEEDORINFORMACIONPROYECTO;
                    _auxTipoProveedorInformacionProyectoEntities.idProveedor = item.IDPROVEEDOR;
                    _auxTipoProveedorInformacionProyectoEntities.idInformacionProyecto = item.IDINFORMACIONPROYECTO;
                    _auxTipoProveedorInformacionProyectoEntities.estadoTipoProveedorInformacionProyecto = item.ESTADOTIPOPROVEEDORINFORMACIONPROYECTO;
                    principal.Add(_auxTipoProveedorInformacionProyectoEntities);
                }

                return principal;
            }
            else
                return null;


        }





    }
}


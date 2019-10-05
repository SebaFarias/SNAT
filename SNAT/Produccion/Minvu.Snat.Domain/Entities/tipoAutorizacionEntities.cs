using Minvu.Snat.IData.DAO;
using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class tipoAutorizacionEntities
    {
        public int idTipoAutorizacion { get; set; }
        public int idSolicitudPago { get; set; }
        public int idAutorizacion { get; set; }
        public bool estadoTipoAutorizacion { get; set; }

        public   tipoAutorizacionEntities()
        {
            idTipoAutorizacion = 0;
            idSolicitudPago = 0;
            idAutorizacion = 0;
            estadoTipoAutorizacion = false;

    }
    public   tipoAutorizacionEntities(int _idTipoAutorizacion, int _idSolicitudPago, int _idAutorizacion, bool _estadoTipoAutorizacion)
        {
            idTipoAutorizacion = _idTipoAutorizacion;
            idSolicitudPago = _idSolicitudPago;
            idAutorizacion = _idAutorizacion;
            estadoTipoAutorizacion = _estadoTipoAutorizacion;
        }
    }


    public class tipoAutorizacionEntitiesFactory
    {
        public static List<tipoAutorizacionEntities> getListTipoAutorizacion(long idAutorizacion)
        {

            List<TIPO_AUTORIZACION> ListaTipoAutorizacion = tipoAutorizacionDAO.GetList(idAutorizacion);

            List<tipoAutorizacionEntities> _listTipoAutorizacion = new List<tipoAutorizacionEntities>();

            foreach (var item in ListaTipoAutorizacion)
            {
                tipoAutorizacionEntities _aux = new tipoAutorizacionEntities();
                _aux.idAutorizacion = Convert.ToInt32(item.IDAUTORIZACION);
                _aux.idSolicitudPago = Convert.ToInt32(item.IDSOLICITUDPAGO);
                _aux.idTipoAutorizacion = Convert.ToInt32(item.IDTIPOAUTORIZACION);
                _aux.estadoTipoAutorizacion = Convert.ToBoolean(item.ESTADOTIPOAUTORIZACION);

                _listTipoAutorizacion.Add(_aux);
            }



            return _listTipoAutorizacion;
        }

    }
}

using System;
using System.Collections.Generic;
using Minvu.Snat.IData.DAO;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.Domain.Entities
{
    public class tipoIncrementoEntities
    {
        public long idTipoIncremento { get; set; }
        public long idMaestroIncremento { get; set; }
        public long idSolicitudPago { get; set; }
        public long idCaracteristicasEspeciales { get; set; }
        public bool estadoTipoIncremento { get; set; }
        public bool seleccionadoTipoIncremento { get; set; }

        public tipoIncrementoEntities()
        {
            idTipoIncremento = 0;
            idMaestroIncremento = 0;
            idCaracteristicasEspeciales = 0;
            estadoTipoIncremento = false;
            seleccionadoTipoIncremento = false;
            idSolicitudPago = 0;
        }
        public tipoIncrementoEntities(long _idTipoIncremento,long _idSolicitudPago, long _idMaestroIncremento, long _idTipoServicio, bool _estadoTipoIncremento, bool _seleccionadoTipoIncremento)
        {
            idTipoIncremento = _idTipoIncremento;
            idMaestroIncremento = _idMaestroIncremento;
            idCaracteristicasEspeciales = _idTipoServicio;
            estadoTipoIncremento = _estadoTipoIncremento;
            seleccionadoTipoIncremento = _seleccionadoTipoIncremento;
            idSolicitudPago = _idSolicitudPago;
        }
    }

    public class tipoIncrementoEntitiesFactory

    {

        internal static List<tipoIncrementoEntities> getListaTipoIncremento(long idCaracteristicasEspeciales, long? idSolicitudPago)
        {
            var _tipoIncrementoEntitiesDAO = tipoIncrementoDAO.GetList(idCaracteristicasEspeciales, idSolicitudPago);

            List<tipoIncrementoEntities> _auxListMaestroTipologiaEntities = new List<tipoIncrementoEntities>();


            if (_tipoIncrementoEntitiesDAO != null)
            {
                foreach (var item in _tipoIncrementoEntitiesDAO)
                {
                    tipoIncrementoEntities _tipoIncremento = new tipoIncrementoEntities();

                    _tipoIncremento.idTipoIncremento = Convert.ToInt32(item.IDTIPOINCREMENTO);
                    _tipoIncremento.idMaestroIncremento = Convert.ToInt64(item.IDMAESTROINCREMENTO);
                    _tipoIncremento.idCaracteristicasEspeciales = Convert.ToInt64(item.IDCARACTERISTICASESPECIALES);
                    _tipoIncremento.seleccionadoTipoIncremento = Convert.ToBoolean(item.SELECCIONADOTIPOINCREMENTO);
                    _tipoIncremento.idSolicitudPago = Convert.ToInt64(item.IDSOLICITUDPAGO);
                    _tipoIncremento.estadoTipoIncremento = Convert.ToBoolean(item.ESTADOTIPOINCREMENTO);


                    _auxListMaestroTipologiaEntities.Add(_tipoIncremento);
                }
                return _auxListMaestroTipologiaEntities;
            }
            else
                return null;


        }

        internal static void saveListaTipoIncremento(List<tipoIncrementoEntities> _tipoIncrementoEntities, long idSolicitudPago)
        {

            foreach (var item in _tipoIncrementoEntities)
            {
                TIPO_INCREMENTO _mae = new TIPO_INCREMENTO();

                _mae.IDTIPOINCREMENTO = item.idTipoIncremento;
                _mae.IDMAESTROINCREMENTO = item.idMaestroIncremento;
                _mae.IDCARACTERISTICASESPECIALES = item.idCaracteristicasEspeciales;
                _mae.SELECCIONADOTIPOINCREMENTO = item.seleccionadoTipoIncremento;
                _mae.ESTADOTIPOINCREMENTO = item.estadoTipoIncremento;
                _mae.IDSOLICITUDPAGO = item.idSolicitudPago;
                tipoIncrementoDAO.Save(_mae);

            }
                    

        }
    }
}

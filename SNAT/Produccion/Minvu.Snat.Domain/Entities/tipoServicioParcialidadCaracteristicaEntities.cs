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
    public class tipoServicioParcialidadCaracteristicaEntities
    {

        public long? idTipoServicioParcialidadCaracteristica { get; set; }
        public long? idServicioParcialidad { get; set; }
        public long? idCaracteristicasEspeciales { get; set; }
        public long? idServicioPago { get; set; }
        public decimal? montoServicioTipoServicioParcialidadCaracteristica { get; set; }
        public decimal? montoParcialidadTipoServicioParcialidadCaracteristica { get; set; }
        public bool? estadoTipoServicioParcialidadCaracteristica { get; set; }



        public tipoServicioParcialidadCaracteristicaEntities()
        {
            idTipoServicioParcialidadCaracteristica = null;
            idServicioParcialidad = null;
            idCaracteristicasEspeciales = null;
            montoServicioTipoServicioParcialidadCaracteristica = null;
            estadoTipoServicioParcialidadCaracteristica = null;
            montoParcialidadTipoServicioParcialidadCaracteristica = null;
            idServicioPago = null;
        }

        public tipoServicioParcialidadCaracteristicaEntities(long? _idTipoServicioParcialidadCaracteristica, long? _idServicioParcialidad, long? _idCaracteristicasEspeciales, decimal? _montoServicioTipoServicioParcialidadCaracteristica, decimal? _montoParcialidadTipoServicioParcialidadCaracteristica, bool? _estadoTipoServicioParcialidadCaracteristica, long? _idServicioPago)
        {
            idServicioPago = _idServicioPago;
            idTipoServicioParcialidadCaracteristica = _idTipoServicioParcialidadCaracteristica;
            idServicioParcialidad = _idServicioParcialidad;
            idCaracteristicasEspeciales = _idCaracteristicasEspeciales;
            montoParcialidadTipoServicioParcialidadCaracteristica = _montoParcialidadTipoServicioParcialidadCaracteristica;
            montoServicioTipoServicioParcialidadCaracteristica = _montoServicioTipoServicioParcialidadCaracteristica;
            estadoTipoServicioParcialidadCaracteristica = _estadoTipoServicioParcialidadCaracteristica;
        }
    }

    public class tipoServicioParcialidadCaracteristicaEntitiesFactory
    {
        internal static tipoServicioParcialidadCaracteristicaEntities getTipoServicioParcialidad(long idCaracteristicasEspeciales)
        {
            var tipoServicioParcialidadCaracteristicaEntitiesFactory = tipoServicioParcialidadCaracteristicaDAO.Get(idCaracteristicasEspeciales);
            if (tipoServicioParcialidadCaracteristicaEntitiesFactory != null)
            {
                return new tipoServicioParcialidadCaracteristicaEntities
                {
                    idTipoServicioParcialidadCaracteristica = tipoServicioParcialidadCaracteristicaEntitiesFactory.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA,
                    idServicioParcialidad = tipoServicioParcialidadCaracteristicaEntitiesFactory.IDSERVICIOPARCIALIDAD,
                    idCaracteristicasEspeciales = tipoServicioParcialidadCaracteristicaEntitiesFactory.IDCARACTERISTICASESPECIALES,
                    montoParcialidadTipoServicioParcialidadCaracteristica = tipoServicioParcialidadCaracteristicaEntitiesFactory.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA,
                    montoServicioTipoServicioParcialidadCaracteristica = tipoServicioParcialidadCaracteristicaEntitiesFactory.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA,
                    estadoTipoServicioParcialidadCaracteristica = tipoServicioParcialidadCaracteristicaEntitiesFactory.ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA,
                };
            }
            else
                return null;
        }

        internal static void saveTipoServicioParcialidad(List<tipoServicioParcialidadCaracteristicaEntities> _tipoServicioParcialidadCaracteristicaEntities)
        {
            foreach (var item in _tipoServicioParcialidadCaracteristicaEntities)
            {
                TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA = new TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA();

                _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.IDCARACTERISTICASESPECIALES = item.idCaracteristicasEspeciales;
                _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.IDSERVICIOPARCIALIDAD = item.idServicioParcialidad;
                _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA = Convert.ToInt64(item.idTipoServicioParcialidadCaracteristica);
                _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA = item.montoServicioTipoServicioParcialidadCaracteristica;
                _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA = item.montoServicioTipoServicioParcialidadCaracteristica;
                _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA = item.estadoTipoServicioParcialidadCaracteristica;

                tipoServicioParcialidadCaracteristicaDAO.save(_TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA);
            }
        }

        internal static void actualizarIdSolicitudPago(long _idTipoServicioParcialidadCaracteristicaEntities, long idSolicitudPago)
        {
            tipoServicioParcialidadCaracteristicaDAO.updateIDSolicitudPago(_idTipoServicioParcialidadCaracteristicaEntities, idSolicitudPago);
        }

        internal static List<tipoServicioParcialidadCaracteristicaEntities> getListTipoServicioParcialidad(long idCaracteristicasEspeciales, long? idSolicitudPago)
        {
            List<tipoServicioParcialidadCaracteristicaEntities> listTipoServicioParcialidadCaracteristicaEntities = new List<tipoServicioParcialidadCaracteristicaEntities>();

            var tipoServicioParcialidadCaracteristicaEntitiesFactory = tipoServicioParcialidadCaracteristicaDAO.getList(idCaracteristicasEspeciales, idSolicitudPago);

            foreach (var item in tipoServicioParcialidadCaracteristicaEntitiesFactory)
            {
                tipoServicioParcialidadCaracteristicaEntities _tipoServicioParcialidadCaracteristicaEntities = new tipoServicioParcialidadCaracteristicaEntities();

                _tipoServicioParcialidadCaracteristicaEntities.idTipoServicioParcialidadCaracteristica = item.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                _tipoServicioParcialidadCaracteristicaEntities.idServicioParcialidad = item.IDSERVICIOPARCIALIDAD;
                _tipoServicioParcialidadCaracteristicaEntities.idCaracteristicasEspeciales = item.IDCARACTERISTICASESPECIALES;
                _tipoServicioParcialidadCaracteristicaEntities.idServicioPago = item.IDSOLICITUDPAGO;
                _tipoServicioParcialidadCaracteristicaEntities.montoParcialidadTipoServicioParcialidadCaracteristica = item.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                _tipoServicioParcialidadCaracteristicaEntities.montoServicioTipoServicioParcialidadCaracteristica = item.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                _tipoServicioParcialidadCaracteristicaEntities.estadoTipoServicioParcialidadCaracteristica = item.ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA;

                listTipoServicioParcialidadCaracteristicaEntities.Add(_tipoServicioParcialidadCaracteristicaEntities);
            }

            return listTipoServicioParcialidadCaracteristicaEntities;
        }

        internal static void ReIniciaPlantillaNew(long? idCaracEspecial)
        {
            var objListPlantillas = tipoServicioParcialidadCaracteristicaDAO.getUltimaPlantillaNull(idCaracEspecial);

            if (objListPlantillas.Count > 0)
            {
                foreach (var item in objListPlantillas)
                {

                    if (item.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA == 0)
                    {
                        item.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA = null;
                    }

                    //Se setean los nuevos valores nulos para la plantilla(nueva) en BD
                    tipoServicioParcialidadCaracteristicaDAO.Update(item);
                }
            }
        }
    }
}

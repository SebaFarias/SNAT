using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.DAO;
using Minvu.Snat.IData.ORM;
using System.ComponentModel.DataAnnotations;
namespace Minvu.Snat.Domain.Entities
{
    public class auxPlantillaEntities
    {
        public string nombreTipologia { get; set; }

        [Display(Name = "Clase:")]
        public string nombreSubModalidad { get; set; }
        public string nombreMaestroServicio { get; set; }
        public string nombreParcialidad { get; set; }
        public int? porcentajeParcialidad { get; set; }
        public bool parcialidadSeleccionada { get; set; }
        public long? idServicio { get; set; }
        public long? idParcialidad { get; set; }
        public long? idServicioParcialidad { get; set; }
        public long? idTipoServicioParcialidadCaracteristica { get; set; }
        public long? idCaracteristicaEspeciales { get; set; }

        [Display(Name = "Monto servicio:")]
        [Required(ErrorMessage = "El campo es obligatorio")]

        public decimal? montoServicio { get; set; }
        [Display(Name = "Parcialidad:")]
        // [RegularExpression(@"^\d+.\d{0,3}$", ErrorMessage = "El campo es solo decimal")]
        public decimal? montoParcialidad { get; set; }
        [Display(Name = "Asignación directa:")]

        public decimal? montoAsignacionDirecta { get; set; }
        public bool? estadoServicio { get; set; }
        public bool? estadoParcialidad { get; set; }

        [Display(Name = "Is Active")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "The field Is Active must be checked.")]
        public bool estadoCheckSerGuardao { get; set; }
        [Display(Name = "Is Active")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "The field Is Active must be checked.")]
        public bool estadoCheckParGuardao { get; set; }

        public auxPlantillaEntities()
        {
            nombreTipologia = "";
            nombreSubModalidad = "";
            nombreMaestroServicio = "";
            nombreParcialidad = "";
            porcentajeParcialidad = null;
            parcialidadSeleccionada = false;
            idServicio = null;
            idParcialidad = null;
            montoServicio = null;
            montoParcialidad = null;
            montoAsignacionDirecta = null;
            estadoServicio = null;
            estadoParcialidad = null;
            idCaracteristicaEspeciales = null;
            estadoCheckSerGuardao = false;
            estadoCheckParGuardao = false;
        }

        public auxPlantillaEntities(string _nombreTipologia, string _nombreSubModalidad, string _nombreMaestroServicio, string _nombreParcialidad, int? _porcentajeParcialidad, bool _parcialidadSeleccionada, long? _idServicio, long? _idParcialidad, decimal? _montoServicio, decimal? _montoParcialidad, decimal? _asignacionDirecta, bool? _estadoServicio, bool? _estadoParcialidad, long? _idCaracteristicaEspeciales, long? _idServicioParcialidad, bool _estadoCheckSerGuardao, bool _estadoCheckParGuardao)
        {
            nombreTipologia = _nombreTipologia;
            nombreSubModalidad = _nombreSubModalidad;
            nombreMaestroServicio = _nombreMaestroServicio;
            nombreParcialidad = _nombreParcialidad;
            porcentajeParcialidad = _porcentajeParcialidad;
            parcialidadSeleccionada = _parcialidadSeleccionada;
            idServicio = _idServicio;
            idParcialidad = _idParcialidad;
            montoServicio = _montoServicio;
            montoParcialidad = _montoParcialidad;
            montoAsignacionDirecta = _asignacionDirecta;
            estadoServicio = _estadoServicio;
            estadoParcialidad = _estadoParcialidad;
            idCaracteristicaEspeciales = _idCaracteristicaEspeciales;
            idServicioParcialidad = _idServicioParcialidad;
            estadoCheckSerGuardao = _estadoCheckSerGuardao;
            estadoCheckParGuardao = _estadoCheckParGuardao;
        }
    }

    public class auxPlantillaEntitiesFactory
    {
        internal static List<auxPlantillaEntities> obtenerPlantillaServicioParcialidades(long idCaracteristicasEspeciales, long? idSolicitudPago)
        {
            List<auxPlantillaEntities> _listAuxPlantillaEntities = new List<auxPlantillaEntities>();
            List<auxPlantillaDAO> _listAuxPlantillaDAO = auxPlantillaDAO.getPlantilla(idCaracteristicasEspeciales, idSolicitudPago);

            foreach (auxPlantillaDAO item in _listAuxPlantillaDAO)
            {
                auxPlantillaEntities _auxPlantillaEntities = new auxPlantillaEntities();

                _auxPlantillaEntities.nombreTipologia = item.nombreTipologia;
                _auxPlantillaEntities.nombreSubModalidad = item.nombreSubModalidad;
                _auxPlantillaEntities.nombreMaestroServicio = item.nombreMaestroServicio;
                _auxPlantillaEntities.nombreParcialidad = item.nombreParcialidad;
                _auxPlantillaEntities.porcentajeParcialidad = item.porcentajeParcialidad;
                _auxPlantillaEntities.idServicioParcialidad = item.idServicioParcialidad;
                _auxPlantillaEntities.idTipoServicioParcialidadCaracteristica = item.idTipoServicioParcialidadCaracteristica;
                _auxPlantillaEntities.idCaracteristicaEspeciales = item.idCaracteristicaEspeciales;
                _auxPlantillaEntities.idServicio = item.idServicio;
                _auxPlantillaEntities.idParcialidad = item.idParcialidad;
                _auxPlantillaEntities.montoServicio = item.montoServicio;
                _auxPlantillaEntities.montoParcialidad = item.montoParcialidad;
                _auxPlantillaEntities.montoAsignacionDirecta = item.asignacionDirecta;

                _listAuxPlantillaEntities.Add(_auxPlantillaEntities);
            }

            return _listAuxPlantillaEntities;
        }


        internal static void saveMontosServicios(List<auxPlantillaEntities> _auxPlantillaEntities)
        {
            foreach (auxPlantillaEntities item in _auxPlantillaEntities)
            {
                TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA = new TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA();

                _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.IDCARACTERISTICASESPECIALES = item.idCaracteristicaEspeciales;
                _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.IDSERVICIOPARCIALIDAD = item.idServicioParcialidad;
                _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA = Convert.ToInt64(item.idTipoServicioParcialidadCaracteristica);
                _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA = item.montoAsignacionDirecta;
                _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA = item.montoServicio;

                auxPlantillaDAO.SaveMontoServicio(_TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA);
            }
        }

        internal static void saveMontoParcialidades(List<auxPlantillaEntities> _auxPlantillaEntities)
        {
            foreach (auxPlantillaEntities item in _auxPlantillaEntities)
            {
                TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA = new TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA();

                _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.IDCARACTERISTICASESPECIALES = item.idCaracteristicaEspeciales;
                _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.IDSERVICIOPARCIALIDAD = item.idServicioParcialidad;
                _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA = Convert.ToInt64(item.idTipoServicioParcialidadCaracteristica);
                _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA = item.montoParcialidad;
                _TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA = item.porcentajeParcialidad;

                auxPlantillaDAO.SaveMontoParcialidad(_TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA);
            }
        }

        internal static void crearNuevaPlantilla(long idCaracteristicasEspecial, long? idSolicitudPago)
        {
            List<auxPlantillaEntities> _plantillaBase = obtenerPlantillaServicioParcialidades(idCaracteristicasEspecial, idSolicitudPago);

            foreach (var item in _plantillaBase)
            {
                item.idTipoServicioParcialidadCaracteristica = null;
                item.estadoCheckParGuardao = false;
                item.estadoCheckSerGuardao = false;
                item.montoAsignacionDirecta = null;
                item.montoParcialidad = null;
                item.parcialidadSeleccionada = false;
            }
        }

    }
}

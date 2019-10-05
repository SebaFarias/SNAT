using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;
namespace Minvu.Snat.Domain.Entities
{
    public class servicioParcialidadEntities
    {

        public long? idServicioParcialidad { get; set; }
        public long? idSubModalidadParcialidad { get; set; }
        public long? idTipologiaServicio { get; set; }
        public long? grupoSubModalidadParcialidad { get; set; }
        public long? grupoTipologiaServicio { get; set; }
        public long? numeroPlantillaServicioParcialidad { get; set; }
        public string observacionServicioParcialidad { get; set; }
        public long? idMaestroPrograma { get; set; }
        public bool? estadoServicioParcialidad { get; set; }





        public servicioParcialidadEntities()
        {
            idServicioParcialidad = null;
            idSubModalidadParcialidad = null;
            idTipologiaServicio = null;
            grupoSubModalidadParcialidad = null;
            grupoTipologiaServicio = null;
            numeroPlantillaServicioParcialidad = null;
            observacionServicioParcialidad = string.Empty;
            idMaestroPrograma = null;
            estadoServicioParcialidad = null;
        }

        public servicioParcialidadEntities(long? _idServicioParcialidad, long? _idSubModalidadParcialidad, long? _idTipologiaServicio, long? _grupoSubModalidadParcialidad, long? _grupoTipologiaServicio, long? _numeroPlantillaServicioParcialidad, string _observacionServicioParcialidad, long? _idMaestroPrograma, bool? _estadoServicioParcialidad)
        {
            idServicioParcialidad = _idServicioParcialidad;
            idSubModalidadParcialidad = _idSubModalidadParcialidad;
            idTipologiaServicio = _idTipologiaServicio;
            grupoSubModalidadParcialidad = _grupoSubModalidadParcialidad;
            grupoTipologiaServicio = _grupoTipologiaServicio;
            numeroPlantillaServicioParcialidad = _numeroPlantillaServicioParcialidad;
            observacionServicioParcialidad = _observacionServicioParcialidad;
            idMaestroPrograma = _idMaestroPrograma;
            estadoServicioParcialidad = _estadoServicioParcialidad;
        }

    }

   
    public class servicioParcialidadEntitiesFactory
    {

        //Metodo encargado de desplegar las plantillas de servicios y parcialidades para la generacion de una solicitud de pago


        internal static servicioParcialidadEntities getListServiciosProyecto(long idServicioParcialidad)
        {
            var _servicioParcialidadesDAO = servicioParcialidadesDAO.Get(idServicioParcialidad);
            if (_servicioParcialidadesDAO != null)
            {
                return new servicioParcialidadEntities
                {
                    idServicioParcialidad = _servicioParcialidadesDAO.IDSERVICIOPARCIALIDAD,
                    idSubModalidadParcialidad = _servicioParcialidadesDAO.IDSUBMODALIDADPARCIALIDAD,
                    idTipologiaServicio = _servicioParcialidadesDAO.IDTIPOLOGIASERVICIO,
                    grupoSubModalidadParcialidad = _servicioParcialidadesDAO.IDGRUPOSUBMODALIDADPARCIALIDAD,
                    grupoTipologiaServicio = _servicioParcialidadesDAO.IDGRUPOTIPOLOGIASERVICIO,
                    numeroPlantillaServicioParcialidad = _servicioParcialidadesDAO.NUMEROPLANTILLASERVICIOPARCIALIDAD,
                    observacionServicioParcialidad = _servicioParcialidadesDAO.OBSERVACIONSERVICIOPARCIALIDAD,
                    idMaestroPrograma = _servicioParcialidadesDAO.IDMAESTROPROGRAMA,
                    estadoServicioParcialidad = _servicioParcialidadesDAO.ESTADOSERVICIOPARCIALIDAD,

                };



            }
            else
                return null;
        }


        internal static servicioParcialidadEntities getServicioParcialidad(long idServicioParcialidad)
        {
            var _servicioParcialidadesDAO = servicioParcialidadesDAO.Get(idServicioParcialidad);
            if (_servicioParcialidadesDAO != null)
            {
                return new servicioParcialidadEntities
                {
                    idServicioParcialidad = _servicioParcialidadesDAO.IDSERVICIOPARCIALIDAD,
                    idSubModalidadParcialidad = _servicioParcialidadesDAO.IDSUBMODALIDADPARCIALIDAD,
                    idTipologiaServicio = _servicioParcialidadesDAO.IDTIPOLOGIASERVICIO,
                    grupoSubModalidadParcialidad = _servicioParcialidadesDAO.IDGRUPOSUBMODALIDADPARCIALIDAD,
                    grupoTipologiaServicio = _servicioParcialidadesDAO.IDGRUPOTIPOLOGIASERVICIO,
                    numeroPlantillaServicioParcialidad = _servicioParcialidadesDAO.NUMEROPLANTILLASERVICIOPARCIALIDAD,
                    observacionServicioParcialidad = _servicioParcialidadesDAO.OBSERVACIONSERVICIOPARCIALIDAD,
                    idMaestroPrograma = _servicioParcialidadesDAO.IDMAESTROPROGRAMA,
                    estadoServicioParcialidad = _servicioParcialidadesDAO.ESTADOSERVICIOPARCIALIDAD,
                    

                };



            }
            else
                return null;
        }



        internal static servicioParcialidadEntities getListServicioParcialidad(long idServicioParcialidad)
        {
            var _servicioParcialidadesDAO = servicioParcialidadesDAO.Get(idServicioParcialidad);
            if (_servicioParcialidadesDAO != null)
            {
                return new servicioParcialidadEntities
                {
                    idServicioParcialidad = _servicioParcialidadesDAO.IDSERVICIOPARCIALIDAD,
                    idSubModalidadParcialidad = _servicioParcialidadesDAO.IDSUBMODALIDADPARCIALIDAD,
                    idTipologiaServicio = _servicioParcialidadesDAO.IDTIPOLOGIASERVICIO,
                    grupoSubModalidadParcialidad = _servicioParcialidadesDAO.IDGRUPOSUBMODALIDADPARCIALIDAD,
                    grupoTipologiaServicio = _servicioParcialidadesDAO.IDGRUPOTIPOLOGIASERVICIO,
                    numeroPlantillaServicioParcialidad = _servicioParcialidadesDAO.NUMEROPLANTILLASERVICIOPARCIALIDAD,
                    observacionServicioParcialidad = _servicioParcialidadesDAO.OBSERVACIONSERVICIOPARCIALIDAD,
                    idMaestroPrograma = _servicioParcialidadesDAO.IDMAESTROPROGRAMA,
                    estadoServicioParcialidad = _servicioParcialidadesDAO.ESTADOSERVICIOPARCIALIDAD,

                };



            }
            else
                return null;
        }


    }
}

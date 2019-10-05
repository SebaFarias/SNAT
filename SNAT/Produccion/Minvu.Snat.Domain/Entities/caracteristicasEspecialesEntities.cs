using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;
namespace Minvu.Snat.Domain.Entities
{
    public class caracteristicasEspecialesEntities
    {

        public long? idCaracteristicasEspeciales { get; set; }
        public long? idMaestroPrograma { get; set; }
        public long? idMaestroModalidad { get; set; }
        [Display(Name = "Sub-modalidad:")]
        public string nombreSubModalidad { get; set; }
        public long? idMaestroTipologia { get; set; }
        public long? idInformacionProyecto { get; set; }

        [Display(Name = "Clase:")]
        public long? idMaestroClase { get; set; }

        [Display(Name = "Titulo:")]
        //[Required(ErrorMessage = "El codigo del proyecto es obligatorio")]
        [RegularExpression(@"(^[0-9]*$)", ErrorMessage = "Existen caracteres inválidos.")]
        public long? tituloCaracteristicasEspeciales { get; set; }




        public caracteristicasEspecialesEntities()
        {
            idCaracteristicasEspeciales = 0;
            idMaestroPrograma = 0;
            idMaestroModalidad = 0;
            idMaestroTipologia = 0;
            idInformacionProyecto = 0;
            idMaestroClase = 0;
            tituloCaracteristicasEspeciales = 0;
            nombreSubModalidad = string.Empty;
        }

        public caracteristicasEspecialesEntities(long _idCaracteristicasEspeciales ,long _idMaestroPrograma,long _idMaestroModalidad,long _idMaestroTipologia,long _idInformacionProyecto, long _claseCaracteristicasEspeciales,long _tituloCaracteristicasEspeciales)
        {
            idCaracteristicasEspeciales = _idCaracteristicasEspeciales;
            idMaestroPrograma = _idMaestroPrograma;
            idMaestroModalidad = _idMaestroModalidad;
            idMaestroTipologia = _idMaestroTipologia;
            idInformacionProyecto = _idInformacionProyecto;
            idMaestroClase = _claseCaracteristicasEspeciales;
            tituloCaracteristicasEspeciales = _tituloCaracteristicasEspeciales;
        }

    }

    public class caracteristicasEspecialesEntitiesFactory
    {
        internal static caracteristicasEspecialesEntities getCaracEspecialesIdInformacionProyecto(long idInformacionProyecto)
        {
            var _caracteristicasEspecialesDAO = caracteristicasEspecialesDAO.getCaracEspecialesIdInformacionProyecto(idInformacionProyecto);
            if (_caracteristicasEspecialesDAO.IDMAESTROSUBMODALIDAD == null || _caracteristicasEspecialesDAO.IDMAESTROSUBMODALIDAD.ToString() == string.Empty)
                _caracteristicasEspecialesDAO.IDMAESTROSUBMODALIDAD = 1;

                var _maestroSubModalidadDAO =  maestroSubModalidadDAO.get(_caracteristicasEspecialesDAO.IDMAESTROSUBMODALIDAD);
            if (_caracteristicasEspecialesDAO != null)
            {
                return new caracteristicasEspecialesEntities
                {
                    idInformacionProyecto = _caracteristicasEspecialesDAO.IDINFORMACIONPROYECTO,
                    idCaracteristicasEspeciales = _caracteristicasEspecialesDAO.IDCARACTERISTICASESPECIALES,
                    nombreSubModalidad = _maestroSubModalidadDAO.NOMBREMAESTROSUBMODALIDAD,
                    idMaestroPrograma = _caracteristicasEspecialesDAO.IDMAESTROPROGRAMA,
                    idMaestroModalidad = _caracteristicasEspecialesDAO.IDMAESTROMODALIDAD,
                    idMaestroTipologia = _caracteristicasEspecialesDAO.IDMAESTROTIPOLOGIA,
                    //tituloCaracteristicasEspeciales = _caracteristicasEspecialesDAO.TITULOCARACTERISTICASESPECIALES,
                    idMaestroClase = _caracteristicasEspecialesDAO.IDMAESTROCLASE
                };
            }
            else
                return null;
        }


        internal static caracteristicasEspecialesEntities getCaracteristicaEspecial(long idCaracEspecial)
        {
            var _caracteristicasEspecialesDAO = caracteristicasEspecialesDAO.getCaracEspecial(idCaracEspecial);
            if (_caracteristicasEspecialesDAO != null)
            {
                return new caracteristicasEspecialesEntities
                {
                    idInformacionProyecto = _caracteristicasEspecialesDAO.IDINFORMACIONPROYECTO,
                    idCaracteristicasEspeciales = _caracteristicasEspecialesDAO.IDCARACTERISTICASESPECIALES,
                    idMaestroPrograma = _caracteristicasEspecialesDAO.IDMAESTROPROGRAMA,
                    idMaestroModalidad = _caracteristicasEspecialesDAO.IDMAESTROMODALIDAD,
                    idMaestroTipologia = _caracteristicasEspecialesDAO.IDMAESTROTIPOLOGIA,
                    //tituloCaracteristicasEspeciales = _caracteristicasEspecialesDAO.TITULOCARACTERISTICASESPECIALES,
                    idMaestroClase = _caracteristicasEspecialesDAO.IDMAESTROCLASE
                };
            }
            else
                return null;
        }

        internal static caracteristicasEspecialesEntities getIdCaracterisacion(long idCaracteristicasEspeciales)
        {
            var _caracteristicasEspecialesDAO = caracteristicasEspecialesDAO.getIdCaracterisacion(idCaracteristicasEspeciales);
            if (_caracteristicasEspecialesDAO != null)
            {
                return new caracteristicasEspecialesEntities
                {
                    idInformacionProyecto = _caracteristicasEspecialesDAO.IDINFORMACIONPROYECTO,
                    idCaracteristicasEspeciales = _caracteristicasEspecialesDAO.IDCARACTERISTICASESPECIALES,
                    idMaestroPrograma = _caracteristicasEspecialesDAO.IDMAESTROPROGRAMA,
                    idMaestroModalidad = _caracteristicasEspecialesDAO.IDMAESTROMODALIDAD,
                    idMaestroTipologia = _caracteristicasEspecialesDAO.IDMAESTROTIPOLOGIA,
                    //tituloCaracteristicasEspeciales = _caracteristicasEspecialesDAO.TITULOCARACTERISTICASESPECIALES,
                    idMaestroClase = _caracteristicasEspecialesDAO.IDMAESTROCLASE

                };



            }
            else
                return null;
        }
    }
}

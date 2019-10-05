using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;

namespace Minvu.Snat.Domain.Entities
{
    public class maestroModalidadEntities
    {
        public long idMaestroModalidad { get; set; }

        [Display(Name = "Modalidad:")]
        [Required(ErrorMessage = "La modalidad es obligatoria")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string nombreMaestroModalidad { get; set; }
        public bool estadoMaestroModalidad { get; set; }

        public maestroModalidadEntities()
        {
            idMaestroModalidad = 0;
            nombreMaestroModalidad = string.Empty;
            estadoMaestroModalidad = false;
        }
        public maestroModalidadEntities(long _idMaestroModalidad, string _nombreMaestroModalidad, bool _estadoMaestroModalidad)
        {
            idMaestroModalidad = _idMaestroModalidad;
            nombreMaestroModalidad = _nombreMaestroModalidad;
            estadoMaestroModalidad = _estadoMaestroModalidad;
        }
    }
    public class maestroModalidadEntitiesFactory
    {

        internal static maestroModalidadEntities getMaestroModalidad(long idModalidad)
        {
            var _maestroModalidadDAO = maestroModalidadDAO.get(idModalidad);
            if (_maestroModalidadDAO != null)
            {
                return new maestroModalidadEntities
                {

                    idMaestroModalidad = Convert.ToInt64(_maestroModalidadDAO.IDMAESTROMODALIDAD),
                    nombreMaestroModalidad = _maestroModalidadDAO.NOMBREMAESTROMODALIDAD,
                    estadoMaestroModalidad = Convert.ToBoolean(_maestroModalidadDAO.ESTADOMAESTROMODALIDAD)

                };
            }
            else
                return null;


        }
        internal static List<maestroModalidadEntities> getListMaestroModalidad()
        {
            var _maestroModalidadDAO = maestroModalidadDAO.GetList();
            List<maestroModalidadEntities> _listmaestroModalidadEntities2 = new List<maestroModalidadEntities>();
            if (_maestroModalidadDAO != null)
            {
                foreach (var item in _maestroModalidadDAO)
                {
                    maestroModalidadEntities _maestroModalidadEntities2 = new maestroModalidadEntities();
                    _maestroModalidadEntities2.idMaestroModalidad = Convert.ToInt64(item.IDMAESTROMODALIDAD);
                    _maestroModalidadEntities2.nombreMaestroModalidad = item.NOMBREMAESTROMODALIDAD;
                    _maestroModalidadEntities2.estadoMaestroModalidad = Convert.ToBoolean(item.ESTADOMAESTROMODALIDAD);


                        _listmaestroModalidadEntities2.Add(_maestroModalidadEntities2);
                 }


                return _listmaestroModalidadEntities2;
            }
            else
                return null;


        }

    }
}

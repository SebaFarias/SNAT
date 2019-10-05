using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;

namespace Minvu.Snat.Domain.Entities
{
    public class maestroAlternativaPostulacionEntities
    {
        public long idMaestroAlternativaPostulacion { get; set; }

        [Display(Name = "Alternativa postulación:")]
        [Required(ErrorMessage = "Alternativa postulación es obligatorio")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string nombreMaestroAlternativaPostulacion { get; set; }
        public bool estadoMaestroAlternativaPostulacion { get; set; }

        public maestroAlternativaPostulacionEntities()
        {
            idMaestroAlternativaPostulacion = 0;
            nombreMaestroAlternativaPostulacion = string.Empty;
            estadoMaestroAlternativaPostulacion = false;
        }
        public maestroAlternativaPostulacionEntities(long _idMaestroAlternativaPostulacion , string _nombreMaestroAlternativaPostulacion,bool _estadoMaestroAlternativaPostulacion)
        {
            idMaestroAlternativaPostulacion = _idMaestroAlternativaPostulacion;
            nombreMaestroAlternativaPostulacion = _nombreMaestroAlternativaPostulacion;
            estadoMaestroAlternativaPostulacion = _estadoMaestroAlternativaPostulacion;
        }
    }

    public class maestroAlternativaPostulacionEntitiesFactory
    {

        internal static maestroAlternativaPostulacionEntities getAlternativaPostulacion(long idAlternativaPostulacion)
        {
            var _maestroAlternativaPostulacionDAO = maestroAlternativaPostulacionDAO.Get(idAlternativaPostulacion);
            if (_maestroAlternativaPostulacionDAO != null)
            {
                return new maestroAlternativaPostulacionEntities
                {

                    idMaestroAlternativaPostulacion = Convert.ToInt64(_maestroAlternativaPostulacionDAO.IDMAESTROALTERNATIVAPOSTULACION),
                    nombreMaestroAlternativaPostulacion = _maestroAlternativaPostulacionDAO.NOMBREMAESTROALTERNATIVAPOSTULACION,
                    estadoMaestroAlternativaPostulacion = Convert.ToBoolean(_maestroAlternativaPostulacionDAO.ESTADOMAESTROALTERNATIVAPOSTULACION)

                };
            }
            else
                return null;


        }

    }
}

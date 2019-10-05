using Minvu.Snat.IData.DAO;
using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class MaestroEstadoBeneficioEntities
    {
        public long idMaestroEstadoBeneficio { get; set; }

        [Display(Name = "Estado beneficio:")]
        public string NombreMaestroEstadoBeneficio { get; set; }
        public bool? EstadoMaestroEstadoBeneficio { get; set; }

        public MaestroEstadoBeneficioEntities()
        {
            idMaestroEstadoBeneficio = 0;
            NombreMaestroEstadoBeneficio = String.Empty;
            EstadoMaestroEstadoBeneficio = null;
        }

        public MaestroEstadoBeneficioEntities(long _idMaestroEstadoBeneficio, string _NombreMaestroEstadoBeneficio, bool? _EstadoMaestroEstadoBeneficio)
        {
            idMaestroEstadoBeneficio = _idMaestroEstadoBeneficio;
            NombreMaestroEstadoBeneficio = _NombreMaestroEstadoBeneficio;
            EstadoMaestroEstadoBeneficio = _EstadoMaestroEstadoBeneficio;
        }
    }

    public class MaestroEstadoBeneficioEntitiesFactory
    {
        public static MaestroEstadoBeneficioEntities getMaestroEstadoBeneficio(long? idMaestroEstadoBeneficio)
        {
            MAESTRO_ESTADO_BENEFICIO _MaestroEstadoBeneficio = MaestroEstadoBeneficioDAO.Get(idMaestroEstadoBeneficio);
            MaestroEstadoBeneficioEntities _MaestroEstadoBeneficioEntities = new MaestroEstadoBeneficioEntities();

            if (_MaestroEstadoBeneficio != null)
            {
                _MaestroEstadoBeneficioEntities.idMaestroEstadoBeneficio = _MaestroEstadoBeneficio.IDMAESTROESTADOBENEFICIO;
                _MaestroEstadoBeneficioEntities.NombreMaestroEstadoBeneficio = _MaestroEstadoBeneficio.NOMBREMAESTROESTADOBENEFICIO;
                _MaestroEstadoBeneficioEntities.EstadoMaestroEstadoBeneficio = _MaestroEstadoBeneficio.ESTADOMAESTROESTADOBENEFICIO;
            }

            return _MaestroEstadoBeneficioEntities;
        }
    }
}

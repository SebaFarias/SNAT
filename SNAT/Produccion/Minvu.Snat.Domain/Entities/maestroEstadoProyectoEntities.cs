using Minvu.Snat.IData.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class maestroEstadoProyectoEntities
    {
        public long idMaestroEstadoProyecto { get; set; }

        [Display(Name = "Estado avance obra:")]
        public string NombreMaestroEstadoProyecto { get; set; }
        public bool EstadoMaestroEstadoProyecto { get; set; }

        public maestroEstadoProyectoEntities()
        {
            idMaestroEstadoProyecto = 0;
            NombreMaestroEstadoProyecto = String.Empty;
            EstadoMaestroEstadoProyecto = false;
        }

        public maestroEstadoProyectoEntities(long _idMaestroEstadoProyecto, string _NombreMaestroEstadoProyecto, bool _EstadoMaestroEstadoProyecto)
        {
            idMaestroEstadoProyecto = _idMaestroEstadoProyecto;
            NombreMaestroEstadoProyecto = _NombreMaestroEstadoProyecto;
            EstadoMaestroEstadoProyecto = _EstadoMaestroEstadoProyecto;
        }
    }

    public class maestroEstadoProyectoEntitiesFactory
    {
        internal static maestroEstadoProyectoEntities getMaestroEstadoProyecto(long? idMaestroEstadoProyecto)
        {
            var objMaestroEstadoProyectoDAO = maestroEstadoProyectoDAO.getMaestroEstadoProyecto(idMaestroEstadoProyecto);

            if (objMaestroEstadoProyectoDAO != null)
            {
                return new maestroEstadoProyectoEntities
                {
                    idMaestroEstadoProyecto = Convert.ToInt64(objMaestroEstadoProyectoDAO.IDMAESTROESTADOPROYECTO),
                    NombreMaestroEstadoProyecto = objMaestroEstadoProyectoDAO.NOMBREMAESTROESTADOPROYECTO,
                    EstadoMaestroEstadoProyecto = objMaestroEstadoProyectoDAO.ESTADOMAESTROESTADOPROYECTO
                };
            }
            else
                return null;
        }
    }
}

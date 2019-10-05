using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class maestroEstadoSolicitudEntities
    {
        public int idMaestroEstadoSolicitud { get; set; }
        public string nombreMaestroEstadoSolicitud { get; set; }
        public bool estadoMaestroEstadoSolicitud { get; set; }

        public maestroEstadoSolicitudEntities()
        {
            idMaestroEstadoSolicitud = 0;
            nombreMaestroEstadoSolicitud = string.Empty;
            estadoMaestroEstadoSolicitud = false;
        }
        public maestroEstadoSolicitudEntities(int _idMaestroEstadoSolicitud, string _nombreMaestroEstadoSolicitud, bool _estadoMaestroEstadoSolicitud)
        {
            idMaestroEstadoSolicitud = _idMaestroEstadoSolicitud;
            nombreMaestroEstadoSolicitud = _nombreMaestroEstadoSolicitud;
            estadoMaestroEstadoSolicitud = _estadoMaestroEstadoSolicitud;
        }
    }
}

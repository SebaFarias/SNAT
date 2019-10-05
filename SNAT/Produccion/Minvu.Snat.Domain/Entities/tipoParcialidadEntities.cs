using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class tipoParcialidadEntities
    {
        public int idTipoParcialidad { get; set; }
        public int idMaestroParcialidad { get; set; }

        
        public int idTipoServicio { get; set; }
        public bool estadoTipoParcialidad { get; set; }
        public decimal porcentajeTipoParcialidad { get; set; }

        public tipoParcialidadEntities()
        {
            idTipoParcialidad = 0;
            idMaestroParcialidad = 0;
            idTipoServicio = 0;
            estadoTipoParcialidad = false;
            porcentajeTipoParcialidad = 0;
    }
        public tipoParcialidadEntities(int _idTipoParcialidad,int _idMaestroParcialidad, int _idTipoServicio, bool _estadoTipoParcialidad, decimal _porcentajeTipoParcialidad)
       {
            idTipoParcialidad = idTipoParcialidad;
            idMaestroParcialidad = _idMaestroParcialidad;
            
            idTipoServicio = _idTipoServicio;
            estadoTipoParcialidad = _estadoTipoParcialidad;
            porcentajeTipoParcialidad = _porcentajeTipoParcialidad;

        }
    }
}

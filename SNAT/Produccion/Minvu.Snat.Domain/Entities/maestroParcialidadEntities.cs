using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class maestroParcialidadEntities
    {
        public int idMaestroParcialidad { get; set; }
        public int idMaestroServicio { get; set; }
        public string nombreMaestroParcialidad { get; set; }
        public bool estadoMaestroParcialidad { get; set; }

        public maestroParcialidadEntities()
        {
            idMaestroServicio = 0;
            idMaestroParcialidad = 0;
            nombreMaestroParcialidad = string.Empty;
            estadoMaestroParcialidad = false;
        }
        public maestroParcialidadEntities(int _idMaestroParcialidad,int _idMaestroServicio,  string _nombreMaestroParcialidad, bool _estadoMaestroParcialidad)
        {
            idMaestroServicio = _idMaestroServicio;
            idMaestroParcialidad = _idMaestroParcialidad;
            nombreMaestroParcialidad = _nombreMaestroParcialidad;
            estadoMaestroParcialidad = _estadoMaestroParcialidad;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.DAO;

namespace Minvu.Snat.Domain.Entities
{
    public class ConvalidacionDatosEntreSistemasEntities
    {

        public static long getIdProgramaInternoPorSistemaExterno(string nombrePrograma, long idProgramaExterno, string nombreSistemaExterno)
        {
            return ConvalidacionDatosEntreSistemasDAO.getIdProgramaInternoPorSistemaExterno(nombrePrograma, idProgramaExterno, nombreSistemaExterno);
        }
         

    }
}

using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.IData.DAO
{
    public class maestroEstadoProyectoDAO
    {
        public static MAESTRO_ESTADO_PROYECTO getMaestroEstadoProyecto(long? idMaestroEstadoProyecto)
        {
            MAESTRO_ESTADO_PROYECTO objMaestroEstadoProyecto = new MAESTRO_ESTADO_PROYECTO();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var objResult = from a in contexto.MAESTRO_ESTADO_PROYECTO
                                where a.IDMAESTROESTADOPROYECTO == idMaestroEstadoProyecto
                                select a;
                foreach (var a in objResult)
                {
                    objMaestroEstadoProyecto.IDMAESTROESTADOPROYECTO = a.IDMAESTROESTADOPROYECTO;
                    objMaestroEstadoProyecto.NOMBREMAESTROESTADOPROYECTO = a.NOMBREMAESTROESTADOPROYECTO;
                    objMaestroEstadoProyecto.ESTADOMAESTROESTADOPROYECTO = a.ESTADOMAESTROESTADOPROYECTO;
                }

                return objMaestroEstadoProyecto;
            }
        }
    }
}

using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.IData.DAO
{
    public class MaestroEstadoBeneficioDAO
    {
        public static MAESTRO_ESTADO_BENEFICIO Get(long? idMaestroEstadoBeneficio)
        {
            MAESTRO_ESTADO_BENEFICIO _MaestroEstadoBeneficio = new MAESTRO_ESTADO_BENEFICIO();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroEstadpoBeneficio = from a in contexto.MAESTRO_ESTADO_BENEFICIO
                                                where a.IDMAESTROESTADOBENEFICIO == idMaestroEstadoBeneficio
                                                select a;

                if (qMaestroEstadpoBeneficio != null)
                {
                    foreach (var a in qMaestroEstadpoBeneficio)
                    {
                        _MaestroEstadoBeneficio.IDMAESTROESTADOBENEFICIO = a.IDMAESTROESTADOBENEFICIO;
                        _MaestroEstadoBeneficio.NOMBREMAESTROESTADOBENEFICIO = a.NOMBREMAESTROESTADOBENEFICIO;
                        _MaestroEstadoBeneficio.ESTADOMAESTROESTADOBENEFICIO = a.ESTADOMAESTROESTADOBENEFICIO;
                    }
                }

                return _MaestroEstadoBeneficio;
            }
        }
    }
}

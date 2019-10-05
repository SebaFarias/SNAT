using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.IData.DAO
{
    public class MaestroBancoDAO
    {
        public static MAESTRO_BANCO Get(long? idMaestroBanco)
        {
            MAESTRO_BANCO _MaestroBanco = new MAESTRO_BANCO();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroBanco = from a in contexto.MAESTRO_BANCO
                                    where a.IDMAESTROBANCO == idMaestroBanco
                                    select a;

                if (qMaestroBanco != null)
                {
                    foreach (var a in qMaestroBanco)
                    {
                        _MaestroBanco.IDMAESTROBANCO = a.IDMAESTROBANCO;
                        _MaestroBanco.NOMBREMAESTROBANCO = a.NOMBREMAESTROBANCO;
                        _MaestroBanco.ESTADOMAESTROBANCO = a.ESTADOMAESTROBANCO;
                    }
                }

                return _MaestroBanco;
            }
        }
    }
}

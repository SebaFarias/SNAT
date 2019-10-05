using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
  public  class DBComun
    {
        static DB_SNAT_VEntities _Contexto;
        public static DB_SNAT_VEntities Contexto
        {
            get
            {
                if (_Contexto == null)
                {
                    _Contexto = new DB_SNAT_VEntities();
                }

                return _Contexto;
            }
        }
    }
}

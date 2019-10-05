using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;

namespace Minvu.Snat.Domain.Entities
{
    public class maestroClaseEntities
    {
        public long idMaestroClase { get; set; }

        [Display(Name = "Clase:")]
        
        public string nombreMaestroClase { get; set; }
        public bool estadoMaestroClase { get; set; }

        public maestroClaseEntities()
        {
            idMaestroClase = 0;
            nombreMaestroClase = string.Empty;
            estadoMaestroClase = false;
        }
        public maestroClaseEntities(long _idMaestroClase, string _nombreMaestroClase, bool _estadoMaestroClase)
        {

            idMaestroClase = _idMaestroClase;
            nombreMaestroClase = _nombreMaestroClase;
            estadoMaestroClase = _estadoMaestroClase;
        }
    }

    public class maestroClaseEntitiesFactory
    {

        internal static maestroClaseEntities getMaestroClase(long idModalidad)
        {
            var _maestroClaseDAO = maestroClaseDAO.Get(idModalidad);
            if (_maestroClaseDAO != null)
            {
                return new maestroClaseEntities
                {

                    idMaestroClase = Convert.ToInt64(_maestroClaseDAO.IDMAESTROCLASE),
                    nombreMaestroClase = _maestroClaseDAO.NOMBREMAESTROCLASE,
                    estadoMaestroClase = Convert.ToBoolean(_maestroClaseDAO.ESTADOMAESTROCLASE)

                };
            }
            else
                return null;


        }

    }
}

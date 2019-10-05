using Minvu.Snat.IData.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class maestroEstadoAutorizacionEntities
    {
        public long idMaestroEstadoAutorizacion { get; set; }
        public string nombreMaestroEstadoAutorizacion { get; set; }
        public bool estadoMaestroEstadoAutorizacion { get; set; }

        public maestroEstadoAutorizacionEntities()
        {
            idMaestroEstadoAutorizacion = 0;
            nombreMaestroEstadoAutorizacion = string.Empty;
            estadoMaestroEstadoAutorizacion = false;
        }
        public maestroEstadoAutorizacionEntities(long _idMaestroEstadoAutorizacion, string _nombreMaestroEstadoAutorizacion, bool _estadoMaestroEstadoAutorizacion)
        {
            idMaestroEstadoAutorizacion = _idMaestroEstadoAutorizacion;
            nombreMaestroEstadoAutorizacion = _nombreMaestroEstadoAutorizacion;
            estadoMaestroEstadoAutorizacion = _estadoMaestroEstadoAutorizacion;
        }
    }

    public class maestroEstadoAutorizacionEntitiesFactory
    {
        internal static List<maestroEstadoAutorizacionEntities> getListEstadoAutorizacion()
        {
            var _maestroEstadoAutorizacionDAO = maestroEstadoAutorizacionDAO.GetList();
            List<maestroEstadoAutorizacionEntities> _listmaestroEstadoAutorizacionEntities = new List<maestroEstadoAutorizacionEntities>();
            if (_maestroEstadoAutorizacionDAO != null)
            {
                foreach (var item in _maestroEstadoAutorizacionDAO)
                {
                    maestroEstadoAutorizacionEntities _maestroEstadoAutorizacionEntities = new maestroEstadoAutorizacionEntities();
                    _maestroEstadoAutorizacionEntities.idMaestroEstadoAutorizacion = Convert.ToInt64(item.IDMAESTROESTADOAUTORIZACION);
                    _maestroEstadoAutorizacionEntities.nombreMaestroEstadoAutorizacion = item.NOMBREMAESTROESTADOAUTORIZACION;
                    _maestroEstadoAutorizacionEntities.estadoMaestroEstadoAutorizacion = Convert.ToBoolean(item.ESTADOMAESTROESTADOAUTORIZACION);

                    _listmaestroEstadoAutorizacionEntities.Add(_maestroEstadoAutorizacionEntities);
                }

                return _listmaestroEstadoAutorizacionEntities;
            }
            else
                return null;
        }
    }
}

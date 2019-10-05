using Minvu.Snat.IData.DAO;
using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class modificacionEstadoSolicitudEntities
    {
        public long IDMODIFICACIONESTADOSOLICITUD { get; set; }
        public long IDSOLICITUDPAGO { get; set; }
        public long IDMAESTROESTADOSOLICITUD { get; set; }
        public DateTime FECHAMODIFICACIONESTADOSOLICITUD { get; set; }
        public string USUARIORESPONSABLEMODIFICACIONESTADOSOLICITUD { get; set; }
        public string mensajeSalida { get; set; }
        public string codigoSalida { get; set; }

        public modificacionEstadoSolicitudEntities()
        {
            IDMODIFICACIONESTADOSOLICITUD = 0;
            IDSOLICITUDPAGO = 0;
            IDMAESTROESTADOSOLICITUD = 0;
            FECHAMODIFICACIONESTADOSOLICITUD = DateTime.Now;
            USUARIORESPONSABLEMODIFICACIONESTADOSOLICITUD = String.Empty;
            mensajeSalida = String.Empty;
            codigoSalida = String.Empty;
        }
    }

    public class ModificacionEstadoSolicitudEntitiesFactory
    {
        internal static modificacionEstadoSolicitudEntities SaveModificacionEstadoSolicitud(modificacionEstadoSolicitudEntities objModificacionEstado)
        {
            var objResultado = modificacionEstadoSolicitudDAO.SaveModificacionEstadoSolicitud(objModificacionEstado.IDSOLICITUDPAGO,
                                                                                objModificacionEstado.IDMAESTROESTADOSOLICITUD,
                                                                                objModificacionEstado.USUARIORESPONSABLEMODIFICACIONESTADOSOLICITUD);

            modificacionEstadoSolicitudEntities objModificacionEnt = new modificacionEstadoSolicitudEntities();
            if (objResultado != null)
            {
                return new modificacionEstadoSolicitudEntities
                {
                    mensajeSalida = objResultado.MSG,
                    codigoSalida = objResultado.ERR.ToString()
                };
            }
            else
                return objModificacionEnt;
        }
    }
}

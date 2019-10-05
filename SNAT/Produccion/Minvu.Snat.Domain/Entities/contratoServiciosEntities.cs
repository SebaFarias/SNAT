using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.DAO;
using System.ComponentModel.DataAnnotations;


namespace Minvu.Snat.Domain.Entities
{
    public class contratoServiciosEntities
    {
        public string nombreServicio { get; set; }
        public string nombreMaestroServicio { get; set; }
        public long idMaestroServicio { get; set; }
        public long avance { get; set; }
        public long montoAPagar { get; set; }
        public long montoServicio { get; set; }

        public contratoServiciosEntities()
        {
            nombreServicio = string.Empty;
            nombreMaestroServicio = string.Empty;
            idMaestroServicio = 0;
            avance = 0;
            montoAPagar = 0;
        }
    }
    public class contratoServiciosEntitiesFactory
    {
        internal static List<contratoServiciosEntities> obtieneServicios(long idContrato)
        {
            //var _obtieneServicios = contratoDAO.GetActividadesContrato(idContrato);
            var _obtieneServiciosContratos = contratoDAO.GetActividadesServicios(idContrato);           

            List<contratoServiciosEntities> contratoSolicitudPagoEntities = new List<contratoServiciosEntities>();

            if (_obtieneServiciosContratos != null)
            {
                foreach (var item in _obtieneServiciosContratos)
                {
                    contratoServiciosEntities info = new contratoServiciosEntities();

                    info.nombreServicio = item.MAESTRO_SERVICIO.NOMBREABREVIADOMAESTROSERVICIO;
                    info.nombreMaestroServicio = item.MAESTRO_SERVICIO.NOMBREMAESTROSERVICIO;
                    info.idMaestroServicio = item.MAESTRO_SERVICIO.IDMAESTROSERVICIO;

                    contratoSolicitudPagoEntities.Add(info);
                }
                return contratoSolicitudPagoEntities;
            }

            return contratoSolicitudPagoEntities;

        }

        internal static List<contratoServiciosEntities> obtieneMontoServicios(long idSolicitud)
        {
            var _obtieneServiciosMontos = contratoDAO.GetMontoServicios(idSolicitud);

            List<contratoServiciosEntities> contratoSolicitudPagoEntities = new List<contratoServiciosEntities>();

            if (_obtieneServiciosMontos != null)
            {
                foreach (var item in _obtieneServiciosMontos)
                {
                    contratoServiciosEntities info = new contratoServiciosEntities();

                    info.nombreServicio = item.MAESTRO_SERVICIO.NOMBREABREVIADOMAESTROSERVICIO;
                    info.nombreMaestroServicio = item.MAESTRO_SERVICIO.NOMBREMAESTROSERVICIO;
                    info.idMaestroServicio = item.MAESTRO_SERVICIO.IDMAESTROSERVICIO;
                    info.montoAPagar = (long)item.MONTOPAGOTIPOACTIVIDADMONTO;

                    contratoSolicitudPagoEntities.Add(info);
                }
                return contratoSolicitudPagoEntities;
            }

            return contratoSolicitudPagoEntities;

        }
    }
}
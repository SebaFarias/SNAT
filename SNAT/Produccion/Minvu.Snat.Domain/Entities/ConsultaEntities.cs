using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class ConsultaEntities
    {
        public long idSolicitud { get; set; }
        public long idregion { get; set; }
        public long idProvincia { get; set; }
        public long idComuna { get; set; }
        public string nombreRegion { get; set; }
        public string nombreTipologia { get; set; }
        public string nombreServicio { get; set; }
        public string rutProveedor { get; set; }
        public string nombreProveedor { get; set; }
        public string estadoDeLaSolicitud { get; set; }
        public long numeroAutorizacion { get; set; }
        public string estadoAutorizacion { get; set; }
        public long codigoProyecto { get; set; }
        public long idTipologia { get; set; }
        public long idLlamado { get; set; }
        public long numeroVivienda { get; set; }
        public long idServicio { get; set; }
        public long idProveedor { get; set; }
        public long idEstadoSolicitud { get; set; }
        public decimal montoSolicitud { get; set; }
        public long idAutorizacion { get; set; }
        public long idTipoProveedor { get; set; }
        public bool aPago { get; set; }

        public long idMaestroModalidad { get; set; }

        public ConsultaEntities()
        {
            idSolicitud = 0;
            idregion = 0;
            codigoProyecto = 0;
            idTipologia = 0;
            idProvincia = 0;
            idComuna = 0;
            numeroVivienda = 0;
            idServicio = 0;
            idProveedor = 0;
            idEstadoSolicitud = 0;
            montoSolicitud = 0;
            idLlamado = 0;
            idTipoProveedor = 0;
            idAutorizacion = 0;
            idMaestroModalidad = 0;
            nombreProveedor = string.Empty;
        }
    }
}

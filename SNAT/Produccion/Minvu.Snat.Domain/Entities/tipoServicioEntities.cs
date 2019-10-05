using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class tipoServicioEntities
    {
        public int idTipoServicio { get; set; }
        public int idSolicitudPago { get; set; }
        public int idMaestroServicio { get; set; }
        public decimal montoAsignacionDirectaTipoServicio { get; set; }
        public decimal totalServicioTipoServicio { get; set; }
        public decimal montoIncrementoTipoServicio { get; set; }
        public tipoServicioEntities()
        {
            idTipoServicio = 0;
            idSolicitudPago = 0;
            idMaestroServicio = 0;
            montoAsignacionDirectaTipoServicio = 0;
            totalServicioTipoServicio = 0;
            montoIncrementoTipoServicio = 0;
    }
        public tipoServicioEntities(int _idTipoServicio, int _idSolicitudPago,int _idMaestroServicio, decimal _montoAsignacionDirectaTipoServicio, decimal _totalServicioTipoServicio, decimal _montoIncrementoTipoServicio)
        {
        idTipoServicio = _idTipoServicio;
        idSolicitudPago = _idSolicitudPago;
        idMaestroServicio = _idMaestroServicio;
        montoAsignacionDirectaTipoServicio = _montoAsignacionDirectaTipoServicio;
        totalServicioTipoServicio = _totalServicioTipoServicio;
        montoIncrementoTipoServicio = _montoIncrementoTipoServicio;
        }
    }
}


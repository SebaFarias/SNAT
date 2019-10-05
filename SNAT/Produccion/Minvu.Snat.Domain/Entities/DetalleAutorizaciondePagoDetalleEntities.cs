using Minvu.Snat.IData.DAO;
using System;
using System.Collections.Generic;
using Minvu.Snat.IData.ORM;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class DetalleAutorizaciondePagoDetalleEntities
    {
        public long IDCARACTERISTICASESPECIALES { get; set; }
        public long IDINFORMACIONPROYECTO { get; set; }
        public int CODIGOPROYECTOINFORMACIONPROYECTO { get; set; }
        public string NOMBREPROYECTOINFORMACIONPROYECTO { get; set; }
        public int CODIGOREGIONDIRECCION { get; set; }
        public string NOMBREREGIONDIRECCION { get; set; }
        public string NOMBREMAESTROPROGRAMA { get; set; }
        public string NOMBREMAESTROMODALIDAD { get; set; }
        public string NOMBREMAESTROSUBMODALIDAD { get; set; }
        public string NOMBREMAESTROTIPOLOGIA { get; set; }
        public string CLASE { get; set; }
        public decimal? S1 { get; set; }
        public decimal? S2 { get; set; }
        public decimal? S3 { get; set; }
        public decimal? S4 { get; set; }
        public decimal? S5 { get; set; }
        public decimal? S6 { get; set; }
        public decimal? S7 { get; set; }
        public decimal? S8 { get; set; }
        public decimal? S9 { get; set; }
        public decimal? S10 { get; set; }
        public decimal? MONTOFTO { get; set; }
        public decimal? SALDOFTO { get; set; }
        public decimal MONTOTOTALPROYECTO { get; set; }
        public decimal MONTOAPAGAR { get; set; }
        public long idSolicitudPago { get; set; }

        public DetalleAutorizaciondePagoDetalleEntities()
        {
            IDCARACTERISTICASESPECIALES = 0;
            IDINFORMACIONPROYECTO = 0;
            CODIGOPROYECTOINFORMACIONPROYECTO = 0;
            NOMBREPROYECTOINFORMACIONPROYECTO = String.Empty;
            NOMBREMAESTROMODALIDAD = String.Empty;
            NOMBREMAESTROSUBMODALIDAD = String.Empty;
            CODIGOREGIONDIRECCION = 0;
            NOMBREREGIONDIRECCION = String.Empty;
            NOMBREMAESTROPROGRAMA = String.Empty;
            NOMBREMAESTROTIPOLOGIA = String.Empty;
            CLASE = String.Empty;
            S1 = null;
            S2 = null;
            S3 = null;
            S4 = null;
            S5 = null;
            S6 = null;
            S7 = null;
            S8 = null;
            S9 = null;
            S10 = null;
            MONTOFTO = null;
            SALDOFTO = null;
            MONTOTOTALPROYECTO = 0;
            MONTOAPAGAR = 0;
            idSolicitudPago = 0;
        }
    }

    public class DetalleAutorizaciondePagoDetalleEntitiesFactory
    {
        internal static List<DetalleAutorizaciondePagoDetalleEntities> getListDetalleAutorizacionesDePagoGrilla(long? IdAutorizacion, long? IdTipoProveedor)
        {
            var _listDetalleAutorizaciondePagoDetalleDAO = DetalleAutorizaciondePagoDAO.GetDetalleAutorizacionesDePagoGrilla(IdAutorizacion, IdTipoProveedor);

            List<DetalleAutorizaciondePagoDetalleEntities> lstDetAutPagoDetalle = new List<DetalleAutorizaciondePagoDetalleEntities>();

            if (_listDetalleAutorizaciondePagoDetalleDAO != null)
            {
                foreach (var item in _listDetalleAutorizaciondePagoDetalleDAO)
                {
                    DetalleAutorizaciondePagoDetalleEntities _AutorizaciondePagoEntities = new DetalleAutorizaciondePagoDetalleEntities();

                    _AutorizaciondePagoEntities.IDCARACTERISTICASESPECIALES = Convert.ToInt64(item.IDCARACTERISTICASESPECIALES);
                    _AutorizaciondePagoEntities.IDINFORMACIONPROYECTO = Convert.ToInt64(item.IDINFORMACIONPROYECTO);
                    _AutorizaciondePagoEntities.CODIGOPROYECTOINFORMACIONPROYECTO = Convert.ToInt32(item.CODIGOPROYECTOINFORMACIONPROYECTO);
                    _AutorizaciondePagoEntities.NOMBREPROYECTOINFORMACIONPROYECTO = item.NOMBREPROYECTOINFORMACIONPROYECTO.ToString();
                    _AutorizaciondePagoEntities.CODIGOREGIONDIRECCION = Convert.ToInt32(item.CODIGOREGIONDIRECCION);
                    _AutorizaciondePagoEntities.NOMBREREGIONDIRECCION = regionDAO.obtenerNombreRegion(_AutorizaciondePagoEntities.CODIGOREGIONDIRECCION);
                    _AutorizaciondePagoEntities.NOMBREMAESTROPROGRAMA = item.NOMBREMAESTROPROGRAMA.ToString();
                    _AutorizaciondePagoEntities.NOMBREMAESTROTIPOLOGIA = item.NOMBREMAESTROTIPOLOGIA.ToString();

                    
                    CARACTERISTICAS_ESPECIALES _caracteristicasEspeciales = caracteristicasEspecialesDAO.getIdCaracterisacion(_AutorizaciondePagoEntities.IDCARACTERISTICASESPECIALES);
                    MAESTRO_MODALIDAD _maestroModalidad = maestroModalidadDAO.get(_caracteristicasEspeciales.IDMAESTROMODALIDAD);
                    _AutorizaciondePagoEntities.NOMBREMAESTROMODALIDAD = _maestroModalidad.NOMBREMAESTROMODALIDAD;
                    MAESTRO_SUB_MODALIDAD _maestroSubModalidad = maestroSubModalidadDAO.get(_caracteristicasEspeciales.IDMAESTROSUBMODALIDAD);
                    _AutorizaciondePagoEntities.NOMBREMAESTROSUBMODALIDAD = _maestroSubModalidad.NOMBREMAESTROSUBMODALIDAD;
                    _AutorizaciondePagoEntities.CLASE = item.CLASE.ToString();
                    _AutorizaciondePagoEntities.S1 = item.S1;
                    _AutorizaciondePagoEntities.S2 = item.S2;
                    _AutorizaciondePagoEntities.S3 = item.S3;
                    _AutorizaciondePagoEntities.S4 = item.S4;
                    _AutorizaciondePagoEntities.S5 = item.S5;
                    _AutorizaciondePagoEntities.S6 = item.S6;
                    _AutorizaciondePagoEntities.S7 = item.S7;
                    _AutorizaciondePagoEntities.S8 = item.S8;
                    _AutorizaciondePagoEntities.S9 = item.S9;
                    _AutorizaciondePagoEntities.S10 = item.S10;
                    _AutorizaciondePagoEntities.MONTOFTO = item.MONTOFTO;
                    _AutorizaciondePagoEntities.SALDOFTO = item.SALDOFTO;
                    _AutorizaciondePagoEntities.MONTOTOTALPROYECTO = Convert.ToDecimal(item.MONTOTOTALPROYECTO);
                    _AutorizaciondePagoEntities.MONTOAPAGAR = Convert.ToDecimal(item.MontoaPagar);
                    _AutorizaciondePagoEntities.idSolicitudPago = Convert.ToInt64(item.IDSOLICITUDPAGO);

                    lstDetAutPagoDetalle.Add(_AutorizaciondePagoEntities);
                }
                return lstDetAutPagoDetalle;
            }
            else
                return null;
        }
    }
}

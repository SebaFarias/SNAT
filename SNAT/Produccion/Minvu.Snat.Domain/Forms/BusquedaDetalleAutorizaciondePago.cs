using Minvu.Snat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Forms
{
    public class BusquedaDetalleAutorizaciondePago
    {
        public BusquedaDetalleAutorizaciondePago() { }
        
        public DetalleAutorizaciondePagoResumenEntities _DetalleAutorizaciondePagoResumenEntities { get; set; }
        public DetalleAutorizaciondePagoDetalleEntities _DetalleAutorizaciondePagoDetalleEntities { get; set; }
        public solicitudAutorizacionEntities _solicitudAutorizacionEntities { get; set; }
        public List<solicitudPagoEntities> _listSolicitudPagoEntities { get; set; }
        public List<solicitudAutorizacionEntities> _listSolicitudAutorizacionEntities { get; set; }
        public DetalleAutorizacionCompletaEntities _DetalleAutorizacionCompletaEntities { get; set; }
        public List<DetalleAutorizacionCompletaEntities> _listDetalleAutorizacionCompletaEntities { get; set; }

        public List<DetalleAutorizacionPDF> _grillaPDF{ get; set; }
        public List<DetalleAutorizacionCompletaEntities> _listSolicitudes { get; set; }

        public List<DetalleAutorizaciondePagoDetalleEntities> lstDetalleAutorizaciondePagoDetalle { get; set; }
        public List<string> listaNombres { get; set; }
    }

    public class BusquedaDetalleAutorizaciondePagoFactory
    {
        public static List<DetalleAutorizaciondePagoDetalleEntities> getListDetalleAutorizacionesDetalleResult(long? IdAutorizacion, long? IdTipoProveedor)
        {
            List<DetalleAutorizaciondePagoDetalleEntities> _lstDetalleAutorizacionesDetalleResult = new List<DetalleAutorizaciondePagoDetalleEntities>();
            _lstDetalleAutorizacionesDetalleResult = DetalleAutorizaciondePagoDetalleEntitiesFactory.getListDetalleAutorizacionesDePagoGrilla(IdAutorizacion, IdTipoProveedor);

            return _lstDetalleAutorizacionesDetalleResult;
        }

        public static DetalleAutorizaciondePagoResumenEntities getDetalleAutorizacionesResumenResult(long? IdAutorizacion)
        {
            DetalleAutorizaciondePagoResumenEntities objDetalleAutorizaciondePagoResumen = new DetalleAutorizaciondePagoResumenEntities();
            objDetalleAutorizaciondePagoResumen = DetalleAutorizaciondePagoResumenEntitiesFactory.getDetalleAutorizaciondePagoResumen(IdAutorizacion);

            int contadorInicial = objDetalleAutorizaciondePagoResumen.NOMBREREGIONDIRECCION.Length;
            string Region = "";

            Region = objDetalleAutorizaciondePagoResumen.NOMBREREGIONDIRECCION.ToString().Replace("Región del ", "");

            if (contadorInicial == Region.Length)
                Region = objDetalleAutorizaciondePagoResumen.NOMBREREGIONDIRECCION.ToString().Replace("Región de ", "");

            if (contadorInicial == Region.Length)
                Region = objDetalleAutorizaciondePagoResumen.NOMBREREGIONDIRECCION.ToString().Replace("Región ", "");

            objDetalleAutorizaciondePagoResumen.NOMBREREGIONDIRECCION = Region;

            return objDetalleAutorizaciondePagoResumen;
        }

        public static List<DetalleAutorizacionCompletaEntities> getListDetalleAutorizacionCompleta(long? IdAutorizacion)
        {
            List<DetalleAutorizacionCompletaEntities> objDetalleAutorizacionCompletaEntitiesn = new List<DetalleAutorizacionCompletaEntities>();
            objDetalleAutorizacionCompletaEntitiesn = DetalleAutorizacionCompletaEntitiesFactory.getListDetalleAutorizacionCompleta(IdAutorizacion);

            return objDetalleAutorizacionCompletaEntitiesn;
        }

        public static void deleteSolicitudAutorizacion(long numeroAutorizacion)
        {
            solicitudAutorizacionEntitiesFactory.delete(numeroAutorizacion);
        }

        public static void saveSolicitudAutorizacion(List<solicitudAutorizacionEntities> _list)
        {
            solicitudAutorizacionEntitiesFactory.save(_list);
        }
        public static string updateSolicitudRechazo(string[] Solicitudes)
        {
            string sReturn = String.Empty;

            return sReturn;
        }
    }
}

using Minvu.Snat.IData.DAO;
using System.Security.Authentication; //Agregar usuario 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class DetalleAutorizaciondePagoResumenEntities
    {
        public long IDAUTORIZACION { get; set; }
        public int CODIGOREGIONDIRECCION { get; set; }
        public string NOMBREREGIONDIRECCION { get; set; }
        public long NROAUTORIZACION { get; set; }
        public DateTime? FECHAINGRESOAUTORIZACION { get; set; }
        public int NROPROYECTOS { get; set; }
        public string NOMBREMAESTROLLAMADO { get; set; }
        public int CANTIDADSOLICITUDES { get; set; }
        public string NOMBREPROVEEDOR { get; set; }
        public string RUTPROVEEDOR { get; set; }
        public string NOMBREMAESTROESTADOAUTORIZACION { get; set; }
        public decimal MONTOTOTALAUTORIZACION { get; set; }
        public string NOMBREMAESTROTIPOPROVEEDOR { get; set; }
        public string USUARIORESPONSABLESOLICITUDPAGO { get; set; }
        public string NOMBREPROGRAMA { get; set; }
        public string USUARIORESPONSABLEAUTORIZACION { get; set; }

        public DetalleAutorizaciondePagoResumenEntities()
        {
            IDAUTORIZACION = 0;
            CODIGOREGIONDIRECCION = 0;
            NOMBREREGIONDIRECCION = String.Empty;
            NROAUTORIZACION = 0;
            FECHAINGRESOAUTORIZACION = null;
            NROPROYECTOS = 0;
            NOMBREMAESTROLLAMADO = String.Empty;
            CANTIDADSOLICITUDES = 0;
            NOMBREPROVEEDOR = String.Empty;
            RUTPROVEEDOR = String.Empty;
            NOMBREMAESTROESTADOAUTORIZACION = String.Empty;
            MONTOTOTALAUTORIZACION = 0;
            NOMBREMAESTROTIPOPROVEEDOR = String.Empty;
            USUARIORESPONSABLESOLICITUDPAGO = String.Empty;
            USUARIORESPONSABLEAUTORIZACION = string.Empty;
            NOMBREPROGRAMA = String.Empty;
        }
    }

    public class DetalleAutorizaciondePagoResumenEntitiesFactory
    {
   

        internal static DetalleAutorizaciondePagoResumenEntities getDetalleAutorizaciondePagoResumen(long? IdAutorizacion)
        {
            var _DetalleAutorizaciondePagoDAO = DetalleAutorizaciondePagoDAO.GetDetalleAutorizacionesDePago(IdAutorizacion);

            if (_DetalleAutorizaciondePagoDAO != null)
            {
                try
                {
                    return new DetalleAutorizaciondePagoResumenEntities
                    {

                        IDAUTORIZACION = Convert.ToInt64(_DetalleAutorizaciondePagoDAO.IDAUTORIZACION),
                        CODIGOREGIONDIRECCION = Convert.ToInt32(_DetalleAutorizaciondePagoDAO.CODIGOREGIONDIRECCION),
                        NOMBREREGIONDIRECCION = regionDAO.obtenerNombreRegion(Convert.ToInt32(_DetalleAutorizaciondePagoDAO.CODIGOREGIONDIRECCION)),
                        NROAUTORIZACION = Convert.ToInt64(_DetalleAutorizaciondePagoDAO.NROAUTORIZACION),
                        FECHAINGRESOAUTORIZACION = Convert.ToDateTime(_DetalleAutorizaciondePagoDAO.FECHAINGRESOAUTORIZACION),
                        NROPROYECTOS = Convert.ToInt32(_DetalleAutorizaciondePagoDAO.NROPROYECTOS),
                        NOMBREMAESTROLLAMADO = _DetalleAutorizaciondePagoDAO.NOMBREMAESTROLLAMADO.ToString(),
                        CANTIDADSOLICITUDES = Convert.ToInt32(_DetalleAutorizaciondePagoDAO.CANTIDADSOLICITUDES),
                        NOMBREPROVEEDOR = _DetalleAutorizaciondePagoDAO.NOMBREPROVEEDOR.ToString(),
                        RUTPROVEEDOR = _DetalleAutorizaciondePagoDAO.RutProveedor.ToString(),
                        NOMBREMAESTROESTADOAUTORIZACION = _DetalleAutorizaciondePagoDAO.NOMBREMAESTROESTADOAUTORIZACION.ToString(),
                        MONTOTOTALAUTORIZACION = Convert.ToDecimal(_DetalleAutorizaciondePagoDAO.MONTOTOTALAUTORIZACION),
                        NOMBREMAESTROTIPOPROVEEDOR = _DetalleAutorizaciondePagoDAO.NOMBREMAESTROTIPOPROVEEDOR.ToString(),
                        USUARIORESPONSABLESOLICITUDPAGO = _DetalleAutorizaciondePagoDAO.USUARIORESPONSABLESOLICITUDPAGO.ToString(),
                        USUARIORESPONSABLEAUTORIZACION = FuncionarioEntitiesFactory.getFuncionarioNombreCompleto(_DetalleAutorizaciondePagoDAO.USUARIORESPONSABLEAUTORIZACION.ToString()),
                        NOMBREPROGRAMA = _DetalleAutorizaciondePagoDAO.NOMBREMAESTROPROGRAMA.ToString(),
                    };
                }
                catch
                {
                    try
                    {
                        return new DetalleAutorizaciondePagoResumenEntities
                        {

                            IDAUTORIZACION = Convert.ToInt64(_DetalleAutorizaciondePagoDAO.IDAUTORIZACION),
                            CODIGOREGIONDIRECCION = Convert.ToInt32(_DetalleAutorizaciondePagoDAO.CODIGOREGIONDIRECCION),
                            NOMBREREGIONDIRECCION = regionDAO.obtenerNombreRegion(Convert.ToInt32(_DetalleAutorizaciondePagoDAO.CODIGOREGIONDIRECCION)),
                            NROAUTORIZACION = Convert.ToInt64(_DetalleAutorizaciondePagoDAO.NROAUTORIZACION),
                            FECHAINGRESOAUTORIZACION = Convert.ToDateTime(_DetalleAutorizaciondePagoDAO.FECHAINGRESOAUTORIZACION),
                            NROPROYECTOS = Convert.ToInt32(_DetalleAutorizaciondePagoDAO.NROPROYECTOS),
                            NOMBREMAESTROLLAMADO = _DetalleAutorizaciondePagoDAO.NOMBREMAESTROLLAMADO.ToString(),
                            CANTIDADSOLICITUDES = Convert.ToInt32(_DetalleAutorizaciondePagoDAO.CANTIDADSOLICITUDES),
                            NOMBREPROVEEDOR = _DetalleAutorizaciondePagoDAO.NOMBREPROVEEDOR.ToString(),
                            RUTPROVEEDOR = _DetalleAutorizaciondePagoDAO.RutProveedor.ToString(),
                            NOMBREMAESTROESTADOAUTORIZACION = _DetalleAutorizaciondePagoDAO.NOMBREMAESTROESTADOAUTORIZACION.ToString(),
                            MONTOTOTALAUTORIZACION = Convert.ToDecimal(_DetalleAutorizaciondePagoDAO.MONTOTOTALAUTORIZACION),
                            NOMBREMAESTROTIPOPROVEEDOR = _DetalleAutorizaciondePagoDAO.NOMBREMAESTROTIPOPROVEEDOR.ToString(),
                            USUARIORESPONSABLESOLICITUDPAGO = _DetalleAutorizaciondePagoDAO.USUARIORESPONSABLESOLICITUDPAGO.ToString(),
                            USUARIORESPONSABLEAUTORIZACION = " ",
                            NOMBREPROGRAMA = _DetalleAutorizaciondePagoDAO.NOMBREMAESTROPROGRAMA.ToString(),
                        };
                    }
                    catch
                    {
                        return null;
                    }

                }
               //USUARIORESPONSABLEAUTORIZACION =  FuncionarioEntitiesFactory.getFuncionarioNombreCompleto(_DetalleAutorizaciondePagoDAO.USUARIORESPONSABLEAUTORIZACION.ToString()),

            }
            else
                return null;
        }

        internal static string updateSolicitudRechazo(string[] Solicitudes)
        {
            string sReturn = String.Empty;

            return sReturn;
        }
    }
}


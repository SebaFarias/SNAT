using Minvu.Snat.IData.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class autorizaciondePagoEntities
    {
        public int? NROAUTORIZACION { get; set; }
        public long? IDAUTORIZACION { get; set; }
        public int? CODIGOREGIONDIRECCION { get; set; }
        public int? NROPROYECTOS { get; set; }
        public string NOMBREMAESTROPROGRAMA { get; set; }
        public string NOMBREMAESTROTIPOLOGIA { get; set; }
        public string NOMBREMAESTROMODALIDAD { get; set; }
        public string CLASE { get; set; }
        public string NOMBREMAESTROTIPOPROVEEDOR { get; set; }
        public string RUTPROVEEDOR { get; set; }
        public string NOMBREPROVEEDOR { get; set; }
        public long? MONTOTOTALAUTORIZACION { get; set; }
        public string NOMBREMAESTROESTADOAUTORIZACION { get; set; }
        public string STRINGSOLICITUD { get; set; }
        public string STRINGPROYECTO { get; set; }
        public long IDMAESTROMODALIDAD { get; set; }
        public long IDMAESTROTITULO { get; set; }
        public string mensajeSalida { get; set; }
        public string codigoSalida { get; set; }

        public autorizaciondePagoEntities()
        {
            NROAUTORIZACION = 0;
            IDAUTORIZACION = 0;
            CODIGOREGIONDIRECCION = 0;
            NROPROYECTOS = 0;
            NOMBREMAESTROPROGRAMA = String.Empty;
            NOMBREMAESTROTIPOLOGIA = String.Empty;
            NOMBREMAESTROMODALIDAD = String.Empty;
            CLASE = String.Empty;
            NOMBREMAESTROTIPOPROVEEDOR = String.Empty;
            RUTPROVEEDOR = String.Empty;
            NOMBREPROVEEDOR = String.Empty;
            MONTOTOTALAUTORIZACION = 0;
            NOMBREMAESTROESTADOAUTORIZACION = String.Empty;
            STRINGSOLICITUD = String.Empty;
            STRINGPROYECTO = String.Empty;
            IDMAESTROMODALIDAD = 0;
            IDMAESTROTITULO = 0;
            mensajeSalida = String.Empty;
            codigoSalida = String.Empty;
        }
    }

    public class AutorizaciondePagoEntitiesFactory
    {
        internal static List<autorizaciondePagoEntities> getListAutorizaciondePago(int? CODIGOPROYECTO, long? IDMAESTROPROGRAMA, long? IDMAESTROTIPOLOGIA,
                                                                                    long? IDAUTORIZACION, string NOMBREPROVEEDOR, long? IDMAESTROMODALIDAD,
                                                                                    long? IDMAESTROLLAMADO, int? CODIGOREGIONDIRECCION, int? CODIGOPROVINCIADIRECCION,
                                                                                    int? CODIGOCOMUNADIRECCION, long? IDMAESTROSERVICIO, long? IDMAESTROTIPOPROVEEDOR,
                                                                                    long? IDMAESTROTITULO, long? IDMAESTROESTADOAUTORIZACION)
        {
            var _listAutorizaciondePagoDAO = AutorizaciondePagoDAO.GetAutorizacionesDePago(CODIGOPROYECTO, IDMAESTROPROGRAMA, IDMAESTROTIPOLOGIA,
                                                                                            IDAUTORIZACION, NOMBREPROVEEDOR, IDMAESTROMODALIDAD,
                                                                                            IDMAESTROLLAMADO, CODIGOREGIONDIRECCION, CODIGOPROVINCIADIRECCION,
                                                                                            CODIGOCOMUNADIRECCION, IDMAESTROSERVICIO, IDMAESTROTIPOPROVEEDOR,
                                                                                            IDMAESTROTITULO, IDMAESTROESTADOAUTORIZACION);

            List<autorizaciondePagoEntities> _auxListMaestroLlamadoEntities = new List<autorizaciondePagoEntities>();

            if (_listAutorizaciondePagoDAO != null)
            {
                foreach (var item in _listAutorizaciondePagoDAO)
                {
                    autorizaciondePagoEntities _AutorizaciondePagoEntities = new autorizaciondePagoEntities();

                    _AutorizaciondePagoEntities.NROAUTORIZACION = item.NROAUTORIZACION;
                    _AutorizaciondePagoEntities.IDAUTORIZACION = item.IDAUTORIZACION;
                    _AutorizaciondePagoEntities.CODIGOREGIONDIRECCION = item.CODIGOREGIONDIRECCION;
                    _AutorizaciondePagoEntities.NROPROYECTOS = item.NROPROYECTOS;
                    _AutorizaciondePagoEntities.NOMBREMAESTROPROGRAMA = item.NOMBREMAESTROPROGRAMA;
                    _AutorizaciondePagoEntities.NOMBREMAESTROTIPOLOGIA = item.NOMBREMAESTROTIPOLOGIA;
                    _AutorizaciondePagoEntities.NOMBREMAESTROMODALIDAD = item.NOMBREMAESTROMODALIDAD;
                    _AutorizaciondePagoEntities.CLASE = item.CLASE;
                    _AutorizaciondePagoEntities.NOMBREMAESTROTIPOPROVEEDOR = item.NOMBREMAESTROTIPOPROVEEDOR;
                    _AutorizaciondePagoEntities.RUTPROVEEDOR = item.RUTPROVEEDOR;
                    _AutorizaciondePagoEntities.NOMBREPROVEEDOR = item.NOMBREPROVEEDOR;
                    _AutorizaciondePagoEntities.MONTOTOTALAUTORIZACION = item.MONTOTOTALAUTORIZACION;
                    _AutorizaciondePagoEntities.NOMBREMAESTROESTADOAUTORIZACION = item.NOMBREMAESTROESTADOAUTORIZACION;
                    _AutorizaciondePagoEntities.STRINGSOLICITUD = item.STRINGSOLICITUD;
                    _AutorizaciondePagoEntities.STRINGPROYECTO = item.STRINGPROYECTO;

                    _auxListMaestroLlamadoEntities.Add(_AutorizaciondePagoEntities);
                }
                return _auxListMaestroLlamadoEntities;
            }
            else
                return null;
        }
        internal static autorizaciondePagoEntities getAutorizaciondePago(int? IdAutorizacion)
        {
            var _listAutorizaciondePagoDAO = AutorizaciondePagoDAO.GetAutorizacionesDePagoById(IdAutorizacion);

            if (_listAutorizaciondePagoDAO != null)
            {
                return new autorizaciondePagoEntities
                {
                    NROAUTORIZACION = Convert.ToInt32(_listAutorizaciondePagoDAO.NROAUTORIZACION),
                    IDAUTORIZACION = Convert.ToInt64(_listAutorizaciondePagoDAO.IDAUTORIZACION),
                    CODIGOREGIONDIRECCION = Convert.ToInt32(_listAutorizaciondePagoDAO.CODIGOREGIONDIRECCION),
                    NROPROYECTOS = Convert.ToInt32(_listAutorizaciondePagoDAO.NROPROYECTOS),
                    NOMBREMAESTROPROGRAMA = _listAutorizaciondePagoDAO.NOMBREMAESTROPROGRAMA.ToString(),
                    NOMBREMAESTROTIPOLOGIA = _listAutorizaciondePagoDAO.NOMBREMAESTROTIPOLOGIA.ToString(),
                    NOMBREMAESTROMODALIDAD = _listAutorizaciondePagoDAO.NOMBREMAESTROMODALIDAD.ToString(),
                    CLASE = _listAutorizaciondePagoDAO.CLASE.ToString(),
                    NOMBREMAESTROTIPOPROVEEDOR = _listAutorizaciondePagoDAO.NOMBREMAESTROTIPOPROVEEDOR.ToString(),
                    RUTPROVEEDOR = _listAutorizaciondePagoDAO.RUTPROVEEDOR.ToString(),
                    NOMBREPROVEEDOR = _listAutorizaciondePagoDAO.NOMBREPROVEEDOR.ToString(),
                    MONTOTOTALAUTORIZACION = Convert.ToInt64(_listAutorizaciondePagoDAO.MONTOTOTALAUTORIZACION),
                    NOMBREMAESTROESTADOAUTORIZACION = _listAutorizaciondePagoDAO.NOMBREMAESTROESTADOAUTORIZACION.ToString(),
                    STRINGSOLICITUD = _listAutorizaciondePagoDAO.STRINGSOLICITUD.ToString(),
                    STRINGPROYECTO = _listAutorizaciondePagoDAO.STRINGPROYECTO.ToString()
                };
            }
            else
                return null;
        }

        internal static autorizaciondePagoEntities InsertaGeneracionSolicitudPago(string idSolicitudCadena, string UsuarioResponsable)
        {
            var objResultado = AutorizaciondePagoDAO.InsertaGeneracionSolicitudPago(idSolicitudCadena, UsuarioResponsable);

            autorizaciondePagoEntities objAutorizacionPago = new autorizaciondePagoEntities();
            if (objResultado != null)
            {
                return new autorizaciondePagoEntities
                {
                    mensajeSalida = objResultado.MSG,
                    codigoSalida = objResultado.ERR.ToString()
                };
            }
            else
                return objAutorizacionPago;
        }

        internal static long ActualizaEstadoAutorizacion(long idAutorizacion)
        {
            return AutorizaciondePagoDAO.ActualizaEstadoAutorizacion(idAutorizacion);
        }
    }
}

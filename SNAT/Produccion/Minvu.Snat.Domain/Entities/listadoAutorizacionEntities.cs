using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.DAO;
using Minvu.Snat.IData.ORM;
using System.ComponentModel.DataAnnotations;
namespace Minvu.Snat.Domain.Entities
{
    public class listadoAutorizacionEntities
    {
        public long? numeroAutorizacion { get; set; }
        public long? idAutorizacion { get; set; }
        public long? codigoRegion { get; set; }
        public long? cantidadProyecto { get; set; }
        public string nombrePrograma { get; set; }
        public string nombreEstadoAutorizacion { get; set; }
        public string nombreTipologia { get; set; }
        public long? IdTipoProveedor { get; set; }
        public string nombreTipoProveedor { get; set; }
        public string rutProveedor { get; set; }
        public string nombreProveedor { get; set; }
        public string nombreRegion { get; set; }
        public string FechaIngresoAutorizacion { get; set; }
        public decimal? montoTotalAutorizacion { get; set; }
        public bool especialAutorizacion { get; set; }
        public string MontoEntero { get; set; }
        public string MontoDecimal { get; set; }


        public listadoAutorizacionEntities()
        {
            numeroAutorizacion = null;
            idAutorizacion = null;
            codigoRegion = null;
            cantidadProyecto = null;
            nombrePrograma = String.Empty;
            nombreEstadoAutorizacion = String.Empty;
            nombreTipologia = String.Empty;
            IdTipoProveedor = null;
            nombreTipoProveedor = String.Empty;
            rutProveedor = String.Empty;
            nombreProveedor = String.Empty;
            nombreRegion = String.Empty;
            FechaIngresoAutorizacion = String.Empty;
            montoTotalAutorizacion = 0;
            especialAutorizacion = false;
            MontoEntero = String.Empty;
            MontoDecimal = String.Empty;
        }

        public listadoAutorizacionEntities(long? _numeroAUtorizacion, long? _idAutorizacion, long? _codigoRegion, long? _cantidadProyecto, string _nombrePrograma, 
                                           string _nombreTipologia, long? _IdTipoProveedor, string _nombreTipoProveedor, string _rutProveedor, string _nombreProveedor,
                                           decimal? _montoTotalAutorizacion, bool _especialAutorizacion, string _nombreEstadoAutorizacion, string _FechaIngresoAutorizacion)
        {
            numeroAutorizacion = _numeroAUtorizacion;
            idAutorizacion = _idAutorizacion;
            codigoRegion = _codigoRegion;
            cantidadProyecto = _cantidadProyecto;
            nombrePrograma = _nombrePrograma;
            nombreTipologia = _nombreTipologia;
            IdTipoProveedor = _IdTipoProveedor;
            nombreTipoProveedor = _nombreTipoProveedor;
            rutProveedor = _rutProveedor;
            nombreProveedor = _nombreProveedor;
            montoTotalAutorizacion = _montoTotalAutorizacion;
            especialAutorizacion = _especialAutorizacion;
            nombreEstadoAutorizacion = _nombreEstadoAutorizacion;
            FechaIngresoAutorizacion = _FechaIngresoAutorizacion;
            if (montoTotalAutorizacion != null)
            {
                string[] montoSplit = montoTotalAutorizacion.ToString().Split(',');
                MontoEntero = Convert.ToInt32(montoSplit[0]).ToString("N0");
                MontoDecimal = montoSplit[1];
            }
        }
    }

    public class listadoAutorizacionsFactory
    {
        public static List<listadoAutorizacionEntities> obtenerListadoAutorizacion(string pCodProyecto, long? pIdMaestroPrograma, long? pIdMaestroTIpologia, long? pIdMaestroLLamado,
                                                                                long? pIdMaestroTIpoProveedor, string pNombreProveedor, long? pIdServicio, long? pRegion,
                                                                                long? pProvincia, long? pComuna, long? pIdModalidad, long? pIdAutorizacion,
                                                                                long? pIdMaestroTitulo, long? pIdMaestroEstadoAutorizacion)
        {
            List<SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result> _objAutResult = new List<SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result>();
            List<listadoAutorizacionEntities> _listadoAutorizacionEntities = new List<listadoAutorizacionEntities>();
            _objAutResult = listadoAutorizacionDAO.obtenerListadoAutorizacion(pCodProyecto, pIdMaestroPrograma, pIdMaestroTIpologia, pIdMaestroLLamado,
                                                                    pIdMaestroTIpoProveedor, pNombreProveedor, pIdServicio, pRegion,
                                                                    pProvincia, pComuna, pIdModalidad, pIdAutorizacion,
                                                                    pIdMaestroTitulo, pIdMaestroEstadoAutorizacion);

            foreach (var item in _objAutResult)
            {
                listadoAutorizacionEntities _auxlistadoAutorizacionEntities = new listadoAutorizacionEntities();

                _auxlistadoAutorizacionEntities.cantidadProyecto = item.CANTIDADPROYECTOSAUTORIZACION;
                _auxlistadoAutorizacionEntities.codigoRegion = item.CODIGOREGIONAUTORIZACION;
                _auxlistadoAutorizacionEntities.especialAutorizacion = Convert.ToBoolean(item.ESPECIALAUTORIZACION);
                _auxlistadoAutorizacionEntities.nombreRegion = regionDAO.obtenerNombreRegion(Convert.ToInt32(item.CODIGOREGIONAUTORIZACION));
                _auxlistadoAutorizacionEntities.idAutorizacion = item.IDAUTORIZACION;
                _auxlistadoAutorizacionEntities.montoTotalAutorizacion = item.MONTOTOTALAUTORIZACION;
                _auxlistadoAutorizacionEntities.nombrePrograma = item.PROGRAMA;
                _auxlistadoAutorizacionEntities.nombreProveedor = item.NOMBREPROVEEDOR;
                _auxlistadoAutorizacionEntities.nombreTipologia = item.TIPOLOGIA;
                _auxlistadoAutorizacionEntities.nombreTipoProveedor = item.TIPO_PROVEEDOR;
                _auxlistadoAutorizacionEntities.numeroAutorizacion = item.NUMEROAUTORIZACION;
                _auxlistadoAutorizacionEntities.rutProveedor = item.RUT_PROVEEDOR;
                _auxlistadoAutorizacionEntities.nombreEstadoAutorizacion = item.NOMBREMAESTROESTADOAUTORIZACION;
                _auxlistadoAutorizacionEntities.FechaIngresoAutorizacion = item.FECHAINGRESOAUTORIZACION.ToString();
                _auxlistadoAutorizacionEntities.IdTipoProveedor = item.ID_TIPO_PROVEEDOR;
                if (_auxlistadoAutorizacionEntities.montoTotalAutorizacion != null)
                {
                    string[] montoSplit = _auxlistadoAutorizacionEntities.montoTotalAutorizacion.ToString().Split(',');
                    _auxlistadoAutorizacionEntities.MontoEntero = Convert.ToInt32(montoSplit[0]).ToString("N0");
                    _auxlistadoAutorizacionEntities.MontoDecimal = montoSplit[1];
                }

                _listadoAutorizacionEntities.Add(_auxlistadoAutorizacionEntities);
            }

            return _listadoAutorizacionEntities;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
    public class listadoAutorizacionDAO
    {

        public long? numeroAUtorizacion { get; set; }
        public long? idAutorizacion { get; set; }
        public long? codigoRegion { get; set; }
        public long? cantidadProyecto { get; set; }
        public string nombrePrograma { get; set; }
        public string nombreTipologia { get; set; }
        public string nombreTipoProveedor { get; set; }
        public string rutProveedor { get; set; }
        public string nombreProveedor { get; set; }
        public decimal? montoTotalAutorizacion { get; set; }
        public bool especialAutorizacion { get; set; }
        public string FechaIngresoAutorizacion { get; set; }

        public listadoAutorizacionDAO()
        {
            numeroAUtorizacion = null;
            idAutorizacion = null;
            codigoRegion = null;
            cantidadProyecto = null;
            nombrePrograma = string.Empty;
            nombreTipologia = string.Empty;
            nombreTipoProveedor = string.Empty;
            rutProveedor = string.Empty;
            nombreProveedor = string.Empty;
            montoTotalAutorizacion = 0;
            especialAutorizacion = false;
            FechaIngresoAutorizacion = String.Empty;
        }


        public static List<SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result> obtenerListadoAutorizacion(string pCodProyecto,long? pIdMaestroPrograma, long? pIdMaestroTIpologia, long? pIdMaestroLLamado, long? pIdMaestroTIpoProveedor, string pNombreProveedor, long? pIdServicio, long? pRegion, long? pProvincia, long? pComuna, long? pIdModalidad, long? pIdAutorizacion, long? pIdMaestroTitulo, long? pIdMaestroEstadoAutorizacion)
        {
           List<SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result> list_SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result  = new List<SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
               var qListadoAutorizacion = contexto.SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION(pCodProyecto, pIdMaestroPrograma, pIdMaestroTIpologia, pIdMaestroLLamado, pIdMaestroTIpoProveedor, pNombreProveedor, pIdServicio, pRegion, pProvincia, pComuna, pIdModalidad, pIdAutorizacion, pIdMaestroTitulo, pIdMaestroEstadoAutorizacion );

                foreach (var item in qListadoAutorizacion)
                {
                    SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result _SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result = new SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result();

                    _SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result.CANTIDADPROYECTOSAUTORIZACION = item.CANTIDADPROYECTOSAUTORIZACION;
                    _SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result.CODIGOREGIONAUTORIZACION = item.CODIGOREGIONAUTORIZACION;
                    _SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result.ESPECIALAUTORIZACION = item.ESPECIALAUTORIZACION;
                    _SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result.IDAUTORIZACION = item.IDAUTORIZACION;
                    _SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result.MONTOTOTALAUTORIZACION = item.MONTOTOTALAUTORIZACION;
                    _SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result.NOMBREMAESTROESTADOAUTORIZACION = item.NOMBREMAESTROESTADOAUTORIZACION;
                    _SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result.NOMBREPROVEEDOR = item.NOMBREPROVEEDOR;
                    _SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result.NUMEROAUTORIZACION = item.NUMEROAUTORIZACION;
                    _SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result.PROGRAMA = item.PROGRAMA;
                    _SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result.RUT_PROVEEDOR = item.RUT_PROVEEDOR;
                    _SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result.TIPOLOGIA = item.TIPOLOGIA;
                    _SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result.TIPO_PROVEEDOR = item.TIPO_PROVEEDOR;
                    _SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result.FECHAINGRESOAUTORIZACION = item.FECHAINGRESOAUTORIZACION;
                    _SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result.ID_TIPO_PROVEEDOR = item.ID_TIPO_PROVEEDOR;

                    list_SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result.Add(_SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result);
                }

                return list_SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION_Result;
            }
        }
    }
}

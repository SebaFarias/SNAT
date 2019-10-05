using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.DAO;
using System.ComponentModel.DataAnnotations;


namespace Minvu.Snat.Domain.Entities
{
    public class informacionPresupuestoRegionalEntities
    {
        [Required(ErrorMessage = "El Número de Resolución es Obligatorio")]
        [RegularExpression(@"(^[0-9]*$)", ErrorMessage = "Existen caracteres inválidos.")]
        public long idPresupuestoRegional { get; set; }
        public int idResolucionPresupuestaria { get; set; }
        public DateTime fechaResolucionPresupuestoRegional { get; set; }
        public int annoPresupuesto { get; set; }
        public long montoPresupuestoRegional { get; set; }
        public int codigoRegionPresupuestoRegional { get; set; }
        public int montoComprometidoSAT { get; set; }
        public int montoComprometidoSET { get; set; }
        public int montoPagadoSAT { get; set; }
        public int montoPagadoSET { get; set; }
        public string mensajeSalida { get; set; }
        public string codigoSalida { get; set; }
        public long suma { get; set; }
        public string vigencia { get; set; }
        public string nombreArchivo { get; set; }
        public long montoContatoSat { get; set; }
        public long montoContatoSet { get; set; }
        public long presupuestoDisponible { get; set; }
       

        public informacionPresupuestoRegionalEntities()
        {
            idPresupuestoRegional = 0;
            idResolucionPresupuestaria = 0;
            fechaResolucionPresupuestoRegional = new DateTime();
            annoPresupuesto = 0;
            montoPresupuestoRegional = 0;
            codigoRegionPresupuestoRegional = 0;
            montoComprometidoSAT = 0;
            montoComprometidoSET = 0;
            montoPagadoSAT = 0;
            montoPagadoSET = 0;
            mensajeSalida = string.Empty;
            codigoSalida = string.Empty;
            montoContatoSat = 0;
            montoContatoSet = 0;
            presupuestoDisponible = 0;
           
        }
        public informacionPresupuestoRegionalEntities(long _idPresupuestoRegional, int _idResolucionPresupuestaria, DateTime _fechaResolucionPresupuestoRegional,
            int _annoPresupuesto, long _montoPresupuestoRegional, int _codigoRegionPresupuestoRegional, int _montoComprometidoSAT, int _montoComprometidoSET,
            int _montoPagadoSAT, int _montoPagadoSET, long _montoContatoSat, long _montoContatoSet , long _presupuestoDisponible , long _satSolicitudesComprometidas ,
            long _setSolicitudesComprometidas , long _setSolicitudesPagadas , long _satSolicitudesPagadas            )
        {
            idPresupuestoRegional = _idPresupuestoRegional;
            idResolucionPresupuestaria = _idResolucionPresupuestaria;
            fechaResolucionPresupuestoRegional = _fechaResolucionPresupuestoRegional;
            annoPresupuesto = _annoPresupuesto;
            montoPresupuestoRegional = _montoPresupuestoRegional;
            codigoRegionPresupuestoRegional = _codigoRegionPresupuestoRegional;
            montoComprometidoSAT = _montoComprometidoSAT;
            montoComprometidoSET = _montoComprometidoSET;
            montoPagadoSAT = _montoPagadoSAT;
            montoPagadoSET = _montoPagadoSET;
            montoContatoSat = _montoContatoSat;
            montoContatoSet = _montoContatoSet;
            presupuestoDisponible = _presupuestoDisponible;
           
        }
    }

    //public class informacionPresupuestoRegionalEntitiesFactory
    //{
    //    internal static List<presupuestoRegionalEntities> obtienePresupuestoRegional(int accion, int anno, int numeroResolucion, DateTime fechaResolucion, int codigoRegion)
    //    {
    //        var _obtienePresupuestoRegional = presupuestoRegionalDAO.ObtienePresupuestoRegional(accion, anno, numeroResolucion, fechaResolucion, codigoRegion);
    //        List<presupuestoRegionalEntities> informacionPresupuestoRegionalEntities = new List<presupuestoRegionalEntities>();
    //        if (_obtienePresupuestoRegional != null)
    //        {
    //            foreach (var a in _obtienePresupuestoRegional)
    //            {
    //                presupuestoRegionalEntities info = new presupuestoRegionalEntities();

    //                info.idResolucionPresupuestaria = Convert.ToInt32(a.NUMERORESOLUCIONPRESUPUESTARIA);
    //                info.annoPresupuesto = Convert.ToInt32(a.ANORESOLUCIONPRESUPUESTARIA);
    //                info.codigoRegionPresupuestoRegional = Convert.ToInt32(a.CODIGOREGIONPRESUPUESTOREGIONAL);
    //                info.montoPresupuestoRegional = Convert.ToInt64(a.MONTOPRESUPUESTOREGIONAL);
    //                info.fechaResolucionPresupuestoRegional = Convert.ToDateTime(a.FECHAPRESUPUESTOREGIONAL);

    //                informacionPresupuestoRegionalEntities.Add(info);
    //            }
    //            return informacionPresupuestoRegionalEntities;                
    //        }
    //        else
    //            return informacionPresupuestoRegionalEntities;
    //    }
    //}
}
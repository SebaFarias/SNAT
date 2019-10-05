using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.DAO;
using System.ComponentModel.DataAnnotations;


namespace Minvu.Snat.Domain.Entities
{
    public class presupuestoRegionalEntities
    {
        //[Required(ErrorMessage = "El Número de Resolución es Obligatorio")]
        //[RegularExpression(@"(^[0-9]*$)", ErrorMessage = "Existen caracteres inválidos.")]

        public long idPresupuestoRegional { get; set; }
        public string idPresupuestoRegionalConsulta { get; set; }
        public int idResolucionPresupuestaria { get; set; }
        public DateTime fechaResolucionPresupuestoRegional { get; set; }
        public int annoPresupuesto { get; set; }
        public List<anoPresupuesto> lstAnoPresupuesto { get; set; }
        public long montoPresupuestoRegional { get; set; }
        public int idRegion { get; set; }
        public int montoComprometidoSAT { get; set; }
        public int montoComprometidoSET { get; set; }
        public int montoPagadoSAT { get; set; }
        public int montoPagadoSET { get; set; }
        public string mensajeSalida { get; set; }
        public string codigoSalida { get; set; }
        public bool todos { get; set; }
        public string vigencia { get; set; }
        
        public presupuestoRegionalEntities()
        {
            idPresupuestoRegional = 0;
            idPresupuestoRegionalConsulta = string.Empty;
            idResolucionPresupuestaria = 0;
            fechaResolucionPresupuestoRegional = new DateTime();
            annoPresupuesto = 0;
            montoPresupuestoRegional = 0;
            idRegion = 0;
            montoComprometidoSAT = 0;
            montoComprometidoSET = 0;
            montoPagadoSAT = 0;
            montoPagadoSET = 0;
            mensajeSalida = string.Empty;
            codigoSalida = string.Empty;
            todos = false;
            vigencia = string.Empty;
            lstAnoPresupuesto = null;
        }
        public presupuestoRegionalEntities(long _idPresupuestoRegional, int _idResolucionPresupuestaria, DateTime _fechaResolucionPresupuestoRegional,
            int _annoPresupuesto, long _montoPresupuestoRegional, int _idRegion, int _montoComprometidoSAT, int _montoComprometidoSET,
            int _montoPagadoSAT, int _montoPagadoSET)
        {
            idPresupuestoRegional = _idPresupuestoRegional;
            idResolucionPresupuestaria = _idResolucionPresupuestaria;
            fechaResolucionPresupuestoRegional = _fechaResolucionPresupuestoRegional;
            annoPresupuesto = _annoPresupuesto;
            montoPresupuestoRegional = _montoPresupuestoRegional;
            idRegion = _idRegion;
            montoComprometidoSAT = _montoComprometidoSAT;
            montoComprometidoSET = _montoComprometidoSET;
            montoPagadoSAT = _montoPagadoSAT;
            montoPagadoSET = _montoPagadoSET;
        }
    }

    public class presupuestoRegionalEntitiesFactory
    {
        internal static presupuestoRegionalEntities insertaPresupuestoRegional(int anno, int numeroResolucion, DateTime fechaResolucion, int codigoRegion, long montoResolucionRegion, string observacion, string nombreArchivo, string usuario)
        {
            var _insertaPresupuestoRegional =  presupuestoRegionalDAO.InsertaPresupuestoRegional(anno, numeroResolucion, fechaResolucion, codigoRegion, montoResolucionRegion, observacion, nombreArchivo, usuario);
            presupuestoRegionalEntities presupuestoRegionalEntities = new presupuestoRegionalEntities();
            if (_insertaPresupuestoRegional != null)
            {
                return new presupuestoRegionalEntities
                {
                    mensajeSalida = _insertaPresupuestoRegional.MSG,
                    codigoSalida = _insertaPresupuestoRegional.err.ToString()
                };
            }
            else
                return presupuestoRegionalEntities;
        }

        internal static List<informacionPresupuestoRegionalEntities> obtienePresupuestoRegional(int accion, int anno, int numeroResolucion, DateTime fechaResolucion, int codigoRegion)
        {
            var _obtienePresupuestoRegional = presupuestoRegionalDAO.ObtienePresupuestoRegional(accion, anno, numeroResolucion, fechaResolucion, codigoRegion);
            List<informacionPresupuestoRegionalEntities> informacionPresupuestoRegionalEntities = new List<informacionPresupuestoRegionalEntities>();
            if (_obtienePresupuestoRegional != null)
            {
                long sumaTotal = 0;
                foreach (var a in _obtienePresupuestoRegional)
                {
                    informacionPresupuestoRegionalEntities info = new informacionPresupuestoRegionalEntities();

                    info.idResolucionPresupuestaria = Convert.ToInt32(a.NUMERORESOLUCIONPRESUPUESTARIA);
                    info.annoPresupuesto = Convert.ToInt32(a.ANORESOLUCIONPRESUPUESTARIA);
                    info.codigoRegionPresupuestoRegional = Convert.ToInt32(a.CODIGOREGIONPRESUPUESTOREGIONAL);
                    info.montoPresupuestoRegional = Convert.ToInt64(a.MONTOPRESUPUESTOREGIONAL);
                    info.fechaResolucionPresupuestoRegional = Convert.ToDateTime(a.FECHAPRESUPUESTOREGIONAL);
                    info.vigencia = a.ESTADORESOLUCIONPRESUPUESTARIA.ToString().Replace("True", "Vigente").Replace("False", "No Vigente");
                    info.nombreArchivo = a.NOMBREARCHIVOPRESUPUESTO;
                    sumaTotal += Convert.ToInt64(a.MONTOPRESUPUESTOREGIONAL);
                    info.suma = sumaTotal;
                    info.montoContatoSat = a.MONTOCONTRATOSAT;
                    info.montoContatoSet = a.MONTOCONTRATOSET;
                    info.presupuestoDisponible = a.PRESUPUESTODISPONIBLE;
                    info.montoComprometidoSAT = Convert.ToInt32(a.SATSOLICITUDESCOMPROMETIDAS);
                    info.montoComprometidoSET = Convert.ToInt32(a.SETSOLICITUDESCOMPROMETIDAS);
                    info.montoPagadoSAT = Convert.ToInt32(a.SATSOLICITUDESPAGADAS);
                    info.montoPagadoSET = Convert.ToInt32(a.SETSOLICITUDESPAGADAS);
                    

                    informacionPresupuestoRegionalEntities.Add(info);
                }

                informacionPresupuestoRegionalEntities auxInfo = new informacionPresupuestoRegionalEntities();

                auxInfo.codigoRegionPresupuestoRegional = 17;
                auxInfo.montoPresupuestoRegional = 0;
                auxInfo.montoContatoSat = 0;
                auxInfo.montoContatoSet = 0;
                auxInfo.presupuestoDisponible = 0;
                auxInfo.montoComprometidoSAT = 0;
                auxInfo.montoComprometidoSET = 0;
                auxInfo.montoPagadoSAT = 0;
                auxInfo.montoPagadoSET = 0;

                foreach (var a in informacionPresupuestoRegionalEntities)
                {



                    auxInfo.montoPresupuestoRegional = a.montoPresupuestoRegional + auxInfo.montoPresupuestoRegional;
                    auxInfo.montoContatoSat = a.montoContatoSat + auxInfo.montoContatoSat;
                    auxInfo.montoContatoSet = a.montoContatoSet + auxInfo.montoContatoSet;
                    auxInfo.presupuestoDisponible = a.presupuestoDisponible + auxInfo.presupuestoDisponible;
                    auxInfo.montoComprometidoSAT = a.montoComprometidoSAT + auxInfo.montoComprometidoSAT;
                    auxInfo.montoComprometidoSET = a.montoComprometidoSET + auxInfo.montoComprometidoSET;
                    auxInfo.montoPagadoSAT = a.montoPagadoSAT + auxInfo.montoPagadoSAT;
                    auxInfo.montoPagadoSET = a.montoPagadoSET + auxInfo.montoPagadoSET;
                  
                }

                informacionPresupuestoRegionalEntities.Add(auxInfo);

                return informacionPresupuestoRegionalEntities;                
            }
            else
                return informacionPresupuestoRegionalEntities;
        }

        internal static presupuestoRegionalEntities obtieneResolucion(int numeroResolucion, int anno)
        {
            var _obtieneResolucion = presupuestoRegionalDAO.ObtieneResolucionPresupuesto(numeroResolucion, anno);
            presupuestoRegionalEntities presupuestoRegionalEntities = new presupuestoRegionalEntities();
            if (_obtieneResolucion.NUMERORESOLUCIONPRESUPUESTARIA > 0)
            {
                return new presupuestoRegionalEntities
                {
                    idResolucionPresupuestaria = (int)_obtieneResolucion.NUMERORESOLUCIONPRESUPUESTARIA,
                };
            }
            else
                return presupuestoRegionalEntities;
        }

        internal static List<anoPresupuesto> getlistAñoPresupuesto()
        {
            var auxLstAñoPresupuesto = presupuestoRegionalDAO.getListAño();
            List<anoPresupuesto> lstAñoPresupuesto = new List<anoPresupuesto>();
            if (auxLstAñoPresupuesto != null)
                foreach (var item in auxLstAñoPresupuesto)
                {
                    anoPresupuesto auxAñoPresupuesto = new anoPresupuesto();

                    auxAñoPresupuesto.idAno = item.idAno;
                    auxAñoPresupuesto.ano = item.ano;
                    lstAñoPresupuesto.Add(auxAñoPresupuesto);
                }

            return lstAñoPresupuesto;
        }
    }


}
using Minvu.Snat.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.DAO;
using System.ComponentModel.DataAnnotations;


namespace Minvu.Snat.Domain.Entities
{
    public class contratoEntities
    {
        [Required(ErrorMessage = "El rut es obligatorio")]
        //[RegularExpression(@"(^[1-9]\d*$)", ErrorMessage = "Rut inválido.")]
        public string rutProfesional { get; set; }

        [Required(ErrorMessage = "El dv es obligatorio")]
        public string dvProfesional { get; set; }
        public int idTipoProveedor { get; set; }

        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string nombreRazonSocial { get; set; }
        public int annoPresupuesto { get; set; }
        public List<anoPresupuesto> lstAnoPresupuesto { get; set; }
        public int idRegion { get; set; }
        public int idProvincia { get; set; }
        public int idComuna { get; set; }
        public int idPropiedadTerreno { get; set; }
        public int numeroResolucion { get; set; }
        public int montoContrato { get; set; }
        public int plazoEjecucion { get; set; }
        public string tipoServicio { get; set; }

        //public string SAT { get; set; }
        //public string SET { get; set; }
        public bool regulaPropiedades { get; set; }
        public bool regulaEstudios { get; set; }
        public bool revisionProyectos { get; set; }
        public bool elaboraProyectos { get; set; }
        public bool identificacionDemanda { get; set; }
        public bool estudiosTecnicos { get; set; }
        public bool elaborarDiagnosticos { get; set; }
        public bool otros { get; set; }
        public string otrosTexto { get; set; }
        public string producto { get; set; }
        public string descripcionProducto { get; set; }
        public string observacion { get; set; }
        public DateTime fechaResolucionContrato { get; set; }
        public DateTime fechaInicioContrato { get; set; }
        public string mensajeSalida { get; set; }
        public string codigoSalida { get; set; }
        public string nombreArchivo { get; set; }
        public string nombrePropiedadTerreno { get; set; }
        public string nombreTipoProveedor { get; set; }
        public int idContrato { get; set; }

        public contratoEntities()
        {
            rutProfesional = string.Empty;
            dvProfesional = string.Empty;
            idTipoProveedor = 0;
            nombreRazonSocial = string.Empty;
            annoPresupuesto = 0;
            idRegion = 0;
            idProvincia = 0;
            idComuna = 0;
            idPropiedadTerreno = 0;
            numeroResolucion = 0;
            montoContrato = 0;
            plazoEjecucion = 0;
            tipoServicio = string.Empty;
            //SAT = string.Empty;
            //SET = string.Empty;
            fechaResolucionContrato = DateTime.Now;
            fechaInicioContrato = DateTime.Now;
            mensajeSalida = string.Empty;
            codigoSalida = string.Empty;
            nombreArchivo = string.Empty;
            nombrePropiedadTerreno = string.Empty;
            lstAnoPresupuesto = null;
        }

        public contratoEntities(DateTime _fechaResolucionContrato, DateTime _fechaInicioContrato)
        {
            fechaResolucionContrato = _fechaResolucionContrato;
            fechaInicioContrato = _fechaInicioContrato;
        }
        //public contratoEntities(long _idPresupuestoRegional, int _idResolucionPresupuestaria, DateTime _fechaResolucionPresupuestoRegional,
        //    int _annoPresupuesto, long _montoPresupuestoRegional, int _idRegion, int _montoComprometidoSAT, int _montoComprometidoSET,
        //    int _montoPagadoSAT, int _montoPagadoSET)
        //{
        //    idPresupuestoRegional = _idPresupuestoRegional;
        //    idResolucionPresupuestaria = _idResolucionPresupuestaria;
        //    fechaResolucionPresupuestoRegional = _fechaResolucionPresupuestoRegional;
        //    annoPresupuesto = _annoPresupuesto;
        //    montoPresupuestoRegional = _montoPresupuestoRegional;
        //    idRegion = _idRegion;
        //    montoComprometidoSAT = _montoComprometidoSAT;
        //    montoComprometidoSET = _montoComprometidoSET;
        //    montoPagadoSAT = _montoPagadoSAT;
        //    montoPagadoSET = _montoPagadoSET;
        //}
    }

    public class contratoEntitiesFactory
    {
        internal static contratoEntities insertaContrato(int rUTPROVEEDOR, string dVPROVEEDOR, string nOMBREPROVEEDOR, int tIPOPROVEEDOR,
            int aNNOCONTRATO, int cODIGOREGION, int cODIGOPROVINCIA, int cODIGOCOMUNA, int pLAZOEJECUCION, string pROPIEDADTERRENO, int nUMERORESOLUCION,
            DateTime fECHARESOLUCIONCONTRATO, DateTime fECHAINICIOCONTRATO, string nOMBREARCHIVO, long tIPOSERVICIO, int eSTADOTIPOSERVICIO1,
            int eSTADOTIPOSERVICIO2, int eSTADOTIPOSERVICIO3, int eSTADOTIPOSERVICIO4, int eSTADOTIPOSERVICIO5, int eSTADOTIPOSERVICIO6,
            int eSTADOTIPOSERVICIO7, string nOMBRETIPOSERVICIO8, string pRODUCTOCONTRATO, string dESCRIPCIONPRODUCTO, int mONTOCONTRATO, string oBSERVACIONCONTRATO,
            string uSUARIO)
        {
            var _insertaContrato = contratoDAO.InsertaContrato(rUTPROVEEDOR, dVPROVEEDOR, nOMBREPROVEEDOR, tIPOPROVEEDOR,
                    aNNOCONTRATO, cODIGOREGION, cODIGOPROVINCIA, cODIGOCOMUNA, pLAZOEJECUCION, pROPIEDADTERRENO, nUMERORESOLUCION,
                    fECHARESOLUCIONCONTRATO, fECHAINICIOCONTRATO, nOMBREARCHIVO, tIPOSERVICIO, eSTADOTIPOSERVICIO1, eSTADOTIPOSERVICIO2, eSTADOTIPOSERVICIO3,
                    eSTADOTIPOSERVICIO4, eSTADOTIPOSERVICIO5, eSTADOTIPOSERVICIO6, eSTADOTIPOSERVICIO7, nOMBRETIPOSERVICIO8,
                    pRODUCTOCONTRATO, dESCRIPCIONPRODUCTO, mONTOCONTRATO, oBSERVACIONCONTRATO, uSUARIO);
            contratoEntities contratoEntities = new contratoEntities();
            if (_insertaContrato != null)
            {
                return new contratoEntities
                {
                    mensajeSalida = _insertaContrato.MSG,
                    codigoSalida = _insertaContrato.err.ToString()
                };
            }
            else
                return contratoEntities;
        }
        
        internal static List<anoPresupuesto> getlistAnoPresupuesto()
        {
            try
            {
                var auxLstAñoPresupuesto = contratoDAO.getListAno();
                List<anoPresupuesto> lstAnoPresupuesto = new List<anoPresupuesto>();
                if (auxLstAñoPresupuesto != null)
                {
                    foreach (var item in auxLstAñoPresupuesto)
                    {
                        anoPresupuesto auxAñoPresupuesto = new anoPresupuesto();

                        auxAñoPresupuesto.idAno = item.idAno;
                        auxAñoPresupuesto.ano = item.ano;
                        lstAnoPresupuesto.Add(auxAñoPresupuesto);

                        Log.Instance.Info(auxAñoPresupuesto.ano + " - metodo: getlistAnoPresupuesto");
                    }
                }
                else
                {

                }

                return lstAnoPresupuesto;
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex + " - metodo: getlistAnoPresupuesto");
                throw;
            }
           
        }


         

    }


    public class anoPresupuesto
    {
        public int idAno { get; set; }
        public int ano { get; set; }
    }
}
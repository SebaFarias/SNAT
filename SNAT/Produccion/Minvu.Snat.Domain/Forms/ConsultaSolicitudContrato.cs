using Minvu.Snat.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.Domain.Entities;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Minvu.Snat.Domain.Forms
{
    public class ConsultaSolicitudContrato
    {
        public contratoEntities _contratoEntities { get; set; }
        public List<RegionesEntities> _regionEntities { get; set; }
        public List<ProvinciasEntities> lstProvincias { get; set; }
        public List<ComunasEntities> lstComunas { get; set; }
        public List<RegionesEntities> _regionPresupuestoEntities { get; set; }
        public List<contratoServiciosEntities> _contratoSolicitudPagoList { get; set; }
        public List<contratoSolicitudPagoEntities> _contratoSolicitudPagoEntities { get; set; }
        public contratoSolicitudPagoEntities _solicitudContratoPagoEntities { get; set; }
        public contratoSolicitudPagoEntities _contratoSolicitudPago { get; set; }
        public string codSalida { get; set; }
    }
    public class ConsultaSolicitudContratoFactory
    {
        public static object getRegiones(string userName)
        {
            ConsultaContrato _PresupuestoRegional = new ConsultaContrato();
            _PresupuestoRegional._listRegionEntities = RegionesEntitiesFactory.getListRegiones();
            //_PresupuestoRegional._regionPresupuestoEntities = RegionesEntitiesFactory.getListRegionesPresupuesto();
            _PresupuestoRegional.lstProvincias = new List<ProvinciasEntities>();
            _PresupuestoRegional.lstComunas = new List<ComunasEntities>();

            return _PresupuestoRegional;
        }

        public static object insertaContrato(int rUTPROVEEDOR, string dVPROVEEDOR, string nOMBREPROVEEDOR, int tIPOPROVEEDOR,
            int aNNOCONTRATO, int cODIGOREGION, int cODIGOPROVINCIA, int cODIGOCOMUNA, int pLAZOEJECUCION, string pROPIEDADTERRENO, int nUMERORESOLUCION,
            DateTime fECHARESOLUCIONCONTRATO, DateTime fECHAINICIOCONTRATO, string nOMBREARCHIVO, long tIPOSERVICIO, int eSTADOTIPOSERVICIO1,
            int eSTADOTIPOSERVICIO2, int eSTADOTIPOSERVICIO3, int eSTADOTIPOSERVICIO4, int eSTADOTIPOSERVICIO5, int eSTADOTIPOSERVICIO6,
            int eSTADOTIPOSERVICIO7, string nOMBRETIPOSERVICIO8, string pRODUCTOCONTRATO, string dESCRIPCIONPRODUCTO, int mONTOCONTRATO, string oBSERVACIONCONTRATO,
            string uSUARIO)
        {
            ConsultaContrato _Contrato = new ConsultaContrato();
            _Contrato._contratoEntities = contratoEntitiesFactory.insertaContrato(rUTPROVEEDOR, dVPROVEEDOR, nOMBREPROVEEDOR, tIPOPROVEEDOR,
                    aNNOCONTRATO, cODIGOREGION, cODIGOPROVINCIA, cODIGOCOMUNA, pLAZOEJECUCION, pROPIEDADTERRENO, nUMERORESOLUCION,
                    fECHARESOLUCIONCONTRATO, fECHAINICIOCONTRATO, nOMBREARCHIVO, tIPOSERVICIO, eSTADOTIPOSERVICIO1, eSTADOTIPOSERVICIO2, eSTADOTIPOSERVICIO3,
                    eSTADOTIPOSERVICIO4, eSTADOTIPOSERVICIO5, eSTADOTIPOSERVICIO6, eSTADOTIPOSERVICIO7, nOMBRETIPOSERVICIO8,
                    pRODUCTOCONTRATO, dESCRIPCIONPRODUCTO, mONTOCONTRATO, oBSERVACIONCONTRATO, uSUARIO);
            _Contrato._listRegionEntities = RegionesEntitiesFactory.getListRegiones();
            _Contrato.lstProvincias = new List<ProvinciasEntities>();
            _Contrato.lstComunas = new List<ComunasEntities>();

            _Contrato.codSalida = _Contrato._contratoEntities.codigoSalida;
            return _Contrato;
        }

        public static object insertaSolicitudATP(long anno, long numeroResolucion, long iDSOLICITUDATP, long iDCONTRATO, int aNNOPRESUPUESTO, long mONTOPRESUPESTO, int cODIGOREGION, long mONTOPAGADO,
                                                                     long mONTOCOMPROMETIDO, long mONTOPORPAGAR, string aCTIVIDADES, long? nUMEROBOLFACT, DateTime? fECHABOLETAFACT,
                                                                     long mONTOTOTALAPAGAR, bool vBSERVICIO, string vBRESPONSABLE, DateTime? vBFECHA, long eSTADOSOLICITUD, long? idMaestroTipoPago, ConsultaContrato consultaContrato )
        {
            string mensajeSalida = string.Empty;
            string mensajeSPS = string.Empty;
            string codigoSPS = string.Empty;
            ConsultaContrato _Solicitud = new ConsultaContrato();
            _Solicitud._contratoSolicitudPago = new contratoSolicitudPagoEntities();
            _Solicitud._contratoSolicitudPago.codigoSalida = "0";
            if (iDSOLICITUDATP == 0)
            {
                _Solicitud._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.insertaSolicitudATP(iDSOLICITUDATP, iDCONTRATO, aNNOPRESUPUESTO, mONTOPRESUPESTO,
                                                                    cODIGOREGION, mONTOPAGADO, mONTOCOMPROMETIDO, mONTOPORPAGAR, aCTIVIDADES, nUMEROBOLFACT, fECHABOLETAFACT,
                                                                    mONTOTOTALAPAGAR, vBSERVICIO, vBRESPONSABLE, vBFECHA, eSTADOSOLICITUD, idMaestroTipoPago);
            }
            ConsultaContrato _auxContrato = new ConsultaContrato();
            _auxContrato = ConsultaContratoFactory.getInstanciasFormulario(vBRESPONSABLE,anno,cODIGOREGION) as ConsultaContrato;


            if (iDSOLICITUDATP == 0)
            {
                Match m = Regex.Match(_Solicitud._contratoSolicitudPago.mensajeSalida, "(\\d+)");
                string num = string.Empty;

                if (m.Success)
                {
                    iDSOLICITUDATP = Convert.ToInt32(m.Value);
                }

                if (eSTADOSOLICITUD == 7)
                {
                    contratoSolicitudPagoEntitiesFactory.ActualizaEstadoInformaciónRatificadorSolicitud(iDSOLICITUDATP, vBRESPONSABLE);
                }

            }

            _Solicitud._listRegionEntities = RegionesEntitiesFactory.getListRegiones();
            _Solicitud.codSalidaRatifica = "";

            if (_Solicitud._contratoSolicitudPago.codigoSalida == "0" && eSTADOSOLICITUD == 6) /* Guardar */
            {
                mensajeSalida = _Solicitud._contratoSolicitudPago.mensajeSalida;
                _Solicitud._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneCabecera(vBRESPONSABLE, aNNOPRESUPUESTO, cODIGOREGION);
                _Solicitud._contratoSolicitudPago.mensajeSalida = mensajeSalida;
                _Solicitud._contratoSolicitudPago.codigoSalida = "0";
                _Solicitud._contratoSolicitudPago.estadoSolicitud = (int)eSTADOSOLICITUD;
                _Solicitud._contratoSolicitudPago.codigoSalidaRatifica = "1";
                _Solicitud._contratoSolicitudPago.mensajeSalidaRatifica = "";
            }

            if (_Solicitud._contratoSolicitudPago.codigoSalida == "0" && eSTADOSOLICITUD == 7) /* Ratifica */
            {
                _Solicitud._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneCabecera(vBRESPONSABLE, aNNOPRESUPUESTO, cODIGOREGION);

                int funcionarioCreaRut = _Solicitud._contratoSolicitudPago.rutUser;
                string nombreRegionPrint = _Solicitud._contratoSolicitudPago.nombreRegionPresupuestoPrint;

                _Solicitud._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneContrato(numeroResolucion, cODIGOREGION);

                /*Inserta Ratifica*/
                long CodigoOrdenAT = 0;
                string idSolConcatena = iDSOLICITUDATP.ToString() + cODIGOREGION.ToString().PadLeft(2, '0') + aNNOPRESUPUESTO.ToString();
                CodigoOrdenAT = Int64.Parse(idSolConcatena);
                int lineaOrigen = 3;
                int lineaAplicacion = 3;
                int tipoLlamado = 1;
                int montoUF = 0;
                int montoPesos = (int)mONTOTOTALAPAGAR;
                string nota = "Asistencia Técnica Previa";

                int rutContrato = 0;
                string dvContrato = string.Empty;
                string nombres = string.Empty;
                int mandatoRut = 0;
                string mandatodv = string.Empty;
                string mandatoNombres = string.Empty;
                if (_Solicitud._contratoSolicitudPago.rutContratoConsultaRat == null)
                {
                    rutContrato = Int32.Parse(consultaContrato._contratoSolicitudPago.rutContratoConsultaRat);
                    dvContrato = consultaContrato._contratoSolicitudPago.dvContratoConsultaRat;
                    nombres = consultaContrato._contratoSolicitudPago.nombreRazonSocial;
                    mandatoRut = Int32.Parse(consultaContrato._contratoSolicitudPago.rutContratoConsultaRat);
                    mandatodv = consultaContrato._contratoSolicitudPago.dvContratoConsultaRat;
                    mandatoNombres = consultaContrato._contratoSolicitudPago.nombreRazonSocial;
                }
                else
                {
                    rutContrato = Int32.Parse(_Solicitud._contratoSolicitudPago.rutContratoConsultaRat);
                    dvContrato = _Solicitud._contratoSolicitudPago.dvContratoConsultaRat;
                    nombres = _Solicitud._contratoSolicitudPago.nombreRazonSocial;
                    mandatoRut = Int32.Parse(_Solicitud._contratoSolicitudPago.rutContratoConsultaRat);
                    mandatodv = _Solicitud._contratoSolicitudPago.dvContratoConsultaRat;
                    mandatoNombres = _Solicitud._contratoSolicitudPago.nombreRazonSocial;
                }

                int organismo = cODIGOREGION;
                int indicaATPrevia = 1;

                _Solicitud._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.insertaRatificaSolicitud(CodigoOrdenAT, lineaOrigen, lineaAplicacion,
                    tipoLlamado, montoUF, montoPesos, nota, rutContrato, dvContrato, nombres, mandatoRut, mandatodv, mandatoNombres, funcionarioCreaRut, organismo, indicaATPrevia);

                codigoSPS = _Solicitud._contratoSolicitudPago.codigoSalidaRatifica;
                mensajeSPS = _Solicitud._contratoSolicitudPago.mensajeSalidaRatifica;

                if (codigoSPS != "1")
                {
                    contratoSolicitudPagoEntitiesFactory.actualizaSolicitud(iDSOLICITUDATP, 6);
                }

                if (codigoSPS == "1")
                {
                    contratoSolicitudPagoEntitiesFactory.actualizaSolicitud(iDSOLICITUDATP, 7);
                }

                mensajeSalida = _Solicitud._contratoSolicitudPago.mensajeSalida;
                _Solicitud._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneContrato(numeroResolucion, cODIGOREGION);
                _Solicitud._contratoSolicitudPago.montoPresupuesto = mONTOPRESUPESTO;
                _Solicitud._contratoSolicitudPago.numeroSolicitudPresupuesto = iDSOLICITUDATP;
                _Solicitud._contratoSolicitudPago.mensajeSalida = mensajeSalida;
                _Solicitud._contratoSolicitudPago.codigoSalida = "0";
                _Solicitud._contratoSolicitudPago.estadoSolicitud = (int)eSTADOSOLICITUD;

                ConsultaSolicitudContrato _ObtieneServicio = new ConsultaSolicitudContrato();
                _ObtieneServicio._contratoSolicitudPagoList = contratoServiciosEntitiesFactory.obtieneMontoServicios(iDSOLICITUDATP);
                _Solicitud._contratoSolicitudPagoList = _ObtieneServicio._contratoSolicitudPagoList;

                _Solicitud._contratoSolicitudPago.numeroBolFact = nUMEROBOLFACT;
                _Solicitud._contratoSolicitudPago.fechaBolFact = fECHABOLETAFACT;
                _Solicitud._contratoSolicitudPago.montoTotalaPagar = (int)mONTOTOTALAPAGAR;
                _Solicitud._contratoSolicitudPago.vbServicio = vBSERVICIO;
                if (vBFECHA != null)
                    _Solicitud._contratoSolicitudPago.fechaVB = Convert.ToDateTime(vBFECHA);
                _Solicitud._contratoSolicitudPago.responsableVB = vBRESPONSABLE;
                _Solicitud._contratoSolicitudPago.codigoSalidaRatifica = codigoSPS;
                _Solicitud._contratoSolicitudPago.mensajeSalidaRatifica = mensajeSPS;
                _Solicitud.codSalidaRatifica = codigoSPS;
                _Solicitud._contratoSolicitudPago.nombreRegionPresupuesto = nombreRegionPrint;
                _Solicitud._contratoSolicitudPago.annoPresupuesto = aNNOPRESUPUESTO;
                _Solicitud._contratoSolicitudPago.idRegion = cODIGOREGION;
                _Solicitud._contratoSolicitudPago.numeroResolucionContrato = (int)numeroResolucion;

            }

            _Solicitud._listMaestroTipoPagoEntities = _auxContrato._listMaestroTipoPagoEntities;

            return _Solicitud;
        }

        public static object ratificaSolicitud(int anno, int numeroResolucion, long iDSOLICITUDATP, int aNNOPRESUPUESTO, int cODIGOREGION,
                                                                     long mONTOTOTALAPAGAR, string vBRESPONSABLE)
        {
            string mensajeSalida = string.Empty;
            string mensajeSPS = string.Empty;
            string codigoSPS = string.Empty;
            ConsultaContrato _Solicitud = new ConsultaContrato();
            ConsultaContrato _auxContrato = new ConsultaContrato();
            _auxContrato = ConsultaContratoFactory.getInstanciasFormulario(vBRESPONSABLE,anno,cODIGOREGION) as ConsultaContrato;

            _Solicitud._listRegionEntities= RegionesEntitiesFactory.getListRegiones();
            _Solicitud.codSalidaRatifica = "";
            _Solicitud._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneCabecera(vBRESPONSABLE, aNNOPRESUPUESTO, cODIGOREGION);
            
            contratoSolicitudPagoEntitiesFactory.ActualizaEstadoInformaciónRatificadorSolicitud(iDSOLICITUDATP, vBRESPONSABLE);

            int funcionarioCreaRut = _Solicitud._contratoSolicitudPago.rutUser;
            string nombreRegionPrint = _Solicitud._contratoSolicitudPago.nombreRegionPresupuestoPrint;

            _Solicitud._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneContrato(numeroResolucion, cODIGOREGION);


            /*Inserta Ratifica*/
            long CodigoOrdenAT = 0;
            string idSolConcatena = iDSOLICITUDATP.ToString() + cODIGOREGION.ToString().PadLeft(2, '0') + aNNOPRESUPUESTO.ToString();
            CodigoOrdenAT = Int64.Parse(idSolConcatena);
            int lineaOrigen = 3;
            int lineaAplicacion = 3;
            int tipoLlamado = 1;
            int montoUF = 0;
            int montoPesos = (int)mONTOTOTALAPAGAR;
            string nota = "Asistencia Técnica Previa";
            int rutContrato = Convert.ToInt32(_Solicitud._contratoSolicitudPago.rutContratoConsultaRat);
            string dvContrato = _Solicitud._contratoSolicitudPago.dvContratoConsultaRat;
            string nombres = _Solicitud._contratoSolicitudPago.nombreRazonSocial;
            int mandatoRut = Int32.Parse(_Solicitud._contratoSolicitudPago.rutContratoConsultaRat);
            string mandatodv = _Solicitud._contratoSolicitudPago.dvContratoConsultaRat;
            string mandatoNombres = _Solicitud._contratoSolicitudPago.nombreRazonSocial;
            int organismo = cODIGOREGION;
            int indicaATPrevia = 1;

            _Solicitud._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.insertaRatificaSolicitud(CodigoOrdenAT, lineaOrigen, lineaAplicacion,
                tipoLlamado, montoUF, montoPesos, nota, rutContrato, dvContrato, nombres, mandatoRut, mandatodv, mandatoNombres, funcionarioCreaRut, organismo, indicaATPrevia);

            codigoSPS = _Solicitud._contratoSolicitudPago.codigoSalidaRatifica;
            mensajeSPS = _Solicitud._contratoSolicitudPago.mensajeSalidaRatifica;

            if (codigoSPS == "1")
            {
                contratoSolicitudPagoEntitiesFactory.actualizaSolicitud(iDSOLICITUDATP, 7);
            }

            mensajeSalida = _Solicitud._contratoSolicitudPago.mensajeSalida;
            _Solicitud._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneContrato(numeroResolucion, cODIGOREGION);
            //_Solicitud._contratoSolicitudPago.montoPresupuesto = mONTOPRESUPESTO;
            _Solicitud._contratoSolicitudPago.numeroSolicitudPresupuesto = iDSOLICITUDATP;
            

            ConsultaSolicitudContrato _ObtieneServicio = new ConsultaSolicitudContrato();
            _ObtieneServicio._contratoSolicitudPagoList = contratoServiciosEntitiesFactory.obtieneMontoServicios(iDSOLICITUDATP);
            _Solicitud._contratoSolicitudPagoList = _ObtieneServicio._contratoSolicitudPagoList;

            //_Solicitud._contratoSolicitudPago.numeroBolFact = (int)nUMEROBOLFACT;
            //_Solicitud._contratoSolicitudPago.fechaBolFact = fECHABOLETAFACT;
            //_Solicitud._contratoSolicitudPago.montoTotalaPagar = (int)mONTOTOTALAPAGAR;
            //_Solicitud._contratoSolicitudPago.vbServicio = vBSERVICIO;
            //_Solicitud._contratoSolicitudPago.fechaVB = vBFECHA;
            _Solicitud._contratoSolicitudPago.responsableVB = vBRESPONSABLE;
            _Solicitud._contratoSolicitudPago.codigoSalidaRatifica = codigoSPS;
            _Solicitud._contratoSolicitudPago.mensajeSalidaRatifica = mensajeSPS;
            _Solicitud.codSalidaRatifica = "1";
            _Solicitud._contratoSolicitudPago.nombreRegionPresupuesto = nombreRegionPrint;
            _Solicitud._contratoSolicitudPago.annoPresupuesto = aNNOPRESUPUESTO;
            _Solicitud._contratoSolicitudPago.idRegion = cODIGOREGION;
            _Solicitud._contratoSolicitudPago.numeroResolucionContrato = (int)numeroResolucion;
            _Solicitud._contratoSolicitudPago.codigoSalidaRatifica = "1";
            _Solicitud._contratoSolicitudPago.codigoSalida = "0";
            _Solicitud._contratoSolicitudPago.estadoSolicitud = 7;
            //_Solicitud._contratoSolicitudPago.fechaVB = vBFECHA;

            _Solicitud._listMaestroTipoPagoEntities = _auxContrato._listMaestroTipoPagoEntities;
            return _Solicitud;
        }

        public static ConsultaContrato EliminaContrato(int idContrato, string userName, long anno)
        {
            ConsultaContrato _Contrato = new ConsultaContrato();
            _Contrato._solicitudContratoPagoEntities = contratoSolicitudPagoEntitiesFactory.eliminaContrato(idContrato);
            _Contrato._listRegionEntities = RegionesEntitiesFactory.getListRegiones();
            _Contrato._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneCabecera(userName, anno, 0);
            _Contrato._contratoSolicitudPagoEntities = contratoSolicitudPagoEntitiesFactory.obtieneListadoContrato(4, (int)anno, 0, 0);

            return _Contrato;

        }
        public static object EliminaSolicitud(int idSolicitud, string userName, long anno, int idRegion, int idResolucion)
        {
            ConsultaContrato _Solicitud = new ConsultaContrato();
            _Solicitud._solicitudContratoPagoEntities = contratoSolicitudPagoEntitiesFactory.eliminaSolicitud(idSolicitud);
            _Solicitud._listRegionEntities = RegionesEntitiesFactory.getListRegiones();
            _Solicitud._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneCabecera(userName, anno, 0);
            _Solicitud._contratoSolicitudPagoEntities = contratoSolicitudPagoEntitiesFactory.obtieneListadoSolicitud(4, idResolucion, idRegion, (int)anno);

            return _Solicitud;
        }

        public static ConsultaContrato ObtieneListadoSolicitud(int accion, long numeroSolicitud, int codigoRegion, int annoPresupuesto, ConsultaContrato consultaContrato)
        {
            consultaContrato.consultaSolicitudContrato = new ConsultaSolicitudContrato();
            consultaContrato.consultaSolicitudContrato._contratoSolicitudPagoEntities = contratoSolicitudPagoEntitiesFactory.obtieneListadoSolicitud(accion, numeroSolicitud, codigoRegion, annoPresupuesto);

            return consultaContrato;
        }

        public static object ObtieneListadoContrato(int accion, int annoPresupuesto, int codigoRegion, long tipoServicio)
        {
            ConsultaSolicitudContrato _ObtieneListadoContrato = new ConsultaSolicitudContrato();
            _ObtieneListadoContrato._contratoSolicitudPagoEntities = contratoSolicitudPagoEntitiesFactory.obtieneListadoContrato(accion, annoPresupuesto, codigoRegion, tipoServicio);

            return _ObtieneListadoContrato;
        }

        public static object ObtieneContrato(int idContrato)
        {
            ConsultaContrato _ObtieneContrato = new ConsultaContrato();

            _ObtieneContrato._listRegionEntities = RegionesEntitiesFactory.getListRegiones();
            _ObtieneContrato.lstProvincias = ProvinciasEntitiesFactory.getListProvincias();
            _ObtieneContrato.lstComunas = ComunasEntitiesFactory.getListComunas();

            _ObtieneContrato._contratoEntities = contratoSolicitudPagoEntitiesFactory.obtieneContrato(idContrato);

            _ObtieneContrato._contratoSolicitudPagoList = contratoServiciosEntitiesFactory.obtieneServicios(idContrato);

            return _ObtieneContrato;
        }

        public static object ConsultaSolicitudPago(string user, long anno, int idRegion, int idSolicitud)
        {
            ConsultaContrato _ConsultaSolicitudPago = new ConsultaContrato();
            ConsultaContrato _auxContrato = new ConsultaContrato();
            _auxContrato = ConsultaContratoFactory.getInstanciasFormulario(user,anno,idRegion) as ConsultaContrato;

            ConsultaSolicitudContrato _ObtieneServicio = new ConsultaSolicitudContrato();
            _ConsultaSolicitudPago._listRegionEntities = RegionesEntitiesFactory.getListRegiones();
            _ConsultaSolicitudPago._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ConsultaSolicitudPago(user, anno, idRegion, idSolicitud);
            long idTipoPago = _ConsultaSolicitudPago._contratoSolicitudPago.idTipoProveedor;
            string nombreRegionPresupuesto = _ConsultaSolicitudPago._contratoSolicitudPago.nombreRegionPresupuesto;
            string nombreRegionPrint = _ConsultaSolicitudPago._contratoSolicitudPago.nombreRegionPresupuestoPrint;
            int numeroResolucion = 0;
            if (_ConsultaSolicitudPago._contratoSolicitudPago.numeroResolucionContrato != null)
                numeroResolucion = Convert.ToInt32(_ConsultaSolicitudPago._contratoSolicitudPago.numeroResolucionContrato);
            long montoPresupuesto = _ConsultaSolicitudPago._contratoSolicitudPago.montoPresupuestoNacional;
            string nombreMaestroEstado = _ConsultaSolicitudPago._contratoSolicitudPago.nombreMaestroEstadoContrato;

            long? numBoleta = _ConsultaSolicitudPago._contratoSolicitudPago.numeroBolFact;
            DateTime? fechaBoleta = _ConsultaSolicitudPago._contratoSolicitudPago.fechaBolFact;
            int montoTotal = _ConsultaSolicitudPago._contratoSolicitudPago.montoTotalaPagar;
            bool vbServicio = _ConsultaSolicitudPago._contratoSolicitudPago.vbServicio;
            DateTime fechaVB = _ConsultaSolicitudPago._contratoSolicitudPago.fechaVB;
            string responsable = string.Empty;
            if (_ConsultaSolicitudPago._contratoSolicitudPago.responsableVB != null)
            { responsable = _ConsultaSolicitudPago._contratoSolicitudPago.responsableVB; }

            else
            {
                responsable = user;
            }


            int estadoSolicitud = _ConsultaSolicitudPago._contratoSolicitudPago.estadoSolicitud;

            _ConsultaSolicitudPago._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneContrato(Convert.ToInt64(_ConsultaSolicitudPago._contratoSolicitudPago.numeroResolucionContrato), idRegion);
            _ConsultaSolicitudPago._contratoSolicitudPago.numeroResolucionContrato = numeroResolucion;
            _ConsultaSolicitudPago._contratoSolicitudPago.numeroResolucionContrato = numeroResolucion;
            _ConsultaSolicitudPago._contratoSolicitudPago.numeroSolicitudPresupuesto = idSolicitud;
            _ConsultaSolicitudPago._contratoSolicitudPago.nombreRegionPresupuesto = nombreRegionPresupuesto;
            _ConsultaSolicitudPago._contratoSolicitudPago.nombreRegionPresupuestoPrint = nombreRegionPrint;
            _ConsultaSolicitudPago._contratoSolicitudPago.montoPresupuesto = montoPresupuesto;

            _ConsultaSolicitudPago._contratoSolicitudPago.numeroBolFact = numBoleta;
            _ConsultaSolicitudPago._contratoSolicitudPago.fechaBolFact = fechaBoleta;
            _ConsultaSolicitudPago._contratoSolicitudPago.montoTotalaPagar = montoTotal;
            _ConsultaSolicitudPago._contratoSolicitudPago.vbServicio = vbServicio;
            _ConsultaSolicitudPago._contratoSolicitudPago.fechaVB = fechaVB;
            _ConsultaSolicitudPago._contratoSolicitudPago.responsableVB = responsable;
            _ConsultaSolicitudPago._contratoSolicitudPago.responsableNombreCompletoVB = FuncionarioEntitiesFactory.getFuncionarioNombreCompleto(responsable);
            _ConsultaSolicitudPago._contratoSolicitudPago.idRegion = idRegion;
            _ConsultaSolicitudPago._contratoSolicitudPago.annoPresupuesto = (int)anno;
            _ConsultaSolicitudPago._contratoSolicitudPago.estadoSolicitud = estadoSolicitud;
            _ConsultaSolicitudPago._contratoSolicitudPago.nombreMaestroEstadoContrato = nombreMaestroEstado;

            if (_ConsultaSolicitudPago._contratoSolicitudPago.idContrato > 0)
            {
                //_ObtieneServicio._contratoSolicitudPagoList = contratoServiciosEntitiesFactory.obtieneServicios(_ConsultaSolicitudPago._contratoSolicitudPago.idContrato);
                //_ConsultaSolicitudPago._contratoSolicitudPagoList = _ObtieneServicio._contratoSolicitudPagoList;

                //ConsultaSolicitudContrato _ObtieneServicio = new ConsultaSolicitudContrato();
                _ObtieneServicio._contratoSolicitudPagoList = contratoServiciosEntitiesFactory.obtieneMontoServicios(idSolicitud);
                _ConsultaSolicitudPago._contratoSolicitudPagoList = _ObtieneServicio._contratoSolicitudPagoList;

            }


            _ConsultaSolicitudPago._auxmaestroTipoPagoEntities = _auxContrato._listMaestroTipoPagoEntities.Find(x => x.idMaestroTipoPago == idTipoPago);

            return _ConsultaSolicitudPago;
        }

        public static object ConsultaContratoPago(string user, long anno, int idRegion, int idContrato)
        {
            ConsultaContrato _ConsultaSolicitudPago = new ConsultaContrato();
            ConsultaSolicitudContrato _ObtieneServicio = new ConsultaSolicitudContrato();
            _ConsultaSolicitudPago._listRegionEntities = RegionesEntitiesFactory.getListRegiones();
            //_ConsultaSolicitudPago._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ConsultaSolicitudPago(user, anno, idRegion, idSolicitud);
            int numeroResolucion = 0;
            string nombreRegion = _ConsultaSolicitudPago._contratoSolicitudPago.nombreRegionPresupuesto;

            if(_ConsultaSolicitudPago._contratoSolicitudPago.numeroResolucionContrato != null)
            numeroResolucion = Convert.ToInt32(_ConsultaSolicitudPago._contratoSolicitudPago.numeroResolucionContrato);
            long montoPresupuesto = _ConsultaSolicitudPago._contratoSolicitudPago.montoPresupuestoNacional;

            long? numBoleta = _ConsultaSolicitudPago._contratoSolicitudPago.numeroBolFact;
            DateTime? fechaBoleta = _ConsultaSolicitudPago._contratoSolicitudPago.fechaBolFact;
            int montoTotal = _ConsultaSolicitudPago._contratoSolicitudPago.montoTotalaPagar;
            bool vbServicio = _ConsultaSolicitudPago._contratoSolicitudPago.vbServicio;
            DateTime fechaVB = _ConsultaSolicitudPago._contratoSolicitudPago.fechaVB;
            string responsable = _ConsultaSolicitudPago._contratoSolicitudPago.responsableVB;
            int estadoSolicitud = _ConsultaSolicitudPago._contratoSolicitudPago.estadoSolicitud;

            _ConsultaSolicitudPago._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneContrato(Convert.ToInt32(_ConsultaSolicitudPago._contratoSolicitudPago.numeroResolucionContrato), idRegion);
            _ConsultaSolicitudPago._contratoSolicitudPago.numeroResolucionContrato = numeroResolucion;
            _ConsultaSolicitudPago._contratoSolicitudPago.nombreRegionPresupuesto = nombreRegion;
            _ConsultaSolicitudPago._contratoSolicitudPago.montoPresupuesto = montoPresupuesto;

            _ConsultaSolicitudPago._contratoSolicitudPago.numeroBolFact = numBoleta;
            _ConsultaSolicitudPago._contratoSolicitudPago.fechaBolFact = fechaBoleta;
            _ConsultaSolicitudPago._contratoSolicitudPago.montoTotalaPagar = montoTotal;
            _ConsultaSolicitudPago._contratoSolicitudPago.vbServicio = vbServicio;
            _ConsultaSolicitudPago._contratoSolicitudPago.fechaVB = fechaVB;
            _ConsultaSolicitudPago._contratoSolicitudPago.responsableVB = responsable;
            _ConsultaSolicitudPago._contratoSolicitudPago.idRegion = idRegion;
            _ConsultaSolicitudPago._contratoSolicitudPago.annoPresupuesto = (int)anno;
            _ConsultaSolicitudPago._contratoSolicitudPago.estadoSolicitud = estadoSolicitud;

            if (_ConsultaSolicitudPago._contratoSolicitudPago.idContrato > 0)
            {
                //_ObtieneServicio._contratoSolicitudPagoList = contratoServiciosEntitiesFactory.obtieneServicios(_ConsultaSolicitudPago._contratoSolicitudPago.idContrato);
                //_ConsultaSolicitudPago._contratoSolicitudPagoList = _ObtieneServicio._contratoSolicitudPagoList;

                //ConsultaSolicitudContrato _ObtieneServicio = new ConsultaSolicitudContrato();
                //_ObtieneServicio._contratoSolicitudPagoList = contratoServiciosEntitiesFactory.obtieneMontoServicios(idSolicitud);
                _ConsultaSolicitudPago._contratoSolicitudPagoList = _ObtieneServicio._contratoSolicitudPagoList;

            }

            return _ConsultaSolicitudPago;
        }


        public static ConsultaContrato ObtieneCabeceraNacional(long anno, string user)
        {
            var _auxContrato = ConsultaContratoFactory.getInstanciasFormulario(user,anno,0);
            
            return _auxContrato;
        }

        public static ConsultaContrato ObtieneCabeceraRegional(long anno, string user)
        {

            var _auxContrato = ConsultaContratoFactory.getInstanciasFormulario(user, anno, 0);

            return _auxContrato;
           
        }

        public static object ObtieneContrato(long numeroResolucion, long idRegion, ConsultaContrato consultaContrato)
        {
            ConsultaContrato _consultaContrato = new ConsultaContrato();
            ConsultaSolicitudContrato _ObtieneServicio = new ConsultaSolicitudContrato();

            _consultaContrato._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneContrato(numeroResolucion, idRegion);


             consultaContrato._contratoSolicitudPago.idContrato = _consultaContrato._contratoSolicitudPago.idContrato;
             consultaContrato._contratoSolicitudPago.plazoEjecucion = _consultaContrato._contratoSolicitudPago.plazoEjecucion;
             consultaContrato._contratoSolicitudPago.rutContrato = _consultaContrato._contratoSolicitudPago.rutContrato;
             consultaContrato._contratoSolicitudPago.rutContratoConsultaRat = _consultaContrato._contratoSolicitudPago.rutContratoConsultaRat;
             consultaContrato._contratoSolicitudPago.dvContratoConsultaRat = _consultaContrato._contratoSolicitudPago.dvContratoConsultaRat;
             consultaContrato._contratoSolicitudPago.idTipoProveedor = _consultaContrato._contratoSolicitudPago.idTipoProveedor;
             consultaContrato._contratoSolicitudPago.nombreTipoProveedor = _consultaContrato._contratoSolicitudPago.nombreTipoProveedor;
             consultaContrato._contratoSolicitudPago.fechaResolucionContrato = _consultaContrato._contratoSolicitudPago.fechaResolucionContrato;
             consultaContrato._contratoSolicitudPago.fechaInicioContrato = _consultaContrato._contratoSolicitudPago.fechaInicioContrato;
             consultaContrato._contratoSolicitudPago.nombreRazonSocial = _consultaContrato._contratoSolicitudPago.nombreRazonSocial;
             consultaContrato._contratoSolicitudPago.montoContrato = _consultaContrato._contratoSolicitudPago.montoContrato;
             consultaContrato._contratoSolicitudPago.montoPorPagar = _consultaContrato._contratoSolicitudPago.montoPorPagar;
             consultaContrato._contratoSolicitudPago.montoComprometidos = _consultaContrato._contratoSolicitudPago.montoComprometidos;
             consultaContrato._contratoSolicitudPago.montoPagados = _consultaContrato._contratoSolicitudPago.montoPagados;
             

            _consultaContrato._contratoSolicitudPago.numeroResolucionContrato = (int)numeroResolucion;

            if (_consultaContrato._contratoSolicitudPago.idContrato > 0)
            {
                consultaContrato._contratoSolicitudPagoList = contratoServiciosEntitiesFactory.obtieneServicios(consultaContrato._contratoSolicitudPago.idContrato);
                
                //_consultaContrato._solicitudEntities = contratoSolicitudPagoEntitiesFactory.obtieneSolicitudATPrevia(_consultaContrato._contratoSolicitudPago.idContrato);
            }

            
            return consultaContrato;
        }
        public static object ObtieneContrato(long numeroResolucion, long idRegion)
        {
            ConsultaContrato _consultaContrato = new ConsultaContrato();
            ConsultaSolicitudContrato _ObtieneServicio = new ConsultaSolicitudContrato();

            _consultaContrato._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneContrato(numeroResolucion, idRegion);
            _consultaContrato._contratoSolicitudPago.numeroResolucionContrato = (int)numeroResolucion;

            if (_consultaContrato._contratoSolicitudPago.idContrato > 0)
            {
                _ObtieneServicio._contratoSolicitudPagoList = contratoServiciosEntitiesFactory.obtieneServicios(_consultaContrato._contratoSolicitudPago.idContrato);
                _consultaContrato._contratoSolicitudPagoList = _ObtieneServicio._contratoSolicitudPagoList;
                //_consultaContrato._solicitudEntities = contratoSolicitudPagoEntitiesFactory.obtieneSolicitudATPrevia(_consultaContrato._contratoSolicitudPago.idContrato);
            }

            return _consultaContrato;
        }

        public static List<RegionesEntities> getListRegiones()
        {
            RegionesEntities _Regiones = new RegionesEntities();
            _Regiones.lstRegiones = RegionesEntitiesFactory.getListRegiones();

            return _Regiones.lstRegiones;
        }
        public static RegionesEntities getRegion(int idRegion)
        {
            return RegionesEntitiesFactory.getRegion(idRegion);
        }
        public static List<ProvinciasEntities> getListProvinciasRegion(long idRegion)
        {
            ProvinciasEntities _Provincias = new ProvinciasEntities();
            _Provincias.lstProvincias = ProvinciasEntitiesFactory.getListProvinciaRegion(idRegion);

            return _Provincias.lstProvincias;
        }
        public static List<ProvinciasEntities> getListProvincias()
        {
            ProvinciasEntities _Provincias = new ProvinciasEntities();
            _Provincias.lstProvincias = ProvinciasEntitiesFactory.getListProvincias();

            return _Provincias.lstProvincias;
        }
        public static List<ComunasEntities> getListComunasProvincia(int idProvincia)
        {
            ComunasEntities _Comunas = new ComunasEntities();
            _Comunas.lstComunas = ComunasEntitiesFactory.getComunasProvincia(idProvincia);

            return _Comunas.lstComunas;
        }
        public static List<ComunasEntities> getListComunas()
        {
            ComunasEntities _Comunas = new ComunasEntities();
            _Comunas.lstComunas = ComunasEntitiesFactory.getListComunas();

            return _Comunas.lstComunas;
        }
        public static ComunasEntities getComuna(int idComuna)
        {
            ComunasEntities _Comunas = new ComunasEntities();
            _Comunas = ComunasEntitiesFactory.getComuna(idComuna);

            return _Comunas;
        }
    }
}

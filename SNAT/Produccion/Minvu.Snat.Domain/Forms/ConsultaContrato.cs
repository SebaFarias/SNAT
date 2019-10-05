using Minvu.Snat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minvu.Snat.Domain.Forms
{
    public class ConsultaContrato
    {
        public contratoEntities _contratoEntities { get; set; }
        public FuncionarioEntities _funcionarioEntities { get; set; }
        public List<ProvinciasEntities> lstProvincias { get; set; }
        public List<ComunasEntities> lstComunas { get; set; }
        public List<RegionesEntities> _regionPresupuestoEntities { get; set; }
        public contratoSolicitudPagoEntities _contratoSolicitudPago { get; set; }
        public List<contratoServiciosEntities> _contratoSolicitudPagoList { get; set; }
        public List<contratoSolicitudPagoEntities> _contratoSolicitudPagoEntities { get; set; }
        public contratoSolicitudPagoEntities _solicitudContratoPagoEntities { get; set; }
        public RegionesEntities _regionEntities { get; set; }
        public List<RegionesEntities> _listRegionEntities { get; set; }
        public maestroTipoPagoEntities _auxmaestroTipoPagoEntities { get; set; }
        public List<maestroTipoPagoEntities> _listMaestroTipoPagoEntities { get; set; }
        public string codSalida { get; set; }
        public string codSalidaRatifica { get; set; }
        public ConsultaSolicitudContrato consultaSolicitudContrato { get; set; }
    }
    public class ConsultaContratoFactory
    {
        //public static object getInfoInicialNacional(string userName, long anno)
        //{
        //    ConsultaContrato _PresupuestoRegional = new ConsultaContrato();
        //    _PresupuestoRegional._listRegionEntities = RegionesEntitiesFactory.getListRegiones();
        //    //_PresupuestoRegional._regionPresupuestoEntities = RegionesEntitiesFactory.getListRegionesPresupuesto();
        //    _PresupuestoRegional.lstProvincias = new List<ProvinciasEntities>();
        //    _PresupuestoRegional.lstComunas = new List<ComunasEntities>();
        //    _PresupuestoRegional._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.getRegionUser(userName);
        //    _PresupuestoRegional.lstProvincias = getListProvinciasRegion(_PresupuestoRegional._contratoSolicitudPago.idRegion);
        //    //_PresupuestoRegional._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneMontoPresupuesto(userName, anno);
        //    //_PresupuestoRegional._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneMontoPresupuesto(userName, anno);

        //    return _PresupuestoRegional;
        //}

        public static ConsultaContrato getInstanciasFormulario(string userName, long anno, int region)
        {
            ConsultaContrato _aux = new ConsultaContrato();

            _aux._listMaestroTipoPagoEntities = maestroTipoPagoEntitiesFactory.getListMaestroTipoPago();
            _aux._listMaestroTipoPagoEntities = _aux._listMaestroTipoPagoEntities.OrderBy(x => x.nombreMaestroTipoPago).ToList();
            _aux._listRegionEntities = RegionesEntitiesFactory.getListRegiones();

            _aux._contratoEntities = new contratoEntities();
            _aux._contratoEntities.lstAnoPresupuesto = contratoEntitiesFactory.getlistAnoPresupuesto();
            _aux._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneCabecera(userName, anno, region);
            //_aux._contratoSolicitudPagoEntities =  (anno, region);
            //getMontoPresupuesto

            return _aux;
        }




        public static object getInfoInicial(string userName, long anno, bool esNacional)
        {
            ConsultaContrato _PresupuestoRegional = new ConsultaContrato();

            _PresupuestoRegional._listRegionEntities = RegionesEntitiesFactory.getListRegiones();
            //_PresupuestoRegional._regionPresupuestoEntities = RegionesEntitiesFactory.getListRegionesPresupuesto();
            _PresupuestoRegional.lstProvincias = new List<ProvinciasEntities>();
            _PresupuestoRegional.lstComunas = new List<ComunasEntities>();
            _PresupuestoRegional._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.getRegionUser(userName);

            if (!esNacional)
            {
                _PresupuestoRegional.lstProvincias = getListProvinciasRegion(_PresupuestoRegional._contratoSolicitudPago.idRegion);
            }
            //_PresupuestoRegional._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneMontoPresupuesto(userName, anno);
            _PresupuestoRegional._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneMontoPresupuesto(userName, anno);

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

            _Contrato._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneMontoPresupuesto(uSUARIO, aNNOCONTRATO);
            _Contrato._listRegionEntities = RegionesEntitiesFactory.getListRegiones();
            //_Contrato.lstProvincias = new List<ProvinciasEntities>();
            _Contrato.lstComunas = new List<ComunasEntities>();
            _Contrato.lstProvincias = new List<ProvinciasEntities>();
            _Contrato.codSalida = _Contrato._contratoEntities.codigoSalida;
            _Contrato._contratoEntities.idRegion = cODIGOREGION;
            _Contrato.lstProvincias = ProvinciasEntitiesFactory.getListProvinciaRegion(cODIGOREGION);
            if (_Contrato.codSalida == "-2")
            {
                _Contrato.lstProvincias = ProvinciasEntitiesFactory.getListProvinciaRegion(cODIGOREGION);
                _Contrato.lstComunas = ComunasEntitiesFactory.getComunasProvincia(cODIGOPROVINCIA);
                _Contrato._contratoEntities.idRegion = cODIGOREGION;
                _Contrato._contratoEntities.idProvincia = cODIGOPROVINCIA;
                _Contrato._contratoEntities.idComuna = cODIGOCOMUNA;
            }

            return _Contrato;
        }

        public static object getMontoPresupuesto(long anno, int idRegion)
        {
            ConsultaContrato _PresupuestoRegional = new ConsultaContrato();
            _PresupuestoRegional._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.ObtieneMontoPresupuestoRegional(anno, idRegion);

            return _PresupuestoRegional;
        }

        public static object getNumeroResolucion(long numeroResolucion)
        {
            ConsultaContrato _ResolucionContrato = new ConsultaContrato();
            _ResolucionContrato._contratoSolicitudPago = contratoSolicitudPagoEntitiesFactory.obtieneNumeroResolucion(numeroResolucion);

            return _ResolucionContrato;
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

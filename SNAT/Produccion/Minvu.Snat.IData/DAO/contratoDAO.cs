using Minvu.Snat.Helper;
using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Minvu.Snat.IData.DAO
{
    public class contratoDAO
    {
        public static INSERTA_CONTRATO_SAT_SET_Result InsertaContrato(int rUTPROVEEDOR, string dVPROVEEDOR, string nOMBREPROVEEDOR, int tIPOPROVEEDOR,
            int aNNOCONTRATO, int cODIGOREGION, int cODIGOPROVINCIA, int cODIGOCOMUNA, int pLAZOEJECUCION, string pROPIEDADTERRENO, int nUMERORESOLUCION,
            DateTime fECHARESOLUCIONCONTRATO, DateTime fECHAINICIOCONTRATO, string nOMBREARCHIVO, long tIPOSERVICIO, int eSTADOTIPOSERVICIO1,
            int eSTADOTIPOSERVICIO2, int eSTADOTIPOSERVICIO3, int eSTADOTIPOSERVICIO4, int eSTADOTIPOSERVICIO5, int eSTADOTIPOSERVICIO6,
            int eSTADOTIPOSERVICIO7, string nOMBRETIPOSERVICIO8, string pRODUCTOCONTRATO, string dESCRIPCIONPRODUCTO, int mONTOCONTRATO, string oBSERVACIONCONTRATO,
            string uSUARIO)
        {
            try
            {
                INSERTA_CONTRATO_SAT_SET_Result _mae = new INSERTA_CONTRATO_SAT_SET_Result();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecContrato = contexto.INSERTA_CONTRATO_SAT_SET(rUTPROVEEDOR, dVPROVEEDOR, nOMBREPROVEEDOR, tIPOPROVEEDOR,
                        aNNOCONTRATO, cODIGOREGION, cODIGOPROVINCIA, cODIGOCOMUNA, pLAZOEJECUCION, pROPIEDADTERRENO, nUMERORESOLUCION,
                        fECHARESOLUCIONCONTRATO, fECHAINICIOCONTRATO, nOMBREARCHIVO, tIPOSERVICIO, eSTADOTIPOSERVICIO1, eSTADOTIPOSERVICIO2, eSTADOTIPOSERVICIO3,
                        eSTADOTIPOSERVICIO4, eSTADOTIPOSERVICIO5, eSTADOTIPOSERVICIO6, eSTADOTIPOSERVICIO7, nOMBRETIPOSERVICIO8,
                        pRODUCTOCONTRATO, dESCRIPCIONPRODUCTO, mONTOCONTRATO, oBSERVACIONCONTRATO, uSUARIO);


                    foreach (var a in qAsistecContrato)
                    {
                        _mae.MSG = a.MSG;
                        _mae.err = a.err;
                    }

                    return _mae;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static ups_ins_FINANZAS_ORDEN_AT_SIMPLICIFICADOYPREVIA_Result InsertaRatificacion(long codigoOrdenAT, int lineaOrigen, int lineaAplicacion, int tipoLlamado, decimal montoUF,
                                                                                                 int montoPesos, string nota, int chequeRut, string chequeDgv, string chequeNombres,
                                                                                                 int mandatoRut, string mandatoDgv, string mandatoNombres, int funcionarioCreaRut, int organismo, int indicaATPrevia)
        {
            Log.Instance.Info("Error de prueba: "+ " codigoOrdenAT : " + codigoOrdenAT + " -lineaOrigen : " + lineaOrigen + " -lineaAplicacion :" + lineaAplicacion + " -tipoLlamado :" + tipoLlamado + " -montoUF :" + montoUF + " -montoPesos :" + montoPesos + " -nota :" + nota + " -chequeRut :" + chequeRut + " -chequeDgv :" + chequeDgv + " -chequeNombres :" + chequeNombres
                                                                                                 + " -mandatoRut :" + mandatoRut + " -mandatoDgv :" + mandatoDgv + " -mandatoNombres :" + mandatoNombres + " - funcionarioCreaRut:" + funcionarioCreaRut + " - organismo : " + organismo + " - indicaATPrevia" + indicaATPrevia);

            //El tercero Mandanta va en mandante Independiente de los montos del Cheque
            //Informacion de Respaldo Mauricio Contreras Torre
            try
            {
                ups_ins_FINANZAS_ORDEN_AT_SIMPLICIFICADOYPREVIA_Result _mae = new ups_ins_FINANZAS_ORDEN_AT_SIMPLICIFICADOYPREVIA_Result();

                using (Pago_Subsidios_FuenteEntities contexto = new Pago_Subsidios_FuenteEntities())
                {
                    var qAsistecRatifica = contexto.ups_ins_FINANZAS_ORDEN_AT_SIMPLICIFICADOYPREVIA(codigoOrdenAT, lineaOrigen, lineaAplicacion, tipoLlamado, montoUF,
                        montoPesos, nota, chequeRut, chequeDgv, chequeNombres, mandatoRut, mandatoDgv, mandatoNombres, funcionarioCreaRut, organismo, indicaATPrevia).FirstOrDefault();

                    if (qAsistecRatifica != null)
                    {
                        _mae.Estado = qAsistecRatifica.Estado;
                        _mae.Mensaje = qAsistecRatifica.Mensaje;
                    }

                    return _mae;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static void ActualizaEstadoSolicitud(long idSolicitud, int estadoSolicitud)
        {
            try
            {
                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecSolicitudATP = (from a in contexto.SOLICITUD_PAGO_ATP
                                                where a.IDSOLICITUDATP == idSolicitud
                                                select a).FirstOrDefault();

                    qAsistecSolicitudATP.IDMAESTROESTADOSOLICITUD = estadoSolicitud;
                    qAsistecSolicitudATP.VBSERVICIOSOLICITUDPAGOATP = true;  //MMR

                    contexto.SaveChanges();

                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static bool ActualizaEstadoInformaciónRatificadorSolicitud(long idSolicitud, string nombreUsuario)
        {
            try
            {
                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecSolicitudATP = (from a in contexto.SOLICITUD_PAGO_ATP
                                                where a.IDSOLICITUDATP == idSolicitud
                                                select a).FirstOrDefault();

                    qAsistecSolicitudATP.VBFECHASOLICITUDPAGOATP = DateTime.Now;
                    qAsistecSolicitudATP.VBRESPONSABLESOLICITUDPAGOATP = nombreUsuario;
                    qAsistecSolicitudATP.VBSERVICIOSOLICITUDPAGOATP = true;
                    qAsistecSolicitudATP.IDMAESTROESTADOSOLICITUD = 7;//MMR

                    contexto.SaveChanges();

                }
                return true;
            }
            catch (Exception Ex)
            {
                return false;
                throw Ex;
            }
        }

        public static INSERTA_SOLICITUD_ATP_Result InsertaSolicitud(long iDSOLICITUDATP, long iDCONTRATO, int aNNOPRESUPUESTO, long mONTOPRESUPESTO, int cODIGOREGION,
                                                                    long mONTOPAGADO, long mONTOCOMPROMETIDO, long mONTOPORPAGAR, string aCTIVIDADES, long? nUMEROBOLFACT, DateTime? fECHABOLETAFACT, long mONTOTOTALAPAGAR,
                                                                    bool vBSERVICIO, string vBRESPONSABLE, DateTime? vBFECHA, long eSTADOSOLICITUD,long? iDMAESTROTIPOPAGO)
        {
            try
            {
                INSERTA_SOLICITUD_ATP_Result _mae = new INSERTA_SOLICITUD_ATP_Result();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {

                    if(iDMAESTROTIPOPAGO == 1)
                    {
                        eSTADOSOLICITUD = 3;
                    
                    }

                    string t = iDSOLICITUDATP.ToString() + "," + iDCONTRATO.ToString() + "," + aNNOPRESUPUESTO.ToString() + "," + mONTOPRESUPESTO.ToString() + "," + cODIGOREGION.ToString() + "," +
                                                               mONTOPAGADO.ToString() + "," + mONTOCOMPROMETIDO.ToString() + "," + mONTOPORPAGAR.ToString() + "," + aCTIVIDADES.ToString() + "," + nUMEROBOLFACT.ToString() + "," +
                                                               fECHABOLETAFACT.ToString() + "," + mONTOTOTALAPAGAR.ToString() + "," + vBSERVICIO.ToString() + "," + vBRESPONSABLE.ToString() + "," + vBFECHA.ToString() + "," + eSTADOSOLICITUD.ToString() + "," + iDMAESTROTIPOPAGO.ToString();
                    var qAsistecSolicitud = contexto.INSERTA_SOLICITUD_ATP(iDSOLICITUDATP, iDCONTRATO, aNNOPRESUPUESTO, mONTOPRESUPESTO, cODIGOREGION, mONTOPAGADO,
                                                                            mONTOCOMPROMETIDO, mONTOPORPAGAR, aCTIVIDADES, nUMEROBOLFACT, fECHABOLETAFACT, mONTOTOTALAPAGAR, vBSERVICIO,
                                                                            vBRESPONSABLE, vBFECHA, eSTADOSOLICITUD,iDMAESTROTIPOPAGO).FirstOrDefault();
                    
                    
                    
                    //foreach (var a in qAsistecSolicitud)
                    //{
                    //    _mae.MSG = a.MSG;
                    //    _mae.err = a.err;                    
                    //}
                    Regex regex = new Regex("[a-zA-Z.]");
                    iDSOLICITUDATP = Convert.ToInt64(regex.Replace(qAsistecSolicitud.MSG, "").Trim());

                    if (qAsistecSolicitud != null)
                    {
                        _mae.MSG = qAsistecSolicitud.MSG;
                        _mae.err = qAsistecSolicitud.err;
                    }

                    return _mae;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        public static PRESUPUESTO_REGIONAL GetMontoPresupuesto(long anno)
        {
            try
            {
                PRESUPUESTO_REGIONAL listaMonto = new PRESUPUESTO_REGIONAL();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecPresupuesto = (from a in contexto.RESOLUCION_PRESUPUESTARIA
                                               join b in contexto.PRESUPUESTO_REGIONAL on a.IDRESOLUCIONPRESUPUESTARIA equals b.IDRESOLUCIONPRESUPUESTARIA
                                               //join c in contexto.MAESTRO_TIPO_RESOLUCION_PRESUPUESTARIA on a.IDMAESTROTIPORESOLUCIONPRESUPUESTARIA equals c.IDMAESTROTIPORESOLUCIONPRESUPUESTARIA
                                               where (a.ANORESOLUCIONPRESUPUESTARIA == anno) && (a.ESTADORESOLUCIONPRESUPUESTARIA == true)
                                               select b.MONTOPRESUPUESTOREGIONAL).Sum();

                    if (qAsistecPresupuesto != null)
                    {
                        listaMonto.MONTOPRESUPUESTOREGIONAL = qAsistecPresupuesto.Value;
                    }

                    return listaMonto;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static USERS GetRutUsuario(string user)
        {
            try
            {
                USERS rutUsuario = new USERS();

                using (Rukan_MigraEntities contexto = new Rukan_MigraEntities())
                {
                    var qAsistecUsers = (from a in contexto.USERS
                                         where (a.UserName == user)
                                         select a.Rut).FirstOrDefault();

                    if (qAsistecUsers > 0)
                    {
                        rutUsuario.Rut = qAsistecUsers;
                    }

                    return rutUsuario;
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static SOLICITUD_PAGO_ATP GetResolucionContrato(int idSolicitud)
        {
            try
            {
                CONTRATO_ATP resolucionContrato = new CONTRATO_ATP();
                SOLICITUD_PAGO_ATP solicitud = new SOLICITUD_PAGO_ATP();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecContrato = (from a in contexto.SOLICITUD_PAGO_ATP
                                            join b in contexto.CONTRATO_ATP on a.IDCONTRATOATP equals b.IDCONTRATOATP
                                            where a.IDSOLICITUDATP == idSolicitud
                                            select new
                                            {
                                                a.NUMEROBOLETAFACTURASOLICITUDPAGOATP,
                                                a.FECHABOLETAFACTURASOLICITUDPAGOATP,
                                                a.MONTOTOTALAPAGARSOLICITUDPAGOATP,
                                                a.VBSERVICIOSOLICITUDPAGOATP,
                                                a.VBFECHASOLICITUDPAGOATP,
                                                a.VBRESPONSABLESOLICITUDPAGOATP,
                                                b.NUMERORESOLUCIONCONTRATOATP,
                                                a.IDMAESTROESTADOSOLICITUD,
                                                a.IDMAESTROTIPOPAGO
                                            }).FirstOrDefault();

                    if (qAsistecContrato != null)
                    {
                        resolucionContrato.NUMERORESOLUCIONCONTRATOATP = qAsistecContrato.NUMERORESOLUCIONCONTRATOATP;
                        solicitud.CONTRATO_ATP = resolucionContrato;
                        solicitud.NUMEROBOLETAFACTURASOLICITUDPAGOATP = qAsistecContrato.NUMEROBOLETAFACTURASOLICITUDPAGOATP;
                        solicitud.FECHABOLETAFACTURASOLICITUDPAGOATP = qAsistecContrato.FECHABOLETAFACTURASOLICITUDPAGOATP;
                        solicitud.MONTOTOTALAPAGARSOLICITUDPAGOATP = qAsistecContrato.MONTOTOTALAPAGARSOLICITUDPAGOATP;
                        solicitud.VBSERVICIOSOLICITUDPAGOATP = qAsistecContrato.VBSERVICIOSOLICITUDPAGOATP;
                        solicitud.VBFECHASOLICITUDPAGOATP = qAsistecContrato.VBFECHASOLICITUDPAGOATP;
                        solicitud.VBRESPONSABLESOLICITUDPAGOATP = qAsistecContrato.VBRESPONSABLESOLICITUDPAGOATP;
                        solicitud.IDMAESTROESTADOSOLICITUD = qAsistecContrato.IDMAESTROESTADOSOLICITUD;
                        solicitud.IDMAESTROTIPOPAGO = qAsistecContrato.IDMAESTROTIPOPAGO;
                    }
                    return solicitud;
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static CONTRATO_ATP GetMontoContratoRegion(long anno, int idRegion)
        {
            try
            {
                CONTRATO_ATP montoTotal = new CONTRATO_ATP();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecContrato = (from a in contexto.CONTRATO_ATP
                                            join b in contexto.TERRITORIO_CONTRATO on a.IDTERRITORIOCONTRATO equals b.IDTERRITORIOCONTRATO
                                            where (a.AGNOPRESUPUESTOCONTRATOATP == anno) && (b.CODIGOREGIONTERRITORIOCONTRATO == idRegion)
                                            select a.MONTOCONTRATOCONTRATOATP).Sum();

                    if (qAsistecContrato != null)
                    {
                        montoTotal.MONTOCONTRATOCONTRATOATP = qAsistecContrato.Value;
                    }
                    return montoTotal;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static CONTRATO_ATP GetMontoContratoPresupuesto(long anno)
        {
            try
            {
                CONTRATO_ATP montoTotal = new CONTRATO_ATP();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecContrato = (from a in contexto.CONTRATO_ATP
                                            where a.AGNOPRESUPUESTOCONTRATOATP == anno
                                            select a.MONTOCONTRATOCONTRATOATP).Sum();

                    if (qAsistecContrato != null)
                    {
                        montoTotal.MONTOCONTRATOCONTRATOATP = qAsistecContrato.Value;
                    }
                    return montoTotal;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static CONTRATO_ATP GetExisteNumeroResolucion(long numeroResolucion)
        {
            try
            {
                CONTRATO_ATP numeroResolucionContrato = new CONTRATO_ATP();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecContrato = (from a in contexto.CONTRATO_ATP
                                            where (a.NUMERORESOLUCIONCONTRATOATP == numeroResolucion)
                                            select a.NUMERORESOLUCIONCONTRATOATP).FirstOrDefault();

                    if (qAsistecContrato != null)
                    {
                        numeroResolucionContrato.NUMERORESOLUCIONCONTRATOATP = qAsistecContrato.Value;
                    }

                    return numeroResolucionContrato;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static PRESUPUESTO_REGIONAL GetMontoNacional(long anno)
        {
            try
            {
                PRESUPUESTO_REGIONAL listaMontoNacional = new PRESUPUESTO_REGIONAL();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecPresupuesto = (from a in contexto.RESOLUCION_PRESUPUESTARIA
                                               join b in contexto.PRESUPUESTO_REGIONAL on a.IDRESOLUCIONPRESUPUESTARIA equals b.IDRESOLUCIONPRESUPUESTARIA
                                               //join c in contexto.MAESTRO_TIPO_RESOLUCION_PRESUPUESTARIA on a.IDMAESTROTIPORESOLUCIONPRESUPUESTARIA equals c.IDMAESTROTIPORESOLUCIONPRESUPUESTARIA
                                               where (a.ANORESOLUCIONPRESUPUESTARIA == anno) && (a.ESTADORESOLUCIONPRESUPUESTARIA == true)
                                               select b.MONTOPRESUPUESTOREGIONAL).Sum();

                    if (qAsistecPresupuesto != null)
                    {
                        listaMontoNacional.MONTOPRESUPUESTOREGIONAL = qAsistecPresupuesto.Value;
                    }

                    return listaMontoNacional;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static List<anoPresupuesto> getListAno()
        {
            try
            {
                SNAT_SIMPLIFICADO_V_CONSULTA_ANO_CONTRATO_ATP_Result _mae = new SNAT_SIMPLIFICADO_V_CONSULTA_ANO_CONTRATO_ATP_Result();
                List<anoPresupuesto> _ListanoPresupuesto = new List<anoPresupuesto>();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecSolicitud = contexto.SNAT_SIMPLIFICADO_V_CONSULTA_ANO_CONTRATO_ATP();

                    if (qAsistecSolicitud != null)
                    {

                        foreach (var item in qAsistecSolicitud)
                        {
                            anoPresupuesto _aux = new anoPresupuesto();

                            _aux.ano = (int)item.AGNOPRESUPUESTOCONTRATOATP;
                            _aux.idAno = (int)item.AGNOPRESUPUESTOCONTRATOATP;

                            Log.Instance.Info(_aux.ano + " - metodo: getListAno");

                            _ListanoPresupuesto.Add(_aux);

                        }
                    }


                    return _ListanoPresupuesto;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex + " - metodo: getListAno");
                throw ex;
            }
        }

        public static PRESUPUESTO_REGIONAL GetMontoPresupuestoRegion(long anno, int idRegion)
        {
            try
            {
                PRESUPUESTO_REGIONAL listaMonto = new PRESUPUESTO_REGIONAL();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecPresupuesto = (from a in contexto.RESOLUCION_PRESUPUESTARIA
                                               join b in contexto.PRESUPUESTO_REGIONAL on a.IDRESOLUCIONPRESUPUESTARIA equals b.IDRESOLUCIONPRESUPUESTARIA
                                               //join c in contexto.MAESTRO_TIPO_RESOLUCION_PRESUPUESTARIA on a.IDMAESTROTIPORESOLUCIONPRESUPUESTARIA equals c.IDMAESTROTIPORESOLUCIONPRESUPUESTARIA
                                               where (a.ANORESOLUCIONPRESUPUESTARIA == anno) && (a.ESTADORESOLUCIONPRESUPUESTARIA == true) && (b.CODIGOREGIONPRESUPUESTOREGIONAL == idRegion)
                                               select new { b.IDRESOLUCIONPRESUPUESTARIA, b.MONTOPRESUPUESTOREGIONAL }).FirstOrDefault();

                    if (qAsistecPresupuesto != null)
                    {
                        listaMonto.IDRESOLUCIONPRESUPUESTARIA = qAsistecPresupuesto.IDRESOLUCIONPRESUPUESTARIA;
                        listaMonto.MONTOPRESUPUESTOREGIONAL = qAsistecPresupuesto.MONTOPRESUPUESTOREGIONAL;
                    }

                    return listaMonto;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static ELIMINA_CONTRATO_Result EliminaContrato(int idContrato)
        {
            try
            {
                ELIMINA_CONTRATO_Result _mae = new ELIMINA_CONTRATO_Result();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecSolicitud = contexto.ELIMINA_CONTRATO(idContrato);
                    foreach (var a in qAsistecSolicitud)
                    {
                        _mae.MSG = a.MSG;
                        _mae.err = a.err;
                    }

                    return _mae;
                }
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static ELIMINA_SOLICITUD_Result EliminaSolicitud(int idSolicitud)
        {
            try
            {
                ELIMINA_SOLICITUD_Result _mae = new ELIMINA_SOLICITUD_Result();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecSolicitud = contexto.ELIMINA_SOLICITUD(idSolicitud);
                    foreach (var a in qAsistecSolicitud)
                    {
                        _mae.MSG = a.MSG;
                        _mae.err = a.err;
                    }

                    return _mae;
                }
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static SOLICITUD_PAGO_ATP GetSolicitudPago()
        {
            try
            {
                SOLICITUD_PAGO_ATP numSolicitud = new SOLICITUD_PAGO_ATP();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecSolicitudATP = (from a in contexto.SOLICITUD_PAGO_ATP
                                                select a.IDSOLICITUDATP).Max();

                    if (qAsistecSolicitudATP > 0)
                    {
                        numSolicitud.IDSOLICITUDATP = qAsistecSolicitudATP;
                    }

                    return numSolicitud;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        public static CONTRATO_ATP GetContratoResolucion(long numeroResolucion, long idRegion)
        {
            try
            {
                CONTRATO_ATP listaContrato = new CONTRATO_ATP();
                PROVEEDOR pROVEEDOR = new PROVEEDOR();
                MAESTRO_TIPO_PROVEEDOR mAESTRO_TIPO_PROVEEDOR = new MAESTRO_TIPO_PROVEEDOR();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecContrato = (from c in contexto.CONTRATO_ATP
                                            join p in contexto.PROVEEDOR on c.IDPROVEEDOR equals p.IDPROVEEDOR
                                            join m in contexto.MAESTRO_TIPO_PROVEEDOR on c.IDMAESTROTIPOPROVEEDOR equals m.IDMAESTROTIPOPROVEEDOR
                                            join t in contexto.TERRITORIO_CONTRATO on c.IDTERRITORIOCONTRATO equals t.IDTERRITORIOCONTRATO

                                            where (c.NUMERORESOLUCIONCONTRATOATP == numeroResolucion) && (t.CODIGOREGIONTERRITORIOCONTRATO == idRegion)
                                            select new
                                            {
                                                p.RUTPROVEEDOR,
                                                p.DVPROVEDIGITOVERIFICADORPROVEEDOR,
                                                p.NOMBREPROVEEDOR,
                                                c.IDPROVEEDOR,
                                                c.NUMERORESOLUCIONCONTRATOATP,
                                                c.IDCONTRATOATP,
                                                c.PLAZOEJECUCIONCONTRATOATP,
                                                c.FECHAINICIOCONTRATOATP,
                                                c.FECHARESOLUCIONCONTRATOATP,
                                                c.MONTOCONTRATOCONTRATOATP,
                                                m.IDMAESTROTIPOPROVEEDOR,
                                                m.NOMBREMAESTROTIPOPROVEEDOR,

                                            }).FirstOrDefault();

                    if (qAsistecContrato != null)
                    {
                        pROVEEDOR.RUTPROVEEDOR = qAsistecContrato.RUTPROVEEDOR;
                        pROVEEDOR.DVPROVEDIGITOVERIFICADORPROVEEDOR = qAsistecContrato.DVPROVEDIGITOVERIFICADORPROVEEDOR;
                        pROVEEDOR.NOMBREPROVEEDOR = qAsistecContrato.NOMBREPROVEEDOR;

                        listaContrato.PROVEEDOR = pROVEEDOR;
                        listaContrato.IDPROVEEDOR = qAsistecContrato.IDPROVEEDOR;
                        listaContrato.NUMERORESOLUCIONCONTRATOATP = qAsistecContrato.NUMERORESOLUCIONCONTRATOATP;
                        listaContrato.IDCONTRATOATP = qAsistecContrato.IDCONTRATOATP;
                        listaContrato.PLAZOEJECUCIONCONTRATOATP = qAsistecContrato.PLAZOEJECUCIONCONTRATOATP;
                        listaContrato.FECHAINICIOCONTRATOATP = qAsistecContrato.FECHAINICIOCONTRATOATP;
                        listaContrato.FECHARESOLUCIONCONTRATOATP = qAsistecContrato.FECHARESOLUCIONCONTRATOATP;
                        listaContrato.MONTOCONTRATOCONTRATOATP = qAsistecContrato.MONTOCONTRATOCONTRATOATP;

                        mAESTRO_TIPO_PROVEEDOR.IDMAESTROTIPOPROVEEDOR = qAsistecContrato.IDMAESTROTIPOPROVEEDOR;
                        mAESTRO_TIPO_PROVEEDOR.NOMBREMAESTROTIPOPROVEEDOR = qAsistecContrato.NOMBREMAESTROTIPOPROVEEDOR;

                        listaContrato.MAESTRO_TIPO_PROVEEDOR = mAESTRO_TIPO_PROVEEDOR;
                    }

                    return listaContrato;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        public static SOLICITUD_PAGO_ATP GetMontoComprometidoContrato(long idContrato)
        {
            try
            {
                SOLICITUD_PAGO_ATP montoComprometido = new SOLICITUD_PAGO_ATP();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecSolicitud = (from a in contexto.SOLICITUD_PAGO_ATP
                                             where (a.IDCONTRATOATP == idContrato)
                                             select a.MONTOCOMPROMETIDOSOLICITUDPAGOATP).Sum();

                    if (qAsistecSolicitud != null)
                    {
                        montoComprometido.MONTOCOMPROMETIDOSOLICITUDPAGOATP = qAsistecSolicitud.Value;
                    }

                    return montoComprometido;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static SOLICITUD_PAGO_ATP GetMontopagadoContrato(long idContrato)
        {
            try
            {
                SOLICITUD_PAGO_ATP montoPagado = new SOLICITUD_PAGO_ATP();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecSolicitud = (from a in contexto.SOLICITUD_PAGO_ATP
                                             where (a.IDCONTRATOATP == idContrato)
                                             select a.MONTOPAGADOSOLICITUDPAGOATP).Sum();

                    if (qAsistecSolicitud != null)
                    {
                        montoPagado.MONTOPAGADOSOLICITUDPAGOATP = qAsistecSolicitud.Value;
                    }
                    else
                    {
                        montoPagado.MONTOPAGADOSOLICITUDPAGOATP = 0;
                    }

                    return montoPagado;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //public static PROVEEDOR GetObtieneProveedor(long idProveedor)
        //{
        //}


        public static SOLICITUD_PAGO_ATP GetSolicitudATPrevia(long idContrato)
        {
            try
            {
                SOLICITUD_PAGO_ATP listaSolicitud = new SOLICITUD_PAGO_ATP();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecSolicitud = (from c in contexto.SOLICITUD_PAGO_ATP
                                             where c.IDCONTRATOATP == idContrato
                                             select new
                                             {
                                                 c.MONTOPAGADOSOLICITUDPAGOATP,
                                                 c.MONTOCOMPROMETIDOSOLICITUDPAGOATP,
                                                 c.MONTOPORPAGARSOLICITUDPAGOATP,
                                                 c.NUMEROBOLETAFACTURASOLICITUDPAGOATP,
                                                 c.FECHABOLETAFACTURASOLICITUDPAGOATP,
                                                 c.MONTOTOTALAPAGARSOLICITUDPAGOATP,
                                                 c.VBSERVICIOSOLICITUDPAGOATP,
                                                 c.VBFECHASOLICITUDPAGOATP
                                             }).FirstOrDefault();

                    if (qAsistecSolicitud != null)
                    {
                        listaSolicitud.MONTOPAGADOSOLICITUDPAGOATP = qAsistecSolicitud.MONTOPAGADOSOLICITUDPAGOATP;
                        listaSolicitud.MONTOCOMPROMETIDOSOLICITUDPAGOATP = qAsistecSolicitud.MONTOCOMPROMETIDOSOLICITUDPAGOATP;
                        listaSolicitud.MONTOPORPAGARSOLICITUDPAGOATP = qAsistecSolicitud.MONTOPORPAGARSOLICITUDPAGOATP;
                        listaSolicitud.NUMEROBOLETAFACTURASOLICITUDPAGOATP = qAsistecSolicitud.NUMEROBOLETAFACTURASOLICITUDPAGOATP;
                        listaSolicitud.FECHABOLETAFACTURASOLICITUDPAGOATP = qAsistecSolicitud.FECHABOLETAFACTURASOLICITUDPAGOATP;
                        listaSolicitud.MONTOTOTALAPAGARSOLICITUDPAGOATP = qAsistecSolicitud.MONTOTOTALAPAGARSOLICITUDPAGOATP;
                        listaSolicitud.VBSERVICIOSOLICITUDPAGOATP = qAsistecSolicitud.VBSERVICIOSOLICITUDPAGOATP;
                        listaSolicitud.VBFECHASOLICITUDPAGOATP = qAsistecSolicitud.VBFECHASOLICITUDPAGOATP;
                    }

                    return listaSolicitud;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static List<CONSULTA_LISTADO_SOLICITUD_Result> GetListaSolicitud(int accion, long numeroSolicitud, int codigoRegion, int annoPresupuesto)
        {
            try
            {
                List<CONSULTA_LISTADO_SOLICITUD_Result> listaSolicitud = new List<CONSULTA_LISTADO_SOLICITUD_Result>();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecSolicitud = contexto.CONSULTA_LISTADO_SOLICITUD(accion, numeroSolicitud, codigoRegion, annoPresupuesto);
                    foreach (var a in qAsistecSolicitud)
                    {
                        CONSULTA_LISTADO_SOLICITUD_Result _mae = new CONSULTA_LISTADO_SOLICITUD_Result();

                        _mae.IDSOLICITUDATP = a.IDSOLICITUDATP;
                        _mae.NUMERORESOLUCIONCONTRATOATP = a.NUMERORESOLUCIONCONTRATOATP;
                        _mae.CODIGOREGIONSOLICITUDPAGOATP = a.CODIGOREGIONSOLICITUDPAGOATP;
                        _mae.RUTPROVEEDOR = a.RUTPROVEEDOR;
                        _mae.DVPROVEDIGITOVERIFICADORPROVEEDOR = a.DVPROVEDIGITOVERIFICADORPROVEEDOR;
                        _mae.NOMBREPROVEEDOR = a.NOMBREPROVEEDOR;
                        _mae.MONTOTOTALAPAGARSOLICITUDPAGOATP = a.MONTOTOTALAPAGARSOLICITUDPAGOATP;
                        _mae.TIPOSERVICIOTIPOACTIVIDADMONTO = a.TIPOSERVICIOTIPOACTIVIDADMONTO;
                        _mae.NOMBREMAESTROESTADOSOLICITUD = a.NOMBREMAESTROESTADOSOLICITUD;
                        _mae.FECHACREACIONSOLICITUDPAGOATP = a.FECHACREACIONSOLICITUDPAGOATP;

                        listaSolicitud.Add(_mae);
                    }

                    return listaSolicitud;
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static List<CONSULTA_LISTADO_CONTRATO_Result> GetListaContrato(int accion, int annoPresupuesto, int codigoRegion, long tipoServicio)
        {
            try
            {
                List<CONSULTA_LISTADO_CONTRATO_Result> listaContrato = new List<CONSULTA_LISTADO_CONTRATO_Result>();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecSolicitud = contexto.CONSULTA_LISTADO_CONTRATO(accion, annoPresupuesto, codigoRegion, tipoServicio);
                    foreach (var a in qAsistecSolicitud)
                    {
                        CONSULTA_LISTADO_CONTRATO_Result _mae = new CONSULTA_LISTADO_CONTRATO_Result();

                        _mae.IDCONTRATOATP = a.IDCONTRATOATP;
                        _mae.CODIGOREGIONTERRITORIOCONTRATO = a.CODIGOREGIONTERRITORIOCONTRATO;
                        _mae.RUTPROVEEDOR = a.RUTPROVEEDOR;
                        _mae.DVPROVEDIGITOVERIFICADORPROVEEDOR = a.DVPROVEDIGITOVERIFICADORPROVEEDOR;
                        _mae.NOMBREPROVEEDOR = a.NOMBREPROVEEDOR;
                        _mae.MONTOCONTRATOCONTRATOATP = a.MONTOCONTRATOCONTRATOATP;
                        _mae.TIPOSERVICIOMAESTROTIPOSERVICIOIGT = a.TIPOSERVICIOMAESTROTIPOSERVICIOIGT;
                        _mae.FECHACREACIONCONTRATOATP = a.FECHACREACIONCONTRATOATP;
                        _mae.CONTRATOS = a.CONTRATOS;
                        _mae.NUMERORESOLUCIONCONTRATOATP = a.NUMERORESOLUCIONCONTRATOATP;
                        _mae.NOMBREMAESTROESTADOCONTRATO = a.NOMBREMAESTROESTADOCONTRATO;

                        listaContrato.Add(_mae);
                    }

                    return listaContrato;
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static CONSULTA_CONTRATO_Result GetContrato(int idContrato)
        {
            try
            {
                CONSULTA_CONTRATO_Result contrato = new CONSULTA_CONTRATO_Result();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecContrato = contexto.CONSULTA_CONTRATO(idContrato);
                    foreach (var a in qAsistecContrato)
                    {

                        contrato.IDCONTRATOATP = a.IDCONTRATOATP;
                        contrato.RUTPROVEEDOR = a.RUTPROVEEDOR;
                        contrato.DVPROVEDIGITOVERIFICADORPROVEEDOR = a.DVPROVEDIGITOVERIFICADORPROVEEDOR;
                        contrato.NOMBREPROVEEDOR = a.NOMBREPROVEEDOR;
                        contrato.NOMBREMAESTROTIPOPROVEEDOR = a.NOMBREMAESTROTIPOPROVEEDOR;
                        contrato.AGNOPRESUPUESTOCONTRATOATP = a.AGNOPRESUPUESTOCONTRATOATP;
                        contrato.CODIGOREGIONTERRITORIOCONTRATO = a.CODIGOREGIONTERRITORIOCONTRATO;
                        contrato.CODIGOPROVINCIATERRITORIOCONTRATO = a.CODIGOPROVINCIATERRITORIOCONTRATO;
                        contrato.CODIGOCOMUNATERRITORIOCONTRATO = a.CODIGOCOMUNATERRITORIOCONTRATO;
                        contrato.NOMBREARCHIVOCONTRATOATP = a.NOMBREARCHIVOCONTRATOATP;
                        contrato.NOMBREPROPIEDADTERRENO = a.NOMBREPROPIEDADTERRENO;
                        contrato.NUMERORESOLUCIONCONTRATOATP = a.NUMERORESOLUCIONCONTRATOATP;
                        contrato.FECHARESOLUCIONCONTRATOATP = a.FECHARESOLUCIONCONTRATOATP;
                        contrato.FECHAINICIOCONTRATOATP = a.FECHAINICIOCONTRATOATP;
                        contrato.PLAZOEJECUCIONCONTRATOATP = a.PLAZOEJECUCIONCONTRATOATP;
                        contrato.NOMBREMAESTROTIPOSERVICIOIGT = a.NOMBREMAESTROTIPOSERVICIOIGT;
                        contrato.PRODUCTOCONTRATOATP = a.PRODUCTOCONTRATOATP;
                        contrato.DESCRIPCIONPRODUCTOCONTRATOATP = a.DESCRIPCIONPRODUCTOCONTRATOATP;
                        contrato.MONTOCONTRATOCONTRATOATP = a.MONTOCONTRATOCONTRATOATP;
                        contrato.OBSERVACIONCONTRATOATP = a.OBSERVACIONCONTRATOATP;
                    }

                    return contrato;
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static List<TIPO_ACTIVIDAD_MONTO> GetMontoServicios(long idSolicitudATP)
        {
            try
            {
                List<TIPO_ACTIVIDAD_MONTO> listaServicios = new List<TIPO_ACTIVIDAD_MONTO>();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecContrato = (from a in contexto.TIPO_ACTIVIDAD_MONTO
                                            join b in contexto.MAESTRO_SERVICIO on a.IDMAESTROSERVICIO equals b.IDMAESTROSERVICIO
                                            where (a.IDSOLICITUDATP == idSolicitudATP)
                                            select new
                                            {
                                                b.NOMBREABREVIADOMAESTROSERVICIO,
                                                b.NOMBREMAESTROSERVICIO,
                                                b.IDMAESTROSERVICIO,
                                                a.MONTOPAGOTIPOACTIVIDADMONTO
                                            }).ToList();

                    foreach (var item in qAsistecContrato)
                    {
                        TIPO_ACTIVIDAD_MONTO servicios = new TIPO_ACTIVIDAD_MONTO();
                        MAESTRO_SERVICIO mAESTRO_SERVICIO = new MAESTRO_SERVICIO();

                        mAESTRO_SERVICIO.NOMBREABREVIADOMAESTROSERVICIO = item.NOMBREABREVIADOMAESTROSERVICIO;
                        mAESTRO_SERVICIO.NOMBREMAESTROSERVICIO = item.NOMBREMAESTROSERVICIO;
                        mAESTRO_SERVICIO.IDMAESTROSERVICIO = item.IDMAESTROSERVICIO;
                        servicios.MAESTRO_SERVICIO = mAESTRO_SERVICIO;
                        servicios.MONTOPAGOTIPOACTIVIDADMONTO = item.MONTOPAGOTIPOACTIVIDADMONTO;

                        listaServicios.Add(servicios);
                    }

                    return listaServicios;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static List<TIPO_SERVICIOS_CONTRATO> GetActividadesServicios(long idContrato)
        {
            try
            {
                List<TIPO_SERVICIOS_CONTRATO> listaServicios = new List<TIPO_SERVICIOS_CONTRATO>();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecContrato = (from t in contexto.TIPO_SERVICIOS_CONTRATO
                                            join m in contexto.MAESTRO_SERVICIO on t.IDMAESTROSERVICIO equals m.IDMAESTROSERVICIO
                                            where (t.IDCONTRATOATP == idContrato) && (t.ESTADOTIPOSERVICIOSCONTRATO == true)
                                            select new
                                            {
                                                m.NOMBREABREVIADOMAESTROSERVICIO,
                                                m.NOMBREMAESTROSERVICIO,
                                                m.IDMAESTROSERVICIO,
                                                t.IDCONTRATOATP,
                                                m.ESTADOMAESTROSERVICIO
                                            }).ToList();

                    foreach (var item in qAsistecContrato)
                    {
                        TIPO_SERVICIOS_CONTRATO servicios = new TIPO_SERVICIOS_CONTRATO();
                        MAESTRO_SERVICIO mAESTRO_SERVICIO = new MAESTRO_SERVICIO();

                        mAESTRO_SERVICIO.NOMBREABREVIADOMAESTROSERVICIO = item.NOMBREABREVIADOMAESTROSERVICIO;
                        mAESTRO_SERVICIO.NOMBREMAESTROSERVICIO = item.NOMBREMAESTROSERVICIO;
                        mAESTRO_SERVICIO.IDMAESTROSERVICIO = item.IDMAESTROSERVICIO;
                        servicios.MAESTRO_SERVICIO = mAESTRO_SERVICIO;
                        servicios.IDCONTRATOATP = item.IDCONTRATOATP;
                        servicios.ESTADOTIPOSERVICIOSCONTRATO = item.ESTADOMAESTROSERVICIO;

                        listaServicios.Add(servicios);
                    }

                    return listaServicios;

                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static List<CONSULTA_ACTIVIDADES_CONTRATO_Result> GetActividadesContrato(long idContrato)
        {
            try
            {
                List<CONSULTA_ACTIVIDADES_CONTRATO_Result> listaServicios = new List<CONSULTA_ACTIVIDADES_CONTRATO_Result>();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecContrato = contexto.CONSULTA_ACTIVIDADES_CONTRATO(idContrato);

                    foreach (var item in qAsistecContrato)
                    {

                        CONSULTA_ACTIVIDADES_CONTRATO_Result _mae = new CONSULTA_ACTIVIDADES_CONTRATO_Result();

                        _mae.NOMBREABREVIADOMAESTROSERVICIO = item.NOMBREABREVIADOMAESTROSERVICIO;
                        _mae.NOMBREMAESTROSERVICIO = item.NOMBREMAESTROSERVICIO;
                        _mae.IDMAESTROSERVICIO = item.IDMAESTROSERVICIO;
                        _mae.MONTOPAGOTIPOACTIVIDADMONTO = item.MONTOPAGOTIPOACTIVIDADMONTO;
                        _mae.IDCONTRATOATP = item.IDCONTRATOATP;
                        _mae.ESTADOMAESTROSERVICIO = item.ESTADOMAESTROSERVICIO;

                        listaServicios.Add(_mae);

                    }

                    return listaServicios;

                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

    }

    public class anoPresupuesto
    {
        public int idAno { get; set; }
        public int ano { get; set; }
    }
}
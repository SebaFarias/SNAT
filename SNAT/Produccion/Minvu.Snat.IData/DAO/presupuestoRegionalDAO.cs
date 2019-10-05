using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minvu.Snat.IData.DAO
{
    public class presupuestoRegionalDAO
    {
        public static INSERTA_PRESUPUESTO_REGIONAL_IGTD_Result InsertaPresupuestoRegional(int anno, int numeroResolucion, DateTime fechaResolucion, int codigoRegion, long montoResolucionRegion, string observacion, string nombreArchivo, string usuario)
        {
            INSERTA_PRESUPUESTO_REGIONAL_IGTD_Result _mae = new INSERTA_PRESUPUESTO_REGIONAL_IGTD_Result();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qAsistecPPPFProyecto = contexto.INSERTA_PRESUPUESTO_REGIONAL_IGTD(Convert.ToInt16(anno), numeroResolucion, fechaResolucion, codigoRegion, montoResolucionRegion, observacion, nombreArchivo, usuario);

                foreach (var a in qAsistecPPPFProyecto)
                {
                    _mae.MSG = a.MSG;
                    _mae.err = a.err;
                }

                return _mae;
            }
        }

        public static List<CONSULTA_PRESUPUESTO_REGIONAL_IGTD_Result> ObtienePresupuestoRegional(int accion, int anno, int numeroResolucion, DateTime fechaResolucion, int codigoRegion)
        {
            List<CONSULTA_PRESUPUESTO_REGIONAL_IGTD_Result> ListaPresupuestoRegional = new List<CONSULTA_PRESUPUESTO_REGIONAL_IGTD_Result>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qAsistecPPPFProyecto = contexto.CONSULTA_PRESUPUESTO_REGIONAL_IGTD(accion, numeroResolucion, anno, codigoRegion);

                foreach (var a in qAsistecPPPFProyecto)
                {
                    CONSULTA_PRESUPUESTO_REGIONAL_IGTD_Result _mae = new CONSULTA_PRESUPUESTO_REGIONAL_IGTD_Result();

                    _mae.NUMERORESOLUCIONPRESUPUESTARIA= a.NUMERORESOLUCIONPRESUPUESTARIA;
                    _mae.ANORESOLUCIONPRESUPUESTARIA = a.ANORESOLUCIONPRESUPUESTARIA;
                    _mae.CODIGOREGIONPRESUPUESTOREGIONAL = a.CODIGOREGIONPRESUPUESTOREGIONAL;
                    _mae.MONTOPRESUPUESTOREGIONAL = a.MONTOPRESUPUESTOREGIONAL;
                    _mae.FECHAPRESUPUESTOREGIONAL = a.FECHAPRESUPUESTOREGIONAL;
                    _mae.ESTADORESOLUCIONPRESUPUESTARIA = a.ESTADORESOLUCIONPRESUPUESTARIA;
                    _mae.NOMBREARCHIVOPRESUPUESTO = a.NOMBREARCHIVOPRESUPUESTO;
                    _mae.MONTOCONTRATOSAT = a.MONTOCONTRATOSAT;
                    _mae.MONTOCONTRATOSET = a.MONTOCONTRATOSET;
                    _mae.PRESUPUESTODISPONIBLE = a.PRESUPUESTODISPONIBLE;
                    _mae.SATSOLICITUDESCOMPROMETIDAS = a.SATSOLICITUDESCOMPROMETIDAS;
                    _mae.MONTOCONTRATOSAT = a.MONTOCONTRATOSAT;
                    _mae.MONTOCONTRATOSET = a.MONTOCONTRATOSET;
                    _mae.PRESUPUESTODISPONIBLE = a.PRESUPUESTODISPONIBLE;
                    _mae.SATSOLICITUDESCOMPROMETIDAS = a.SATSOLICITUDESCOMPROMETIDAS;
                    _mae.SETSOLICITUDESCOMPROMETIDAS = a.SETSOLICITUDESCOMPROMETIDAS;
                    _mae.SETSOLICITUDESPAGADAS = a.SETSOLICITUDESPAGADAS;
                    _mae.SATSOLICITUDESPAGADAS = a.SATSOLICITUDESPAGADAS;

                    ListaPresupuestoRegional.Add(_mae);
                }


                return ListaPresupuestoRegional;
            }
        }

        public static RESOLUCION_PRESUPUESTARIA ObtieneResolucionPresupuesto(int numeroResolucion, int anno)
        {
            try
            {
                RESOLUCION_PRESUPUESTARIA resolucion = new RESOLUCION_PRESUPUESTARIA();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecResolucion = (from a in contexto.RESOLUCION_PRESUPUESTARIA
                                              where (a.NUMERORESOLUCIONPRESUPUESTARIA == numeroResolucion && a.ANORESOLUCIONPRESUPUESTARIA == anno)
                                              select a.NUMERORESOLUCIONPRESUPUESTARIA).FirstOrDefault();
                    if (qAsistecResolucion != null)
                    {
                        resolucion.NUMERORESOLUCIONPRESUPUESTARIA = qAsistecResolucion.Value;
                    }

                    return resolucion;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static List<anoPresupuesto> getListAño()
        {
            try
            {

                RESOLUCION_PRESUPUESTARIA resolucion = new RESOLUCION_PRESUPUESTARIA();
                //Lista de años existentes en la tabla RESOLUCION_PRESUPUESTARIA
                List<anoPresupuesto> lstAñosContrato = new List<anoPresupuesto>();

                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qAsistecResolucion = (from a in contexto.RESOLUCION_PRESUPUESTARIA
                                              select a.ANORESOLUCIONPRESUPUESTARIA).Distinct(); ;
                    if (qAsistecResolucion != null)
                    {
                        foreach (var item in qAsistecResolucion)
                        {
                            anoPresupuesto añosContrato = new anoPresupuesto();

                            añosContrato.ano = (Convert.ToInt32(item));
                            añosContrato.idAno = (Convert.ToInt32(item));
                            lstAñosContrato.Add(añosContrato);
                        }
                    }

                    return lstAñosContrato;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
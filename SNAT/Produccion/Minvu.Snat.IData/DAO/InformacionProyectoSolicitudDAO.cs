using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.IData.DAO
{
    public class InformacionProyectoSolicitudDAO
    {
        public static int Save(INFORMACION_PROYECTO_SOLICITUD objInformacionProyectoSolicitud)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                INFORMACION_PROYECTO_SOLICITUD _inf = new INFORMACION_PROYECTO_SOLICITUD();
                try
                {
                    _inf = contexto.INFORMACION_PROYECTO_SOLICITUD.Where(c => c.IDINFORMACIONPROYECTOSOLICITUD == objInformacionProyectoSolicitud.IDINFORMACIONPROYECTOSOLICITUD).FirstOrDefault<INFORMACION_PROYECTO_SOLICITUD>();

                    if (_inf == null)
                    {
                        _inf = objInformacionProyectoSolicitud;
                        contexto.INFORMACION_PROYECTO_SOLICITUD.Add(_inf);
                    }
                    else
                    {
                        _inf.IDINFORMACIONPROYECTO = objInformacionProyectoSolicitud.IDINFORMACIONPROYECTO;
                        _inf.IDMAESTROPROGRAMA = objInformacionProyectoSolicitud.IDMAESTROPROGRAMA;
                        _inf.IDMAESTROLLAMADO = objInformacionProyectoSolicitud.IDMAESTROLLAMADO;
                        _inf.IDMAESTROMODALIDAD = objInformacionProyectoSolicitud.IDMAESTROMODALIDAD;
                        _inf.IDMAESTROTITULO = objInformacionProyectoSolicitud.IDMAESTROTITULO;
                        _inf.IDMAESTRORESOLUCION = objInformacionProyectoSolicitud.IDMAESTRORESOLUCION;
                        _inf.IDDIRECCION = objInformacionProyectoSolicitud.IDDIRECCION;
                        _inf.IDMAESTROTIPOLOGIA = objInformacionProyectoSolicitud.IDMAESTROTIPOLOGIA;
                        _inf.IDMAESTROALTERNATIVAPOSTULACION = objInformacionProyectoSolicitud.IDMAESTROALTERNATIVAPOSTULACION;
                        _inf.CODIGOPROYECTOINFORMACIONPROYECTOSOLICITUD = objInformacionProyectoSolicitud.CODIGOPROYECTOINFORMACIONPROYECTOSOLICITUD;
                        _inf.NOMBREPROYECTOINFORMACIONPROYECTOSOLICITUD = objInformacionProyectoSolicitud.NOMBREPROYECTOINFORMACIONPROYECTOSOLICITUD;
                        _inf.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTOSOLICITUD = objInformacionProyectoSolicitud.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTOSOLICITUD;
                        _inf.RESOLUCIONATINFORMACIONPROYECTOSOLICITUD = objInformacionProyectoSolicitud.RESOLUCIONATINFORMACIONPROYECTOSOLICITUD;
                        _inf.FECHARESOLUCIONATINFORMACIONPROYECTOSOLICITUD = objInformacionProyectoSolicitud.FECHARESOLUCIONATINFORMACIONPROYECTOSOLICITUD;
                        _inf.CANTIDADVIVIENDASINFORMACIONPROYECTOSOLICITUD = objInformacionProyectoSolicitud.CANTIDADVIVIENDASINFORMACIONPROYECTOSOLICITUD;
                        _inf.CANTIDADBENEFICIARIOSINFORMACIONPROYECTOSOLICITUD = objInformacionProyectoSolicitud.CANTIDADBENEFICIARIOSINFORMACIONPROYECTOSOLICITUD;
                        _inf.MONTOSUBSIDIOBASEINFORMACIONPROYECTOSOLICITUD = objInformacionProyectoSolicitud.MONTOSUBSIDIOBASEINFORMACIONPROYECTOSOLICITUD;
                        _inf.MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTOSOLICITUD = objInformacionProyectoSolicitud.MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTOSOLICITUD;
                        _inf.MARCALOCALIZACIONINFORMACIONPROYECTOSOLICITUD = objInformacionProyectoSolicitud.MARCALOCALIZACIONINFORMACIONPROYECTOSOLICITUD;
                        _inf.MARCAFACTIBILIZACIONINFORMACIONPROYECTOSOLICITUD = objInformacionProyectoSolicitud.MARCAFACTIBILIZACIONINFORMACIONPROYECTOSOLICITUD;
                        _inf.FECHAFACTIBILIZACIONINFORMACIONPROYECTOSOLICITUD = objInformacionProyectoSolicitud.FECHAFACTIBILIZACIONINFORMACIONPROYECTOSOLICITUD;
                        _inf.NUMEROFAMILIASADSCRITASINFORMACIONPROYECTOSOLICITUD = objInformacionProyectoSolicitud.NUMEROFAMILIASADSCRITASINFORMACIONPROYECTOSOLICITUD;
                        _inf.AGNOFACTIBILIDADINFORMACIONPROYECTOSOLICITUD = objInformacionProyectoSolicitud.AGNOFACTIBILIDADINFORMACIONPROYECTOSOLICITUD;
                        _inf.IDMAESTROESTADOPROYECTO = objInformacionProyectoSolicitud.IDMAESTROESTADOPROYECTO;
                        _inf.FECHAINGRESOINFORMACIONPROYECTOSOLICITUD = objInformacionProyectoSolicitud.FECHAINGRESOINFORMACIONPROYECTOSOLICITUD;
                        _inf.PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTOSOLICITUD = objInformacionProyectoSolicitud.PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTOSOLICITUD;
                        _inf.NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTOSOLICITUD = objInformacionProyectoSolicitud.NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTOSOLICITUD;

                    }

                    contexto.SaveChanges();
                    return (int)_inf.IDINFORMACIONPROYECTOSOLICITUD;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        }

        public static INFORMACION_PROYECTO_SOLICITUD get(long idInformacionProyectoSolicitud)
        {
            INFORMACION_PROYECTO_SOLICITUD _inf = new INFORMACION_PROYECTO_SOLICITUD();

            try
            {
                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qInformacionProyectoSolicitud = from a in contexto.INFORMACION_PROYECTO_SOLICITUD
                                                        where a.IDINFORMACIONPROYECTOSOLICITUD == idInformacionProyectoSolicitud
                                                        select a;
                    foreach (var a in qInformacionProyectoSolicitud)
                    {
                        _inf.IDINFORMACIONPROYECTOSOLICITUD = a.IDINFORMACIONPROYECTOSOLICITUD;
                        _inf.IDINFORMACIONPROYECTO = a.IDINFORMACIONPROYECTO;
                        _inf.IDMAESTROPROGRAMA = a.IDMAESTROPROGRAMA;
                        _inf.IDMAESTROLLAMADO = a.IDMAESTROLLAMADO;
                        _inf.IDMAESTROMODALIDAD = a.IDMAESTROMODALIDAD;
                        _inf.IDMAESTROTITULO = a.IDMAESTROTITULO;
                        _inf.IDMAESTRORESOLUCION = a.IDMAESTRORESOLUCION;
                        _inf.IDDIRECCION = a.IDDIRECCION;
                        _inf.IDMAESTROTIPOLOGIA = a.IDMAESTROTIPOLOGIA;
                        _inf.IDMAESTROALTERNATIVAPOSTULACION = a.IDMAESTROALTERNATIVAPOSTULACION;
                        _inf.CODIGOPROYECTOINFORMACIONPROYECTOSOLICITUD = a.CODIGOPROYECTOINFORMACIONPROYECTOSOLICITUD;
                        _inf.NOMBREPROYECTOINFORMACIONPROYECTOSOLICITUD = a.NOMBREPROYECTOINFORMACIONPROYECTOSOLICITUD;
                        _inf.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTOSOLICITUD = a.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTOSOLICITUD;
                        _inf.RESOLUCIONATINFORMACIONPROYECTOSOLICITUD = a.RESOLUCIONATINFORMACIONPROYECTOSOLICITUD;
                        _inf.FECHARESOLUCIONATINFORMACIONPROYECTOSOLICITUD = a.FECHARESOLUCIONATINFORMACIONPROYECTOSOLICITUD;
                        _inf.CANTIDADVIVIENDASINFORMACIONPROYECTOSOLICITUD = a.CANTIDADVIVIENDASINFORMACIONPROYECTOSOLICITUD;
                        _inf.CANTIDADBENEFICIARIOSINFORMACIONPROYECTOSOLICITUD = a.CANTIDADBENEFICIARIOSINFORMACIONPROYECTOSOLICITUD;
                        _inf.MONTOSUBSIDIOBASEINFORMACIONPROYECTOSOLICITUD = a.MONTOSUBSIDIOBASEINFORMACIONPROYECTOSOLICITUD;
                        _inf.MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTOSOLICITUD = a.MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTOSOLICITUD;
                        _inf.MARCALOCALIZACIONINFORMACIONPROYECTOSOLICITUD = a.MARCALOCALIZACIONINFORMACIONPROYECTOSOLICITUD;
                        _inf.MARCAFACTIBILIZACIONINFORMACIONPROYECTOSOLICITUD = a.MARCAFACTIBILIZACIONINFORMACIONPROYECTOSOLICITUD;
                        _inf.FECHAFACTIBILIZACIONINFORMACIONPROYECTOSOLICITUD = a.FECHAFACTIBILIZACIONINFORMACIONPROYECTOSOLICITUD;
                        _inf.NUMEROFAMILIASADSCRITASINFORMACIONPROYECTOSOLICITUD = a.NUMEROFAMILIASADSCRITASINFORMACIONPROYECTOSOLICITUD;
                        _inf.AGNOFACTIBILIDADINFORMACIONPROYECTOSOLICITUD = a.AGNOFACTIBILIDADINFORMACIONPROYECTOSOLICITUD;
                        _inf.IDMAESTROESTADOPROYECTO = a.IDMAESTROESTADOPROYECTO;
                        _inf.FECHAINGRESOINFORMACIONPROYECTOSOLICITUD = a.FECHAINGRESOINFORMACIONPROYECTOSOLICITUD;
                        _inf.PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTOSOLICITUD = a.PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTOSOLICITUD;
                        _inf.NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTOSOLICITUD = a.NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTOSOLICITUD;

                        
                    }
                }
                return _inf;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static INFORMACION_PROYECTO_SOLICITUD getgetInfoProyectoCodigoIdSolicitud(string CodProyecto, long idPrograma, long idSolicitudPago)
        {
            try
            {
                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qInformacionProyectoSolicitud = (from a in contexto.INFORMACION_PROYECTO_SOLICITUD
                                                         join s in contexto.SOLICITUD_PAGO on a.IDINFORMACIONPROYECTOSOLICITUD equals s.IDINFORMACIONPROYECTOSOLICITUD
                                                         where a.CODIGOPROYECTOINFORMACIONPROYECTOSOLICITUD == CodProyecto
                                                         && a.IDMAESTROPROGRAMA == idPrograma
                                                         && s.IDSOLICITUDPAGO == idSolicitudPago
                                                         orderby a.FECHAINGRESOINFORMACIONPROYECTOSOLICITUD descending
                                                         select a).FirstOrDefault();

                    if (qInformacionProyectoSolicitud.IDINFORMACIONPROYECTOSOLICITUD > 0)
                    {
                        return new INFORMACION_PROYECTO_SOLICITUD
                        {
                            IDINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.IDINFORMACIONPROYECTOSOLICITUD,
                            IDINFORMACIONPROYECTO = qInformacionProyectoSolicitud.IDINFORMACIONPROYECTO,
                            IDMAESTROPROGRAMA = qInformacionProyectoSolicitud.IDMAESTROPROGRAMA,
                            IDMAESTROLLAMADO = qInformacionProyectoSolicitud.IDMAESTROLLAMADO,
                            IDMAESTROMODALIDAD = qInformacionProyectoSolicitud.IDMAESTROMODALIDAD,
                            IDMAESTROTITULO = qInformacionProyectoSolicitud.IDMAESTROTITULO,
                            IDMAESTRORESOLUCION = qInformacionProyectoSolicitud.IDMAESTRORESOLUCION,
                            IDDIRECCION = qInformacionProyectoSolicitud.IDDIRECCION,
                            IDMAESTROTIPOLOGIA = qInformacionProyectoSolicitud.IDMAESTROTIPOLOGIA,
                            IDMAESTROALTERNATIVAPOSTULACION = qInformacionProyectoSolicitud.IDMAESTROALTERNATIVAPOSTULACION,
                            CODIGOPROYECTOINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.CODIGOPROYECTOINFORMACIONPROYECTOSOLICITUD,
                            NOMBREPROYECTOINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.NOMBREPROYECTOINFORMACIONPROYECTOSOLICITUD,
                            FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTOSOLICITUD,
                            RESOLUCIONATINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.RESOLUCIONATINFORMACIONPROYECTOSOLICITUD,
                            FECHARESOLUCIONATINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.FECHARESOLUCIONATINFORMACIONPROYECTOSOLICITUD,
                            CANTIDADVIVIENDASINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.CANTIDADVIVIENDASINFORMACIONPROYECTOSOLICITUD,
                            CANTIDADBENEFICIARIOSINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.CANTIDADBENEFICIARIOSINFORMACIONPROYECTOSOLICITUD,
                            MONTOSUBSIDIOBASEINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.MONTOSUBSIDIOBASEINFORMACIONPROYECTOSOLICITUD,
                            MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTOSOLICITUD,
                            MARCALOCALIZACIONINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.MARCALOCALIZACIONINFORMACIONPROYECTOSOLICITUD,
                            MARCAFACTIBILIZACIONINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.MARCAFACTIBILIZACIONINFORMACIONPROYECTOSOLICITUD,
                            FECHAFACTIBILIZACIONINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.FECHAFACTIBILIZACIONINFORMACIONPROYECTOSOLICITUD,
                            NUMEROFAMILIASADSCRITASINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.NUMEROFAMILIASADSCRITASINFORMACIONPROYECTOSOLICITUD,
                            AGNOFACTIBILIDADINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.AGNOFACTIBILIDADINFORMACIONPROYECTOSOLICITUD,
                            IDMAESTROESTADOPROYECTO = qInformacionProyectoSolicitud.IDMAESTROESTADOPROYECTO,
                            FECHAINGRESOINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.FECHAINGRESOINFORMACIONPROYECTOSOLICITUD,
                            AVANCEOBRAINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.AVANCEOBRAINFORMACIONPROYECTOSOLICITUD,
                            ESTADOAVANCEOBRAINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.ESTADOAVANCEOBRAINFORMACIONPROYECTOSOLICITUD,
                            PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTOSOLICITUD,
                            NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTOSOLICITUD = qInformacionProyectoSolicitud.NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTOSOLICITUD
                        };
                    }
                    else
                        return new INFORMACION_PROYECTO_SOLICITUD();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
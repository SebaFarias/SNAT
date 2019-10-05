using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
    public class informacionProyectoDAO
    {

        public static int Save(INFORMACION_PROYECTO _informacionProyecto)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                INFORMACION_PROYECTO _inf = new INFORMACION_PROYECTO();
                try
                {
                    _inf = contexto.INFORMACION_PROYECTO.Where(c => c.IDINFORMACIONPROYECTO == _informacionProyecto.IDINFORMACIONPROYECTO).FirstOrDefault<INFORMACION_PROYECTO>();

                    if (_inf == null)
                    {
                        _inf = _informacionProyecto;
                        contexto.INFORMACION_PROYECTO.Add(_inf);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _inf.CANTIDADBENEFICIARIOSINFORMACIONPROYECTO = _informacionProyecto.CANTIDADBENEFICIARIOSINFORMACIONPROYECTO;
                        _inf.CANTIDADVIVIENDASINFORMACIONPROYECTO = _informacionProyecto.CANTIDADVIVIENDASINFORMACIONPROYECTO;
                        _inf.CODIGOPROYECTOINFORMACIONPROYECTO = _informacionProyecto.CODIGOPROYECTOINFORMACIONPROYECTO;
                        _inf.ESTADOPROYECTOINFORMACIONPROYECTO = _informacionProyecto.ESTADOPROYECTOINFORMACIONPROYECTO;
                        _inf.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO = _informacionProyecto.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO;
                        _inf.FECHARESOLUCIONATINFORMACIONPROYECTO = _informacionProyecto.FECHARESOLUCIONATINFORMACIONPROYECTO;
                        _inf.IDDIRECCION = _informacionProyecto.IDDIRECCION;
                        _inf.IDINFORMACIONPROYECTO = _informacionProyecto.IDINFORMACIONPROYECTO;
                        _inf.IDMAESTROALTERNATIVAPOSTULACION = _informacionProyecto.IDMAESTROALTERNATIVAPOSTULACION;
                        _inf.IDMAESTROLLAMADO = _informacionProyecto.IDMAESTROLLAMADO;
                        _inf.IDMAESTROPROGRAMA = _informacionProyecto.IDMAESTROPROGRAMA;
                        _inf.IDMAESTROMODALIDAD = _informacionProyecto.IDMAESTROMODALIDAD;
                        _inf.IDMAESTROTITULO = _informacionProyecto.IDMAESTROTITULO;
                        _inf.NOMBREPROYECTOINFORMACIONPROYECTO = _informacionProyecto.NOMBREPROYECTOINFORMACIONPROYECTO;
                        _inf.RESOLUCIONATINFORMACIONPROYECTO = _informacionProyecto.RESOLUCIONATINFORMACIONPROYECTO;
                        _inf.IDMAESTROESTADOBENEFICIO = _informacionProyecto.IDMAESTROESTADOBENEFICIO;
                        _inf.IDMAESTROBANCO = _informacionProyecto.IDMAESTROBANCO;
                        _inf.AVANCEOBRAINFORMACIONPROYECTO = _informacionProyecto.AVANCEOBRAINFORMACIONPROYECTO;
                        _inf.ESTADOAVANCEOBRAINFORMACIONPROYECTO = _informacionProyecto.ESTADOAVANCEOBRAINFORMACIONPROYECTO;
                        _inf.PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTO = _informacionProyecto.PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTO;
                        _inf.NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTO = _informacionProyecto.NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTO;

                    }

                    contexto.SaveChanges();
                    return (int)_inf.IDINFORMACIONPROYECTO;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }

        public static INFORMACION_PROYECTO get(string codigoProyecto)
        {
            INFORMACION_PROYECTO _inf = new INFORMACION_PROYECTO();

            try
            {
                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qInformacionProyecto = from a in contexto.INFORMACION_PROYECTO
                                               where a.CODIGOPROYECTOINFORMACIONPROYECTO.ToString() == codigoProyecto
                                               select a;

                    foreach (var a in qInformacionProyecto)
                    {
                        _inf.CANTIDADBENEFICIARIOSINFORMACIONPROYECTO = a.CANTIDADBENEFICIARIOSINFORMACIONPROYECTO;
                        _inf.CANTIDADVIVIENDASINFORMACIONPROYECTO = a.CANTIDADVIVIENDASINFORMACIONPROYECTO;
                        _inf.CODIGOPROYECTOINFORMACIONPROYECTO = a.CODIGOPROYECTOINFORMACIONPROYECTO;
                        _inf.ESTADOPROYECTOINFORMACIONPROYECTO = a.ESTADOPROYECTOINFORMACIONPROYECTO;
                        _inf.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO = a.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO;
                        _inf.FECHARESOLUCIONATINFORMACIONPROYECTO = a.FECHARESOLUCIONATINFORMACIONPROYECTO;
                        _inf.IDDIRECCION = a.IDDIRECCION;
                        _inf.IDINFORMACIONPROYECTO = a.IDINFORMACIONPROYECTO;
                        _inf.IDMAESTROALTERNATIVAPOSTULACION = a.IDMAESTROALTERNATIVAPOSTULACION;
                        _inf.IDMAESTROLLAMADO = a.IDMAESTROLLAMADO;
                        _inf.IDMAESTROMODALIDAD = a.IDMAESTROMODALIDAD;
                        _inf.IDMAESTROTITULO = a.IDMAESTROTITULO;
                        _inf.IDMAESTROPROGRAMA = a.IDMAESTROPROGRAMA;
                        _inf.NOMBREPROYECTOINFORMACIONPROYECTO = a.NOMBREPROYECTOINFORMACIONPROYECTO;
                        _inf.RESOLUCIONATINFORMACIONPROYECTO = a.RESOLUCIONATINFORMACIONPROYECTO;
                        _inf.IDMAESTROESTADOBENEFICIO = a.IDMAESTROESTADOBENEFICIO;
                        _inf.IDMAESTROBANCO = a.IDMAESTROBANCO;
                        _inf.PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTO = a.PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTO;
                        _inf.NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTO = a.NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTO;

                    }

                    return _inf;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static INFORMACION_PROYECTO get(string codigoProyecto, long idprograma)
        {
            INFORMACION_PROYECTO _inf = null;

            
            
                        
            try
            {
                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qInformacionProyecto = from a in contexto.INFORMACION_PROYECTO
                                               where a.CODIGOPROYECTOINFORMACIONPROYECTO == codigoProyecto && a.IDMAESTROPROGRAMA == idprograma
                                               select a;

                    foreach (var a in qInformacionProyecto)
                    {


                        _inf = new INFORMACION_PROYECTO();
                        _inf.AVANCEOBRAINFORMACIONPROYECTO = a.AVANCEOBRAINFORMACIONPROYECTO;
                        _inf.CANTIDADBENEFICIARIOSINFORMACIONPROYECTO = a.CANTIDADBENEFICIARIOSINFORMACIONPROYECTO;
                        _inf.CANTIDADVIVIENDASINFORMACIONPROYECTO = a.CANTIDADVIVIENDASINFORMACIONPROYECTO;
                        _inf.CODIGOPROYECTOINFORMACIONPROYECTO = a.CODIGOPROYECTOINFORMACIONPROYECTO;
                        _inf.ESTADOPROYECTOINFORMACIONPROYECTO = a.ESTADOPROYECTOINFORMACIONPROYECTO;
                        _inf.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO = a.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO;
                        _inf.FECHARESOLUCIONATINFORMACIONPROYECTO = a.FECHARESOLUCIONATINFORMACIONPROYECTO;
                        _inf.IDDIRECCION = a.IDDIRECCION;
                        _inf.IDINFORMACIONPROYECTO = a.IDINFORMACIONPROYECTO;
                        _inf.IDMAESTROALTERNATIVAPOSTULACION = a.IDMAESTROALTERNATIVAPOSTULACION;
                        _inf.IDMAESTROLLAMADO = a.IDMAESTROLLAMADO;
                        _inf.IDMAESTROMODALIDAD = a.IDMAESTROMODALIDAD;
                        _inf.IDMAESTROTITULO = a.IDMAESTROTITULO;
                        _inf.NOMBREPROYECTOINFORMACIONPROYECTO = a.NOMBREPROYECTOINFORMACIONPROYECTO;
                        _inf.RESOLUCIONATINFORMACIONPROYECTO = a.RESOLUCIONATINFORMACIONPROYECTO;
                        _inf.IDMAESTROESTADOPROYECTO = a.IDMAESTROESTADOPROYECTO;
                        _inf.IDMAESTROESTADOBENEFICIO = a.IDMAESTROESTADOBENEFICIO;
                        _inf.IDMAESTROBANCO = a.IDMAESTROBANCO;
                        _inf.IDMAESTROPROGRAMA = a.IDMAESTROPROGRAMA;
                        _inf.PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTO = a.PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTO;
                        _inf.NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTO = a.NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTO;


                     
                    }
                    

                   

                    return _inf;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<INFORMACION_PROYECTO> GetList()
        {

            List<INFORMACION_PROYECTO> ListaInformacionProyecto = new List<INFORMACION_PROYECTO>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qInformacionProyecto = from a in contexto.INFORMACION_PROYECTO
                                           select a;
                foreach (var a in qInformacionProyecto)
                {

                    INFORMACION_PROYECTO _inf = new INFORMACION_PROYECTO();


                    _inf.CANTIDADBENEFICIARIOSINFORMACIONPROYECTO = a.CANTIDADBENEFICIARIOSINFORMACIONPROYECTO;
                    _inf.CANTIDADVIVIENDASINFORMACIONPROYECTO = a.CANTIDADVIVIENDASINFORMACIONPROYECTO;
                    _inf.CODIGOPROYECTOINFORMACIONPROYECTO = a.CODIGOPROYECTOINFORMACIONPROYECTO;
                    _inf.ESTADOPROYECTOINFORMACIONPROYECTO = a.ESTADOPROYECTOINFORMACIONPROYECTO;
                    _inf.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO = a.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO;
                    _inf.FECHARESOLUCIONATINFORMACIONPROYECTO = a.FECHARESOLUCIONATINFORMACIONPROYECTO;
                    _inf.IDDIRECCION = a.IDDIRECCION;
                    _inf.IDINFORMACIONPROYECTO = a.IDINFORMACIONPROYECTO;
                    _inf.IDMAESTROALTERNATIVAPOSTULACION = a.IDMAESTROALTERNATIVAPOSTULACION;
                    _inf.IDMAESTROLLAMADO = a.IDMAESTROLLAMADO;
                    _inf.IDMAESTROMODALIDAD = a.IDMAESTROMODALIDAD;
                    _inf.IDMAESTROTITULO = a.IDMAESTROTITULO;
                    _inf.NOMBREPROYECTOINFORMACIONPROYECTO = a.NOMBREPROYECTOINFORMACIONPROYECTO;
                    _inf.RESOLUCIONATINFORMACIONPROYECTO = a.RESOLUCIONATINFORMACIONPROYECTO;
                    _inf.PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTO = a.PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTO;
                    _inf.NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTO = a.NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTO;

                    ListaInformacionProyecto.Add(_inf);


                }

                return ListaInformacionProyecto;
            }

        }


        public void Delete(INFORMACION_PROYECTO _informacionProyecto)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                INFORMACION_PROYECTO qInformacionProyecto = (from c in contexto.INFORMACION_PROYECTO
                                                             where c.IDINFORMACIONPROYECTO == _informacionProyecto.IDINFORMACIONPROYECTO
                                                             select c).FirstOrDefault();

                contexto.INFORMACION_PROYECTO.Remove(qInformacionProyecto);
                contexto.SaveChanges();
            }

        }


        public static void ChangeStatus(INFORMACION_PROYECTO _informacionProyecto)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                INFORMACION_PROYECTO qInformacionProyecto = (from c in contexto.INFORMACION_PROYECTO
                                                             where c.IDINFORMACIONPROYECTO == _informacionProyecto.IDINFORMACIONPROYECTO
                                                             select c).FirstOrDefault();


                qInformacionProyecto.ESTADOPROYECTOINFORMACIONPROYECTO = false;

                contexto.SaveChanges();
            }


        }

        //Verifica la existencia del proyecto en las otras lineas de proceso de SNAT  DS49,DS174,PPPF,DS105,DS49 reconstrución ,DS1
        public static string verificarExistenciaProyectoSNAT(long idPrograma, string codigoProyecto)
        {
            using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
            {
                string resultado = "0";
                var qAsistecPPPFProyecto = contexto.WEB_ASISTEC_SNATIII_VERIFICAR_PROYECTOS_SNAT(idPrograma, codigoProyecto);

                foreach (var item in qAsistecPPPFProyecto)
                {
                    resultado = item.Resultado.ToString();
                }

                return resultado;
            }

        }
        public static int verificarProyectoRegionUsuarioSNAT(long idPrograma, string codigoProyecto, int codigoRegionUsuario)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                int? region = (from c in contexto.INFORMACION_PROYECTO
                               join d in contexto.DIRECCION on c.IDDIRECCION equals d.IDDIRECCION
                               where c.IDMAESTROPROGRAMA == idPrograma && c.CODIGOPROYECTOINFORMACIONPROYECTO == codigoProyecto
                               select d.CODIGOREGIONDIRECCION).FirstOrDefault();


                if (region != null)
                {
                    if (codigoRegionUsuario == region)
                        return Convert.ToInt32(region);
                    else
                        return 0;
                }
                else
                    return 0;
            }

        }



    }
}



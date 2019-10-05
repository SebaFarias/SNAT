using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;
using Minvu.Snat.Helper;
namespace Minvu.Snat.IData.DAO
{
    public class OperacionesMuninDAO
    {
        public static IList<WEB_TRAZABILIDAD_ups_con_PROYECTO_AVANCE_OBRA_SNAT_SIMPLIFICADO_Result> getAvanceObraTrazabilidadProyecto(int idPrograma, string codProyecto, int? region, int? titulo, int? año, int? numeroLLamado)
        {
            try
            {
                List<WEB_TRAZABILIDAD_ups_con_PROYECTO_AVANCE_OBRA_SNAT_SIMPLIFICADO_Result> ListaConsultaProyecto = new List<WEB_TRAZABILIDAD_ups_con_PROYECTO_AVANCE_OBRA_SNAT_SIMPLIFICADO_Result>();

                using (Web_Trazabilidad_Test_Entities contexto = new Web_Trazabilidad_Test_Entities())
                {
                    var qProyectos = contexto.WEB_TRAZABILIDAD_ups_con_PROYECTO_AVANCE_OBRA_SNAT_SIMPLIFICADO(idPrograma, codProyecto, region, titulo, año, numeroLLamado);

                    foreach (var a in qProyectos)
                    {
                        WEB_TRAZABILIDAD_ups_con_PROYECTO_AVANCE_OBRA_SNAT_SIMPLIFICADO_Result _mae = new WEB_TRAZABILIDAD_ups_con_PROYECTO_AVANCE_OBRA_SNAT_SIMPLIFICADO_Result();

                        _mae.AlternativaPostulacion = a.AlternativaPostulacion;
                        _mae.AnoLlamadoProyecto = a.AnoLlamadoProyecto;
                        _mae.AvanceRealActual = a.AvanceRealActual;
                        _mae.CodigoPrograma = a.CodigoPrograma;
                        _mae.CodigoProyecto = a.CodigoProyecto;
                        _mae.DvEC = a.DvEC;
                        _mae.EstadoFiniquitadaEC = a.EstadoFiniquitadaEC;
                        _mae.EstadoGeneral = a.EstadoGeneral;
                        _mae.EstadoGeneralDescripcion = a.EstadoGeneralDescripcion;
                        _mae.IdEC = a.IdEC;
                        _mae.NombreEC = a.NombreEC;
                        _mae.NombreEmpresaFiscalizadora = a.NombreEmpresaFiscalizadora;
                        _mae.NroLlamadoProyecto = a.NroLlamadoProyecto;
                        _mae.Programa = a.Programa;
                        _mae.RegiónProyecto = a.RegiónProyecto;
                        _mae.RutEC = a.RutEC;
                        _mae.TipologiaProyecto = a.TipologiaProyecto;
                        _mae.TituloProyecto = a.TituloProyecto;

                        ListaConsultaProyecto.Add(_mae);
                    }

                    try
                    {

                        var aux = ListaConsultaProyecto.FindAll(x => x.CodigoProyecto == codProyecto);

                        return aux;

                    }
                    catch (Exception ex) 
                    {
                        Log.Instance.Error("Error en getAvanceObraTrazabilidadProyecto nivel 1: " + ex);
                        return null;
                    }
                    

                }
            }
            catch (Exception Ex)
            {
                Log.Instance.Error("Error en getAvanceObraTrazabilidadProyecto nivel 2: " + Ex);
                return null;
            }

        }
    }
}

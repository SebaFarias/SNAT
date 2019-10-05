using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{

   public class ConvalidacionDatosEntreSistemasDAO 
    {


        public static long getIdProgramaInternoPorSistemaExterno(string nombrePrograma,  long idProgramaExterno, string nombreSistemaExterno)
        {

            string programaSeleccionado = "";
            

            List<string> listadoHomologacion = new List<string>();
            listadoHomologacion.Add("49");
            listadoHomologacion.Add("105");
            listadoHomologacion.Add("174");
            listadoHomologacion.Add("10");
            listadoHomologacion.Add("RECONSTRUCCIÓN");
            listadoHomologacion.Add("255");
            listadoHomologacion.Add("PPPF");

            foreach (string item in listadoHomologacion)
            {
                if(nombrePrograma.Contains(item))
                {
                    programaSeleccionado = item;
                }
            }
            
            if(programaSeleccionado.Contains("PPPF"))
            {
                programaSeleccionado = "255";
            }

            try
            {
                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    var qIdProgramaInterno = (from cps in contexto.CONVALIDACION_PROGRAMA_SISTEMA
                                                         join p in contexto.MAESTRO_PROGRAMA on cps.IDMAESTROPROGRAMA equals p.IDMAESTROPROGRAMA
                                                         join s in contexto.MAESTRO_SISTEMA on cps.IDMAESTROSISTEMA equals s.IDMAESTROSISTEMA
                                                         where p.NOMBREMAESTROPROGRAMA.Contains(programaSeleccionado)
                                                         
                                                         && cps.CODIGOPROGRAMAEXTERNOCONVALIDACIONPROGRAMASISTEMA == idProgramaExterno
                                                         && s.NOMBREMAESTROSISTEMA.ToUpper() == nombreSistemaExterno.ToUpper()

                                                         select cps.IDMAESTROPROGRAMA).FirstOrDefault();
                    if (qIdProgramaInterno != null)
                    {
                        return Convert.ToInt64(qIdProgramaInterno);
                    }
                    else
                        return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

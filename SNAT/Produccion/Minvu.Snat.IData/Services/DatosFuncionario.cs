using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.WSS_ObtenerDatosFuncionario;

namespace Minvu.Snat.IData.Services
{
    public class DatosFuncionario
    {

        public static string obtenerDatosFuncionario(string UserName)
        {
            try
            {

                funcionarioSoapClient sii = new funcionarioSoapClient();
                return sii.ObtenerDatosFuncionario(UserName);

            }
            catch (Exception Ex)
            {
                //Log.Instance.Error("Error en PropiedadHabitacional.ListaPropiedadesHabitacionales  RUT -> " + rut + "DV ->" + dv,Ex);
                throw new ApplicationException("Ha ocurrido un error de comunicación con WS Funcionarios", Ex.InnerException);
            }
        }
    }
}

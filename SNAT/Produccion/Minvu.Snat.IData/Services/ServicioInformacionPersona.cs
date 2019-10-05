using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Minvu.Snat.IData.ServicioInformacionPersona
{
    public static class ServicioInformacionPersona
    {
        #region VALORES FIJOS (http://serviciosweb.minvu.cl/Servicio/Detalle/47)
        public const int PERIODO = -1;
        public const int TRAMITE = 13;
        #endregion

        /// <summary>
        /// Obtiene informacion de la persona desde el SRCeI, a partir del Rut de la misma
        /// </summary>
        /// <param name="rut">Rut de la persona</param>
        /// <param name="dv">Digito verificador</param>
        /// <returns>Informacion encapsulada de: Estado,Rut,dv,Nombre,MenorEdad (bool),Fallecido(bool)</returns>
        public static dynamic InformacionPorRut(int rut, char dv)
        {
            Infopersona p = new Infopersona { Rut = rut, Dv = dv.ToString(), Periodo = PERIODO, Ussist = TRAMITE };

            REGCIVIL_orc_datos_persona_prt_regcivil_info_personaSoapClient client = new REGCIVIL_orc_datos_persona_prt_regcivil_info_personaSoapClient();

            try
            {

                ICE r = client.ope_prt_regcivil_info_persona(p);

                if (r.RESULTADO.ESTADO != 1)
                    return new { Estado = r.RESULTADO.ESTADO, Rut = rut, dv = dv, Nombre = r.RESULTADO.DESCRIPCION, MenorEdad = false, Fallecido = false };


                return new
                { 
                    Estado = r.RESULTADO.ESTADO,
                    Rut = rut,
                    dv = dv,
                    NombreCompleto = String.Format("{0} {1} {2}", GetNames(r.minvuRutData.persona.nombres), FirstCharToUpper(r.minvuRutData.persona.apPaterno), !string.IsNullOrEmpty(r.minvuRutData?.persona?.apMaterno) ? FirstCharToUpper(r.minvuRutData.persona.apMaterno) : string.Empty),
                    Nombres = r.minvuRutData.persona.nombres,
                    ApellidoPaterno = r.minvuRutData.persona.apPaterno,
                    ApellidoMaterno = r.minvuRutData.persona.apMaterno,
                    MenorEdad = DateTime.ParseExact(r.minvuRutData.persona.fechaNaci, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date.AddYears(18) <= DateTime.Now.Date ? false : true,
                    Fallecido = r.minvuRutData.persona.fechaDefun.Equals("0000-00-00") ? false : true
                };
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Deja la primera letra en mayúscula, el resto en minúscula
        /// </summary>
        /// <param name="input">String con la palabra</param>
        /// <returns>String</returns>
        private static string FirstCharToUpper(string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
        }

        /// <summary>
        /// Divide un string por espacios, en sub string y deja la primera letra en mayúscula, el resto en minúscula por cada uno. Luego los vuelve a juntar
        /// </summary>
        /// <param name="input">String que contiene las palabras</param>
        /// <returns>String modificado</returns>
        private static string GetNames(string input)
        {
            string[] splitString = input.Split();
            string response = "";
            foreach (var item in splitString.Select((value, i) => new { i, value }))
                splitString[item.i] = FirstCharToUpper(item.value);

            foreach (var item in splitString.Select((value, i) => new { i, value }))
                if (item.i == 0)
                    response = splitString[item.i];
                else
                    response = response + " " + splitString[item.i];

            return response;
        }
    }
}
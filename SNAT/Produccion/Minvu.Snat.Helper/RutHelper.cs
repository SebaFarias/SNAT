using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Helper
{
    public static class RutHelper
    {
        public const string SIN_INFORMACION = "-----";

        public static string formatearRut(string cuerpoRut, string dv)
        {
            if (!string.IsNullOrEmpty(cuerpoRut) && !string.IsNullOrEmpty(dv))
            {
                int rut = 0;
                if (int.TryParse(cuerpoRut, out rut))
                {
                    return rut.ToString("N0") + "-" + dv.ToUpper();
                }
                else
                    return null;
            }
            else
                return null;
        }

        public static string cuerpoRut(string RutCompleto)
        {
            if (!string.IsNullOrEmpty(RutCompleto) && RutCompleto != "-")
            {
                string rutSinPuntos = RutCompleto.Replace(".", "");
                string rutSinPuntosSinGuion = rutSinPuntos.Replace("-", "");
                string rut = rutSinPuntosSinGuion.Substring(0, rutSinPuntosSinGuion.Length - 1);
                return rut;
            }
            else
                return null;
        }

        public static string dvRut(string RutCompleto)
        {
            if (!string.IsNullOrEmpty(RutCompleto) && RutCompleto != "-")
            {
                string rutSinPuntos = RutCompleto.Replace(".", "");
                string rutSinPuntosSinGuion = rutSinPuntos.Replace("-", "");
                string digito = rutSinPuntosSinGuion.Substring(rutSinPuntosSinGuion.Length - 1);
                return digito;
            }
            else
                return null;
        }

        public static bool validarRut(string pRut, string pDigito)
        {
            int cuerpo;
            int digitoAux;
            int contador;
            int multiplo;
            int acumulador;
            string rutDigito;

            try
            {
                if (pDigito != null)
                    pDigito = pDigito.ToUpper();

                cuerpo = int.Parse(pRut);
            }
            catch
            {
                return false;
            }

            if (cuerpo <= 100000)
            {
                return false;
            }

            contador = 2;
            acumulador = 0;

            while (cuerpo != 0)
            {
                multiplo = (cuerpo % 10) * contador;
                acumulador += multiplo;
                cuerpo = cuerpo / 10;
                contador++;
                if (contador == 8)
                {
                    contador = 2;
                }
            }

            digitoAux = 11 - (acumulador % 11);
            rutDigito = digitoAux.ToString().Trim().ToUpper();

            if (digitoAux == 10)
            {
                rutDigito = "K";
            }

            if (digitoAux == 11)
            {
                rutDigito = "0";
            }

            if (rutDigito == pDigito && !(int.Parse(pRut) == 0))
            {
                return true;
            }
            return false;
        }

        public static bool ValidaRutValidator(string pRut, string pDigito)
        {
            int cuerpo;
            int digitoAux;
            int contador;
            int multiplo;
            int acumulador;
            string rutDigito;

            try
            {
                if (pDigito != null)
                    pDigito = pDigito.ToUpper();

                cuerpo = int.Parse(pRut);
            }
            catch
            {
                return false;
            }

            contador = 2;
            acumulador = 0;

            while (cuerpo != 0)
            {
                multiplo = (cuerpo % 10) * contador;
                acumulador += multiplo;
                cuerpo = cuerpo / 10;
                contador++;
                if (contador == 8)
                {
                    contador = 2;
                }
            }

            digitoAux = 11 - (acumulador % 11);
            rutDigito = digitoAux.ToString().Trim().ToUpper();

            if (digitoAux == 10)
            {
                rutDigito = "K";
            }

            if (digitoAux == 11)
            {
                rutDigito = "0";
            }

            if (rutDigito == pDigito && !(int.Parse(pRut) == 0))
            {
                return true;
            }
            return false;
        }

        public static bool TieneRut(string rut)
        {
            if (!string.IsNullOrWhiteSpace(rut))
            {
                string cuerpo = cuerpoRut(rut);
                string dv = dvRut(rut);

                return (cuerpo != null && cuerpo != "" && cuerpo != "0" && dv != null && dv != "");
            }
            else
            {
                return false;
            }
        }

        public static string CalcularDv(string rut)
        {
            int cuerpo;
            int digitoAux;
            int contador;
            int multiplo;
            int acumulador;
            string rutDigito;
            string pRut = cuerpoRut(rut);
            string pDigito = dvRut(rut);

            try
            {
                if (pDigito != null)
                    pDigito = pDigito.ToUpper();

                cuerpo = int.Parse(pRut);
            }
            catch
            {
                return null;
            }

            if (cuerpo <= 100000)
            {
                return null;
            }

            contador = 2;
            acumulador = 0;

            while (cuerpo != 0)
            {
                multiplo = (cuerpo % 10) * contador;
                acumulador += multiplo;
                cuerpo = cuerpo / 10;
                contador++;
                if (contador == 8)
                {
                    contador = 2;
                }
            }

            digitoAux = 11 - (acumulador % 11);
            rutDigito = digitoAux.ToString().Trim().ToUpper();

            if (digitoAux == 10)
            {
                rutDigito = "K";
            }

            if (digitoAux == 11)
            {
                rutDigito = "0";
            }

            if (!(int.Parse(pRut) == 0))
            {
                return formatearRut(pRut, rutDigito);
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Minvu.Snat.IData.SI
{
    public static class SII
    {
        #region VALORES FIJOS (http://serviciosweb.minvu.cl/Servicio/Detalle/47)
        public const int PERIODO = -1;
        public const int TRAMITE = 13;
        #endregion

        public static dynamic InformacionPorRut(int rut, char dv)
        {
            

            SII_orc_datcontrib_prt_sii_datcontribSoapClient client = new SII_orc_datcontrib_prt_sii_datcontribSoapClient();

            try
            {
                DatContrib _datContrib = new DatContrib();

                _datContrib.Dv = dv.ToString();
                _datContrib.Periodo = PERIODO;
                _datContrib.Rut =rut.ToString();
                _datContrib.Ussist = TRAMITE.ToString();

               ICE r = client.ope_prt_sii_datcontrib(_datContrib);

                if (r.RESULTADO.ESTADO != 1)
                    return new { Estado = r.RESULTADO.ESTADO, Rut = rut, dv = dv, Nombre = r.RESULTADO.DESCRIPCION, MenorEdad = false, Fallecido = false };


                return new
                {
                    Estado = r.RESULTADO.ESTADO,
                    Rut = rut,
                    dv = dv,
                    NombreCompleto = r.RESPUESTA.RESP_BODY.NOMBRE + " " + r.RESPUESTA.RESP_BODY.AP_PATERNO + " " + r.RESPUESTA.RESP_BODY.AP_MATERNO,
                    Nombres = r.RESPUESTA.RESP_BODY.NOMBRE,
                    ApellidoPaterno = r.RESPUESTA.RESP_BODY.AP_PATERNO,
                    ApellidoMaterno = r.RESPUESTA.RESP_BODY.AP_MATERNO,
                    razonSocial = r.RESPUESTA.RESP_BODY.RAZON_SOCIAL
                };
            }
            catch (Exception e)
            {
                return null;
            }
        }


    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;
using Minvu.Snat.IData.Services;
using Minvu.Snat.IData.ORM;
using System.Xml.Linq;

namespace Minvu.Snat.IData.DAO
{

    public class FuncionarioDAO
    {
        public string userName { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int rut { get; set; }
        public char dv { get; set; }
        public int idRegion { get; set; }
        public string region { get; set; }
    }

    public class FuncionarioDAOFactory
    {
        public static FuncionarioDAO getFuncionario(string UserName)
        {
            var _funcionarioWS = XDocument.Parse(DatosFuncionario.obtenerDatosFuncionario(UserName));


            if (_funcionarioWS != null)
            {
                if (_funcionarioWS.Root.Element("RESULTADO").Element("ESTADO").Value == "1")
                {
                    try
                    {
                        if (_funcionarioWS.Root.Element("RESPUESTA").Element("Resultado").Value == "OK")
                        {
                            return new FuncionarioDAO
                            {


                                userName = UserName,
                                Nombre = _funcionarioWS.Root.Element("RESPUESTA").Element("Nombre").Value,
                                ApellidoPaterno = _funcionarioWS.Root.Element("RESPUESTA").Element("ApellidoPaterno").Value,
                                ApellidoMaterno = _funcionarioWS.Root.Element("RESPUESTA").Element("ApellidoMaterno").Value,
                                rut = Convert.ToInt32(_funcionarioWS.Root.Element("RESPUESTA").Element("Rut").Value),
                                dv = Convert.ToChar(_funcionarioWS.Root.Element("RESPUESTA").Element("DVRut").Value.ToString()),
                                region = regionDAO.Obtener(Convert.ToInt32((_funcionarioWS.Root.Element("RESPUESTA").Element("IDRegion").Value))).REG_DES,
                                idRegion = Convert.ToInt32((_funcionarioWS.Root.Element("RESPUESTA").Element("IDRegion").Value))

                            };
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }

            }
            return null;
        }

        //internal static bool saveLogFuncionario(string nombreUsuario, short tipo, string contexto, long idObjeto, long? idPersona, long? idGrupo, int estado)
        //{
        //    try
        //    {

        //        //Tipo: 1 = Formulario ingreso persona
        //        //Tipo: 2 = Formulario ingreso Grupo
        //        //TIpo: 3 = Formulario ingreso integrante
        //        if (nombreUsuario != null && tipo != null && contexto != null && estado != null)
        //        {
        //            LOG auxLog = new LOG();

        //            auxLog.USUARIO = nombreUsuario;
        //            auxLog.TIPO = tipo;
        //            auxLog.CONTEXTO = contexto;
        //            auxLog.FECHA = DateTime.Now;
        //            auxLog.CODIGOOBJETO = idObjeto;
        //            auxLog.CODIGOPERSONA = idPersona;
        //            auxLog.CODIGOGRUPO = idGrupo;
        //            auxLog.ESTADO = estado;


        //            if (LogDAO.Save(auxLog) != 0)
        //                return true;
        //            else
        //                return false;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //        // return false;

        //    }
        //}
    }

}

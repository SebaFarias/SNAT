using Minvu.Security;
using Minvu.Snat.Helper;
using Minvu.Snat.Helper.PssimRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Minvu.Snat.Domain.Entities;
using System.Xml;
using System.Configuration;

namespace Minvu.Snat.Site
{
    public class SiteHelper
    {
        public static string GetVersion()
        {
            string version = "v";
            Assembly ass = Assembly.GetExecutingAssembly();
            AssemblyName an = ass.GetName();
            version += an.Version.ToString();
            return version;
        }

        public static string UserName
        {
            get
            {
                return SingleSignOn.CurrentPrincipal.MinvuIdentity.UserName;
            }
        }


        public static string UserRol
        {
            get
            {

                SistemaSoapClient client = new SistemaSoapClient();



                SistemaSoapClient cnx = new SistemaSoapClient();
                Entrada entrada = new Entrada();

                entrada.Id_Sistema = Convert.ToInt32(ConfigurationManager.AppSettings["id_sistema"]); 
                entrada.Ussist = "0";
                entrada.Usuario = UserName;


                XmlDocument xD = new XmlDocument();
                string objResultRUS = cnx.Roles_Usuario_Sistema(entrada);

                if (objResultRUS != "")
                    xD.LoadXml(objResultRUS);

                //return xD;


                //XmlNodeList nodoRespuesta = documento.SelectNodes("/ICE/RESULTADO/DESCRIPCION");
                string rol = xD.GetElementsByTagName("RolName")[0]?.InnerText;
                //string nombre = documento.GetElementsByTagName("NombreUsuario")[0]?.InnerText;
                //int region = Convert.ToInt32(documento.GetElementsByTagName("RegionUsuario")[0]?.InnerText);
                return rol;



            }
        }

      

        public static string getRegion
        {
            get
            {
                FuncionarioEntities aux = FuncionarioEntitiesFactory.getFuncionario(UserName);

                return aux.region;
            }

        }


        public static string NombreCompletoUsuario
        {
            get
            {
                return TypeHelper.UppercaseWords(SingleSignOn.CurrentPrincipal.CompleteName);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
    public class regionDAO
    {
        public static long obtenerIdRegion(string nombreRegion)
        {
            try
            {
                using (Rukan_MigraEntities contexto = new Rukan_MigraEntities())
                {
                    var id = (from r in contexto.REGION
                              where r.REG_DES.ToString().ToLower() == nombreRegion.ToString().ToLower() || r.REG_DES.Contains(nombreRegion)
                              select r.REG_ID).FirstOrDefault();

                        return Convert.ToInt64(id);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        public static List<REGION> obtenerRegiones()
        {
            try
            {
                PA_LISTAR_REGION_Result _mae = new PA_LISTAR_REGION_Result();
                List<REGION> _listadoRegion = new List<REGION>();

                using (Rukan_MigraEntities contexto = new Rukan_MigraEntities())
                {
                    var qAsistecSolicitud = contexto.PA_LISTAR_REGION();

                    if (qAsistecSolicitud != null)
                    {

                        foreach (var item in qAsistecSolicitud)
                        {
                            REGION _aux = new REGION();

                            _aux.REG_ID = item.REG_ID;
                            _aux.REG_ORD = item.REG_ORD;

                            int contadorInicial = item.REG_DES.Length;
                            string Region = "";

                            Region = item.REG_DES.ToString().Replace("Región del ", "");

                            if (contadorInicial == Region.Length)
                                Region = item.REG_DES.ToString().Replace("Región de ", "");

                            if (contadorInicial == Region.Length)
                                Region = item.REG_DES.ToString().Replace("Región ", "");




                            _aux.REG_DES = Region;

                            _listadoRegion.Add(_aux);

                        }


                    }

                    return _listadoRegion;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


     

        public static string obtenerNombreRegion(int idRegion)
        {
            try
            {
                using (Rukan_MigraEntities contexto = new Rukan_MigraEntities())
                {
                    var id = (from r in contexto.REGION
                              where r.REG_ID == idRegion
                              select r.REG_DES).FirstOrDefault();

                    if (id == null)
                        return string.Empty;
                    else
                    {

                        int contadorInicial = id.Length;
                        string Region = "";

                        Region = id.ToString().Replace("Región del ", "");

                        if (contadorInicial == Region.Length)
                            Region = id.ToString().Replace("Región de ", "");

                        if (contadorInicial == Region.Length)
                            Region = id.ToString().Replace("Región ", "");
                        return id;
                    }
                        
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static List<REGION> ObtenerRegion(int idRegion)
        {
            try
            {
                using (Rukan_MigraEntities modelo = new Rukan_MigraEntities())
                {
                    var query = from r in modelo.REGION
                                where r.REG_ID == idRegion
                                select r;

                    foreach (var item in query.ToList<REGION>())
                    {
                        int contadorInicial = item.REG_DES.Length;
                        string Region = "";

                        Region = item.REG_DES.ToString().Replace("Región del ", "");

                        if (contadorInicial == Region.Length)
                            Region = item.REG_DES.ToString().Replace("Región de ", "");

                        if (contadorInicial == Region.Length)
                            Region = item.REG_DES.ToString().Replace("Región ", "");
                         item.REG_DES = Region;

                        
                    }
                    return query.ToList<REGION>();


                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static REGION Obtener(int idRegion)
        {
            try
            {
                using (Rukan_MigraEntities modelo = new Rukan_MigraEntities())
                {
                    var query = from r in modelo.REGION
                                where r.REG_ID == idRegion
                                select r;


                    REGION _aux = query.FirstOrDefault<REGION>();

                    
                        int contadorInicial = _aux.REG_DES.Length;
                        string Region = "";

                        Region = _aux.REG_DES.ToString().Replace("Región del ", "");

                        if (contadorInicial == Region.Length)
                            Region = _aux.REG_DES.ToString().Replace("Región de ", "");

                        if (contadorInicial == Region.Length)
                            Region = _aux.REG_DES.ToString().Replace("Región ", "");


                    _aux.REG_DES = Region;

                    return _aux;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static USERREGION GetUSERREGION(string userName)
        {
            try
            {
                using (Rukan_MigraEntities modelo = new Rukan_MigraEntities())
                {
                    var query = from r in modelo.USERREGION
                                where r.UserName == userName
                                select r;
                    return query.FirstOrDefault<USERREGION>();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
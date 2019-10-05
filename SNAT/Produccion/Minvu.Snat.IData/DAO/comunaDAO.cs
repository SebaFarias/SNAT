using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM; 

namespace Minvu.Snat.IData.DAO
{
    public class comunaDAO
    {

        public static List<COMUNA> obtenerComunas()
        {

            try
            {
                using (Rukan_MigraEntities modelo = new Rukan_MigraEntities())
                {

                    var query = from c in modelo.COMUNA
                                orderby c.COM_DES ascending
                                select c;
                    var list = query.ToList<COMUNA>();
                    list.RemoveAll(x => x.COM_DES.Contains("Ex"));
                    list.Remove(list.Find(x => x.COM_ID == 13134));
                    list.Remove(list.Find(x => x.COM_ID == 13133));
                    return list;
                }
            }
            catch (Exception Ex)
            {
                //Log.Instance.Error("Error ComunaDAO.obtenerComunas IDREGION", Ex);
                throw Ex;
            }

        }

        public static IList<COMUNA> obtenerComunas(int? provincia_id)
        {
           // Log.Instance.Info("ComunaDAO.obtenerComunas IDREGION -> " + codigoRegion);
            try
            {
                using (Rukan_MigraEntities modelo = new Rukan_MigraEntities())
                {

                    var query = from c in modelo.COMUNA
                                where c.PRV_ID == provincia_id
                                orderby c.COM_DES ascending
                                select c;
                    var list = query.ToList<COMUNA>();
                    list.RemoveAll(x => x.COM_DES.Contains("Ex"));
                    list.Remove(list.Find(x => x.COM_ID == 13134));
                    list.Remove(list.Find(x => x.COM_ID == 13133));
                    return list;
                }
            }
            catch (Exception Ex)
            {
               // Log.Instance.Error("Error ComunaDAO.obtenerComunas IDREGION -> " + codigoRegion, Ex);
                throw Ex;
            }

        }

        public static List<COMUNA> listaComunas()
        {
            try
            {
                using (Rukan_MigraEntities modelo = new Rukan_MigraEntities())
                {

                    var query = from c in modelo.COMUNA
                                orderby c.COM_DES ascending
                                select c;
                    var list = query.ToList<COMUNA>();
                    list.RemoveAll(x => x.COM_DES.Contains("Ex"));
                    list.Remove(list.Find(x => x.COM_ID == 13134));
                    list.Remove(list.Find(x => x.COM_ID == 13133));
                    return list;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public static int obtenerIdComuna(long idRegion, string nombreComuna)
        {
            try
            {
                using (Rukan_MigraEntities rk = new Rukan_MigraEntities())
                {
                    var r = (from c in rk.COMUNA
                             where c.REG_ID == idRegion && c.COM_DES.ToString().ToLower().Contains(nombreComuna.Trim().ToString().ToLower())
                             && c.COM_ID != 13134 //Santiago Sur
                             && c.COM_ID != 13133 //Santiago Oeste
                             select c.COM_ID).FirstOrDefault();

                    if (r == 0)
                    {
                        var inputString = nombreComuna;
                        var normalizedString = inputString.Normalize(NormalizationForm.FormD);
                        var sb = new StringBuilder();
                        for (int i = 0; i < normalizedString.Length; i++)
                        {
                            var uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]);
                            if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                            {
                                sb.Append(normalizedString[i]);
                            }
                        }
                        string aux = (sb.ToString().Normalize(NormalizationForm.FormC));

                        r = (from c in rk.COMUNA
                             where c.REG_ID == idRegion && c.COM_DES.ToString().ToLower().Contains(aux.Trim().ToString().ToLower())
                             && c.COM_ID != 13134 //Santiago Sur
                             && c.COM_ID != 13133 //Santiago Oeste
                             select c.COM_ID).FirstOrDefault();

                        if (r == 0)
                        {
                            return 0;
                        }
                        else
                        {
                            return Convert.ToInt32(r);
                        }

                    }
                    else
                        return Convert.ToInt32(r);
                }
            }
            catch (Exception Ex)
            {
               // Log.Instance.Info("Error ComunaDAO.obtenerIdComuna " + Ex);
                throw Ex;
            }
        }

        public static COMUNA Obtener(int idComuna)
        {
           // Log.Instance.Info("ComunaDAO.Obtener idComuna -> " + idComuna);
            try
            {
                using (Rukan_MigraEntities modelo = new Rukan_MigraEntities())
                {

                    var query = from c in modelo.COMUNA
                                where c.COM_ID == idComuna
                                orderby c.COM_DES ascending
                                select c;


                    return query.FirstOrDefault<COMUNA>();
                }
            }
            catch (Exception Ex)
            {
              //  Log.Instance.Error("Error ComunaDAO.Obtener IDREGION -> " + idComuna, Ex);
                throw Ex;
            }

        }

        public static string ObtenerNombreComuna(int idComuna)
        {
            // Log.Instance.Info("ComunaDAO.Obtener idComuna -> " + idComuna);
            try
            {
                using (Rukan_MigraEntities modelo = new Rukan_MigraEntities())
                {

                    string query = (from c in modelo.COMUNA
                                where c.COM_ID == idComuna
                                orderby c.COM_DES ascending
                                select c.COM_DES).FirstOrDefault();


                    return query;
                }
            }
            catch (Exception Ex)
            {
                //  Log.Instance.Error("Error ComunaDAO.Obtener IDREGION -> " + idComuna, Ex);
                throw Ex;
            }

        }

        public static COMUNA ObtenerComunaPropiedad(int idComuna)
        {
           // Log.Instance.Info("ComunaDAO.Obtener idComuna -> " + idComuna);
            try
            {
                using (Rukan_MigraEntities modelo = new Rukan_MigraEntities())
                {

                    var query = from c in modelo.COMUNA
                                where c.COM_ID_SII == idComuna
                                orderby c.COM_DES ascending
                                select c;


                    return query.FirstOrDefault<COMUNA>();
                }
            }
            catch (Exception Ex)
            {
               // Log.Instance.Error("Error ComunaDAO.Obtener IDREGION -> " + idComuna, Ex);
                throw Ex;
            }

        }
    }
}

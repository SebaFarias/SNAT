using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
    public class ProvinciaDAO
    {
        public static string getProvinciaDESC(int idProvincia)
        {
            try
            {
                using (Rukan_MigraEntities contexto = new Rukan_MigraEntities())
                {
                    string n = (from m in contexto.PROVINCIA
                             where m.PRV_ID == idProvincia
                             select m.PRV_DES).FirstOrDefault();

                

                    return n ;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static List<PROVINCIA> obtenerProvincias()
        {
            try
            {
                using (Rukan_MigraEntities contexto = new Rukan_MigraEntities())
                {
                    var aux = (from p in contexto.PROVINCIA
                               orderby p.PRV_DES
                               select p);

                    var list = aux.ToList<PROVINCIA>();
                    list.RemoveAll(x => x.PRV_DES.Contains("Ex"));

                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
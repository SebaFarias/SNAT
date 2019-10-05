using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM; 

namespace Minvu.Snat.IData.DAO
{
    public class rukanMigra
    {
        public static RUKAN_MIGRA_USP_CON_OBTIENE_FAMILIA_CAMBIO_VIGENCIA_AVC_RUT_Result GetRukan(int rutFamilia)
        {
            RUKAN_MIGRA_USP_CON_OBTIENE_FAMILIA_CAMBIO_VIGENCIA_AVC_RUT_Result _mae = new RUKAN_MIGRA_USP_CON_OBTIENE_FAMILIA_CAMBIO_VIGENCIA_AVC_RUT_Result();

            using (Rukan_MigraEntities contexto = new Rukan_MigraEntities())
            {
                var qAsistecRukan = contexto.RUKAN_MIGRA_USP_CON_OBTIENE_FAMILIA_CAMBIO_VIGENCIA_AVC_RUT(rutFamilia.ToString());

                foreach (var a in qAsistecRukan)
                {
                    _mae.RUT_FAMILIA = a.RUT_FAMILIA;
                    _mae.DV_FAMILIA = a.DV_FAMILIA;
                    _mae.NUEVA_VIGENCIA = a.NUEVA_VIGENCIA;
                    _mae.NOMBRES = a.NOMBRES;
                    _mae.APELLIDO_PATERNO = a.APELLIDO_PATERNO;
                    _mae.APELLIDO_MATERNO = a.APELLIDO_MATERNO;
                }

                return _mae;
            }
        }

        public static RUKAN_MIGRA_USP_CON_OBTIENE_NUMERO_CERTIFICADO_PPPF_Result GetCertificadoRukan(int accion, string rut, string cod_certificado)
        {
            RUKAN_MIGRA_USP_CON_OBTIENE_NUMERO_CERTIFICADO_PPPF_Result _mae = new RUKAN_MIGRA_USP_CON_OBTIENE_NUMERO_CERTIFICADO_PPPF_Result();

            using (Rukan_MigraEntities contexto = new Rukan_MigraEntities())
            {
                var qAsistecRukan = contexto.RUKAN_MIGRA_USP_CON_OBTIENE_NUMERO_CERTIFICADO_PPPF(accion, rut, cod_certificado);

                foreach (var a in qAsistecRukan)
                {
                    _mae.PER_RUT = a.PER_RUT;
                    _mae.PER_DIG = a.PER_DIG;
                    _mae.BNF_CER_NUM = a.BNF_CER_NUM;
                    _mae.ID_PER_NOM = a.ID_PER_NOM;
                    _mae.ID_PER_PAT = a.ID_PER_PAT;
                    _mae.ID_PER_MAT = a.ID_PER_MAT;
                    _mae.LLA_NUM = a.LLA_NUM;
                    _mae.LLA_ANO = a.LLA_ANO;
                    _mae.LIN_PRO_ID = a.LIN_PRO_ID;
                }

                return _mae;
            }

        }



        //public static WEB_ASISTEC_SNATIII_PPPF_PROYECTO GetCertificadoRukan(string cod_certificado)
        //{
        //    WEB_ASISTEC_SNATIII_PPPF_PROYECTO _mae = new WEB_ASISTEC_SNATIII_PPPF_PROYECTO();

        //    using (Web_Asistec_SnatIII_ContingenciaEntities contexto = new Web_Asistec_SnatIII_ContingenciaEntities())
        //    {
        //        var qAsistecPPPFProyecto = from a in contexto.WEB_ASISTEC_SNATIII_PPPF_PROYECTO
        //                                   where a.PPPF_PRY_CER == cod_certificado
        //                                   select a;
        //        foreach (var a in qAsistecPPPFProyecto)
        //        {
        //            _mae.PPPF_PRY_ID = a.PPPF_PRY_ID;
        //            _mae.PPPF_PRY_COD = a.PPPF_PRY_COD;
        //            _mae.PPPF_PRY_CER = a.PPPF_PRY_CER;
        //            _mae.PPPF_PRY_NOM = a.PPPF_PRY_NOM;
        //        }

        //        return _mae;
        //    }

        //}

        //public static BENEFICIO_CERTIFICADO GetCertificadoRukan(string cod_certificado)
        //{
        //    BENEFICIO_CERTIFICADO _mae = new BENEFICIO_CERTIFICADO();

        //    using (Rukan_MigraEntities contexto = new Rukan_MigraEntities())
        //    {
        //        var qAsistecPPPFProyecto = from a in contexto.BENEFICIO_CERTIFICADO
        //                                   where a.BNF_CER_NUM == cod_certificado
        //                                   select a;
        //        foreach (var a in qAsistecPPPFProyecto)
        //        {
        //            _mae.PPPF_PRY_ID = a.PPPF_PRY_ID;
        //            _mae.PPPF_PRY_COD = a.PPPF_PRY_COD;
        //            _mae.PPPF_PRY_CER = a.PPPF_PRY_CER;
        //            _mae.PPPF_PRY_NOM = a.PPPF_PRY_NOM;
        //        }

        //        return _mae;
        //    }

        //}

        //public static string getProvinciaDESC(int idProvincia)
        //{
        //    try
        //    {
        //        using (Rukan_MigraEntities contexto = new Rukan_MigraEntities())
        //        {
        //            var n = (from m in contexto.PROVINCIA
        //                     where m.PRV_ID == idProvincia
        //                     select m.PRV_DES).FirstOrDefault();

        //            return n.ToString();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public static List<PROVINCIA> obtenerProvincias()
        //{
        //   // Log.Instance.Info("ProvinciaDAO.obtenerProvincias");

        //    try
        //    {
        //        using (Rukan_MigraEntities contexto = new Rukan_MigraEntities())
        //        {
        //            var aux = (from p in contexto.PROVINCIA
        //                       select p);

        //            return aux.ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}

using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minvu.Snat.IData.DAO
{
   public class webAsistecSnatiiiFamiliaDAO
    {

        public static WEB_ASISTEC_SNATIII_FAMILIA Get(int rutFamilia)
        {

            WEB_ASISTEC_SNATIII_FAMILIA _mae = new WEB_ASISTEC_SNATIII_FAMILIA();

            using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
            {
                var qAsistecFamilia = from a in contexto.WEB_ASISTEC_SNATIII_FAMILIA
                                      where a.FAM_RUT == rutFamilia
                                      select a;
                foreach (var a in qAsistecFamilia)
                {
                    _mae.FAM_RUT = a.FAM_RUT;
                    _mae.FAM_DGV = a.FAM_DGV;
                    _mae.FAM_NOM = a.FAM_NOM;
                    _mae.FAM_APP_PAT = a.FAM_APP_PAT;
                    _mae.FAM_APP_MAT = a.FAM_APP_MAT;
                    _mae.FAM_FEC_FIN_VIG_SUB = a.FAM_FEC_FIN_VIG_SUB;                    
                }

                return _mae;
            }

        }

        public static WEB_ASISTEC_SNATIII_FAMILIA GetRukan(int rutFamilia)
        {
            WEB_ASISTEC_SNATIII_FAMILIA _mae = new WEB_ASISTEC_SNATIII_FAMILIA();

            using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
            {
                var qAsistecRukan = from a in contexto.WEB_ASISTEC_SNATIII_FAMILIA
                                    where a.FAM_RUT == rutFamilia
                                      select a;
                foreach (var a in qAsistecRukan)
                {
                    _mae.FAM_RUT = a.FAM_RUT;
                    _mae.FAM_DGV = a.FAM_DGV;
                    _mae.FAM_NOM = a.FAM_NOM;
                    _mae.FAM_APP_PAT = a.FAM_APP_PAT;
                    _mae.FAM_APP_MAT = a.FAM_APP_MAT;
                    _mae.FAM_FEC_FIN_VIG_SUB = a.FAM_FEC_FIN_VIG_SUB;
                }

                return _mae;

            }

        }

        public static WEB_ASISTEC_SNATIII_ACTUALIZA_VIGENCIA_Result ActulizaVigencia(long codProyecto, DateTime fechaVigencia, string usuario)
        {
            WEB_ASISTEC_SNATIII_ACTUALIZA_VIGENCIA_Result _mae = new WEB_ASISTEC_SNATIII_ACTUALIZA_VIGENCIA_Result();

            using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
            {
                var qAsistecPPPFProyecto = contexto.WEB_ASISTEC_SNATIII_ACTUALIZA_VIGENCIA(Convert.ToInt32(codProyecto), fechaVigencia, usuario);

                foreach (var a in qAsistecPPPFProyecto)
                {
                    _mae.MSG = a.MSG;
                    _mae.err = a.err;
                }

                return _mae;
            }


        }


        protected void ChangeStatus(WEB_ASISTEC_SNATIII_FAMILIA _asistecFamilia)
        {
            using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
            {
                WEB_ASISTEC_SNATIII_FAMILIA qAsistecFamilia = (from c in contexto.WEB_ASISTEC_SNATIII_FAMILIA
                                                               where c.FAM_RUT == _asistecFamilia.FAM_RUT
                                                 select c).FirstOrDefault();

                qAsistecFamilia.FAM_FEC_FIN_VIG_SUB = _asistecFamilia.FAM_FEC_FIN_VIG_SUB;

                contexto.SaveChanges();
            }

        }
    }
}



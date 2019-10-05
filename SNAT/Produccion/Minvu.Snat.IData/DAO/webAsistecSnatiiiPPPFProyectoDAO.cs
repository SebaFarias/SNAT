using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minvu.Snat.IData.DAO
{
    public class webAsistecSnatiiiPPPFProyectoDAO
    {
        public static List<WEB_ASISTEC_SNATIII_PPPF_PROYECTO> GetCertificado(string cod_certificado)
        {
            try
            {
                List<WEB_ASISTEC_SNATIII_PPPF_PROYECTO> ListadoCert = new List<WEB_ASISTEC_SNATIII_PPPF_PROYECTO>();

                using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
                {
                    var qAsistecPPPFProyecto = (from a in contexto.WEB_ASISTEC_SNATIII_PPPF_PROYECTO
                                                join b in contexto.WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO on a.PPPF_PRY_ID equals b.PPPF_PRY_ID
                                                where a.PPPF_PRY_CER == cod_certificado
                                                select new
                                                {
                                                    a.PPPF_PRY_ID,
                                                    a.PPPF_PRY_COD,
                                                    a.PPPF_PRY_CER,
                                                    a.PPPF_PRY_NOM,
                                                    a.PPPF_PRY_TIT,
                                                    b.PPPF_CTO_ID,
                                                    a.PPPF_PRY_ANO,
                                                    a.PPPF_NUM_LLAM
                                                }).ToList();
                    foreach (var a in qAsistecPPPFProyecto)
                    {
                        WEB_ASISTEC_SNATIII_PPPF_PROYECTO _mae = new WEB_ASISTEC_SNATIII_PPPF_PROYECTO();

                        _mae.PPPF_PRY_ID = a.PPPF_PRY_ID;
                        _mae.PPPF_PRY_COD = a.PPPF_PRY_COD;
                        _mae.PPPF_PRY_CER = a.PPPF_PRY_CER;
                        _mae.PPPF_PRY_NOM = a.PPPF_PRY_NOM;
                        _mae.PPPF_PRY_TIT = a.PPPF_PRY_TIT;
                        _mae.PPPF_PRY_ID = Int32.Parse(a.PPPF_CTO_ID.ToString());
                        _mae.PPPF_PRY_ANO = a.PPPF_PRY_ANO;
                        _mae.PPPF_NUM_LLAM = a.PPPF_NUM_LLAM;

                        ListadoCert.Add(_mae);
                    }

                    return ListadoCert;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static WEB_ASISTEC_SNATIII_PPPF_PROYECTO GetCertificadoRukan(string cod_certificado)
        {
            try
            {
                WEB_ASISTEC_SNATIII_PPPF_PROYECTO _mae = new WEB_ASISTEC_SNATIII_PPPF_PROYECTO();

                using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
                {
                    var qAsistecPPPFProyecto = from a in contexto.WEB_ASISTEC_SNATIII_PPPF_PROYECTO
                                               where a.PPPF_PRY_CER == cod_certificado
                                               select a;
                    foreach (var a in qAsistecPPPFProyecto)
                    {
                        _mae.PPPF_PRY_ID = a.PPPF_PRY_ID;
                        _mae.PPPF_PRY_COD = a.PPPF_PRY_COD;
                        _mae.PPPF_PRY_CER = a.PPPF_PRY_CER;
                        _mae.PPPF_PRY_NOM = a.PPPF_PRY_NOM;
                    }

                    return _mae;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static List<WEB_ASISTEC_SNATIII_PPPF_PROYECTO> GetRut(string rut_proyecto)
        {
            try
            {
                List<WEB_ASISTEC_SNATIII_PPPF_PROYECTO> ListadoCert = new List<WEB_ASISTEC_SNATIII_PPPF_PROYECTO>();

                using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
                {
                    var qAsistecPPPFProyecto = (from a in contexto.WEB_ASISTEC_SNATIII_PPPF_PROYECTO
                                                join b in contexto.WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO on a.PPPF_PRY_ID equals b.PPPF_PRY_ID
                                                where a.PPPF_PRY_COD.Contains(rut_proyecto)
                                                select new
                                                {
                                                    a.PPPF_PRY_ID,
                                                    a.PPPF_PRY_COD,
                                                    a.PPPF_PRY_CER,
                                                    a.PPPF_PRY_NOM,
                                                    a.PPPF_PRY_TIT,
                                                    b.PPPF_CTO_ID,
                                                    a.PPPF_PRY_ANO,
                                                    a.PPPF_NUM_LLAM
                                                }).ToList();
                    foreach (var a in qAsistecPPPFProyecto)
                    {
                        WEB_ASISTEC_SNATIII_PPPF_PROYECTO _mae = new WEB_ASISTEC_SNATIII_PPPF_PROYECTO();

                        _mae.PPPF_PRY_ID = a.PPPF_PRY_ID;
                        _mae.PPPF_PRY_COD = a.PPPF_PRY_COD;
                        _mae.PPPF_PRY_CER = a.PPPF_PRY_CER;
                        _mae.PPPF_PRY_NOM = a.PPPF_PRY_NOM;
                        _mae.PPPF_PRY_TIT = a.PPPF_PRY_TIT;
                        _mae.COM_COD = Int32.Parse(a.PPPF_CTO_ID.ToString());
                        _mae.PPPF_PRY_ANO = a.PPPF_PRY_ANO;
                        _mae.PPPF_NUM_LLAM = a.PPPF_NUM_LLAM;

                        ListadoCert.Add(_mae);
                    }

                    return ListadoCert;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static List<WEB_ASISTEC_SNATIII_PPPF_PROYECTO> GetRutTodos(string rut_proyecto, string cod_certificado)
        {
            try
            {
                List<WEB_ASISTEC_SNATIII_PPPF_PROYECTO> ListadoCert = new List<WEB_ASISTEC_SNATIII_PPPF_PROYECTO>();

                using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
                {
                    var qAsistecPPPFProyecto = (from a in contexto.WEB_ASISTEC_SNATIII_PPPF_PROYECTO
                                                join b in contexto.WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO on a.PPPF_PRY_ID equals b.PPPF_PRY_ID
                                                where (a.PPPF_PRY_COD == rut_proyecto) && (a.PPPF_PRY_CER == cod_certificado)
                                                select new
                                                {
                                                    a.PPPF_PRY_ID,
                                                    a.PPPF_PRY_COD,
                                                    a.PPPF_PRY_CER,
                                                    a.PPPF_PRY_NOM,
                                                    a.PPPF_PRY_TIT,
                                                    b.PPPF_CTO_ID,
                                                    a.PPPF_PRY_ANO,
                                                    a.PPPF_NUM_LLAM
                                                }).ToList();
                    foreach (var a in qAsistecPPPFProyecto)
                    {
                        WEB_ASISTEC_SNATIII_PPPF_PROYECTO _mae = new WEB_ASISTEC_SNATIII_PPPF_PROYECTO();

                        _mae.PPPF_PRY_ID = a.PPPF_PRY_ID;
                        _mae.PPPF_PRY_COD = a.PPPF_PRY_COD;
                        _mae.PPPF_PRY_CER = a.PPPF_PRY_CER;
                        _mae.PPPF_PRY_NOM = a.PPPF_PRY_NOM;
                        _mae.PPPF_PRY_TIT = a.PPPF_PRY_TIT;
                        _mae.PPPF_PRY_ID = Int32.Parse(a.PPPF_CTO_ID.ToString());
                        _mae.PPPF_PRY_ANO = a.PPPF_PRY_ANO;
                        _mae.PPPF_NUM_LLAM = a.PPPF_NUM_LLAM;

                        ListadoCert.Add(_mae);
                    }

                    return ListadoCert;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static List<WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_PPPF_Result> GetInfoProyecto(int accion, int codProyecto, string rut, string nombreProyecto)
        {
            try
            {
                List<WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_PPPF_Result> ListaConsultaProyecto = new List<WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_PPPF_Result>();

                using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
                {
                    var qAsistecPPPFProyecto = contexto.WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_PPPF(accion, codProyecto, rut, nombreProyecto);

                    foreach (var a in qAsistecPPPFProyecto)
                    {
                        WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_PPPF_Result _mae = new WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_PPPF_Result();

                        _mae.PPPF_PRY_COD = a.PPPF_PRY_COD;
                        _mae.PPPF_PRY_CER = a.PPPF_PRY_CER;
                        _mae.PPPF_PRY_NOM = a.PPPF_PRY_NOM;
                        _mae.PPPF_PRY_TIT = a.PPPF_PRY_TIT;
                        _mae.NOMBREITO = a.NOMBREITO;
                        _mae.NOMBREITOR = a.NOMBREITOR;
                        _mae.RUTITO = a.RUTITO;
                        _mae.RUTITOR = a.RUTITOR;
                        _mae.PPPF_PRY_ID = a.PPPF_PRY_ID;
                        _mae.PPPF_CTO_ID = a.PPPF_CTO_ID;
                        _mae.PPPF_PSAT_NOM = a.PPPF_PSAT_NOM;
                        _mae.NOMBRESPV = a.NOMBRESPV;
                        _mae.PPPF_CTO_TIP = a.PPPF_CTO_TIP;
                        _mae.NOMBREITOSERVIU = a.NOMBREITOSERVIU;
                        _mae.RUTITOSERVIU = a.RUTITOSERVIU;
                        _mae.RUTITORSERVIU = a.RUTITORSERVIU;
                        _mae.NOMBREITORSERVIU = a.NOMBREITORSERVIU;

                        ListaConsultaProyecto.Add(_mae);
                    }

                    return ListaConsultaProyecto;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static List<WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_SERVIU_PPPF_Result> GetInfoProyectoServiu(int accion, int codProyecto, string rut, string nombreProyecto)
        {
            try
            {
                List<WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_SERVIU_PPPF_Result> ListaConsultaProyecto = new List<WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_SERVIU_PPPF_Result>();

                using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
                {
                    var qAsistecPPPFProyecto = contexto.WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_SERVIU_PPPF(accion, codProyecto, rut, nombreProyecto);

                    foreach (var a in qAsistecPPPFProyecto)
                    {
                        WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_SERVIU_PPPF_Result _mae = new WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_SERVIU_PPPF_Result();

                        _mae.PPPF_PRY_COD = a.PPPF_PRY_COD;
                        _mae.PPPF_PRY_CER = a.PPPF_PRY_CER;
                        _mae.PPPF_PRY_NOM = a.PPPF_PRY_NOM;
                        _mae.PPPF_PRY_TIT = a.PPPF_PRY_TIT;
                        _mae.NOMBREITO = a.NOMBREITO;
                        _mae.NOMBREITOR = a.NOMBREITOR;
                        _mae.RUTITO = a.RUTITO;
                        _mae.RUTITOR = a.RUTITOR;
                        _mae.PPPF_PRY_ID = a.PPPF_PRY_ID;
                        _mae.PPPF_CTO_ID = a.PPPF_CTO_ID;
                        _mae.PPPF_PSAT_NOM = a.PPPF_PSAT_NOM;
                        _mae.NOMBRESPV = a.NOMBRESPV;
                        _mae.PPPF_CTO_TIP = a.PPPF_CTO_TIP;
                        _mae.NOMBREITOSERVIU = a.NOMBREITOSERVIU;
                        _mae.RUTITOSERVIU = a.RUTITOSERVIU;
                        _mae.RUTITORSERVIU = a.RUTITORSERVIU;
                        _mae.NOMBREITORSERVIU = a.NOMBREITORSERVIU;

                        ListaConsultaProyecto.Add(_mae);
                    }

                    return ListaConsultaProyecto;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static List<WEB_ASISTEC_SNATIII_CONSULTA_CONTRATO_PPPF_Result> GetInfoContrato(int accion, int rut, string codProyecto, string nombreProyecto)
        {
            try
            {
                List<WEB_ASISTEC_SNATIII_CONSULTA_CONTRATO_PPPF_Result> ListaConsultaContrato = new List<WEB_ASISTEC_SNATIII_CONSULTA_CONTRATO_PPPF_Result>();

                using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
                {
                    var qAsistecPPPFProyecto = contexto.WEB_ASISTEC_SNATIII_CONSULTA_CONTRATO_PPPF(accion, rut, codProyecto, nombreProyecto);

                    foreach (var a in qAsistecPPPFProyecto)
                    {
                        WEB_ASISTEC_SNATIII_CONSULTA_CONTRATO_PPPF_Result _mae = new WEB_ASISTEC_SNATIII_CONSULTA_CONTRATO_PPPF_Result();

                        _mae.PPPF_CTO_ID = a.PPPF_CTO_ID;
                        _mae.PPPF_CTO_NOM = a.PPPF_CTO_NOM;
                        _mae.PPPF_PSAT_NOM = a.PPPF_PSAT_NOM;
                        _mae.PPPF_CANT_PROYECTOS = a.PPPF_CANT_PROYECTOS;
                        _mae.PPPF_CTO_TIP = a.PPPF_CTO_TIP;

                        ListaConsultaContrato.Add(_mae);
                    }

                    return ListaConsultaContrato;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static WEB_ASISTEC_SNATIII_CAMBIO_TIPO_CONTRATO_Result ModificaTipoContrato(int idContrato, int idTipoContrato, string usuario)
        {
            try
            {
                WEB_ASISTEC_SNATIII_CAMBIO_TIPO_CONTRATO_Result _mae = new WEB_ASISTEC_SNATIII_CAMBIO_TIPO_CONTRATO_Result();

                using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
                {
                    var qAsistecPPPFProyecto = contexto.WEB_ASISTEC_SNATIII_CAMBIO_TIPO_CONTRATO(idContrato, idTipoContrato, usuario);

                    foreach (var a in qAsistecPPPFProyecto)
                    {
                        _mae.err = a.err;
                        _mae.MSG = a.MSG;
                    }

                    return _mae;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static WEB_ASISTEC_SNATIII_ACTUALIZA_PROFESIONAL_Result ModificaProfesional(int idProyecto, int rutProfesional, int rutProfReemplazo, int tipoContrato, string usuario)
        {
            try
            {
                WEB_ASISTEC_SNATIII_ACTUALIZA_PROFESIONAL_Result _mae = new WEB_ASISTEC_SNATIII_ACTUALIZA_PROFESIONAL_Result();

                using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
                {
                    var qAsistecPPPFProyecto = contexto.WEB_ASISTEC_SNATIII_ACTUALIZA_PROFESIONAL(idProyecto, rutProfesional, rutProfReemplazo, tipoContrato, usuario);

                    foreach (var a in qAsistecPPPFProyecto)
                    {
                        _mae.err = a.err;
                        _mae.MSG = a.MSG;
                    }

                    return _mae;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static WEB_ASISTEC_SNATIII_PPPF_PROYECTO GetRutRukan(string rut_proyecto)
        {
            try
            {
                WEB_ASISTEC_SNATIII_PPPF_PROYECTO _mae = new WEB_ASISTEC_SNATIII_PPPF_PROYECTO();

                using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
                {
                    var qAsistecPPPFProyecto = from a in contexto.WEB_ASISTEC_SNATIII_PPPF_PROYECTO
                                               where a.PPPF_PRY_COD == rut_proyecto
                                               select a;
                    foreach (var a in qAsistecPPPFProyecto)
                    {
                        _mae.PPPF_PRY_ID = a.PPPF_PRY_ID;
                        _mae.PPPF_PRY_COD = a.PPPF_PRY_COD;
                        _mae.PPPF_PRY_CER = a.PPPF_PRY_CER;
                        _mae.PPPF_PRY_NOM = a.PPPF_PRY_NOM;
                    }

                    return _mae;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }


        public static WEB_ASISTEC_SNATIII_ELIMINA_PROYECTO_PPPF_Result EliminaProyecto(string cod_proyecto, int idContrato, string usuario)
        {
            try
            {
                WEB_ASISTEC_SNATIII_ELIMINA_PROYECTO_PPPF_Result _mae = new WEB_ASISTEC_SNATIII_ELIMINA_PROYECTO_PPPF_Result();

                using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
                {
                    var qAsistecPPPFProyecto = contexto.WEB_ASISTEC_SNATIII_ELIMINA_PROYECTO_PPPF(cod_proyecto, idContrato, usuario);

                    foreach (var a in qAsistecPPPFProyecto)
                    {
                        _mae.MSG = a.MSG;
                        _mae.err = a.err;
                    }

                    return _mae;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static WEB_ASISTEC_SNATIII_ACTUALIZA_CERTIFICADO_Result ModificaCertificado(string codProyecto, string numCertificado, string usuario)
        {
            try
            {
                WEB_ASISTEC_SNATIII_ACTUALIZA_CERTIFICADO_Result _mae = new WEB_ASISTEC_SNATIII_ACTUALIZA_CERTIFICADO_Result();

                using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
                {
                    var qAsistecPPPFProyecto = contexto.WEB_ASISTEC_SNATIII_ACTUALIZA_CERTIFICADO(codProyecto, numCertificado, usuario);

                    foreach (var a in qAsistecPPPFProyecto)
                    {
                        _mae.MSG = a.MSG;
                        _mae.err = a.err;
                    }

                    return _mae;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static WEB_ASISTEC_SNATIII_PROFESIONALES GetProfesional(int rut_profesional)
        {
            try
            {
                WEB_ASISTEC_SNATIII_PROFESIONALES _mae = new WEB_ASISTEC_SNATIII_PROFESIONALES();

                using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
                {
                    var qAsistecPPPFProyecto = from a in contexto.WEB_ASISTEC_SNATIII_PROFESIONALES
                                               where a.PROF_RUT == rut_profesional
                                               select a;
                    foreach (var a in qAsistecPPPFProyecto)
                    {
                        _mae.PROF_RUT = a.PROF_RUT;
                        _mae.PROF_DGV = a.PROF_DGV;
                        _mae.PROF_NOM = a.PROF_NOM;
                        _mae.PROF_APP_PAT = a.PROF_APP_PAT;
                        _mae.PROF_APP_MAT = a.PROF_APP_MAT;
                        _mae.PROF_EST = a.PROF_EST;
                        _mae.PROF_ITO = a.PROF_ITO;
                        _mae.PROF_PROF_ID = a.PROF_PROF_ID;
                    }

                    return _mae;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static List<WEB_ASISTEC_SNATIII_PROFESION> GetProfesion()
        {
            try
            {
                List<WEB_ASISTEC_SNATIII_PROFESION> _ListadoProfesiones = new List<WEB_ASISTEC_SNATIII_PROFESION>();

                using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
                {
                    var qAsistecPPPFProyecto = from a in contexto.WEB_ASISTEC_SNATIII_PROFESION
                                               where a.PROF_PROF_EST == 1
                                               select a;

                    foreach (var a in qAsistecPPPFProyecto)
                    {
                        WEB_ASISTEC_SNATIII_PROFESION _mae = new WEB_ASISTEC_SNATIII_PROFESION();

                        _mae.PROF_PROF_ID = a.PROF_PROF_ID;
                        _mae.PROF_PROF_NOM = a.PROF_PROF_NOM;

                        _ListadoProfesiones.Add(_mae);
                    }

                    return _ListadoProfesiones;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static WEB_ASISTEC_SNATIII_INSERT_UPDATE_PROFESIONAL_Result InsertaProfesional(int rut_profesional, string dv_profesional, string nombre_profesional, string apellido_paterno,
                                                                                                string apellido_materno, int id_profesion, int profesionalFTO, string usuario)
        {
            try
            {
                WEB_ASISTEC_SNATIII_INSERT_UPDATE_PROFESIONAL_Result _mae = new WEB_ASISTEC_SNATIII_INSERT_UPDATE_PROFESIONAL_Result();

                using (Web_Asistec_SnatIII_Contingencia_Entities contexto = new Web_Asistec_SnatIII_Contingencia_Entities())
                {
                    var qAsistecPPPFProyecto = contexto.WEB_ASISTEC_SNATIII_INSERT_UPDATE_PROFESIONAL(rut_profesional, dv_profesional, nombre_profesional, apellido_paterno, apellido_materno,
                                                                                                       id_profesion, profesionalFTO, usuario);

                    foreach (var a in qAsistecPPPFProyecto)
                    {
                        _mae.MSG = a.MSG;
                        _mae.err = a.err;
                    }

                    return _mae;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
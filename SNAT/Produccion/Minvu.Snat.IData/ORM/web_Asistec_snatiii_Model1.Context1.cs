﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Minvu.Snat.IData.ORM
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Web_Asistec_SnatIII_Contingencia_Entities : DbContext
    {
        public Web_Asistec_SnatIII_Contingencia_Entities()
            : base("name=Web_Asistec_SnatIII_Contingencia_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<WEB_ASISTEC_SNATIII_FAMILIA> WEB_ASISTEC_SNATIII_FAMILIA { get; set; }
        public virtual DbSet<WEB_ASISTEC_SNATIII_PPPF_PROYECTO> WEB_ASISTEC_SNATIII_PPPF_PROYECTO { get; set; }
        public virtual DbSet<WEB_ASISTEC_SNATIII_PROFESION> WEB_ASISTEC_SNATIII_PROFESION { get; set; }
        public virtual DbSet<WEB_ASISTEC_SNATIII_PROFESIONALES> WEB_ASISTEC_SNATIII_PROFESIONALES { get; set; }
        public virtual DbSet<WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO> WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO { get; set; }
    
        public virtual ObjectResult<WEB_ASISTEC_SNATIII_ACTUALIZA_CERTIFICADO_Result> WEB_ASISTEC_SNATIII_ACTUALIZA_CERTIFICADO(string nCodProyecto, string nNumCertificado, string vUsuario)
        {
            var nCodProyectoParameter = nCodProyecto != null ?
                new ObjectParameter("nCodProyecto", nCodProyecto) :
                new ObjectParameter("nCodProyecto", typeof(string));
    
            var nNumCertificadoParameter = nNumCertificado != null ?
                new ObjectParameter("nNumCertificado", nNumCertificado) :
                new ObjectParameter("nNumCertificado", typeof(string));
    
            var vUsuarioParameter = vUsuario != null ?
                new ObjectParameter("vUsuario", vUsuario) :
                new ObjectParameter("vUsuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_ASISTEC_SNATIII_ACTUALIZA_CERTIFICADO_Result>("WEB_ASISTEC_SNATIII_ACTUALIZA_CERTIFICADO", nCodProyectoParameter, nNumCertificadoParameter, vUsuarioParameter);
        }
    
        public virtual ObjectResult<WEB_ASISTEC_SNATIII_ACTUALIZA_PROFESIONAL_Result> WEB_ASISTEC_SNATIII_ACTUALIZA_PROFESIONAL(Nullable<int> nIdProyecto, Nullable<int> nRutProfesional, Nullable<int> nRutReemplazo, Nullable<int> nTipoContrato, string vUsuario)
        {
            var nIdProyectoParameter = nIdProyecto.HasValue ?
                new ObjectParameter("nIdProyecto", nIdProyecto) :
                new ObjectParameter("nIdProyecto", typeof(int));
    
            var nRutProfesionalParameter = nRutProfesional.HasValue ?
                new ObjectParameter("nRutProfesional", nRutProfesional) :
                new ObjectParameter("nRutProfesional", typeof(int));
    
            var nRutReemplazoParameter = nRutReemplazo.HasValue ?
                new ObjectParameter("nRutReemplazo", nRutReemplazo) :
                new ObjectParameter("nRutReemplazo", typeof(int));
    
            var nTipoContratoParameter = nTipoContrato.HasValue ?
                new ObjectParameter("nTipoContrato", nTipoContrato) :
                new ObjectParameter("nTipoContrato", typeof(int));
    
            var vUsuarioParameter = vUsuario != null ?
                new ObjectParameter("vUsuario", vUsuario) :
                new ObjectParameter("vUsuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_ASISTEC_SNATIII_ACTUALIZA_PROFESIONAL_Result>("WEB_ASISTEC_SNATIII_ACTUALIZA_PROFESIONAL", nIdProyectoParameter, nRutProfesionalParameter, nRutReemplazoParameter, nTipoContratoParameter, vUsuarioParameter);
        }
    
        public virtual ObjectResult<WEB_ASISTEC_SNATIII_ACTUALIZA_VIGENCIA_Result> WEB_ASISTEC_SNATIII_ACTUALIZA_VIGENCIA(Nullable<int> nCodProyecto, Nullable<System.DateTime> nFechaVigencia, string vUsuario)
        {
            var nCodProyectoParameter = nCodProyecto.HasValue ?
                new ObjectParameter("nCodProyecto", nCodProyecto) :
                new ObjectParameter("nCodProyecto", typeof(int));
    
            var nFechaVigenciaParameter = nFechaVigencia.HasValue ?
                new ObjectParameter("nFechaVigencia", nFechaVigencia) :
                new ObjectParameter("nFechaVigencia", typeof(System.DateTime));
    
            var vUsuarioParameter = vUsuario != null ?
                new ObjectParameter("vUsuario", vUsuario) :
                new ObjectParameter("vUsuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_ASISTEC_SNATIII_ACTUALIZA_VIGENCIA_Result>("WEB_ASISTEC_SNATIII_ACTUALIZA_VIGENCIA", nCodProyectoParameter, nFechaVigenciaParameter, vUsuarioParameter);
        }
    
        public virtual ObjectResult<WEB_ASISTEC_SNATIII_CAMBIO_TIPO_CONTRATO_Result> WEB_ASISTEC_SNATIII_CAMBIO_TIPO_CONTRATO(Nullable<int> nIdContrato, Nullable<int> nTipoContrato, string vUsuario)
        {
            var nIdContratoParameter = nIdContrato.HasValue ?
                new ObjectParameter("nIdContrato", nIdContrato) :
                new ObjectParameter("nIdContrato", typeof(int));
    
            var nTipoContratoParameter = nTipoContrato.HasValue ?
                new ObjectParameter("nTipoContrato", nTipoContrato) :
                new ObjectParameter("nTipoContrato", typeof(int));
    
            var vUsuarioParameter = vUsuario != null ?
                new ObjectParameter("vUsuario", vUsuario) :
                new ObjectParameter("vUsuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_ASISTEC_SNATIII_CAMBIO_TIPO_CONTRATO_Result>("WEB_ASISTEC_SNATIII_CAMBIO_TIPO_CONTRATO", nIdContratoParameter, nTipoContratoParameter, vUsuarioParameter);
        }
    
        public virtual ObjectResult<WEB_ASISTEC_SNATIII_CONSULTA_CONTRATO_PPPF_Result> WEB_ASISTEC_SNATIII_CONSULTA_CONTRATO_PPPF(Nullable<int> aCCION, Nullable<int> pPPF_CTO_ID, string pPPF_CTO_RUT, string pPPF_NOMBRE)
        {
            var aCCIONParameter = aCCION.HasValue ?
                new ObjectParameter("ACCION", aCCION) :
                new ObjectParameter("ACCION", typeof(int));
    
            var pPPF_CTO_IDParameter = pPPF_CTO_ID.HasValue ?
                new ObjectParameter("PPPF_CTO_ID", pPPF_CTO_ID) :
                new ObjectParameter("PPPF_CTO_ID", typeof(int));
    
            var pPPF_CTO_RUTParameter = pPPF_CTO_RUT != null ?
                new ObjectParameter("PPPF_CTO_RUT", pPPF_CTO_RUT) :
                new ObjectParameter("PPPF_CTO_RUT", typeof(string));
    
            var pPPF_NOMBREParameter = pPPF_NOMBRE != null ?
                new ObjectParameter("PPPF_NOMBRE", pPPF_NOMBRE) :
                new ObjectParameter("PPPF_NOMBRE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_ASISTEC_SNATIII_CONSULTA_CONTRATO_PPPF_Result>("WEB_ASISTEC_SNATIII_CONSULTA_CONTRATO_PPPF", aCCIONParameter, pPPF_CTO_IDParameter, pPPF_CTO_RUTParameter, pPPF_NOMBREParameter);
        }
    
        public virtual ObjectResult<WEB_ASISTEC_SNATIII_CONSULTA_PROFESIONAL_PPPF_Result> WEB_ASISTEC_SNATIII_CONSULTA_PROFESIONAL_PPPF(Nullable<int> pROF_RUT)
        {
            var pROF_RUTParameter = pROF_RUT.HasValue ?
                new ObjectParameter("PROF_RUT", pROF_RUT) :
                new ObjectParameter("PROF_RUT", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_ASISTEC_SNATIII_CONSULTA_PROFESIONAL_PPPF_Result>("WEB_ASISTEC_SNATIII_CONSULTA_PROFESIONAL_PPPF", pROF_RUTParameter);
        }
    
        public virtual ObjectResult<WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_PPPF_Result> WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_PPPF(Nullable<int> aCCION, Nullable<int> pPPF_CTO_ID, string pPPF_CTO_RUT, string pPPF_NOMBRE)
        {
            var aCCIONParameter = aCCION.HasValue ?
                new ObjectParameter("ACCION", aCCION) :
                new ObjectParameter("ACCION", typeof(int));
    
            var pPPF_CTO_IDParameter = pPPF_CTO_ID.HasValue ?
                new ObjectParameter("PPPF_CTO_ID", pPPF_CTO_ID) :
                new ObjectParameter("PPPF_CTO_ID", typeof(int));
    
            var pPPF_CTO_RUTParameter = pPPF_CTO_RUT != null ?
                new ObjectParameter("PPPF_CTO_RUT", pPPF_CTO_RUT) :
                new ObjectParameter("PPPF_CTO_RUT", typeof(string));
    
            var pPPF_NOMBREParameter = pPPF_NOMBRE != null ?
                new ObjectParameter("PPPF_NOMBRE", pPPF_NOMBRE) :
                new ObjectParameter("PPPF_NOMBRE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_PPPF_Result>("WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_PPPF", aCCIONParameter, pPPF_CTO_IDParameter, pPPF_CTO_RUTParameter, pPPF_NOMBREParameter);
        }
    
        public virtual ObjectResult<WEB_ASISTEC_SNATIII_ELIMINA_PROYECTO_PPPF_Result> WEB_ASISTEC_SNATIII_ELIMINA_PROYECTO_PPPF(string vCodProyecto, Nullable<int> nIdContrato, string vUsuario)
        {
            var vCodProyectoParameter = vCodProyecto != null ?
                new ObjectParameter("vCodProyecto", vCodProyecto) :
                new ObjectParameter("vCodProyecto", typeof(string));
    
            var nIdContratoParameter = nIdContrato.HasValue ?
                new ObjectParameter("nIdContrato", nIdContrato) :
                new ObjectParameter("nIdContrato", typeof(int));
    
            var vUsuarioParameter = vUsuario != null ?
                new ObjectParameter("vUsuario", vUsuario) :
                new ObjectParameter("vUsuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_ASISTEC_SNATIII_ELIMINA_PROYECTO_PPPF_Result>("WEB_ASISTEC_SNATIII_ELIMINA_PROYECTO_PPPF", vCodProyectoParameter, nIdContratoParameter, vUsuarioParameter);
        }
    
        public virtual ObjectResult<WEB_ASISTEC_SNATIII_INSERT_UPDATE_PROFESIONAL_Result> WEB_ASISTEC_SNATIII_INSERT_UPDATE_PROFESIONAL(Nullable<int> nRutProfesional, string nDvProfesional, string vNombreProfesional, string vApellidoPatProfesional, string vApellidoMatProfesional, Nullable<int> nIdProfesion, Nullable<int> nProfesionalITO, string vUsuario)
        {
            var nRutProfesionalParameter = nRutProfesional.HasValue ?
                new ObjectParameter("nRutProfesional", nRutProfesional) :
                new ObjectParameter("nRutProfesional", typeof(int));
    
            var nDvProfesionalParameter = nDvProfesional != null ?
                new ObjectParameter("nDvProfesional", nDvProfesional) :
                new ObjectParameter("nDvProfesional", typeof(string));
    
            var vNombreProfesionalParameter = vNombreProfesional != null ?
                new ObjectParameter("vNombreProfesional", vNombreProfesional) :
                new ObjectParameter("vNombreProfesional", typeof(string));
    
            var vApellidoPatProfesionalParameter = vApellidoPatProfesional != null ?
                new ObjectParameter("vApellidoPatProfesional", vApellidoPatProfesional) :
                new ObjectParameter("vApellidoPatProfesional", typeof(string));
    
            var vApellidoMatProfesionalParameter = vApellidoMatProfesional != null ?
                new ObjectParameter("vApellidoMatProfesional", vApellidoMatProfesional) :
                new ObjectParameter("vApellidoMatProfesional", typeof(string));
    
            var nIdProfesionParameter = nIdProfesion.HasValue ?
                new ObjectParameter("nIdProfesion", nIdProfesion) :
                new ObjectParameter("nIdProfesion", typeof(int));
    
            var nProfesionalITOParameter = nProfesionalITO.HasValue ?
                new ObjectParameter("nProfesionalITO", nProfesionalITO) :
                new ObjectParameter("nProfesionalITO", typeof(int));
    
            var vUsuarioParameter = vUsuario != null ?
                new ObjectParameter("vUsuario", vUsuario) :
                new ObjectParameter("vUsuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_ASISTEC_SNATIII_INSERT_UPDATE_PROFESIONAL_Result>("WEB_ASISTEC_SNATIII_INSERT_UPDATE_PROFESIONAL", nRutProfesionalParameter, nDvProfesionalParameter, vNombreProfesionalParameter, vApellidoPatProfesionalParameter, vApellidoMatProfesionalParameter, nIdProfesionParameter, nProfesionalITOParameter, vUsuarioParameter);
        }
    
        public virtual ObjectResult<WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_SERVIU_PPPF_Result> WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_SERVIU_PPPF(Nullable<int> aCCION, Nullable<int> pPPF_CTO_ID, string pPPF_CTO_RUT, string pPPF_NOMBRE)
        {
            var aCCIONParameter = aCCION.HasValue ?
                new ObjectParameter("ACCION", aCCION) :
                new ObjectParameter("ACCION", typeof(int));
    
            var pPPF_CTO_IDParameter = pPPF_CTO_ID.HasValue ?
                new ObjectParameter("PPPF_CTO_ID", pPPF_CTO_ID) :
                new ObjectParameter("PPPF_CTO_ID", typeof(int));
    
            var pPPF_CTO_RUTParameter = pPPF_CTO_RUT != null ?
                new ObjectParameter("PPPF_CTO_RUT", pPPF_CTO_RUT) :
                new ObjectParameter("PPPF_CTO_RUT", typeof(string));
    
            var pPPF_NOMBREParameter = pPPF_NOMBRE != null ?
                new ObjectParameter("PPPF_NOMBRE", pPPF_NOMBRE) :
                new ObjectParameter("PPPF_NOMBRE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_SERVIU_PPPF_Result>("WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_SERVIU_PPPF", aCCIONParameter, pPPF_CTO_IDParameter, pPPF_CTO_RUTParameter, pPPF_NOMBREParameter);
        }
    
        public virtual ObjectResult<WEB_ASISTEC_SNATIII_VERIFICAR_PROYECTOS_SNAT_Result> WEB_ASISTEC_SNATIII_VERIFICAR_PROYECTOS_SNAT(Nullable<long> pIdprograma, string pCodigo)
        {
            var pIdprogramaParameter = pIdprograma.HasValue ?
                new ObjectParameter("pIdprograma", pIdprograma) :
                new ObjectParameter("pIdprograma", typeof(long));
    
            var pCodigoParameter = pCodigo != null ?
                new ObjectParameter("pCodigo", pCodigo) :
                new ObjectParameter("pCodigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_ASISTEC_SNATIII_VERIFICAR_PROYECTOS_SNAT_Result>("WEB_ASISTEC_SNATIII_VERIFICAR_PROYECTOS_SNAT", pIdprogramaParameter, pCodigoParameter);
        }
    }
}

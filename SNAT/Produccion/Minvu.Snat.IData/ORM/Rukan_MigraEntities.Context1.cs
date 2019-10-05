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
    
    public partial class Rukan_MigraEntities : DbContext
    {
        public Rukan_MigraEntities()
            : base("name=Rukan_MigraEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<COMUNA> COMUNA { get; set; }
        public virtual DbSet<PROVINCIA> PROVINCIA { get; set; }
        public virtual DbSet<REGION> REGION { get; set; }
        public virtual DbSet<USERS> USERS { get; set; }
        public virtual DbSet<USERREGION> USERREGION { get; set; }
    
        public virtual ObjectResult<PA_LISTAR_REGION_Result> PA_LISTAR_REGION()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_LISTAR_REGION_Result>("PA_LISTAR_REGION");
        }
    
        public virtual ObjectResult<RUKAN_MIGRA_USP_CON_OBTIENE_FAMILIA_CAMBIO_VIGENCIA_AVC_RUT_Result> RUKAN_MIGRA_USP_CON_OBTIENE_FAMILIA_CAMBIO_VIGENCIA_AVC_RUT(string rUT_BENEFICIARIO)
        {
            var rUT_BENEFICIARIOParameter = rUT_BENEFICIARIO != null ?
                new ObjectParameter("RUT_BENEFICIARIO", rUT_BENEFICIARIO) :
                new ObjectParameter("RUT_BENEFICIARIO", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RUKAN_MIGRA_USP_CON_OBTIENE_FAMILIA_CAMBIO_VIGENCIA_AVC_RUT_Result>("RUKAN_MIGRA_USP_CON_OBTIENE_FAMILIA_CAMBIO_VIGENCIA_AVC_RUT", rUT_BENEFICIARIOParameter);
        }
    
        public virtual ObjectResult<RUKAN_MIGRA_USP_CON_OBTIENE_NUMERO_CERTIFICADO_PPPF_Result> RUKAN_MIGRA_USP_CON_OBTIENE_NUMERO_CERTIFICADO_PPPF(Nullable<int> aCCION, string rUT, string nUM_CERTIFICADO)
        {
            var aCCIONParameter = aCCION.HasValue ?
                new ObjectParameter("ACCION", aCCION) :
                new ObjectParameter("ACCION", typeof(int));
    
            var rUTParameter = rUT != null ?
                new ObjectParameter("RUT", rUT) :
                new ObjectParameter("RUT", typeof(string));
    
            var nUM_CERTIFICADOParameter = nUM_CERTIFICADO != null ?
                new ObjectParameter("NUM_CERTIFICADO", nUM_CERTIFICADO) :
                new ObjectParameter("NUM_CERTIFICADO", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RUKAN_MIGRA_USP_CON_OBTIENE_NUMERO_CERTIFICADO_PPPF_Result>("RUKAN_MIGRA_USP_CON_OBTIENE_NUMERO_CERTIFICADO_PPPF", aCCIONParameter, rUTParameter, nUM_CERTIFICADOParameter);
        }
    }
}
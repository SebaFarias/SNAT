//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class PROVEEDOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROVEEDOR()
        {
            this.AUTORIZACION = new HashSet<AUTORIZACION>();
            this.CONTRATO_ATP = new HashSet<CONTRATO_ATP>();
            this.TIPO_PROVEEDOR_INFORMACION_PROYECTO = new HashSet<TIPO_PROVEEDOR_INFORMACION_PROYECTO>();
            this.SOLICITUD_PAGO = new HashSet<SOLICITUD_PAGO>();
            this.SOLICITUD_PAGO1 = new HashSet<SOLICITUD_PAGO>();
        }
    
        public long IDPROVEEDOR { get; set; }
        public Nullable<long> IDMAESTROTIPOPROVEEDOR { get; set; }
        public Nullable<int> RUTPROVEEDOR { get; set; }
        public string DVPROVEDIGITOVERIFICADORPROVEEDOR { get; set; }
        public string NOMBREPROVEEDOR { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AUTORIZACION> AUTORIZACION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTRATO_ATP> CONTRATO_ATP { get; set; }
        public virtual MAESTRO_TIPO_PROVEEDOR MAESTRO_TIPO_PROVEEDOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TIPO_PROVEEDOR_INFORMACION_PROYECTO> TIPO_PROVEEDOR_INFORMACION_PROYECTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOLICITUD_PAGO> SOLICITUD_PAGO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOLICITUD_PAGO> SOLICITUD_PAGO1 { get; set; }
    }
}

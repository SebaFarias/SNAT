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
    
    public partial class MAESTRO_TIPO_PAGO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MAESTRO_TIPO_PAGO()
        {
            this.SOLICITUD_PAGO_ATP = new HashSet<SOLICITUD_PAGO_ATP>();
            this.SOLICITUD_PAGO = new HashSet<SOLICITUD_PAGO>();
        }
    
        public long IDMAESTROTIPOPAGO { get; set; }
        public string NOMBREMAESTROTIPOPAGO { get; set; }
        public Nullable<bool> ESTADOMAESTROTIPOPAGO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOLICITUD_PAGO_ATP> SOLICITUD_PAGO_ATP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOLICITUD_PAGO> SOLICITUD_PAGO { get; set; }
    }
}

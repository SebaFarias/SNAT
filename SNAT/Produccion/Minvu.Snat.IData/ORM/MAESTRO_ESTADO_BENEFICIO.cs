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
    
    public partial class MAESTRO_ESTADO_BENEFICIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MAESTRO_ESTADO_BENEFICIO()
        {
            this.INFORMACION_PROYECTO = new HashSet<INFORMACION_PROYECTO>();
            this.INFORMACION_PROYECTO_SOLICITUD = new HashSet<INFORMACION_PROYECTO_SOLICITUD>();
        }
    
        public long IDMAESTROESTADOBENEFICIO { get; set; }
        public string NOMBREMAESTROESTADOBENEFICIO { get; set; }
        public Nullable<bool> ESTADOMAESTROESTADOBENEFICIO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INFORMACION_PROYECTO> INFORMACION_PROYECTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INFORMACION_PROYECTO_SOLICITUD> INFORMACION_PROYECTO_SOLICITUD { get; set; }
    }
}

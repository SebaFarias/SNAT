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
    
    public partial class PROPIEDAD_TERRENO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROPIEDAD_TERRENO()
        {
            this.CONTRATO_ATP = new HashSet<CONTRATO_ATP>();
        }
    
        public long IDPROPIEDADTERRENO { get; set; }
        public string NOMBREPROPIEDADTERRENO { get; set; }
        public Nullable<bool> ESTADOPROPIEDADTERRENO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTRATO_ATP> CONTRATO_ATP { get; set; }
    }
}
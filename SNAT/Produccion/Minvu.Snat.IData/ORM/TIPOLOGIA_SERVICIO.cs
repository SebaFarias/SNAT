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
    
    public partial class TIPOLOGIA_SERVICIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TIPOLOGIA_SERVICIO()
        {
            this.SERVICIO_PARCIALIDAD = new HashSet<SERVICIO_PARCIALIDAD>();
        }
    
        public Nullable<long> IDMAESTROTIPOLOGIA { get; set; }
        public long IDTIPOLOGIASERVICIO { get; set; }
        public long IDGRUPOTIPOLOGIASERVICIO { get; set; }
        public Nullable<long> IDMAESTROSERVICIO { get; set; }
    
        public virtual MAESTRO_SERVICIO MAESTRO_SERVICIO { get; set; }
        public virtual MAESTRO_TIPOLOGIA MAESTRO_TIPOLOGIA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SERVICIO_PARCIALIDAD> SERVICIO_PARCIALIDAD { get; set; }
    }
}
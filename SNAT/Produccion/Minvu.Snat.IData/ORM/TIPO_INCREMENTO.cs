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
    
    public partial class TIPO_INCREMENTO
    {
        public long IDTIPOINCREMENTO { get; set; }
        public Nullable<long> IDMAESTROINCREMENTO { get; set; }
        public Nullable<long> IDCARACTERISTICASESPECIALES { get; set; }
        public Nullable<long> IDSOLICITUDPAGO { get; set; }
        public Nullable<bool> SELECCIONADOTIPOINCREMENTO { get; set; }
        public Nullable<bool> ESTADOTIPOINCREMENTO { get; set; }
    
        public virtual CARACTERISTICAS_ESPECIALES CARACTERISTICAS_ESPECIALES { get; set; }
        public virtual MAESTRO_INCREMENTO MAESTRO_INCREMENTO { get; set; }
        public virtual SOLICITUD_PAGO SOLICITUD_PAGO { get; set; }
    }
}

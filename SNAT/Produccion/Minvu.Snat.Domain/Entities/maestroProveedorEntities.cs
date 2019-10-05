using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Minvu.Snat.Domain.Entities
{
    public class proovedorEntities
    {
        [Display(Name = "Tipo proveedor:")]
        public int idProovedor { get; set; }
        public int idMaestroTipoProveedor { get; set; }

        [Display(Name = "Nombre proveedor:")]
        public string nombreProveedor { get; set; }
        public int rutProveedor { get; set; }
        public char dvProveedor { get; set; }
        

        public proovedorEntities()
        {
            idMaestroTipoProveedor = 0;
            idProovedor = 0;
            rutProveedor = 0;
            nombreProveedor = string.Empty;
            dvProveedor = '0';
        }

        public proovedorEntities(int _idMaestroTipoProveedor, int _idProovedor, int _rutProveedor, string _nombreProveedor, char _dvProveedor)
        {
            idMaestroTipoProveedor = idMaestroTipoProveedor;
            idProovedor = _idProovedor;
            rutProveedor = _rutProveedor;
            nombreProveedor = _nombreProveedor;
            dvProveedor = _dvProveedor;
        }
    } 
}


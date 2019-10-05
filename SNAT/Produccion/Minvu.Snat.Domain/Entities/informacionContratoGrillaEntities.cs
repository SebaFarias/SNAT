using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.Domain.Entities
{
    public class informacionContratoGrillaEntities
    {
        public long idContratoProyecto { get; set; }
        public string idNombreContrato { get; set; }
        public string tipoContrato { get; set; }
        public string idNombrePSAT { get; set; }
        public long cantidadProyectos { get; set; }
        

        public informacionContratoGrillaEntities()
        {
            idContratoProyecto = 0;
            idNombreContrato = string.Empty;
            tipoContrato = string.Empty;
            idNombrePSAT = string.Empty;
            cantidadProyectos = 0;
        }

        public informacionContratoGrillaEntities(int _idContratoProyecto, string _idNombreContrato, string _tipoContrato, string _idNombrePSAT, int _cantidadProyectos)
        {
            idContratoProyecto = _idContratoProyecto;
            idNombreContrato = _idNombreContrato;
            tipoContrato = _tipoContrato;
            idNombrePSAT = _idNombrePSAT;
            cantidadProyectos = _cantidadProyectos;            
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.Domain.Entities;
using Minvu.Snat.Domain.Controls;

namespace Minvu.Snat.Domain.Forms
{
    public class ConsultaContratoPPPF
    {
        public ConsultaContratoPPPF() { }
        public informacionContratoEntities _informacionContratoEntities { get; set; }
        public List<informacionContratoGrillaEntities> _auxinformacionContratoEntities { get; set; }
    }

    public class ConsultaContratoPPPFFactory
    {
        public static List<informacionContratoGrillaEntities> getContratoPPPF(int accion, int idContrato, string codProyecto, string nombreProyecto)
        {
            ConsultaContratoPPPF _ConsultaContratoPPPF = new ConsultaContratoPPPF();
            _ConsultaContratoPPPF._auxinformacionContratoEntities = informacionContratoEntitiesFactory.getConsultaContratoPPPFRutEntities(accion, idContrato, codProyecto, nombreProyecto);
            
            return _ConsultaContratoPPPF._auxinformacionContratoEntities;

        }

        public static object modificaTipoContratoPPPF(int idContrato, int idTipoContrato, string usuario)
        {
            ConsultaContratoPPPF _ConsultaContratoPPPF = new ConsultaContratoPPPF();
            _ConsultaContratoPPPF._informacionContratoEntities = informacionContratoEntitiesFactory.getModificaContratoPPPFEntities(idContrato, idTipoContrato, usuario);
            _ConsultaContratoPPPF._auxinformacionContratoEntities = informacionContratoEntitiesFactory.getConsultaContratoPPPFRutEntities(5, idContrato, string.Empty, string.Empty);
            return _ConsultaContratoPPPF;
        }        
    }
}

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
    public class informacionProyectoGrillaEntities
    {
        public string codigoProyectoPPPF { get; set; }
        public int idContrato { get; set; }
        public string numeroCertificadoPPPF { get; set; }
        public string idMaestroTitulo { get; set; }
        public string nombrePSATPPPF { get; set; }
        public string nombreProyectoPPPF { get; set; }
        public string profesionalFTO { get; set; }
        public string rutProfesionalFTO { get; set; }
        public string profesionalReemplazoFTO { get; set; }        
        public string rutprofesionalReemplazoFTO { get; set; }
        public string idProyectoPPPF { get; set; }
        public string nombreSupervisor { get; set; }
        public bool informacionDisponible { get; set; }
        public string tipoContrato { get; set; }

        public informacionProyectoGrillaEntities()
        {
            codigoProyectoPPPF = string.Empty;
            idContrato = 0;
            numeroCertificadoPPPF = string.Empty;
            idMaestroTitulo = string.Empty;
            nombrePSATPPPF = string.Empty;
            nombreProyectoPPPF = string.Empty;                  
            profesionalFTO = string.Empty;
            rutProfesionalFTO = string.Empty;
            profesionalReemplazoFTO = string.Empty;            
            rutprofesionalReemplazoFTO = string.Empty;
            idProyectoPPPF = string.Empty;
            tipoContrato = string.Empty;
        }

        public informacionProyectoGrillaEntities(string _codigoProyectoPPPF, int _idContrato, string _numeroCertificadoPPPF, string _idMaestroTitulo,
            string _nombrePSATPPPF, string _nombreProyectoPPPF, string _profesionalFTO, string _rutProfesionalFTO, string _profesionalReemplazoFTO, 
            string _rutprofesionalReemplazoFTO, string _idProyectoPPPF)
        {
            codigoProyectoPPPF = _codigoProyectoPPPF;
            idContrato = _idContrato;
            numeroCertificadoPPPF = _numeroCertificadoPPPF;
            idMaestroTitulo = _idMaestroTitulo;
            nombrePSATPPPF = _nombrePSATPPPF;
            nombreProyectoPPPF = _nombreProyectoPPPF;
            profesionalFTO = _profesionalFTO;
            rutProfesionalFTO = _rutProfesionalFTO;
            profesionalReemplazoFTO = _profesionalReemplazoFTO;
            rutprofesionalReemplazoFTO = _rutprofesionalReemplazoFTO;
            idProyectoPPPF = _idProyectoPPPF;
        }
    }   
}
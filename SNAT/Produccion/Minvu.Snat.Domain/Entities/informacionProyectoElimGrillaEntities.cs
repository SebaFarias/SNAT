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
    public class informacionProyectoElimGrillaEntities
    {
        public string codigoProyectoPPPF { get; set; }
        public int idContrato { get; set; }
        public string numeroCertificadoPPPF { get; set; }
        public string nombrePSATPPPF { get; set; }
        public string nombreProyectoPPPF { get; set; }
        public string idMaestroTitulo { get; set; }
        public string profesionalFTO { get; set; }
        public string profesionalReemplazoFTO { get; set; }
        public string nombreSupervisor { get; set; }
        public string idProyectoPPPF { get; set; }
        public bool informacionDisponible { get; set; }
        public string tipoContrato { get; set; }

        public informacionProyectoElimGrillaEntities()
        {
            codigoProyectoPPPF = string.Empty;
            idContrato = 0;
            numeroCertificadoPPPF = string.Empty;
            nombrePSATPPPF = string.Empty;
            nombreProyectoPPPF = string.Empty;
            idMaestroTitulo = string.Empty;
            profesionalFTO = string.Empty;
            profesionalReemplazoFTO = string.Empty;
            nombreSupervisor = string.Empty;
            idProyectoPPPF = string.Empty;
            tipoContrato = string.Empty;
        }

        public informacionProyectoElimGrillaEntities(string _codigoProyectoPPPF, int _idContrato, string _numeroCertificadoPPPF, string _nombrePSATPPPF,
            string _nombreProyectoPPPF, string _idMaestroTitulo, string _profesionalFTO, string _profesionalReemplazoFTO, string _nombreSupervisor, string _idProyectoPPPF)
        {
            codigoProyectoPPPF = _codigoProyectoPPPF;
            idContrato = _idContrato;
            numeroCertificadoPPPF = _numeroCertificadoPPPF;
            nombrePSATPPPF = _nombrePSATPPPF;
            nombreProyectoPPPF = _nombreProyectoPPPF;
            idMaestroTitulo = _idMaestroTitulo;
            profesionalFTO = _profesionalFTO;
            profesionalReemplazoFTO = _profesionalReemplazoFTO;
            nombreSupervisor = _nombreSupervisor;
            idProyectoPPPF = _idProyectoPPPF;
        }
    }   
}
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
    public class informacionContratoEntities
    {

        public long idContratoProyecto { get; set; }
        public string idNombreContrato { get; set; }
        public string idNombrePSAT { get; set; }
        public long cantidadProyectos { get; set; }
        public string tipoContrato { get; set; }

        [RegularExpression(@"(^[1-9]\d*$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string rutContrato { get; set; }
        public string dvContrato { get; set; }

        [RegularExpression(@"(^[0-9]*$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string idContratoConsulta { get; set; }

        //[RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ)('´\-\.\,\'a-z ]{3,200}$)", ErrorMessage = "Debe contener al menos tres caracteres.")]
        public string nombreContratoConsulta { get; set; }

        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string idProyectoContratoConsulta { get; set; }
        public string mensajeSalida { get; set; }
        public string codigoSalida { get; set; }

        public informacionContratoEntities()
        {
            idContratoProyecto = 0;
            idNombreContrato = string.Empty;
            idNombrePSAT = string.Empty;
            cantidadProyectos = 0;
            tipoContrato = string.Empty;
            rutContrato = string.Empty;
            dvContrato = string.Empty;
            idContratoConsulta = string.Empty;
            nombreContratoConsulta = string.Empty;
            mensajeSalida = string.Empty;
            codigoSalida = string.Empty;
        }

        public informacionContratoEntities(int _idContratoProyecto, string _idNombreContrato, string _idNombrePSAT, int _cantidadProyectos, string _tipoContrato, string _rutProyecto,
                                           string _dvProyecto)
        {
            idContratoProyecto = _idContratoProyecto;
            idNombreContrato = _idNombreContrato;
            idNombrePSAT = _idNombrePSAT;
            cantidadProyectos = _cantidadProyectos;
            tipoContrato = _tipoContrato;
            rutContrato = _rutProyecto;
            dvContrato = _dvProyecto;
        }

    }

    public class informacionContratoEntitiesFactory
    {
        internal static List<informacionContratoGrillaEntities> getConsultaContratoPPPFRutEntities(int accion, int idContrato, string codProyecto, string nombreProyecto)
        {
            var _informacionProyectoDAO = webAsistecSnatiiiPPPFProyectoDAO.GetInfoContrato(accion, idContrato, codProyecto, nombreProyecto);
            List<informacionContratoGrillaEntities> informacionProyectoEntities = new List<informacionContratoGrillaEntities>();
            if (_informacionProyectoDAO != null)
            {
                foreach (var a in _informacionProyectoDAO)
                {
                    informacionContratoGrillaEntities info = new informacionContratoGrillaEntities();

                    info.idContratoProyecto = Convert.ToInt64(a.PPPF_CTO_ID);
                    info.idNombreContrato = a.PPPF_CTO_NOM;
                    info.idNombrePSAT = a.PPPF_PSAT_NOM;
                    info.cantidadProyectos = Convert.ToInt64(a.PPPF_CANT_PROYECTOS);
                    info.tipoContrato = a.PPPF_CTO_TIP;

                    informacionProyectoEntities.Add(info);
                }
                return informacionProyectoEntities;
            }
            else
                return informacionProyectoEntities;
        }

        internal static informacionContratoEntities getModificaContratoPPPFEntities(int idContrato, int idTipoContrato, string usuario)
        {
            var _informacionProyectoDAO = webAsistecSnatiiiPPPFProyectoDAO.ModificaTipoContrato(idContrato, idTipoContrato, usuario);
            informacionContratoEntities informacionProyectoEntities = new informacionContratoEntities();
            if (_informacionProyectoDAO != null)
            {
                return new informacionContratoEntities
                {
                    mensajeSalida = _informacionProyectoDAO.MSG,
                    codigoSalida = _informacionProyectoDAO.err.ToString()
                };
            }
            else
                return informacionProyectoEntities;
        }
    }    
}

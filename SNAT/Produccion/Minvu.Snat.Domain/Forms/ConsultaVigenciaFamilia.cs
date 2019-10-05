using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.Domain.Entities;
using Minvu.Snat.Domain.Controls;
using static Minvu.Snat.Domain.Entities.beneficiarioEntities;

namespace Minvu.Snat.Domain.Forms
{
    public class ConsultaVigenciaFamilia
    {
        public ConsultaVigenciaFamilia() { }

        public beneficiarioEntities _beneficiarioEntities { get; set; }
        public beneficiarioEntities _beneficiarioRukanEntities { get; set; }
        public informacionProyectoEntities _informacionProyectoEntities { get; set; }
        public maestroLlamadoEntities _maestroLlamadoEntities { get; set; }
        //public List<maestroProgramaEntities> _listMaestroProgramaEntities { get; set; }
        public maestroProgramaEntities _maestroProgramaEntities { get; set; }
        public maestroModalidadEntities _maestroModalidadEntities { get; set; }
        public maestroTipologiaEntities _maestroTipologiaEntities { get; set; }
        public maestroTituloEntities _maestroTituloEntities { get; set; }
        public maestroAlternativaPostulacionEntities _maestroAlternativaPostulacionEntities { get; set; }
        public maestroTipoProveedorEntities _maestroTipoProveedorEntities { get; set; }
        public maestroResolucionEntities _maestroResolucionEntities { get; set; }
        public proveedorEntities _proveedorEntities { get; set; }
        public direccionEntities _direccionEntities { get; set; }
        public regionControl _regionControl { get; set; }
        public provinciaControl _provinciaControl { get; set; }
        public comunaControl _comunaControl { get; set; }
        public caracteristicasEspecialesEntities _caracteristicasEspecialesEntities { get; set; }
        public List<auxPlantillaEntities> _auxPlantillaEntities { get; set; }
        public List<auxPlantillaEntities> _aux2PlantillaEntities { get; set; }
        public beneficiarioEntities ConsultaInformacionProyectoEntities { get; set; }
    }

    public class ConsultaVigenciaFamiliaFactory
    {
        public static object getBeneficiario(int rutFamilia)
        {
            ConsultaVigenciaFamilia _ConsultaVigenciaFamilia = new ConsultaVigenciaFamilia();
            _ConsultaVigenciaFamilia._beneficiarioEntities = informacionBeneficiarioEntitiesFactory.getinformacionBeneficiarioEntities(rutFamilia);
            _ConsultaVigenciaFamilia._beneficiarioRukanEntities = informacionBeneficiarioEntitiesFactory.getinformacionBeneficiarioRukanEntities(rutFamilia);
            return _ConsultaVigenciaFamilia;


        }

        public static object getBeneficiarioRukan(int rutFamilia)
        {
            ConsultaVigenciaFamilia _ConsultaVigenciaFamilia = new ConsultaVigenciaFamilia();
            _ConsultaVigenciaFamilia._beneficiarioRukanEntities = informacionBeneficiarioEntitiesFactory.getinformacionBeneficiarioRukanEntities(rutFamilia);

            return _ConsultaVigenciaFamilia;
        }

        public static void DeleteProyecto(long codProyecto)
        {
            ConsultaInformacionProyecto _ConsultaInformacionProyecto = new ConsultaInformacionProyecto();
            _ConsultaInformacionProyecto._informacionProyectoEntities = informacionProyectoEntitiesFactory.deleteProyectoEntities(codProyecto);

        }

        public static object changeVigenciaPPPF(long idProyecto, DateTime fechaVigencia, string usuario)
        {
            ConsultaVigenciaFamilia _ConsultaProyectoPPPF = new ConsultaVigenciaFamilia();
            _ConsultaProyectoPPPF._beneficiarioRukanEntities = informacionBeneficiarioEntitiesFactory.changeVigenciaPPPFEntities(idProyecto, fechaVigencia, usuario);
            _ConsultaProyectoPPPF._beneficiarioEntities = _ConsultaProyectoPPPF._beneficiarioRukanEntities;
            return _ConsultaProyectoPPPF;
        }
    }
}

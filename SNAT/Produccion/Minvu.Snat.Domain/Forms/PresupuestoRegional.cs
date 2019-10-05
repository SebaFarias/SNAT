using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.Domain.Entities;
using Minvu.Snat.Domain.Controls;
using System.Web.Mvc;

namespace Minvu.Snat.Domain.Forms
{
    public class PresupuestoRegional
    {
        public PresupuestoRegional() { }
        public List<RegionesEntities> _regionEntities { get; set; }
        public List<RegionesEntities> _regionPresupuestoEntities { get; set; }
        public List<RegionesEntities> _regionUserEntities { get; set; }
        public presupuestoRegionalEntities _presupuestoRegionalEntities { get; set; }
        public List<informacionPresupuestoRegionalEntities> _obtienePresupuestoRegionalEntities { get; set; }

        public string _RegionUser { get; set; }     
        public string codSalida { get; set; }
    }

    public class PresupuestoRegionalFactory
    {
        public static object getRegiones(string userName)
        {
            PresupuestoRegional _PresupuestoRegional = new PresupuestoRegional();
            FuncionarioEntities _funcionarioEntities = FuncionarioEntitiesFactory.getFuncionario(userName);
            _PresupuestoRegional._regionEntities = RegionesEntitiesFactory.getListRegiones();
            _PresupuestoRegional._regionPresupuestoEntities = RegionesEntitiesFactory.getListRegionesPresupuesto();
            _PresupuestoRegional._RegionUser = _funcionarioEntities.idRegion.ToString();

            return _PresupuestoRegional;
        }

        public static object insertaPresupuestoRegional(int anno, int numeroResolucion, DateTime fechaResolucion, int codigoRegion, long montoResolucionRegion, string observacion, string nombreArchivo, string usuario)
        {
            PresupuestoRegional _PresupuestoRegional = new PresupuestoRegional();
            _PresupuestoRegional._presupuestoRegionalEntities = presupuestoRegionalEntitiesFactory.insertaPresupuestoRegional(anno, numeroResolucion, fechaResolucion, codigoRegion, montoResolucionRegion, observacion, nombreArchivo, usuario);
            _PresupuestoRegional._regionEntities = RegionesEntitiesFactory.getListRegiones();
            _PresupuestoRegional._regionPresupuestoEntities = RegionesEntitiesFactory.getListRegionesPresupuesto();
            _PresupuestoRegional.codSalida = _PresupuestoRegional._presupuestoRegionalEntities.codigoSalida;
            return _PresupuestoRegional;
        }

        public static object obtienePresupuestoRegional(int accion, int anno, int numeroResolucion, DateTime fechaResolucion, int codigoRegion, string userName, bool usuarioFull)
        {
            PresupuestoRegional _PresupuestoRegional = new PresupuestoRegional();
            FuncionarioEntities _funcionarioEntities = FuncionarioEntitiesFactory.getFuncionario(userName);
            _PresupuestoRegional._RegionUser = _funcionarioEntities.idRegion.ToString();
            _PresupuestoRegional._presupuestoRegionalEntities = new presupuestoRegionalEntities();
            _PresupuestoRegional._presupuestoRegionalEntities.lstAnoPresupuesto = presupuestoRegionalEntitiesFactory.getlistAñoPresupuesto();

            //_PresupuestoRegional._regionEntities = RegionesEntitiesFactory.getListRegiones();


            if ((_PresupuestoRegional._RegionUser != null) && (usuarioFull == false))
            {            
                    codigoRegion = Int32.Parse(_PresupuestoRegional._RegionUser);
                    _PresupuestoRegional._regionUserEntities = RegionesEntitiesFactory.GetRegion(codigoRegion);
                    _PresupuestoRegional._regionEntities = RegionesEntitiesFactory.getListRegiones();
                _PresupuestoRegional._regionPresupuestoEntities = RegionesEntitiesFactory.getListRegionesPresupuesto();
                accion = 6;
             }
            else
            {
                _PresupuestoRegional._regionEntities = RegionesEntitiesFactory.getListRegiones();
                _PresupuestoRegional._regionPresupuestoEntities = RegionesEntitiesFactory.getListRegionesPresupuesto();
            }

            RegionesEntities _regionesEntities = new RegionesEntities();

            _regionesEntities.idRegion = 17;
            _regionesEntities.nombreRegion = "Total presupuesto";
            _PresupuestoRegional._regionEntities.Add(_regionesEntities);

            _PresupuestoRegional._obtienePresupuestoRegionalEntities = presupuestoRegionalEntitiesFactory.obtienePresupuestoRegional(accion, anno, numeroResolucion, fechaResolucion, codigoRegion);

            if(_PresupuestoRegional._obtienePresupuestoRegionalEntities.Count >0)
            {
                foreach (var item in _PresupuestoRegional._obtienePresupuestoRegionalEntities)
                {
                    
                    _PresupuestoRegional._presupuestoRegionalEntities.idPresupuestoRegionalConsulta =  item.idResolucionPresupuestaria.ToString();
                    _PresupuestoRegional._presupuestoRegionalEntities.annoPresupuesto= item.annoPresupuesto;
                }
            }
            

            return _PresupuestoRegional;
        }

        public static object obtieneNumeroResolucion(int numeroResolucion, int anno)
        {
            PresupuestoRegional _PresupuestoRegional = new PresupuestoRegional();
            _PresupuestoRegional._presupuestoRegionalEntities = presupuestoRegionalEntitiesFactory.obtieneResolucion(numeroResolucion, anno);

            return _PresupuestoRegional;
        }

        //public static object modificaTipoContratoPPPF(int idContrato, int idTipoContrato, string usuario)
        //{
        //    ConsultaContratoPPPF _ConsultaContratoPPPF = new ConsultaContratoPPPF();
        //    _ConsultaContratoPPPF._informacionContratoEntities = informacionContratoEntitiesFactory.getModificaContratoPPPFEntities(idContrato, idTipoContrato, usuario);
        //    _ConsultaContratoPPPF._auxinformacionContratoEntities = informacionContratoEntitiesFactory.getConsultaContratoPPPFRutEntities(5, idContrato, string.Empty, string.Empty);
        //    return _ConsultaContratoPPPF;
        //}


        //public static object getProyectoPPPFCertificado(string certificadoProyecto)
        //{
        //    ConsultaProyectoPPPF _ConsultaProyectoPPPF = new ConsultaProyectoPPPF();
        //    _ConsultaProyectoPPPF._informacionProyectoEntities = informacionProyectoEntitiesFactory.getinformacionProyectoPPPFCertEntities(certificadoProyecto);
        //    _ConsultaProyectoPPPF._informacionProyectoRukanEntities = informacionProyectoEntitiesFactory.getinformacionProyectoPPPFCertRukanEntities(certificadoProyecto);

        //    return _ConsultaProyectoPPPF;

        //}

        //public static object deleteProyectoPPPF(long idProyecto)
        //{
        //    ConsultaProyectoPPPF _ConsultaProyectoPPPF = new ConsultaProyectoPPPF();
        //    _ConsultaProyectoPPPF._informacionProyectoEntities = informacionProyectoEntitiesFactory.deleteProyectoPPPFEntities(idProyecto);
        //    return _ConsultaProyectoPPPF;
        //}

        //public static object changeCertificadoProyectoPPPF(string idProyecto, string numCertificado)
        //{
        //    ConsultaProyectoPPPF _ConsultaProyectoPPPF = new ConsultaProyectoPPPF();
        //    _ConsultaProyectoPPPF._informacionProyectoEntities = informacionProyectoEntitiesFactory.changeProyectoPPPFEntities(idProyecto, numCertificado);
        //    return _ConsultaProyectoPPPF;
        //}
    }
}

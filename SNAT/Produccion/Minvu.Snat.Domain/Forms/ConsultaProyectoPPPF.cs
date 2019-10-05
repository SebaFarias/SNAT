using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.Domain.Entities;
using Minvu.Snat.Domain.Controls;

namespace Minvu.Snat.Domain.Forms
{
    public class ConsultaProyectoPPPF
    {
        public ConsultaProyectoPPPF() { }
        public informacionProyectoEntities _informacionProyectoEntities { get; set; }
        public List<informacionProyectoEntities> _informacionProyectoCertEntities { get; set; }
        public informacionProyectoEntities _informacionProyectoRukanEntities { get; set; }
        public List<informacionProyectoGrillaEntities> _auxinformacionProyectoEntities { get; set; }
        public List<informacionProyectoElimGrillaEntities> _auxinformacionProyectoElimEntities { get; set; }

    }

    public class ConsultaProyectoPPPFFactory
    {
        public static object getProyectoPPPFRut(string rutProyecto)
        {
            ConsultaProyectoPPPF _ConsultaProyectoPPPF = new ConsultaProyectoPPPF();
            _ConsultaProyectoPPPF._informacionProyectoCertEntities = informacionProyectoEntitiesFactory.getinformacionProyectoPPPFRutEntities(rutProyecto);
            _ConsultaProyectoPPPF._informacionProyectoRukanEntities = informacionProyectoEntitiesFactory.getinformacionProyectoPPPFRutRukanEntities(rutProyecto);

            return _ConsultaProyectoPPPF;

        }

        public static object getProyectoPPPFRutCertificado(int accion, string rut, string certificadoProyecto)
        {
            string[] rutCertificado = rut.Split('-');
            string rutRukan = rutCertificado[0];

            ConsultaProyectoPPPF _ConsultaProyectoPPPF = new ConsultaProyectoPPPF();
            _ConsultaProyectoPPPF._informacionProyectoCertEntities = informacionProyectoEntitiesFactory.getinformacionProyectoPPPFRutEntities(rut);
            _ConsultaProyectoPPPF._informacionProyectoRukanEntities = informacionProyectoEntitiesFactory.getinformacionProyectoPPPFCertRukanEntities(accion, rutRukan, certificadoProyecto);

            return _ConsultaProyectoPPPF;

        }

        public static object getConsultaProyectoPPPF(int accion, int codProyecto, string rutProyecto, string nombreProyecto)
        {
            ConsultaProyectoPPPF _ConsultaProyectoPPPF = new ConsultaProyectoPPPF();
            //var _consulta = informacionProyectoEntitiesFactory.getConsultaProyectoPPPFEntities(accion, codProyecto, rutProyecto, nombreProyecto);
            _ConsultaProyectoPPPF._auxinformacionProyectoEntities = informacionProyectoEntitiesFactory.getConsultaProyectoPPPFEntities(accion, codProyecto, rutProyecto, nombreProyecto);

            return _ConsultaProyectoPPPF;

        }
        //public static object getConsultaProyectoServiuPPPF(int accion, int codProyecto, string rutProyecto, string nombreProyecto)
        //{
        //    ConsultaProyectoPPPF _ConsultaProyectoPPPF = new ConsultaProyectoPPPF();
        //    //var _consulta = informacionProyectoEntitiesFactory.getConsultaProyectoPPPFEntities(accion, codProyecto, rutProyecto, nombreProyecto);
        //    _ConsultaProyectoPPPF._auxinformacionProyectoEntities = informacionProyectoEntitiesFactory.getConsultaProyectoServiuPPPFEntities(accion, codProyecto, rutProyecto, nombreProyecto);

        //    return _ConsultaProyectoPPPF;

        //}

        public static List<informacionProyectoGrillaEntities> getConsultaProyectoTipoPPPF(int accion, int codProyecto, string rutProyecto, string nombreProyecto)
        {
            ConsultaProyectoPPPF _ConsultaProyectoPPPF = new ConsultaProyectoPPPF();
            //var _consulta = informacionProyectoEntitiesFactory.getConsultaProyectoPPPFEntities(accion, codProyecto, rutProyecto, nombreProyecto);
            _ConsultaProyectoPPPF._auxinformacionProyectoEntities = informacionProyectoEntitiesFactory.getConsultaProyectoPPPFEntities(accion, codProyecto, rutProyecto, nombreProyecto);

            return _ConsultaProyectoPPPF._auxinformacionProyectoEntities;

        }

        public static List<informacionProyectoGrillaEntities> getConsultaProyectoTipoServiuPPPF(int accion, int codProyecto, string rutProyecto, string nombreProyecto)
        {
            ConsultaProyectoPPPF _ConsultaProyectoPPPF = new ConsultaProyectoPPPF();
            //var _consulta = informacionProyectoEntitiesFactory.getConsultaProyectoPPPFEntities(accion, codProyecto, rutProyecto, nombreProyecto);
            _ConsultaProyectoPPPF._auxinformacionProyectoEntities = informacionProyectoEntitiesFactory.getConsultaProyectoServiuPPPFEntities(accion, codProyecto, rutProyecto, nombreProyecto);

            return _ConsultaProyectoPPPF._auxinformacionProyectoEntities;

        }

        public static List<informacionProyectoElimGrillaEntities> getConsultaProyectoTipoElimPPPF(int accion, int codProyecto, string rutProyecto, string nombreProyecto)
        {
            ConsultaProyectoPPPF _ConsultaProyectoPPPF = new ConsultaProyectoPPPF();
            _ConsultaProyectoPPPF._auxinformacionProyectoElimEntities = informacionProyectoEntitiesFactory.getConsultaProyectoPPPFElimEntities(accion, codProyecto, rutProyecto, nombreProyecto);

            return _ConsultaProyectoPPPF._auxinformacionProyectoElimEntities;

        }

        public static object getProyectoPPPFCertificado(int accion, string rut, string certificadoProyecto)
        {
            ConsultaProyectoPPPF _ConsultaProyectoPPPF = new ConsultaProyectoPPPF();
            _ConsultaProyectoPPPF._informacionProyectoRukanEntities = informacionProyectoEntitiesFactory.getinformacionProyectoPPPFCertRukanEntities(accion, rut, certificadoProyecto);
            _ConsultaProyectoPPPF._informacionProyectoCertEntities = informacionProyectoEntitiesFactory.getinformacionProyectoPPPFCertEntities(certificadoProyecto);
            
            return _ConsultaProyectoPPPF;

        }

        public static object getProyectoPPPFCertificadoTodos(int accion, string rut, string certificadoProyecto)
        {
            string[] codigo = rut.Split('-');
            string rutRukan = codigo[0];

            ConsultaProyectoPPPF _ConsultaProyectoPPPF = new ConsultaProyectoPPPF();
            _ConsultaProyectoPPPF._informacionProyectoRukanEntities = informacionProyectoEntitiesFactory.getinformacionProyectoPPPFCertRukanEntities(accion, rutRukan, certificadoProyecto);
            _ConsultaProyectoPPPF._informacionProyectoCertEntities = informacionProyectoEntitiesFactory.getinformacionProyectoPPPFRutTodosEntities(rut, certificadoProyecto);

            return _ConsultaProyectoPPPF;

        }

        public static object deleteProyectoPPPF(string codProyecto, int idContrato, string usuario)
        {
            ConsultaProyectoPPPF _ConsultaProyectoPPPF = new ConsultaProyectoPPPF();
            _ConsultaProyectoPPPF._informacionProyectoEntities = informacionProyectoEntitiesFactory.deleteProyectoPPPFEntities(codProyecto, idContrato, usuario);
            _ConsultaProyectoPPPF._auxinformacionProyectoEntities = informacionProyectoEntitiesFactory.getConsultaProyectoPPPFEntities(5, idContrato, string.Empty, string.Empty);
            //auxConsultaInformacionProyecto = ConsultaProyectoPPPFFactory.getConsultaProyectoTipoElimPPPF(5, Int32.Parse(idContrato), string.Empty, string.Empty);
            return _ConsultaProyectoPPPF;
        }

        public static object changeCertificadoProyectoPPPF(string idProyecto, string numCertificado, string rut, string rutRukan, string usuario)
        {
            ConsultaProyectoPPPF _ConsultaProyectoPPPF = new ConsultaProyectoPPPF();
            _ConsultaProyectoPPPF._informacionProyectoEntities = informacionProyectoEntitiesFactory.changeProyectoPPPFEntities(idProyecto, numCertificado, usuario);
            _ConsultaProyectoPPPF._informacionProyectoRukanEntities = _ConsultaProyectoPPPF._informacionProyectoEntities;
            if (_ConsultaProyectoPPPF._informacionProyectoRukanEntities.codigoSalida == "0")
            {
                _ConsultaProyectoPPPF._informacionProyectoRukanEntities = informacionProyectoEntitiesFactory.getinformacionProyectoPPPFCertRukanEntities(1, rutRukan, numCertificado);
                _ConsultaProyectoPPPF._informacionProyectoCertEntities = informacionProyectoEntitiesFactory.getinformacionProyectoPPPFRutEntities(rut);
                _ConsultaProyectoPPPF._informacionProyectoRukanEntities.codigoSalida = "0";
            }
            return _ConsultaProyectoPPPF;
        }

        public static ConsultaProyectoPPPF modificaProfesionalPPPF(int idProyecto, int rutProfesional, int rutProfReemplazo, int tipoContrato, string usuario)
        {
            ConsultaProyectoPPPF _ConsultaProyectoPPPF = new ConsultaProyectoPPPF();
            _ConsultaProyectoPPPF._informacionProyectoEntities = informacionProyectoEntitiesFactory.modificaProfesionalEntities(idProyecto, rutProfesional, rutProfReemplazo, tipoContrato, usuario);
            _ConsultaProyectoPPPF._auxinformacionProyectoEntities = informacionProyectoEntitiesFactory.getConsultaProyectoPPPFEntities(11, idProyecto, string.Empty, usuario);
            return _ConsultaProyectoPPPF;
        }
    }
}

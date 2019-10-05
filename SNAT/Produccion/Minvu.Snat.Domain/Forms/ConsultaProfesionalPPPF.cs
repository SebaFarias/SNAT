using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.Domain.Entities;
using Minvu.Snat.Domain.Controls;

namespace Minvu.Snat.Domain.Forms
{
    public class ConsultaProfesionalPPPF
    {
        public ConsultaProfesionalPPPF() { }
        public informacionProfesionalesEntities _informacionProfesionalEntities { get; set; }
        public List<informacionProfesionalesEntities> _auxinformacionProfesionalEntities { get; set; }
    }

    public class ConsultaProfesionalPPPFFactory
    {
        public static object getProfesionalPPPF(int rutProfesional, char dvProfesional)
        {
            ConsultaProfesionalPPPF _ConsultaProfesionalPPPF = new ConsultaProfesionalPPPF();
            _ConsultaProfesionalPPPF._informacionProfesionalEntities = informacionProfesionalesEntitiesFactory.getProfesional(rutProfesional, dvProfesional);
            if (_ConsultaProfesionalPPPF._informacionProfesionalEntities.codigoSalida == "")
            {
                _ConsultaProfesionalPPPF._informacionProfesionalEntities.codigoSalida = "1";
                _ConsultaProfesionalPPPF._auxinformacionProfesionalEntities = informacionProfesionalesEntitiesFactory.getProfesiones();
            }

            return _ConsultaProfesionalPPPF;
        }



        public static object getProfesionalPPPFPorRut(int rutProfesional,char dvProfesional)
        {
            ConsultaProfesionalPPPF _ConsultaProfesionalPPPF = new ConsultaProfesionalPPPF();
            _ConsultaProfesionalPPPF._informacionProfesionalEntities = informacionProfesionalesEntitiesFactory.getProveedorBd(rutProfesional);
            if (_ConsultaProfesionalPPPF._informacionProfesionalEntities == null)
            {
                return null;
            }
            else
            {
                return _ConsultaProfesionalPPPF;
            }
        }

        public static object getProfesionalRegistroCivil(int rutProfesional, char dvProfesional)
        {
            ConsultaProfesionalPPPF _ConsultaProfesionalPPPF = new ConsultaProfesionalPPPF();
            _ConsultaProfesionalPPPF._informacionProfesionalEntities = informacionProfesionalesEntitiesFactory.getProfesionalRegistroCivil(rutProfesional, dvProfesional);

            return _ConsultaProfesionalPPPF;
        }

        public static object insertaProfesionalPPPF(int rutProfesional, string dvProfesional, string nombreProfesional, string apellidoPaterno, string apellidoMaterno, int idProfesion, int profesionalFTO, string usuario)
        {
            ConsultaProfesionalPPPF _ConsultaProfesionalPPPF = new ConsultaProfesionalPPPF();
            _ConsultaProfesionalPPPF._informacionProfesionalEntities = informacionProfesionalesEntitiesFactory.insertaProfesionales(rutProfesional, dvProfesional, nombreProfesional, apellidoPaterno, apellidoMaterno, idProfesion, profesionalFTO, usuario);

            if (_ConsultaProfesionalPPPF._informacionProfesionalEntities.codigoSalida == "0")
            {
                _ConsultaProfesionalPPPF._informacionProfesionalEntities = informacionProfesionalesEntitiesFactory.getProfesional(rutProfesional, Convert.ToChar(dvProfesional));
                _ConsultaProfesionalPPPF._informacionProfesionalEntities.codigoSalida = "0";
                _ConsultaProfesionalPPPF._auxinformacionProfesionalEntities = informacionProfesionalesEntitiesFactory.getProfesiones();
            }
            return _ConsultaProfesionalPPPF;
        }
    }
}

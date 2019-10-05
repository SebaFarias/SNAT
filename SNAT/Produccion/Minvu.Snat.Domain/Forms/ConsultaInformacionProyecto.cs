using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.Domain.Entities;
using Minvu.Snat.Domain.Controls;


namespace Minvu.Snat.Domain.Forms
{
    public class ConsultaInformacionProyecto
    {
        public ConsultaInformacionProyecto() { }
        public informacionProyectoEntities _informacionProyectoEntities { get; set; }
    }

    public class ConsultaInformacionProyectoFactory
    {

        public static object getProyecto(string codProyecto)
        {
            ConsultaInformacionProyecto _ConsultaInformacionProyecto = new ConsultaInformacionProyecto();
            _ConsultaInformacionProyecto._informacionProyectoEntities = informacionProyectoEntitiesFactory.getinformacionProyectoEntities(codProyecto);

            return _ConsultaInformacionProyecto;
        }

        public static void DeleteProyecto(long codProyecto)
        {
            ConsultaInformacionProyecto _ConsultaInformacionProyecto = new ConsultaInformacionProyecto();
            _ConsultaInformacionProyecto._informacionProyectoEntities = informacionProyectoEntitiesFactory.deleteProyectoEntities(codProyecto);

        }

    }
}

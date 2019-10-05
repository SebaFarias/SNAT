using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;

namespace Minvu.Snat.Domain.Entities
{
    public class maestroResolucionEntities
    {
        public long idMaestroResolucion { get; set; }
        public long idMaestroPrograma { get; set; }

        [Display(Name = "Resolución AT:")]
        //[Required(ErrorMessage = "La resoluci es obligatoria")]
        //[RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string nombreMaestroResolucion { get; set; }
        [Display(Name = "Fecha resolución AT:")]
        public string fechaMaestroResolucion{ get; set; }
        public bool estadoMaestroResolucion { get; set; }

        public maestroResolucionEntities()
        {
            idMaestroResolucion = 0;
            idMaestroPrograma = 0;
            nombreMaestroResolucion = string.Empty;
            estadoMaestroResolucion = false;
            fechaMaestroResolucion = string.Empty;
        }
        public maestroResolucionEntities(long _idMaestroResolucion, string _nombreMaestroResolucion, bool _estadoMaestroModalidad,string _fechaMaestroResolucion, long _idMaestroPrograma)
        {
            idMaestroPrograma = _idMaestroPrograma;
            idMaestroResolucion = _idMaestroResolucion;
            nombreMaestroResolucion = _nombreMaestroResolucion;
            estadoMaestroResolucion = _estadoMaestroModalidad;
            fechaMaestroResolucion = _fechaMaestroResolucion;
        }
    }
    public class maestroResolucionEntitiesFactory
    {

        internal static maestroResolucionEntities getMaestroResolucion(long idResolucion)
        {
            var _maestroResolucionDAO = maestroResolucionDAO.GetporPograma(idResolucion);
            if (_maestroResolucionDAO != null)
            {
                return new maestroResolucionEntities
                {

                    idMaestroResolucion = Convert.ToInt64(_maestroResolucionDAO.IDMAESTRORESOLUCION),
                    idMaestroPrograma = Convert.ToInt64(_maestroResolucionDAO.IDMAESTROPROGRAMA),
                    nombreMaestroResolucion = _maestroResolucionDAO.NOMBREMAESTRORESOLUCION,
                    estadoMaestroResolucion = Convert.ToBoolean(_maestroResolucionDAO.ESTADOMAESTRORESOLUCION),
                    fechaMaestroResolucion = Convert.ToDateTime(_maestroResolucionDAO.FECHAMAESTRORESOLUCION).ToShortDateString()

            };
            }
            else
                return null;
        }
        internal static maestroResolucionEntities getMaestroResolucionPorPrograma(long idPrograma)
        {
            var _maestroResolucionDAO = maestroResolucionDAO.Get(idPrograma);
            if (_maestroResolucionDAO != null)
            {
                return new maestroResolucionEntities
                {

                    idMaestroResolucion = Convert.ToInt64(_maestroResolucionDAO.IDMAESTRORESOLUCION),
                    idMaestroPrograma = Convert.ToInt64(_maestroResolucionDAO.IDMAESTROPROGRAMA),
                    nombreMaestroResolucion = _maestroResolucionDAO.NOMBREMAESTRORESOLUCION,
                    estadoMaestroResolucion = Convert.ToBoolean(_maestroResolucionDAO.ESTADOMAESTRORESOLUCION),
                    fechaMaestroResolucion = Convert.ToDateTime(_maestroResolucionDAO.FECHAMAESTRORESOLUCION).ToShortDateString()

                };
            }
            else
                return null;
        }

    }
}

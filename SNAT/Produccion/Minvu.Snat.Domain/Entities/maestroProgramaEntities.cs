using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.DAO;
using System.ComponentModel.DataAnnotations;


namespace Minvu.Snat.Domain.Entities
{
    public class maestroProgramaEntities
    {
        [Required(ErrorMessage = "El nombre del programa es obligatorio")]
        public long idMaestroPrograma { get; set; }

        [Display(Name = "Programa:")]
        [Required(ErrorMessage = "El nombre del programa es obligatorio")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string nombreMaestroPrograma { get; set; }
        public bool estadoMaestroPrograma { get; set; }

        public maestroProgramaEntities()
        {
            idMaestroPrograma = 0;
            nombreMaestroPrograma = string.Empty;
            estadoMaestroPrograma = false;
        }
        public maestroProgramaEntities(long _idMaestroPrograma, string _nombreMaestroPrograma, bool _estadoMaestroPrograma)
        {
            idMaestroPrograma = _idMaestroPrograma;
            nombreMaestroPrograma = _nombreMaestroPrograma;
            estadoMaestroPrograma = _estadoMaestroPrograma;
        }
    }

    public class maestroProgramaEntitiesFactory
    {

        public static maestroProgramaEntities getMaestroPrograma(long idPrograma)
        {
            var _maestroProgramaDAO = maestroProgramaDAO.get(Convert.ToInt32(idPrograma));
            if (_maestroProgramaDAO != null)
            {
                return new maestroProgramaEntities
                {

                    idMaestroPrograma = Convert.ToInt64(_maestroProgramaDAO.IDMAESTROPROGRAMA),
                    nombreMaestroPrograma = _maestroProgramaDAO.NOMBREMAESTROPROGRAMA,
                    estadoMaestroPrograma = Convert.ToBoolean(_maestroProgramaDAO.ESTADOMAESTROPROGRAMA)

                };
            }
            else
                return null;

        }
        internal static List<maestroProgramaEntities> getListMaestroPrograma()
        {
            var _maestroProgramaDAO = maestroProgramaDAO.getList();
            if (_maestroProgramaDAO != null)
            {
                List<maestroProgramaEntities> _listMaestroProgramaEntities = new List<maestroProgramaEntities>();
                foreach (var item in _maestroProgramaDAO)
                {
                    maestroProgramaEntities _maestroProgramaEntities = new maestroProgramaEntities();

                    _maestroProgramaEntities.idMaestroPrograma = Convert.ToInt64(item.IDMAESTROPROGRAMA);
                    _maestroProgramaEntities.nombreMaestroPrograma = item.NOMBREMAESTROPROGRAMA;
                    _maestroProgramaEntities.estadoMaestroPrograma = Convert.ToBoolean(item.ESTADOMAESTROPROGRAMA);


                       _listMaestroProgramaEntities.Add(_maestroProgramaEntities);

                }
                return _listMaestroProgramaEntities;
            }
            else
                return null;


        }

    }
}



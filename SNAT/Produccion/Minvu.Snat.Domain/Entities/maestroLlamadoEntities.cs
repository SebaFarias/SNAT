using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;

namespace Minvu.Snat.Domain.Entities
{
    public class maestroLlamadoEntities
    {

        public long idMaestroLlamado { get; set; }

        [Display(Name = "Llamado:")]
        [Required(ErrorMessage = "El nombre del llamado es obligatorio")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string nombreMaestroLlamado { get; set; }
        public bool estadoMaestroLlamado { get; set; }

        public maestroLlamadoEntities()
        {
            idMaestroLlamado = 0;
            nombreMaestroLlamado = string.Empty;
            estadoMaestroLlamado = false;
        }
        public maestroLlamadoEntities(long _idMaestroLlamado, string _nombreMaestroLlamado, bool _estadoMaestroLlamado)
        {
            idMaestroLlamado = _idMaestroLlamado;
            nombreMaestroLlamado = _nombreMaestroLlamado;
            estadoMaestroLlamado = _estadoMaestroLlamado;
        }
    }

    public class maestroLlamadoEntitiesFactory
    {

        internal static maestroLlamadoEntities getLlamado(long idLlamado)
        {
            var _maestroLlamadoDAO = maestroLlamadoDAO.Get(idLlamado);
            if (_maestroLlamadoDAO != null)
            {
                return new maestroLlamadoEntities
                {
                    idMaestroLlamado = _maestroLlamadoDAO.IDMAESTROLLAMADO,
                    nombreMaestroLlamado = _maestroLlamadoDAO.NOMBREMAESTROLLAMADO,
                    estadoMaestroLlamado = Convert.ToBoolean(_maestroLlamadoDAO.ESTADOMAESTROLLAMADO)

                };
            }
            else
              return null;
            

        }

        internal static List<maestroLlamadoEntities> getListLlamado()
        {
            var _maestroLlamadoDAO = maestroLlamadoDAO.GetList();
            List<maestroLlamadoEntities> _listMaestroLLamadoEntities = new List<maestroLlamadoEntities>();



            if (_maestroLlamadoDAO != null)
            {

                foreach (var item in _maestroLlamadoDAO)
                {
                    maestroLlamadoEntities _maestroLlamadoEntities = new maestroLlamadoEntities();

                    _maestroLlamadoEntities.idMaestroLlamado = item.IDMAESTROLLAMADO;
                    _maestroLlamadoEntities.nombreMaestroLlamado = item.NOMBREMAESTROLLAMADO;
                    _maestroLlamadoEntities.estadoMaestroLlamado = Convert.ToBoolean(item.ESTADOMAESTROLLAMADO);

                    _listMaestroLLamadoEntities.Add(_maestroLlamadoEntities);
                }
                return _listMaestroLLamadoEntities;
            }
            else
                return null;


        }

    }
}

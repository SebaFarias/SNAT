using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;

namespace Minvu.Snat.Domain.Entities
{
    public class maestroTituloEntities
    {
        public long idMaestroTitulo { get; set; }

        [Display(Name = "Título:")]
        [Required(ErrorMessage = "El título es obligatoria")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string nombreMaestroTitulo { get; set; }
        public bool estadoMaestroTitulo { get; set; }

        public maestroTituloEntities()
        {
            idMaestroTitulo = 0;
            nombreMaestroTitulo = string.Empty;
            estadoMaestroTitulo = false;
        }
        public maestroTituloEntities(long _idMaestroTitulo, string _nombreMaestroTitulo, bool _estadoMaestroTitulo)
        {
            
            idMaestroTitulo = _idMaestroTitulo;
            nombreMaestroTitulo = _nombreMaestroTitulo;
            estadoMaestroTitulo = _estadoMaestroTitulo;
        }
    }

    public class maestroTituloEntitiesFactory
    {


        internal static maestroTituloEntities getMaestroTitulo(long idModalidad)
        {
            var _maestroTituloDAO = maestroTituloDAO.Get(idModalidad);
            if (_maestroTituloDAO != null)
            {
                return new maestroTituloEntities
                {
                    idMaestroTitulo = Convert.ToInt64(_maestroTituloDAO.IDMAESTROTITULO),
                    nombreMaestroTitulo = _maestroTituloDAO.NOMBREMAESTROTITULO,
                    estadoMaestroTitulo = Convert.ToBoolean(_maestroTituloDAO.ESTADOMAESTROTITULO)
                };
            }
            else
                return null;
        }

        internal static List<maestroTituloEntities> getListMaestroTitulo()
        {
            var _maestroTituloDAO = maestroTituloDAO.GetList();
            List<maestroTituloEntities> _listmaestroTituloEntities = new List<maestroTituloEntities>();
            if (_maestroTituloDAO != null)
            {
                foreach (var item in _maestroTituloDAO)
                {
                    maestroTituloEntities _maestroTituloEntities = new maestroTituloEntities();
                    _maestroTituloEntities.idMaestroTitulo = Convert.ToInt64(item.IDMAESTROTITULO);
                    _maestroTituloEntities.nombreMaestroTitulo = item.NOMBREMAESTROTITULO;
                    _maestroTituloEntities.estadoMaestroTitulo = Convert.ToBoolean(item.ESTADOMAESTROTITULO);

                    _listmaestroTituloEntities.Add(_maestroTituloEntities);
                }

                return _listmaestroTituloEntities;
            }
            else
                return null;
        }


    }
}

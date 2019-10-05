using Minvu.Snat.IData.DAO;
using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class MaestroBancoEntities
    {
        public long idMaestroBanco { get; set; }

        [Display(Name = "Banco:")]
        public string NombreMaestroBanco { get; set; }
        public bool? EstadoMaestroBanco { get; set; }

        public MaestroBancoEntities()
        {
            idMaestroBanco = 0;
            NombreMaestroBanco = String.Empty;
            EstadoMaestroBanco = null;
        }

        public MaestroBancoEntities(long _idMaestroBanco, string _NombreMaestroBanco, bool? _EstadoMaestroBanco)
        {
            idMaestroBanco = _idMaestroBanco;
            NombreMaestroBanco = _NombreMaestroBanco;
            EstadoMaestroBanco = _EstadoMaestroBanco;
        }
    }

    public class MaestroBancoEntitiesFactory
    {
        public static MaestroBancoEntities getMaestroBanco(long? idMaestroBanco)
        {
            MAESTRO_BANCO _MaestroBanco = MaestroBancoDAO.Get(idMaestroBanco);
            MaestroBancoEntities _MaestroBancoEntities = new MaestroBancoEntities();

            if (_MaestroBanco != null)
            {
                _MaestroBancoEntities.idMaestroBanco = _MaestroBanco.IDMAESTROBANCO;
                _MaestroBancoEntities.NombreMaestroBanco = _MaestroBanco.NOMBREMAESTROBANCO;
                _MaestroBancoEntities.EstadoMaestroBanco = _MaestroBanco.ESTADOMAESTROBANCO;
            }

            return _MaestroBancoEntities;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.DAO;
using System.ComponentModel.DataAnnotations;


namespace Minvu.Snat.Domain.Entities
{
    public class maestroTipoPagoEntities
    {
        [Required(ErrorMessage = "El campo tipo de pago es obligatorio")]
        public long idMaestroTipoPago { get; set; }

        [Display(Name = "Tipo Pago:")]
        [Required(ErrorMessage = "El nombre del programa es obligatorio")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string nombreMaestroTipoPago { get; set; }
        public bool estadoMaestroTipoPago { get; set; }

        public maestroTipoPagoEntities()
        {
            idMaestroTipoPago = 0;
            nombreMaestroTipoPago = string.Empty;
            estadoMaestroTipoPago = false;
        }
        public maestroTipoPagoEntities(long _idMaestroTipoPago, string _nombreMaestroTipoPago, bool _estadoMaestroTipoPago)
        {
            idMaestroTipoPago = _idMaestroTipoPago;
            nombreMaestroTipoPago = _nombreMaestroTipoPago;
            estadoMaestroTipoPago = _estadoMaestroTipoPago;
        }
    }

    public class maestroTipoPagoEntitiesFactory
    {

        internal static maestroTipoPagoEntities getMaestroTipoPago(long idTipoPago)
        {
            var _maestroTipoPagoDAO = maestroTipoPagoDAO.get(Convert.ToInt32(idTipoPago));
            if (_maestroTipoPagoDAO != null)
            {
                return new maestroTipoPagoEntities
                {

                    idMaestroTipoPago = Convert.ToInt64(_maestroTipoPagoDAO.IDMAESTROTIPOPAGO),
                    nombreMaestroTipoPago = _maestroTipoPagoDAO.NOMBREMAESTROTIPOPAGO,
                    estadoMaestroTipoPago = Convert.ToBoolean(_maestroTipoPagoDAO.ESTADOMAESTROTIPOPAGO),

                };
            }
            else
                return null;


        }
        internal static List<maestroTipoPagoEntities> getListMaestroTipoPago()
        {
            var _maestroTipoPagoDAO = maestroTipoPagoDAO.getList();
            if (_maestroTipoPagoDAO != null)
            {
                List<maestroTipoPagoEntities> _listMaestroTipoPagoEntities = new List<maestroTipoPagoEntities>();
                foreach (var item in _maestroTipoPagoDAO)
                {
                    maestroTipoPagoEntities _maestroProgramaEntities = new maestroTipoPagoEntities();

                    _maestroProgramaEntities.idMaestroTipoPago = Convert.ToInt64(item.IDMAESTROTIPOPAGO);
                    _maestroProgramaEntities.nombreMaestroTipoPago = item.NOMBREMAESTROTIPOPAGO;
                    _maestroProgramaEntities.estadoMaestroTipoPago = Convert.ToBoolean(item.ESTADOMAESTROTIPOPAGO);


                       _listMaestroTipoPagoEntities.Add(_maestroProgramaEntities);

                }
                return _listMaestroTipoPagoEntities;
            }
            else
                return null;


        }

    }
}



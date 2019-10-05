using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
 public class maestroTipoPagoDAO
    {
        public static int Save(MAESTRO_TIPO_PAGO _maestroTipoPago)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_TIPO_PAGO _mas = new MAESTRO_TIPO_PAGO();
                try
                {
                    _mas = contexto.MAESTRO_TIPO_PAGO.Where(c => c.IDMAESTROTIPOPAGO == _maestroTipoPago.IDMAESTROTIPOPAGO).FirstOrDefault<MAESTRO_TIPO_PAGO>();

                    if (_mas == null)
                    {
                        _mas = _maestroTipoPago;
                        contexto.MAESTRO_TIPO_PAGO.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.ESTADOMAESTROTIPOPAGO = _maestroTipoPago.ESTADOMAESTROTIPOPAGO;
                        _mas.IDMAESTROTIPOPAGO = _maestroTipoPago.IDMAESTROTIPOPAGO;
                        _mas.NOMBREMAESTROTIPOPAGO = _maestroTipoPago.NOMBREMAESTROTIPOPAGO;


                    }

                    contexto.SaveChanges();
                    return (int)_mas.IDMAESTROTIPOPAGO;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }



        public static MAESTRO_TIPO_PAGO get(int idMaestroTipoPago)
        {

            MAESTRO_TIPO_PAGO _mae = new MAESTRO_TIPO_PAGO();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroPrograma = from a in contexto.MAESTRO_TIPO_PAGO
                                       where a.IDMAESTROTIPOPAGO == idMaestroTipoPago
                                          select a;
                foreach (var a in qMaestroPrograma)
                {

                    _mae.ESTADOMAESTROTIPOPAGO = a.ESTADOMAESTROTIPOPAGO;
                    _mae.IDMAESTROTIPOPAGO = a.IDMAESTROTIPOPAGO;
                    _mae.NOMBREMAESTROTIPOPAGO = a.NOMBREMAESTROTIPOPAGO;
                    
                    
                }

                return _mae;
            }

        }

        

        public static List<MAESTRO_TIPO_PAGO> getList()
        {

            List<MAESTRO_TIPO_PAGO> ListaMaestroTipoPago = new List<MAESTRO_TIPO_PAGO>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qMaestroPrograma = from a in contexto.MAESTRO_TIPO_PAGO
                                       select a;
                foreach (var a in qMaestroPrograma)
                {

                    MAESTRO_TIPO_PAGO _mae = new MAESTRO_TIPO_PAGO();


                    _mae.ESTADOMAESTROTIPOPAGO = a.ESTADOMAESTROTIPOPAGO;
                    _mae.IDMAESTROTIPOPAGO = a.IDMAESTROTIPOPAGO;
                    _mae.NOMBREMAESTROTIPOPAGO = a.NOMBREMAESTROTIPOPAGO;
                    
                    

                    ListaMaestroTipoPago.Add(_mae);


                }

                return ListaMaestroTipoPago;
            }

        }


        protected void Delete(MAESTRO_TIPO_PAGO _maestroTipoPago)
        {

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_TIPO_PAGO qMaestroTipoPago = (from c in contexto.MAESTRO_TIPO_PAGO
                                                      where c.IDMAESTROTIPOPAGO == _maestroTipoPago.IDMAESTROTIPOPAGO
                                                      select c).FirstOrDefault();

                contexto.MAESTRO_TIPO_PAGO.Remove(qMaestroTipoPago);
                contexto.SaveChanges();
            }

        }


        protected void ChangeStatus(MAESTRO_TIPO_PAGO _maestroTipoPago)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                MAESTRO_TIPO_PAGO qMaestroTipoPago = (from c in contexto.MAESTRO_TIPO_PAGO
                                                      where c.IDMAESTROTIPOPAGO == _maestroTipoPago.IDMAESTROTIPOPAGO
                                                      select c).FirstOrDefault();


                qMaestroTipoPago.ESTADOMAESTROTIPOPAGO = false;

                contexto.SaveChanges();
            }


        }

    }
}



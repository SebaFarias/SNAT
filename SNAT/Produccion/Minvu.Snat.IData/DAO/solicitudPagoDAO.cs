using Minvu.Snat.IData.ORM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minvu.Snat.IData.DAO
{
    public class solicitudPagoDAO
    {
        public static long Save(SOLICITUD_PAGO _maestroTitulo)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                SOLICITUD_PAGO _mas = new SOLICITUD_PAGO();
                try
                {
                    _mas = contexto.SOLICITUD_PAGO.Where(c => c.IDSOLICITUDPAGO == _maestroTitulo.IDSOLICITUDPAGO).FirstOrDefault<SOLICITUD_PAGO>();

                    if (_mas == null)
                    {
                        _mas = _maestroTitulo;
                        contexto.SOLICITUD_PAGO.Add(_mas);
                    }
                    else
                    {
                        //_aut.IDAUTORIZACION = _autorizacion.IDAUTORIZACION;
                        _mas.FOLIOBOLETAGARANTIASOLICITUDPAGO = _maestroTitulo.FOLIOBOLETAGARANTIASOLICITUDPAGO;
                        _mas.IDMAESTROESTADOSOLICITUD = _maestroTitulo.IDMAESTROESTADOSOLICITUD;
                        _mas.IDSOLICITUDPAGO = _maestroTitulo.IDSOLICITUDPAGO;
                        _mas.NUMEROFAMILIASPAGARSOLICITUDPAGO = _maestroTitulo.NUMEROFAMILIASPAGARSOLICITUDPAGO;
                        _mas.USUARIORESPONSABLESOLICITUDPAGO = _maestroTitulo.USUARIORESPONSABLESOLICITUDPAGO;
                        _mas.MONTOTOTALPROYECTOSOLICITUDPAGO = _maestroTitulo.MONTOTOTALPROYECTOSOLICITUDPAGO;
                        _mas.MONTOPAGADOSOLICITUDPAGO = _maestroTitulo.MONTOPAGADOSOLICITUDPAGO;
                        _mas.MONTOCOMPROMETIDOSOLICITUDPAGO = _maestroTitulo.MONTOCOMPROMETIDOSOLICITUDPAGO;
                        _mas.SALDOPORPAGARSOLICITUDPAGO = _maestroTitulo.SALDOPORPAGARSOLICITUDPAGO;
                        _mas.IDMAESTROTIPODESTINOPAGO = _maestroTitulo.IDMAESTROTIPODESTINOPAGO;
                        _mas.AVANCEOBRASOLICITUDPAGO = _maestroTitulo.AVANCEOBRASOLICITUDPAGO;
                        _mas.NUMERORESOLUCIONCONTRATOSOLICITUDPAGO = _maestroTitulo.NUMERORESOLUCIONCONTRATOSOLICITUDPAGO;
                        _mas.MONTOSOLICITUDSOLICITUDPAGO = _maestroTitulo.MONTOSOLICITUDSOLICITUDPAGO;
                        _mas.IDPROVEEDOR = _maestroTitulo.IDPROVEEDOR;
                        _mas.NUMEROVIVIENDASSOLICITUDPAGO = _maestroTitulo.NUMEROVIVIENDASSOLICITUDPAGO;
                        _mas.IDCARACTERISTICASESPECIALES = _maestroTitulo.IDCARACTERISTICASESPECIALES;
                        _mas.FECHARESOLUCIONSOLICITUDPAGO = _maestroTitulo.FECHARESOLUCIONSOLICITUDPAGO;
                        _mas.FECHABOLETAGARANTIASOLICITUDPAGO= _maestroTitulo.FECHABOLETAGARANTIASOLICITUDPAGO;                        
                        _mas.PAGOMANDANTOSOLICITUDPAGO = _maestroTitulo.PAGOMANDANTOSOLICITUDPAGO;
                        _mas.IDMANDANTOPROVEEDORSOLICITUDPAGO = _maestroTitulo.IDMANDANTOPROVEEDORSOLICITUDPAGO;




                    }

                    contexto.SaveChanges();
                    return (long)_mas.IDSOLICITUDPAGO;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }

        public static SOLICITUD_PAGO Get(long? idSolicitudPago)
        {
            SOLICITUD_PAGO _mae = new SOLICITUD_PAGO();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qSolicitudPago = from a in contexto.SOLICITUD_PAGO
                                     where a.IDSOLICITUDPAGO == idSolicitudPago
                                     select a;
                foreach (var a in qSolicitudPago)
                {
                    _mae.IDSOLICITUDPAGO = a.IDSOLICITUDPAGO;
                    _mae.IDMAESTROESTADOSOLICITUD = a.IDMAESTROESTADOSOLICITUD;
                    _mae.FOLIOBOLETAGARANTIASOLICITUDPAGO = a.FOLIOBOLETAGARANTIASOLICITUDPAGO;
                    _mae.NUMEROFAMILIASPAGARSOLICITUDPAGO = a.NUMEROFAMILIASPAGARSOLICITUDPAGO;
                    _mae.USUARIORESPONSABLESOLICITUDPAGO = a.USUARIORESPONSABLESOLICITUDPAGO;
                    _mae.IDCARACTERISTICASESPECIALES = a.IDCARACTERISTICASESPECIALES;
                    _mae.IDMAESTROTIPOPAGO = a.IDMAESTROTIPOPAGO;
                    _mae.FECHAREALCREACIONSOLICITUDPAGO = a.FECHAREALCREACIONSOLICITUDPAGO;
                    _mae.FECHACREACIONSOLICITUDPAGO = a.FECHACREACIONSOLICITUDPAGO;
                    _mae.IDMAESTROTIPODESTINOPAGO = a.IDMAESTROTIPODESTINOPAGO;
                    _mae.MONTOTOTALPROYECTOSOLICITUDPAGO = a.MONTOTOTALPROYECTOSOLICITUDPAGO;
                    _mae.MONTOPAGADOSOLICITUDPAGO = a.MONTOPAGADOSOLICITUDPAGO;
                    _mae.MONTOCOMPROMETIDOSOLICITUDPAGO = a.MONTOCOMPROMETIDOSOLICITUDPAGO;
                    _mae.SALDOPORPAGARSOLICITUDPAGO = a.SALDOPORPAGARSOLICITUDPAGO;
                    _mae.AVANCEOBRASOLICITUDPAGO = a.AVANCEOBRASOLICITUDPAGO;
                    _mae.NUMERORESOLUCIONCONTRATOSOLICITUDPAGO = a.NUMERORESOLUCIONCONTRATOSOLICITUDPAGO;
                    _mae.MONTOSOLICITUDSOLICITUDPAGO = a.MONTOSOLICITUDSOLICITUDPAGO;
                    _mae.IDPROVEEDOR = a.IDPROVEEDOR;
                    _mae.NUMEROVIVIENDASSOLICITUDPAGO = a.NUMEROVIVIENDASSOLICITUDPAGO;
                    _mae.OBSERVACIONESSOLICITUDPAGO = a.OBSERVACIONESSOLICITUDPAGO;
                    _mae.FECHARESOLUCIONSOLICITUDPAGO = a.FECHARESOLUCIONSOLICITUDPAGO;
                    _mae.FECHABOLETAGARANTIASOLICITUDPAGO = a.FECHABOLETAGARANTIASOLICITUDPAGO;
                    _mae.PAGOMANDANTOSOLICITUDPAGO = a.PAGOMANDANTOSOLICITUDPAGO;
                    _mae.IDMANDANTOPROVEEDORSOLICITUDPAGO = a.IDMANDANTOPROVEEDORSOLICITUDPAGO;

                }

                return _mae;
            }
        }

        public static List<SOLICITUD_PAGO> GetList()
        {
            List<SOLICITUD_PAGO> ListaSolictudPago = new List<SOLICITUD_PAGO>();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qSolicitudPago = from a in contexto.SOLICITUD_PAGO
                                     select a;
                foreach (var a in qSolicitudPago)
                {
                    SOLICITUD_PAGO _mae = new SOLICITUD_PAGO();

                    _mae.FOLIOBOLETAGARANTIASOLICITUDPAGO = a.FOLIOBOLETAGARANTIASOLICITUDPAGO;
                    _mae.IDMAESTROESTADOSOLICITUD = a.IDMAESTROESTADOSOLICITUD;
                    _mae.IDSOLICITUDPAGO = a.IDSOLICITUDPAGO;
                    _mae.NUMEROFAMILIASPAGARSOLICITUDPAGO = a.NUMEROFAMILIASPAGARSOLICITUDPAGO;
                    _mae.USUARIORESPONSABLESOLICITUDPAGO = a.USUARIORESPONSABLESOLICITUDPAGO;
                    _mae.MONTOTOTALPROYECTOSOLICITUDPAGO = a.MONTOTOTALPROYECTOSOLICITUDPAGO;
                    _mae.MONTOPAGADOSOLICITUDPAGO = a.MONTOPAGADOSOLICITUDPAGO;
                    _mae.MONTOCOMPROMETIDOSOLICITUDPAGO = a.MONTOCOMPROMETIDOSOLICITUDPAGO;
                    _mae.SALDOPORPAGARSOLICITUDPAGO = a.SALDOPORPAGARSOLICITUDPAGO;
                    _mae.IDMAESTROTIPODESTINOPAGO = a.IDMAESTROTIPODESTINOPAGO;
                    _mae.AVANCEOBRASOLICITUDPAGO = a.AVANCEOBRASOLICITUDPAGO;
                    _mae.NUMERORESOLUCIONCONTRATOSOLICITUDPAGO = a.NUMERORESOLUCIONCONTRATOSOLICITUDPAGO;
                    _mae.NUMEROVIVIENDASSOLICITUDPAGO = a.NUMEROVIVIENDASSOLICITUDPAGO;
                    _mae.IDCARACTERISTICASESPECIALES = a.IDCARACTERISTICASESPECIALES;
                    _mae.FECHARESOLUCIONSOLICITUDPAGO = a.FECHARESOLUCIONSOLICITUDPAGO;
                    _mae.FECHABOLETAGARANTIASOLICITUDPAGO = a.FECHABOLETAGARANTIASOLICITUDPAGO;
                    _mae.PAGOMANDANTOSOLICITUDPAGO = a.PAGOMANDANTOSOLICITUDPAGO;
                    _mae.IDMANDANTOPROVEEDORSOLICITUDPAGO = a.IDMANDANTOPROVEEDORSOLICITUDPAGO;


                    ListaSolictudPago.Add(_mae);
                }

                return ListaSolictudPago;
            }
        }

        protected void Delete(SOLICITUD_PAGO _solicitudPago)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                SOLICITUD_PAGO qSolcitudPago = (from c in contexto.SOLICITUD_PAGO
                                                where c.IDSOLICITUDPAGO == _solicitudPago.IDSOLICITUDPAGO
                                                select c).FirstOrDefault();

                contexto.SOLICITUD_PAGO.Remove(qSolcitudPago);
                contexto.SaveChanges();
            }
        }

        protected void ChangeStatus(SOLICITUD_PAGO _solicitudPago)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                SOLICITUD_PAGO qSolicitudPago = (from c in contexto.SOLICITUD_PAGO
                                                 where c.IDSOLICITUDPAGO == _solicitudPago.IDSOLICITUDPAGO
                                                 select c).FirstOrDefault();

                qSolicitudPago.IDMAESTROESTADOSOLICITUD = 0;

                contexto.SaveChanges();
            }
        }

        public static SOLICITUD_PAGO getLast(string CodigoProyecto, long idMaestroPrograma)
        {
            SOLICITUD_PAGO objSolicitudPago = new SOLICITUD_PAGO();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qSolicitudPago = (from sp in contexto.SOLICITUD_PAGO
                                      join ce in contexto.CARACTERISTICAS_ESPECIALES on sp.IDCARACTERISTICASESPECIALES equals ce.IDCARACTERISTICASESPECIALES
                                      join ip in contexto.INFORMACION_PROYECTO on ce.IDINFORMACIONPROYECTO equals ip.IDINFORMACIONPROYECTO
                                      where ip.CODIGOPROYECTOINFORMACIONPROYECTO.ToString() == CodigoProyecto
                                      && ip.IDMAESTROPROGRAMA == idMaestroPrograma
                                      && sp.FECHAREALCREACIONSOLICITUDPAGO != null
                                      && sp.IDMAESTROESTADOSOLICITUD != 2 //Rechazada
                                      && sp.IDMAESTROESTADOSOLICITUD != 4 //Eliminada
                                      orderby sp.IDSOLICITUDPAGO descending
                                      select sp).FirstOrDefault();

                if (qSolicitudPago != null)
                {
                    objSolicitudPago.FOLIOBOLETAGARANTIASOLICITUDPAGO = qSolicitudPago.FOLIOBOLETAGARANTIASOLICITUDPAGO;
                    objSolicitudPago.IDMAESTROESTADOSOLICITUD = qSolicitudPago.IDMAESTROESTADOSOLICITUD;
                    objSolicitudPago.IDSOLICITUDPAGO = qSolicitudPago.IDSOLICITUDPAGO;
                    objSolicitudPago.IDMAESTROTIPOPAGO = qSolicitudPago.IDMAESTROTIPOPAGO;
                    objSolicitudPago.FECHACREACIONSOLICITUDPAGO = qSolicitudPago.FECHACREACIONSOLICITUDPAGO;
                    objSolicitudPago.FECHAREALCREACIONSOLICITUDPAGO = qSolicitudPago.FECHAREALCREACIONSOLICITUDPAGO;
                    objSolicitudPago.NUMEROFAMILIASPAGARSOLICITUDPAGO = qSolicitudPago.NUMEROFAMILIASPAGARSOLICITUDPAGO;
                    objSolicitudPago.USUARIORESPONSABLESOLICITUDPAGO = qSolicitudPago.USUARIORESPONSABLESOLICITUDPAGO;
                    objSolicitudPago.MONTOTOTALPROYECTOSOLICITUDPAGO = qSolicitudPago.MONTOTOTALPROYECTOSOLICITUDPAGO;
                    objSolicitudPago.MONTOPAGADOSOLICITUDPAGO = qSolicitudPago.MONTOPAGADOSOLICITUDPAGO;
                    objSolicitudPago.MONTOCOMPROMETIDOSOLICITUDPAGO = qSolicitudPago.MONTOCOMPROMETIDOSOLICITUDPAGO;
                    objSolicitudPago.SALDOPORPAGARSOLICITUDPAGO = qSolicitudPago.SALDOPORPAGARSOLICITUDPAGO;
                    objSolicitudPago.IDMAESTROTIPODESTINOPAGO = qSolicitudPago.IDMAESTROTIPODESTINOPAGO;
                    objSolicitudPago.AVANCEOBRASOLICITUDPAGO = qSolicitudPago.AVANCEOBRASOLICITUDPAGO;
                    objSolicitudPago.NUMERORESOLUCIONCONTRATOSOLICITUDPAGO = qSolicitudPago.NUMERORESOLUCIONCONTRATOSOLICITUDPAGO;
                    objSolicitudPago.MONTOSOLICITUDSOLICITUDPAGO = qSolicitudPago.MONTOSOLICITUDSOLICITUDPAGO;
                    objSolicitudPago.NUMEROVIVIENDASSOLICITUDPAGO = qSolicitudPago.NUMEROVIVIENDASSOLICITUDPAGO;
                    objSolicitudPago.IDCARACTERISTICASESPECIALES = qSolicitudPago.IDCARACTERISTICASESPECIALES;
                    objSolicitudPago.OBSERVACIONESSOLICITUDPAGO = qSolicitudPago.OBSERVACIONESSOLICITUDPAGO;
                    objSolicitudPago.FECHARESOLUCIONSOLICITUDPAGO = qSolicitudPago.FECHARESOLUCIONSOLICITUDPAGO;
                    objSolicitudPago.FECHABOLETAGARANTIASOLICITUDPAGO = qSolicitudPago.FECHABOLETAGARANTIASOLICITUDPAGO;
                    objSolicitudPago.PAGOMANDANTOSOLICITUDPAGO = qSolicitudPago.PAGOMANDANTOSOLICITUDPAGO;
                    objSolicitudPago.IDMANDANTOPROVEEDORSOLICITUDPAGO = qSolicitudPago.IDMANDANTOPROVEEDORSOLICITUDPAGO;

                }

                return objSolicitudPago;
            }
        }

        public static INSERTA_GENERACION_SOLICITUD_PAGO_Result GeneraSolicitudPago(SOLICITUD_PAGO objGeneraSolicitud, long idMaestroTipoProveedor,
                                                                                    string nombreProveedor, int rutProveedor, string dvDigitoprovedor, decimal? avanceObra, string descripcionAvanceObra)
        {
            INSERTA_GENERACION_SOLICITUD_PAGO_Result objSolicitudPago = new INSERTA_GENERACION_SOLICITUD_PAGO_Result();

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var objSolicitud = from a in contexto.INSERTA_GENERACION_SOLICITUD_PAGO(objGeneraSolicitud.IDMAESTROESTADOSOLICITUD, objGeneraSolicitud.FOLIOBOLETAGARANTIASOLICITUDPAGO,
                                                                                        objGeneraSolicitud.NUMEROFAMILIASPAGARSOLICITUDPAGO, objGeneraSolicitud.USUARIORESPONSABLESOLICITUDPAGO,
                                                                                        objGeneraSolicitud.IDCARACTERISTICASESPECIALES, objGeneraSolicitud.IDMAESTROTIPOPAGO,
                                                                                        objGeneraSolicitud.FECHAREALCREACIONSOLICITUDPAGO, objGeneraSolicitud.FECHACREACIONSOLICITUDPAGO,
                                                                                        objGeneraSolicitud.IDMAESTROTIPODESTINOPAGO, objGeneraSolicitud.MONTOTOTALPROYECTOSOLICITUDPAGO,
                                                                                        objGeneraSolicitud.MONTOPAGADOSOLICITUDPAGO, objGeneraSolicitud.MONTOCOMPROMETIDOSOLICITUDPAGO,
                                                                                        objGeneraSolicitud.SALDOPORPAGARSOLICITUDPAGO, objGeneraSolicitud.NUMERORESOLUCIONCONTRATOSOLICITUDPAGO,
                                                                                        objGeneraSolicitud.MONTOSOLICITUDSOLICITUDPAGO, objGeneraSolicitud.IDPROVEEDOR,
                                                                                        objGeneraSolicitud.NUMEROVIVIENDASSOLICITUDPAGO, objGeneraSolicitud.OBSERVACIONESSOLICITUDPAGO,
                                                                                        idMaestroTipoProveedor, nombreProveedor, rutProveedor, dvDigitoprovedor,
                                                                                        avanceObra, descripcionAvanceObra, objGeneraSolicitud.FECHARESOLUCIONSOLICITUDPAGO, objGeneraSolicitud.FECHABOLETAGARANTIASOLICITUDPAGO, objGeneraSolicitud.IDMANDANTOPROVEEDORSOLICITUDPAGO)
                                   select a;

                foreach (var a in objSolicitud)
                {
                    objSolicitudPago.ERR = a.ERR;
                    objSolicitudPago.MSG = a.MSG.ToString();
                    objSolicitudPago.IDSOLICITUDPAGO = a.IDSOLICITUDPAGO;
                }

                return objSolicitudPago;
            }
        }

        public static decimal? GetSolicitudesProyectoMontoPagadoHist(string CodigoProyecto, long? idMaestroPrograma)
        {
            decimal? MontoPagadoHist = 0;

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qSolicitudPago = from s in contexto.SOLICITUD_PAGO
                                     join c in contexto.CARACTERISTICAS_ESPECIALES
                                        on s.IDCARACTERISTICASESPECIALES equals c.IDCARACTERISTICASESPECIALES
                                     join i in contexto.INFORMACION_PROYECTO
                                        on c.IDINFORMACIONPROYECTO equals i.IDINFORMACIONPROYECTO
                                     where i.CODIGOPROYECTOINFORMACIONPROYECTO.ToString() == CodigoProyecto
                                        && i.IDMAESTROPROGRAMA == idMaestroPrograma
                                        && s.IDMAESTROTIPOPAGO == 1 //Historico
                                        && s.IDMAESTROESTADOSOLICITUD != 2 //Rechazada
                                        && s.IDMAESTROESTADOSOLICITUD != 4 //Eliminada
                                     select s;

                foreach (var a in qSolicitudPago)
                {
                    MontoPagadoHist = MontoPagadoHist + (a.MONTOSOLICITUDSOLICITUDPAGO == null ? 0 : a.MONTOSOLICITUDSOLICITUDPAGO);
                }

                return MontoPagadoHist;
            }
        }

        public static decimal? GetSolicitudesProyectoMontoPagadoSIGFE(string CodigoProyecto, long? idMaestroPrograma)
        {
            decimal? MontoPagadoSIGFE = 0;

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qSolicitudPago = from s in contexto.SOLICITUD_PAGO
                                     join c in contexto.CARACTERISTICAS_ESPECIALES
                                         on s.IDCARACTERISTICASESPECIALES equals c.IDCARACTERISTICASESPECIALES
                                     join i in contexto.INFORMACION_PROYECTO
                                         on c.IDINFORMACIONPROYECTO equals i.IDINFORMACIONPROYECTO
                                     join ta in contexto.TIPO_AUTORIZACION
                                         on s.IDSOLICITUDPAGO equals ta.IDSOLICITUDPAGO
                                     join a in contexto.AUTORIZACION
                                         on ta.IDAUTORIZACION equals a.IDAUTORIZACION
                                     where i.CODIGOPROYECTOINFORMACIONPROYECTO.ToString() == CodigoProyecto
                                         && i.IDMAESTROPROGRAMA == idMaestroPrograma
                                         && a.IDMAESTROESTADOAUTORIZACION == 3 //SIGFE
                                         && s.IDMAESTROESTADOSOLICITUD != 2 //Rechazada
                                         && s.IDMAESTROESTADOSOLICITUD != 4 //Eliminada
                                     select s;

                foreach (var a in qSolicitudPago)
                {
                    MontoPagadoSIGFE = MontoPagadoSIGFE + (a.MONTOSOLICITUDSOLICITUDPAGO == null ? 0 : a.MONTOSOLICITUDSOLICITUDPAGO);
                }

                return MontoPagadoSIGFE;
            }
        }

        public static decimal? GetSolicitudesProyectoMontoSolicitudesGeneradas(string CodigoProyecto, long? idMaestroPrograma)
        {
            decimal? MontoComprometidoAutorizacion = 0;

            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                var qSolicitudPago = from s in contexto.SOLICITUD_PAGO
                                     join c in contexto.CARACTERISTICAS_ESPECIALES
                                         on s.IDCARACTERISTICASESPECIALES equals c.IDCARACTERISTICASESPECIALES
                                     join i in contexto.INFORMACION_PROYECTO
                                         on c.IDINFORMACIONPROYECTO equals i.IDINFORMACIONPROYECTO
                                     where i.CODIGOPROYECTOINFORMACIONPROYECTO.ToString() == CodigoProyecto
                                         && i.IDMAESTROPROGRAMA == idMaestroPrograma
                                         && s.IDMAESTROESTADOSOLICITUD != 2 //Rechazada
                                         && s.IDMAESTROESTADOSOLICITUD != 4 //Eliminada
                                     select s;

                foreach (var a in qSolicitudPago)
                {
                    MontoComprometidoAutorizacion = MontoComprometidoAutorizacion + (a.MONTOSOLICITUDSOLICITUDPAGO == null ? 0 : a.MONTOSOLICITUDSOLICITUDPAGO);
                }

                return MontoComprometidoAutorizacion;
            }
        }
    }
}
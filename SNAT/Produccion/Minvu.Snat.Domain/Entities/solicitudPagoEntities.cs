using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.Domain.Entities
{
    public class solicitudPagoEntities
    {
        [Display(Name = "ID solicitud de pago:")]
        public long? idSolicitudPago { get; set; }
        public long? idMaestroEstadoSolicitud { get; set; }
        public long? idCaracteristicasEspeciales { get; set; }
        public long? idTipoPago { get; set; }
        public long? idProveedor { get; set; }
        public long? idMandatoProveedor { get; set; }

        [Display(Name = "Fecha creación solicitud:")]
        [Required(ErrorMessage = "El campo fecha creación solicitud es obligatoria")]

        public string fechaCreacionSolicitud { get; set; }


        [Display(Name = "Monto total proyecto:")]
        [RegularExpression(@"^\d+.\d{0,3}$", ErrorMessage = "El campo es solo numérico no puede tener más de 3 decimales después de la coma")]
        public decimal? montoTotalProyecto { get; set; }

        [Display(Name = "Monto pagado:")]
        [RegularExpression(@"^\d+.\d{0,3}$", ErrorMessage = "El campo es solo numérico no puede tener más de 3 decimales después de la coma")]
        public decimal? montoPagado { get; set; }

        [Display(Name = "Monto solicitud (UF):")]
        public decimal? montoSolicitud { get; set; }
        [Display(Name = "Monto comprometido:")]
        [RegularExpression(@"^\d+.\d{0,3}$", ErrorMessage = "El campo es solo numérico no puede tener más de 3 decimales después de la coma")]
        public decimal? montoComprometido { get; set; }
        [Display(Name = "Saldo por pagar:")]
        [RegularExpression(@"^\d+.\d{0,3}$", ErrorMessage = "El campo es solo numérico no puede tener más de 3 decimales después de la coma")]
        public decimal? SaldoPorPagar { get; set; }


        [Display(Name = "% Avance de obras:")]
        [RegularExpression(@"^\d+.\d{0,3}$", ErrorMessage = "El campo es solo numérico no puede tener más de 3 decimales después de la coma")]
        public decimal? avanceObra { get; set; }

        [Display(Name = "N° Resolución contrato:")]
        public long? resolucionContrato { get; set; }

        [Display(Name = "Fecha Resolución contrato:")]
        public String fechaResolucionContrato { get; set; }

        public long? idMaestroTipoDestinoPago { get; set; }


        [Display(Name = "Folio boleta garantía:")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string folioBoletaGarantiaSolicitudPago { get; set; }
        [Display(Name = "Fecha boleta garantía:")]
        public string fechaBoletaGarantiaSolicitudPago { get; set; }

        public int numeroFamiliasPagadarSolicitudPago { get; set; }

        [Display(Name = "Usuario:")]
        //[Required(ErrorMessage = "La clase es obligatorio")]
        [RegularExpression(@"(^[A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string usuariosReponsableSolicitudPago { get; set; }
        public int numeroViviendasSolicitudPago { get; set; }
        public string mensajeSalida { get; set; }
        public string codigoSalida { get; set; }
        public string Observaciones { get; set; }
        public bool? pagoMandato { get; set; }

        public solicitudPagoEntities()
        {
            idSolicitudPago = 0;
            idMaestroEstadoSolicitud = 0;
            idCaracteristicasEspeciales = 0;
            montoTotalProyecto = 0;
            montoPagado = 0;
            montoComprometido = 0;
            SaldoPorPagar = 0;
            idMaestroTipoDestinoPago = 0;
            folioBoletaGarantiaSolicitudPago = string.Empty;
            numeroFamiliasPagadarSolicitudPago = 0;
            usuariosReponsableSolicitudPago = string.Empty;
            fechaCreacionSolicitud = string.Empty;
            avanceObra = null;
            resolucionContrato = 0;
            montoSolicitud = 0;
            idProveedor = null;
            numeroViviendasSolicitudPago = 0;
            mensajeSalida = String.Empty;
            codigoSalida = String.Empty;
            Observaciones = String.Empty;
            fechaResolucionContrato = null;
            fechaBoletaGarantiaSolicitudPago = string.Empty;
            idMandatoProveedor = null;
        }
        public solicitudPagoEntities(int _idSolicitudPago, int _idMaestroEstadoSolicitud, long? _idCaracteristicasEspeciales, decimal _montoSolicitudPago,
                                    string _folioBoletaGarantiaSolicitudPago, int _numeroFamiliasPagadarSolicitudPago, string _usuariosReponsableSolicitudPago,
                                    string _fechaCreacionSolicitud, decimal? _montoTotalProyecto, decimal? _montoPagado, decimal? _montoComprometido,
                                    decimal? _SaldoPorPagar, long? _idMaestroTipoDestinoPago, decimal? _avanceObra, long? _resolucionContrato,
                                    decimal? _montoSolicitud, long? _idProveedor, int _numeroViviendasSolicitudPago,
                                    string _mensajeSalida, string _codigoSalida, string _Observaciones, String _fechaResolucionContrato, string _fechaBoletaGarantiaSolicitudPago,
                                    bool? _pagoMandato, long? _idMandatoProveedor)
        {
            idSolicitudPago = _idSolicitudPago;
            idMaestroEstadoSolicitud = _idMaestroEstadoSolicitud;
            idCaracteristicasEspeciales = _idCaracteristicasEspeciales;
            folioBoletaGarantiaSolicitudPago = _folioBoletaGarantiaSolicitudPago;
            numeroFamiliasPagadarSolicitudPago = _numeroFamiliasPagadarSolicitudPago;
            usuariosReponsableSolicitudPago = _usuariosReponsableSolicitudPago;
            fechaCreacionSolicitud = _fechaCreacionSolicitud;
            montoTotalProyecto = _montoTotalProyecto;
            montoPagado = _montoPagado;
            montoComprometido = _montoComprometido;
            SaldoPorPagar = _SaldoPorPagar;
            idMaestroTipoDestinoPago = _idMaestroTipoDestinoPago;
            avanceObra = _avanceObra;
            resolucionContrato = _resolucionContrato;
            montoSolicitud = _montoSolicitud;
            idProveedor = _idProveedor;
            numeroViviendasSolicitudPago = _numeroViviendasSolicitudPago;
            mensajeSalida = _mensajeSalida;
            codigoSalida = _codigoSalida;
            Observaciones = _Observaciones;
            fechaResolucionContrato = _fechaResolucionContrato;
            fechaBoletaGarantiaSolicitudPago = _fechaBoletaGarantiaSolicitudPago;
            pagoMandato = _pagoMandato;
            idMandatoProveedor = _idMandatoProveedor;
        }
    }

    public class solicitudPagoEntitiesFactory
    {
        public static solicitudPagoEntities GeneraSolicitudPago(solicitudPagoEntities _solicitudPagoEntities, proveedorEntities objProveedorInsert, decimal? avanceObra, string descripcionAvanceObra)//saveSolicitudPago
        {
            SOLICITUD_PAGO _solicitudPago = new SOLICITUD_PAGO();

            _solicitudPago.IDMAESTROESTADOSOLICITUD = _solicitudPagoEntities.idMaestroEstadoSolicitud;
            if (_solicitudPagoEntities.fechaResolucionContrato != string.Empty)
                _solicitudPago.FECHARESOLUCIONSOLICITUDPAGO = Convert.ToDateTime(_solicitudPagoEntities.fechaResolucionContrato);
            else
                _solicitudPago.FECHARESOLUCIONSOLICITUDPAGO = null;

            _solicitudPago.FOLIOBOLETAGARANTIASOLICITUDPAGO = _solicitudPagoEntities.folioBoletaGarantiaSolicitudPago;
            _solicitudPago.NUMEROFAMILIASPAGARSOLICITUDPAGO = _solicitudPagoEntities.numeroFamiliasPagadarSolicitudPago;
            _solicitudPago.USUARIORESPONSABLESOLICITUDPAGO = _solicitudPagoEntities.usuariosReponsableSolicitudPago;
            _solicitudPago.IDCARACTERISTICASESPECIALES = _solicitudPagoEntities.idCaracteristicasEspeciales;
            _solicitudPago.IDMAESTROTIPOPAGO = _solicitudPagoEntities.idTipoPago;
            _solicitudPago.FECHAREALCREACIONSOLICITUDPAGO = DateTime.Today;
            _solicitudPago.FECHACREACIONSOLICITUDPAGO = DateTime.Now;
            _solicitudPago.IDMAESTROTIPODESTINOPAGO = _solicitudPagoEntities.idMaestroTipoDestinoPago;
            _solicitudPago.MONTOTOTALPROYECTOSOLICITUDPAGO = _solicitudPagoEntities.montoTotalProyecto;
            _solicitudPago.MONTOPAGADOSOLICITUDPAGO = _solicitudPagoEntities.montoPagado;
            _solicitudPago.MONTOCOMPROMETIDOSOLICITUDPAGO = _solicitudPagoEntities.montoComprometido;
            _solicitudPago.SALDOPORPAGARSOLICITUDPAGO = _solicitudPagoEntities.SaldoPorPagar;
            _solicitudPago.AVANCEOBRASOLICITUDPAGO = _solicitudPagoEntities.avanceObra;
            _solicitudPago.NUMERORESOLUCIONCONTRATOSOLICITUDPAGO = _solicitudPagoEntities.resolucionContrato;
            _solicitudPago.MONTOSOLICITUDSOLICITUDPAGO = _solicitudPagoEntities.montoSolicitud;
            _solicitudPago.IDPROVEEDOR = _solicitudPagoEntities.idProveedor;
            _solicitudPago.NUMEROVIVIENDASSOLICITUDPAGO = _solicitudPagoEntities.numeroViviendasSolicitudPago;
            _solicitudPago.OBSERVACIONESSOLICITUDPAGO = _solicitudPagoEntities.Observaciones;
            _solicitudPago.PAGOMANDANTOSOLICITUDPAGO = _solicitudPagoEntities.pagoMandato;
            if (_solicitudPagoEntities.idMandatoProveedor != null)
                _solicitudPago.IDMANDANTOPROVEEDORSOLICITUDPAGO = _solicitudPagoEntities.idMandatoProveedor;
            else
                _solicitudPago.IDMANDANTOPROVEEDORSOLICITUDPAGO = null;
            if (_solicitudPagoEntities.fechaBoletaGarantiaSolicitudPago != string.Empty)
                _solicitudPago.FECHABOLETAGARANTIASOLICITUDPAGO = Convert.ToDateTime(_solicitudPagoEntities.fechaBoletaGarantiaSolicitudPago);
            else
                _solicitudPago.FECHABOLETAGARANTIASOLICITUDPAGO = null;

            var objResultado = solicitudPagoDAO.GeneraSolicitudPago(_solicitudPago, objProveedorInsert.idMaestroTipoProveedor, objProveedorInsert.nombreProveedor,
                                                                    objProveedorInsert.rutProveedor, objProveedorInsert.dvDigitoprovedor.ToString(), avanceObra, descripcionAvanceObra);

            solicitudPagoEntities objSolicitud = new solicitudPagoEntities();
            if (objResultado != null)
            {
                return new solicitudPagoEntities
                {
                    mensajeSalida = objResultado.MSG,
                    codigoSalida = objResultado.ERR.ToString(),
                    idSolicitudPago = objResultado.IDSOLICITUDPAGO
                };
            }
            else
                return objSolicitud;
        }

        public static solicitudPagoEntities getSolicitudPago(long? idSolicitudPago)
        {
            SOLICITUD_PAGO _solicitudPago = solicitudPagoDAO.Get(idSolicitudPago);
            solicitudPagoEntities _solicitudPagoEntities = new solicitudPagoEntities();

            _solicitudPagoEntities.idCaracteristicasEspeciales = _solicitudPago.IDCARACTERISTICASESPECIALES;
            _solicitudPagoEntities.fechaCreacionSolicitud = _solicitudPago.FECHACREACIONSOLICITUDPAGO == null ? String.Empty : _solicitudPago.FECHACREACIONSOLICITUDPAGO.Value.ToString("dd/MM/yyyy");
            _solicitudPagoEntities.idMaestroEstadoSolicitud = _solicitudPago.IDMAESTROESTADOSOLICITUD;
            _solicitudPagoEntities.idSolicitudPago = _solicitudPago.IDSOLICITUDPAGO;
            _solicitudPagoEntities.idTipoPago = _solicitudPago.IDMAESTROTIPOPAGO;
            _solicitudPagoEntities.numeroFamiliasPagadarSolicitudPago = Convert.ToInt32(_solicitudPago.NUMEROFAMILIASPAGARSOLICITUDPAGO);
            _solicitudPagoEntities.usuariosReponsableSolicitudPago = _solicitudPago.USUARIORESPONSABLESOLICITUDPAGO;
            _solicitudPagoEntities.folioBoletaGarantiaSolicitudPago = _solicitudPago.FOLIOBOLETAGARANTIASOLICITUDPAGO;
            _solicitudPagoEntities.montoTotalProyecto = _solicitudPago.MONTOTOTALPROYECTOSOLICITUDPAGO;
            _solicitudPagoEntities.montoPagado = _solicitudPago.MONTOPAGADOSOLICITUDPAGO;
            _solicitudPagoEntities.montoComprometido = _solicitudPago.MONTOCOMPROMETIDOSOLICITUDPAGO;
            _solicitudPagoEntities.SaldoPorPagar = _solicitudPago.SALDOPORPAGARSOLICITUDPAGO;
            _solicitudPagoEntities.idMaestroTipoDestinoPago = _solicitudPago.IDMAESTROTIPODESTINOPAGO;
            _solicitudPagoEntities.avanceObra = _solicitudPago.AVANCEOBRASOLICITUDPAGO;
             _solicitudPagoEntities.resolucionContrato = _solicitudPago.NUMERORESOLUCIONCONTRATOSOLICITUDPAGO;
            _solicitudPagoEntities.montoSolicitud = _solicitudPago.MONTOSOLICITUDSOLICITUDPAGO;
            _solicitudPagoEntities.idProveedor = _solicitudPago.IDPROVEEDOR;
            _solicitudPagoEntities.pagoMandato = _solicitudPago.PAGOMANDANTOSOLICITUDPAGO;
            _solicitudPagoEntities.idMandatoProveedor = _solicitudPago.IDMANDANTOPROVEEDORSOLICITUDPAGO;
            if (_solicitudPago.FECHABOLETAGARANTIASOLICITUDPAGO != null)
            {
                _solicitudPagoEntities.fechaBoletaGarantiaSolicitudPago = Convert.ToDateTime(_solicitudPago.FECHABOLETAGARANTIASOLICITUDPAGO).ToShortDateString();
            }


            _solicitudPagoEntities.numeroViviendasSolicitudPago = Convert.ToInt32(_solicitudPago.NUMEROVIVIENDASSOLICITUDPAGO);
            _solicitudPagoEntities.Observaciones = _solicitudPago.OBSERVACIONESSOLICITUDPAGO;
            _solicitudPagoEntities.fechaResolucionContrato = _solicitudPago.FECHARESOLUCIONSOLICITUDPAGO == null ? String.Empty : _solicitudPago.FECHARESOLUCIONSOLICITUDPAGO.Value.ToString("dd/MM/yyyy"); ;

            return _solicitudPagoEntities;
        }

        public static solicitudPagoEntities getLastSolicitudPago(string CodigoProyecto, long idMaestroPrograma)
        {
            SOLICITUD_PAGO _solicitudPago = solicitudPagoDAO.getLast(CodigoProyecto, idMaestroPrograma);
            solicitudPagoEntities _solicitudPagoEntities = new solicitudPagoEntities();

            if (_solicitudPago.IDSOLICITUDPAGO != 0)
            {
                _solicitudPagoEntities.idCaracteristicasEspeciales = _solicitudPago.IDCARACTERISTICASESPECIALES;
                _solicitudPagoEntities.fechaCreacionSolicitud = _solicitudPago.FECHACREACIONSOLICITUDPAGO == null ? String.Empty : _solicitudPago.FECHACREACIONSOLICITUDPAGO.Value.ToString("dd/MM/yyyy");
                _solicitudPagoEntities.idMaestroEstadoSolicitud = _solicitudPago.IDMAESTROESTADOSOLICITUD;
                _solicitudPagoEntities.idSolicitudPago = _solicitudPago.IDSOLICITUDPAGO;
                _solicitudPagoEntities.idTipoPago = _solicitudPago.IDMAESTROTIPOPAGO;
                _solicitudPagoEntities.numeroFamiliasPagadarSolicitudPago = Convert.ToInt32(_solicitudPago.NUMEROFAMILIASPAGARSOLICITUDPAGO);
                _solicitudPagoEntities.usuariosReponsableSolicitudPago = _solicitudPago.USUARIORESPONSABLESOLICITUDPAGO;
                _solicitudPagoEntities.folioBoletaGarantiaSolicitudPago = _solicitudPago.FOLIOBOLETAGARANTIASOLICITUDPAGO;
                _solicitudPagoEntities.montoTotalProyecto = _solicitudPago.MONTOTOTALPROYECTOSOLICITUDPAGO;
                _solicitudPagoEntities.montoPagado = _solicitudPago.MONTOPAGADOSOLICITUDPAGO;
                _solicitudPagoEntities.montoComprometido = _solicitudPago.MONTOCOMPROMETIDOSOLICITUDPAGO;
                _solicitudPagoEntities.SaldoPorPagar = _solicitudPago.SALDOPORPAGARSOLICITUDPAGO;
                _solicitudPagoEntities.idMaestroTipoDestinoPago = _solicitudPago.IDMAESTROTIPODESTINOPAGO;
                _solicitudPagoEntities.avanceObra = _solicitudPago.AVANCEOBRASOLICITUDPAGO;
                _solicitudPagoEntities.resolucionContrato = _solicitudPago.NUMERORESOLUCIONCONTRATOSOLICITUDPAGO;
                _solicitudPagoEntities.montoSolicitud = _solicitudPago.MONTOSOLICITUDSOLICITUDPAGO;
                _solicitudPagoEntities.idProveedor = _solicitudPago.IDPROVEEDOR;
                _solicitudPagoEntities.numeroViviendasSolicitudPago = Convert.ToInt32(_solicitudPago.NUMEROVIVIENDASSOLICITUDPAGO);
                _solicitudPagoEntities.Observaciones = _solicitudPago.OBSERVACIONESSOLICITUDPAGO;
                _solicitudPagoEntities.Observaciones = _solicitudPago.OBSERVACIONESSOLICITUDPAGO;
                _solicitudPagoEntities.fechaResolucionContrato = _solicitudPago.FECHARESOLUCIONSOLICITUDPAGO == null ? String.Empty : _solicitudPago.FECHARESOLUCIONSOLICITUDPAGO.Value.ToString("dd/MM/yyyy"); ;
                _solicitudPagoEntities.pagoMandato = _solicitudPago.PAGOMANDANTOSOLICITUDPAGO;
                _solicitudPagoEntities.idMandatoProveedor = _solicitudPago.IDMANDANTOPROVEEDORSOLICITUDPAGO;


            }

            return _solicitudPagoEntities;
        }

        public static decimal? GetSolicitudesProyectoMontoPagadoHist(string CodigoProyecto, long? idMaestroPrograma)
        {
            return solicitudPagoDAO.GetSolicitudesProyectoMontoPagadoHist(CodigoProyecto, idMaestroPrograma);
        }

        public static decimal? GetSolicitudesProyectoMontoPagadoSIGFE(string CodigoProyecto, long? idMaestroPrograma)
        {
            return solicitudPagoDAO.GetSolicitudesProyectoMontoPagadoSIGFE(CodigoProyecto, idMaestroPrograma);
        }

        public static decimal? GetSolicitudesProyectoMontoSolicitudesGeneradas(string CodigoProyecto, long? idMaestroPrograma)
        {
            return solicitudPagoDAO.GetSolicitudesProyectoMontoSolicitudesGeneradas(CodigoProyecto, idMaestroPrograma);
        }
    }
}
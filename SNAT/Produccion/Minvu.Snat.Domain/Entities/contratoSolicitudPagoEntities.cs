using Minvu.Snat.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.DAO;
using Minvu.Snat.IData.ORM;
using System.ComponentModel.DataAnnotations;


namespace Minvu.Snat.Domain.Entities
{
    public class contratoSolicitudPagoEntities
    {
        [Display(Name = "Año Presupuesto:")]
        public int annoPresupuesto { get; set; }

        [Display(Name = "Presupuesto Disponible ($):")]
        public long montoPresupuesto { get; set; }

        [Display(Name = "Número de Solicitud:")]
        public long numeroSolicitudPresupuesto { get; set; }
        public string nombreRegionPresupuesto { get; set; }
        [Display(Name = "Región:")]

        public int idRegion { get; set; }
        public int idProvincia { get; set; }
        public int idComuna { get; set; }

        [Display(Name = "N° Resolución Contrato:")]
        public int? numeroResolucionContrato { get; set; }

        [Display(Name = "Plazo Ejecución:")]
        public int plazoEjecucion { get; set; }
        public long idContrato { get; set; }

        [Display(Name = "Rut contrato:")]
        public string rutContrato { get; set; }
        public string rutContratoConsultaRat { get; set; }
        public string dvContratoConsultaRat { get; set; }


        [Display(Name = "Tipo Proveedor:")]
        public long idTipoProveedor { get; set; }
        public string nombreTipoProveedor { get; set; }


        [Display(Name = "Fecha de Resolución Contrato:")]
        public DateTime fechaResolucionContrato { get; set; }


        [Display(Name = "Fecha de Inicio Contrato:")]
        public DateTime fechaInicioContrato { get; set; }
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ°'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]


        [Display(Name = "Nombre/Razón Social:")]
        public string nombreRazonSocial { get; set; }
        public long montoTotalContratoRegion { get; set; }
        public long montoDisponibleContratoRegion { get; set; }

        [Display(Name = "Monto Contrato ($):")]
        public long montoContrato { get; set; }

        [Display(Name = "Monto Contrato Pagado ($):")]
        public long montoPagados { get; set; }


        [Display(Name = "Monto Contrato Comprometido ($):")]
        public long montoComprometidos { get; set; }

        [Display(Name = "Monto Contrato por Pagar ($):")]
        public long montoPorPagar { get; set; }
        public string nombreServicio { get; set; }
        public string nombreMaestroServicio { get; set; }
        public long idMaestroServicio { get; set; }
        [Display(Name = "N° Boleta o Factura:")]
        public long? numeroBolFact { get; set; }
        [Display(Name = "V°B° Servicio:")]
        public bool vbServicio { get; set; }
        public DateTime? fechaBolFact { get; set; }
        public string fechaBolFactSolicitud { get; set; }

        [Display(Name = "Responsable V°B°:")]
        public string responsableVB { get; set; }
        public string responsableNombreCompletoVB { get; set; }
        [Display(Name = "Monto Total a Pagar ($):")]
        public int montoTotalaPagar { get; set; }
        [Display(Name = "Fecha V°B°:")]
        public DateTime fechaVB { get; set; }
        public string mensajeSalida { get; set; }
        public string codigoSalida { get; set; }
        public int estadoSolicitud { get; set; }

        public string codigoSalidaRatifica { get; set; }
        public string mensajeSalidaRatifica { get; set; }
        public int rutUser { get; set; }
        public long montoPresupuestoNacional { get; set; }
        public long montoPresupuestoRegional { get; set; }
        public string idSolicitudRegion { get; set; }
        public string idContratoRegion { get; set; }
        public long saldoPresupuesto { get; set; }
        public int cantidadContratos { get; set; }
        public long saldoPresupuestoNacional { get; set; }
        public long montoContratoPresupuestoNacional { get; set; }
        public long saldoPresupuestoRegion { get; set; }
        public string nombreRegionPresupuestoPrint { get; set; }
        public string nombreMaestroEstadoContrato { get; set; }


        public contratoSolicitudPagoEntities()
        {
            annoPresupuesto = 0;
            montoPresupuesto = 0;
            numeroSolicitudPresupuesto = 0;
            nombreRegionPresupuesto = string.Empty;
            numeroResolucionContrato = null;            
            plazoEjecucion = 0;
            rutContrato = string.Empty;
            idTipoProveedor = 0;
            nombreRazonSocial = string.Empty;
            montoContrato = 0;
            montoPagados = 0;
            montoComprometidos = 0;
            montoPorPagar = 0;
            numeroBolFact = null;
            vbServicio = false;
            fechaBolFact = null;
            responsableVB = string.Empty;
            montoTotalaPagar = 0;
            fechaVB = DateTime.Now;
            mensajeSalida = string.Empty;
            codigoSalida = string.Empty;
            estadoSolicitud = 0;
            codigoSalidaRatifica = string.Empty;
            mensajeSalidaRatifica = string.Empty;
            rutUser = 0;
            fechaBolFactSolicitud = string.Empty;
            montoPresupuestoNacional = 0;
            montoPresupuestoRegional = 0;
            idSolicitudRegion = string.Empty;
            idContratoRegion = string.Empty;
            saldoPresupuesto = 0;
            cantidadContratos = 0;
            saldoPresupuestoNacional = 0;
            montoContratoPresupuestoNacional = 0;
            saldoPresupuestoRegion = 0;
            nombreMaestroEstadoContrato = string.Empty;
            responsableNombreCompletoVB = string.Empty;
            //nombreEstadoSolicitud = string.Empty;
        }

        public contratoSolicitudPagoEntities(int _annoPresupuesto, int _montoPresupuesto, int _numeroSolicitudPresupuesto, string _nombreRegionPresupuesto,
            int _numeroResolucionContrato, int _plazoEjecucion, string _rutContrato, int _idTipoProveedor, DateTime _fechaResolucionContrato, DateTime _fechaInicioContrato,
            string _nombreRazonSocial, int _montoContrato, int _montoPagados, int _montoComprometidos, int _montoPorPagar, int _numeroBolFact, bool _vbServicio, DateTime _fechaBolFact,
            string _responsableVB, int _montoTotalaPagar, DateTime _fechaVB, string _codigoSalidaRatifica, string _mensajeSalidaRatifica, long _montoPresupuestoRegional, long _saldoPresupuesto,
            int _cantidadContratos, string _nombreMaestroEstadoContrato)
        {
            annoPresupuesto = _annoPresupuesto;
            montoPresupuesto = _montoPresupuesto;
            numeroSolicitudPresupuesto = _numeroSolicitudPresupuesto;
            nombreRegionPresupuesto = _nombreRegionPresupuesto;
            numeroResolucionContrato = _numeroResolucionContrato;
            plazoEjecucion = _plazoEjecucion;
            rutContrato = _rutContrato;
            idTipoProveedor = _idTipoProveedor;
            fechaResolucionContrato = _fechaResolucionContrato;
            fechaInicioContrato = _fechaInicioContrato;
            nombreRazonSocial = _nombreRazonSocial;
            montoContrato = _montoContrato;
            montoPagados = _montoPagados;
            montoComprometidos = _montoComprometidos;
            montoPorPagar = _montoPorPagar;
            numeroBolFact = _numeroBolFact;
            vbServicio = _vbServicio;
            fechaBolFact = _fechaBolFact;
            responsableVB = _responsableVB;
            montoTotalaPagar = _montoTotalaPagar;
            fechaVB = _fechaVB;
            codigoSalidaRatifica = _codigoSalidaRatifica;
            mensajeSalidaRatifica = _mensajeSalidaRatifica;
            montoPresupuestoRegional = _montoPresupuestoRegional;
            saldoPresupuesto = _saldoPresupuesto;
            cantidadContratos = _cantidadContratos;
            nombreMaestroEstadoContrato = _nombreMaestroEstadoContrato;
        }
    }

    public class contratoSolicitudPagoEntitiesFactory
    {
        internal static contratoSolicitudPagoEntities getRegionUser(string userName)
        {
            var _RegionUser = regionDAO.GetUSERREGION(userName);
            contratoSolicitudPagoEntities contratoSolicitudPagoEntities = new contratoSolicitudPagoEntities();
            if (_RegionUser != null)
            {
                return new contratoSolicitudPagoEntities
                {
                    idRegion = _RegionUser.IDRegion
                };
            }

            return contratoSolicitudPagoEntities;
        }

        internal static contratoSolicitudPagoEntities getRegion(int idRegion)
        {
            var _RegionUser = regionDAO.obtenerNombreRegion(idRegion);
            contratoSolicitudPagoEntities contratoSolicitudPagoEntities = new contratoSolicitudPagoEntities();
            if (_RegionUser != null)
            {
                return new contratoSolicitudPagoEntities
                {
                    nombreRegionPresupuesto = _RegionUser
                };

            }

            return contratoSolicitudPagoEntities;
        }

        internal static contratoSolicitudPagoEntities eliminaContrato(int idContrato)
        {
            var _eliminaContrato = contratoDAO.EliminaContrato(idContrato);
            contratoSolicitudPagoEntities contratoSolicitudPagoEntities = new contratoSolicitudPagoEntities();
            if (_eliminaContrato != null)
            {
                return new contratoSolicitudPagoEntities
                {
                    mensajeSalida = _eliminaContrato.MSG,
                    codigoSalida = _eliminaContrato.err.ToString()
                };
            }
            return contratoSolicitudPagoEntities;
        }

        internal static contratoSolicitudPagoEntities eliminaSolicitud(int idSolicitud)
        {
            var _eliminaSolicitud = contratoDAO.EliminaSolicitud(idSolicitud);
            contratoSolicitudPagoEntities contratoSolicitudPagoEntities = new contratoSolicitudPagoEntities();
            if (_eliminaSolicitud != null)
            {
                return new contratoSolicitudPagoEntities
                {
                    mensajeSalida = _eliminaSolicitud.MSG,
                    codigoSalida = _eliminaSolicitud.err.ToString()
                };
            }
            return contratoSolicitudPagoEntities;
        }

        internal static contratoSolicitudPagoEntities ConsultaSolicitudPago(string user, long anno, int idRegion, int idSolicitud)
        {
            var _RegionUser = regionDAO.GetUSERREGION(user);
            var _RegionUserNombre = regionDAO.obtenerNombreRegion(_RegionUser.IDRegion);
            var _RegionUserNombrePrint = regionDAO.obtenerNombreRegion(idRegion);
            var _obtieneMontoPresupuesto = contratoDAO.GetMontoPresupuestoRegion(anno, _RegionUser.IDRegion);
            var _obtieneNumeroSolicitud = contratoDAO.GetSolicitudPago();
            var _obtieneMontoContrato = contratoDAO.GetMontoContratoRegion(anno, _RegionUser.IDRegion);
            var _ObtieneRutUser = contratoDAO.GetRutUsuario(user);
            var _obtieneMontoPresupuestoNacional = contratoDAO.GetMontoNacional(anno);
            var _obtieneMontoContratoNacional = contratoDAO.GetMontoContratoPresupuesto(anno);
            var _obtieneNumeroResolucion = contratoDAO.GetResolucionContrato(idSolicitud);

            contratoSolicitudPagoEntities contratoSolicitudPagoEntities = new contratoSolicitudPagoEntities();
            if (_obtieneMontoContrato.MONTOCONTRATOCONTRATOATP == null)
                _obtieneMontoContrato.MONTOCONTRATOCONTRATOATP = 0;
            if (_obtieneMontoContratoNacional.MONTOCONTRATOCONTRATOATP == null)
                _obtieneMontoContratoNacional.MONTOCONTRATOCONTRATOATP = 0;


            if (_obtieneMontoPresupuesto.MONTOPRESUPUESTOREGIONAL > 0)
            {
                return new contratoSolicitudPagoEntities
                {
                    montoPresupuesto = ((long)_obtieneMontoPresupuesto.MONTOPRESUPUESTOREGIONAL - (long)_obtieneMontoContrato.MONTOCONTRATOCONTRATOATP),
                    idRegion = _RegionUser.IDRegion,
                    nombreRegionPresupuesto = _RegionUserNombre,
                    nombreRegionPresupuestoPrint = _RegionUserNombrePrint,
                    numeroSolicitudPresupuesto = _obtieneNumeroSolicitud.IDSOLICITUDATP,
                    rutUser = (int)_ObtieneRutUser.Rut,
                    montoPresupuestoRegional = (long)_obtieneMontoPresupuesto.MONTOPRESUPUESTOREGIONAL,
                    saldoPresupuesto = ((long)_obtieneMontoPresupuesto.MONTOPRESUPUESTOREGIONAL - (long)_obtieneMontoContrato.MONTOCONTRATOCONTRATOATP),
                    montoPresupuestoNacional = (long)_obtieneMontoPresupuestoNacional.MONTOPRESUPUESTOREGIONAL,
                    montoContratoPresupuestoNacional = (long)_obtieneMontoContratoNacional.MONTOCONTRATOCONTRATOATP,
                    saldoPresupuestoNacional = (long)_obtieneMontoPresupuestoNacional.MONTOPRESUPUESTOREGIONAL - (long)_obtieneMontoContratoNacional.MONTOCONTRATOCONTRATOATP,
                    numeroResolucionContrato = (int)_obtieneNumeroResolucion.CONTRATO_ATP.NUMERORESOLUCIONCONTRATOATP,

                    numeroBolFact = _obtieneNumeroResolucion.NUMEROBOLETAFACTURASOLICITUDPAGOATP,
                    fechaBolFact = _obtieneNumeroResolucion.FECHABOLETAFACTURASOLICITUDPAGOATP,
                    montoTotalaPagar = (int)_obtieneNumeroResolucion.MONTOTOTALAPAGARSOLICITUDPAGOATP,
                    vbServicio = (bool)_obtieneNumeroResolucion.VBSERVICIOSOLICITUDPAGOATP,
                    fechaVB = (DateTime)_obtieneNumeroResolucion.VBFECHASOLICITUDPAGOATP,
                    responsableVB = _obtieneNumeroResolucion.VBRESPONSABLESOLICITUDPAGOATP,
                    estadoSolicitud = (int)_obtieneNumeroResolucion.IDMAESTROESTADOSOLICITUD,
                    nombreMaestroEstadoContrato = contratoSolicitudPagoEntitiesFactory.obtieneNombreEstadoSolicitud((int)_obtieneNumeroResolucion.IDMAESTROESTADOSOLICITUD),
                    idTipoProveedor = (long)_obtieneNumeroResolucion.IDMAESTROTIPOPAGO
                };
            }
            else
            {
                return new contratoSolicitudPagoEntities
                {
                    mensajeSalida = "No se encuentra Presupuesto disponible para el año en curso.",
                    codigoSalidaRatifica = "-3",
                    codigoSalida = "-3",
                };
            }

        }

        internal static string obtieneNombreEstadoSolicitud(int idMaestroEstadoSolicitud)
        {

            MAESTRO_ESTADO_SOLICITUD aux_ = maestroEstadoSolicitudDAO.Get(idMaestroEstadoSolicitud);
            return aux_.NOMBREMAESTROESTADOSOLICITUD;

        }

        internal static contratoSolicitudPagoEntities ObtieneCabecera(string userName, long anno, int idRegion)
        {
            Log.Instance.Info("Ingresado a: ObtieneCabecera " + userName + " " + anno + " " + idRegion);

            FuncionarioEntities _funcionarioEntities = FuncionarioEntitiesFactory.getFuncionario(userName);

            if (_funcionarioEntities == null)
            {
                Log.Instance.Info("metodo-controlador: _funcionarioEntities null - ObtieneCabecera");
                if (_funcionarioEntities.rut > 0)
                    Log.Instance.Info("metodo-controlador:" + _funcionarioEntities.rut.ToString());
            }



            var _RegionUserNombrePrint = regionDAO.obtenerNombreRegion(idRegion);
            var _obtieneMontoPresupuesto = contratoDAO.GetMontoPresupuestoRegion(anno, _funcionarioEntities.idRegion);
            //var _obtieneNumeroSolicitud = contratoDAO.GetSolicitudPago();
            var _obtieneMontoContrato = contratoDAO.GetMontoContratoRegion(anno, _funcionarioEntities.idRegion);
            var _obtieneMontoPresupuestoNacional = contratoDAO.GetMontoNacional(anno);
            var _obtieneMontoContratoNacional = contratoDAO.GetMontoContratoPresupuesto(anno);


            //Verificar el 

            if (_obtieneMontoContrato.MONTOCONTRATOCONTRATOATP == null)
                _obtieneMontoContrato.MONTOCONTRATOCONTRATOATP = 0;
            if (_obtieneMontoContratoNacional.MONTOCONTRATOCONTRATOATP == null)
                _obtieneMontoContratoNacional.MONTOCONTRATOCONTRATOATP = 0;


            contratoSolicitudPagoEntities contratoSolicitudPagoEntities = new contratoSolicitudPagoEntities();
            if (_obtieneMontoPresupuesto.IDRESOLUCIONPRESUPUESTARIA != null)
            {
                if (_obtieneMontoPresupuesto.MONTOPRESUPUESTOREGIONAL > 0 && _obtieneMontoContratoNacional != null)
                {
                    Log.Instance.Info("contratoSolicitudPagoEntities");
                    try
                    {
                        Log.Instance.Info("contratoSolicitudPagoEntities 1");

                        var contrato = new contratoSolicitudPagoEntities
                        {

                            montoPresupuesto = ((long)_obtieneMontoPresupuesto.MONTOPRESUPUESTOREGIONAL - (long)_obtieneMontoContrato.MONTOCONTRATOCONTRATOATP),
                            idRegion = _funcionarioEntities.idRegion,
                            nombreRegionPresupuesto = _funcionarioEntities.region,
                            nombreRegionPresupuestoPrint = _RegionUserNombrePrint,
                            //numeroSolicitudPresupuesto = _obtieneNumeroSolicitud.IDSOLICITUDATP + 1,
                            rutUser = (int)_funcionarioEntities.rut,
                            montoPresupuestoRegional = (long)_obtieneMontoPresupuesto.MONTOPRESUPUESTOREGIONAL,
                            saldoPresupuesto = ((long)_obtieneMontoPresupuesto.MONTOPRESUPUESTOREGIONAL - (long)_obtieneMontoContrato.MONTOCONTRATOCONTRATOATP),
                            montoPresupuestoNacional = (long)_obtieneMontoPresupuestoNacional.MONTOPRESUPUESTOREGIONAL,
                            montoContratoPresupuestoNacional = (long)_obtieneMontoContratoNacional.MONTOCONTRATOCONTRATOATP,
                            saldoPresupuestoNacional = (long)_obtieneMontoPresupuestoNacional.MONTOPRESUPUESTOREGIONAL - (long)_obtieneMontoContratoNacional.MONTOCONTRATOCONTRATOATP
                        };

                        Log.Instance.Info("contratoSolicitudPagoEntities 2");

                        return contrato;
                        Log.Instance.Info("contratoSolicitudPagoEntities 3");
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Info("contratoSolicitudPagoEntities" + ex);
                        throw;
                    }

                }
                else
                {
                    Log.Instance.Info("contratoSolicitudPagoEntities else 1");
                    return new contratoSolicitudPagoEntities
                    {
                        idRegion = _funcionarioEntities.idRegion,
                        nombreRegionPresupuesto = _funcionarioEntities.region,
                        nombreRegionPresupuestoPrint = _RegionUserNombrePrint,
                        rutUser = (int)_funcionarioEntities.rut,


                        mensajeSalida = "No se encuentra Presupuesto disponible para el ano en curso.",
                        codigoSalidaRatifica = "-3",
                        codigoSalida = "-3",
                    };
                }
            }
            else
            {
                Log.Instance.Info("contratoSolicitudPagoEntities else 2");
                return new contratoSolicitudPagoEntities
                {
                    idRegion = _funcionarioEntities.idRegion,
                    nombreRegionPresupuesto = _funcionarioEntities.region,
                    nombreRegionPresupuestoPrint = _RegionUserNombrePrint,
                    rutUser = (int)_funcionarioEntities.rut,

                    mensajeSalida = "No se ha ingresado presupuesto para el ano en curso, ingrese presupuesto.",
                    codigoSalidaRatifica = "-3",
                    codigoSalida = "-3",
                };
            }

        }

        internal static contratoSolicitudPagoEntities ObtieneMontoPresupuesto(string userName, long anno)
        {
            var _RegionUser = regionDAO.GetUSERREGION(userName);
            var _RegionUserNombre = regionDAO.obtenerNombreRegion(_RegionUser.IDRegion);
            var _obtieneMontoContrato = contratoDAO.GetMontoContratoRegion(anno, _RegionUser.IDRegion);
            var _obtieneMontoPresupuesto = contratoDAO.GetMontoPresupuestoRegion(anno, _RegionUser.IDRegion);
            var _obtieneMontoPresupuestoNacional = contratoDAO.GetMontoNacional(anno);

            if (_obtieneMontoContrato.MONTOCONTRATOCONTRATOATP == null)
            {
                _obtieneMontoContrato.MONTOCONTRATOCONTRATOATP = 0;
            }

            contratoSolicitudPagoEntities contratoSolicitudPagoEntities = new contratoSolicitudPagoEntities();

            if (_obtieneMontoContrato != null)
            {
                return new contratoSolicitudPagoEntities
                {
                    montoPresupuesto = (long)_obtieneMontoPresupuesto.MONTOPRESUPUESTOREGIONAL,
                    montoTotalContratoRegion = (long)_obtieneMontoContrato.MONTOCONTRATOCONTRATOATP,
                    montoDisponibleContratoRegion = ((long)_obtieneMontoPresupuesto.MONTOPRESUPUESTOREGIONAL - (long)_obtieneMontoContrato.MONTOCONTRATOCONTRATOATP),
                    idRegion = _RegionUser.IDRegion,
                    nombreRegionPresupuesto = _RegionUserNombre,
                    montoPresupuestoNacional = (long)_obtieneMontoPresupuestoNacional.MONTOPRESUPUESTOREGIONAL,
                };
            }

            else
                return contratoSolicitudPagoEntities;
        }

        internal static contratoSolicitudPagoEntities obtieneNumeroResolucion(long numeroResolucion)
        {
            var _obtieneNumeroResolucion = contratoDAO.GetExisteNumeroResolucion(numeroResolucion);

            contratoSolicitudPagoEntities contratoSolicitudPagoEntities = new contratoSolicitudPagoEntities();

            if (_obtieneNumeroResolucion.NUMERORESOLUCIONCONTRATOATP > 0)
            {
                return new contratoSolicitudPagoEntities
                {
                    numeroResolucionContrato = (int)_obtieneNumeroResolucion.NUMERORESOLUCIONCONTRATOATP
                };
            }
            else
                return contratoSolicitudPagoEntities;

        }

        internal static contratoSolicitudPagoEntities ObtieneMontoPresupuestoRegional(long anno, int idRegion)
        {
            var _obtieneMontoContrato = contratoDAO.GetMontoContratoRegion(anno, idRegion);
            var _obtieneMontoPresupuesto = contratoDAO.GetMontoPresupuestoRegion(anno, idRegion);
            var _obtieneMontoPresupuestoNacional = contratoDAO.GetMontoNacional(anno);

            if (_obtieneMontoContrato.MONTOCONTRATOCONTRATOATP == null)
            {
                _obtieneMontoContrato.MONTOCONTRATOCONTRATOATP = 0;
            }

            contratoSolicitudPagoEntities contratoSolicitudPagoEntities = new contratoSolicitudPagoEntities();

            if (_obtieneMontoPresupuesto.MONTOPRESUPUESTOREGIONAL > 0)
            {
                return new contratoSolicitudPagoEntities
                {
                    montoPresupuesto = (long)_obtieneMontoPresupuesto.MONTOPRESUPUESTOREGIONAL,
                    montoTotalContratoRegion = (long)_obtieneMontoContrato.MONTOCONTRATOCONTRATOATP,
                    montoDisponibleContratoRegion = ((long)_obtieneMontoPresupuesto.MONTOPRESUPUESTOREGIONAL - (long)_obtieneMontoContrato.MONTOCONTRATOCONTRATOATP),
                    montoPresupuestoNacional = (long)_obtieneMontoPresupuestoNacional.MONTOPRESUPUESTOREGIONAL,
                    saldoPresupuestoRegion = (long)_obtieneMontoPresupuesto.MONTOPRESUPUESTOREGIONAL - (long)_obtieneMontoContrato.MONTOCONTRATOCONTRATOATP,
                };
            }

            else
                return contratoSolicitudPagoEntities;
        }

        internal static contratoSolicitudPagoEntities ObtieneContrato(long numeroResolucion, long idRegion)
        {
            var _obtieneContrato = contratoDAO.GetContratoResolucion(numeroResolucion, idRegion);

            contratoSolicitudPagoEntities contratoSolicitudPagoEntities = new contratoSolicitudPagoEntities();

            if (_obtieneContrato.IDCONTRATOATP > 0)
            {
                var _obtieneMontoComprometido = contratoDAO.GetMontoComprometidoContrato(_obtieneContrato.IDCONTRATOATP);
                var _obtieneMontoPagado = contratoDAO.GetMontopagadoContrato(_obtieneContrato.IDCONTRATOATP);

                int montoComprometidos = 0;

                if (_obtieneMontoComprometido.MONTOCOMPROMETIDOSOLICITUDPAGOATP == null)
                {
                    _obtieneMontoComprometido.MONTOCOMPROMETIDOSOLICITUDPAGOATP = montoComprometidos;
                }

                return new contratoSolicitudPagoEntities
                {
                    idContrato = _obtieneContrato.IDCONTRATOATP,
                    plazoEjecucion = (int)_obtieneContrato.PLAZOEJECUCIONCONTRATOATP,
                    rutContrato = _obtieneContrato.PROVEEDOR.RUTPROVEEDOR.ToString() + "-" + _obtieneContrato.PROVEEDOR.DVPROVEDIGITOVERIFICADORPROVEEDOR,
                    rutContratoConsultaRat = _obtieneContrato.PROVEEDOR.RUTPROVEEDOR.ToString(),
                    dvContratoConsultaRat = _obtieneContrato.PROVEEDOR.DVPROVEDIGITOVERIFICADORPROVEEDOR.ToString(),
                    idTipoProveedor = (long)_obtieneContrato.MAESTRO_TIPO_PROVEEDOR.IDMAESTROTIPOPROVEEDOR,
                    nombreTipoProveedor = _obtieneContrato.MAESTRO_TIPO_PROVEEDOR.NOMBREMAESTROTIPOPROVEEDOR,
                    fechaResolucionContrato = (DateTime)_obtieneContrato.FECHARESOLUCIONCONTRATOATP,
                    fechaInicioContrato = (DateTime)_obtieneContrato.FECHAINICIOCONTRATOATP,
                    nombreRazonSocial = _obtieneContrato.PROVEEDOR.NOMBREPROVEEDOR,
                    montoContrato = (long)_obtieneContrato.MONTOCONTRATOCONTRATOATP,
                    montoPorPagar = ((long)_obtieneContrato.MONTOCONTRATOCONTRATOATP - (long)_obtieneMontoComprometido.MONTOCOMPROMETIDOSOLICITUDPAGOATP)- (long)_obtieneMontoPagado.MONTOPAGADOSOLICITUDPAGOATP,
                    montoComprometidos = (long)_obtieneMontoComprometido.MONTOCOMPROMETIDOSOLICITUDPAGOATP,
                    montoPagados = (long)_obtieneMontoPagado.MONTOPAGADOSOLICITUDPAGOATP,
                    


                };
            }
            else
                return contratoSolicitudPagoEntities;
        }

        internal static List<contratoSolicitudPagoEntities> obtieneServicios(long idContrato)
        {
            var _obtieneServicios = contratoDAO.GetActividadesContrato(idContrato);

            List<contratoSolicitudPagoEntities> contratoSolicitudPagoEntities = new List<contratoSolicitudPagoEntities>();
            if (_obtieneServicios != null)
            {
                foreach (var item in _obtieneServicios)
                {
                    contratoSolicitudPagoEntities info = new contratoSolicitudPagoEntities();

                    info.nombreServicio = item.NOMBREABREVIADOMAESTROSERVICIO;
                    info.nombreMaestroServicio = item.NOMBREMAESTROSERVICIO;
                    info.idMaestroServicio = item.IDMAESTROSERVICIO;

                    contratoSolicitudPagoEntities.Add(info);
                }
                return contratoSolicitudPagoEntities;
            }
            else
                return contratoSolicitudPagoEntities;

        }

        internal static bool ActualizaEstadoInformaciónRatificadorSolicitud(long idSolicitud, string nombreUsuario)
        {


            return contratoDAO.ActualizaEstadoInformaciónRatificadorSolicitud(idSolicitud, nombreUsuario);
        }
        internal static List<contratoSolicitudPagoEntities> obtieneListadoSolicitud(int accion, long numeroSolicitud, int codigoRegion, int annoPresupuesto)
        {
            var _ObtieneSolicitud = contratoDAO.GetListaSolicitud(accion, numeroSolicitud, codigoRegion, annoPresupuesto);

            List<contratoSolicitudPagoEntities> contratoSolicitudPagoEntities = new List<contratoSolicitudPagoEntities>();
            if (_ObtieneSolicitud != null)
            {
                foreach (var item in _ObtieneSolicitud)
                {
                    contratoSolicitudPagoEntities info = new contratoSolicitudPagoEntities();

                    info.numeroSolicitudPresupuesto = item.IDSOLICITUDATP;
                    info.numeroResolucionContrato = (int)item.NUMERORESOLUCIONCONTRATOATP;
                    info.idRegion = (int)item.CODIGOREGIONSOLICITUDPAGOATP;
                    info.rutContratoConsultaRat = item.RUTPROVEEDOR.ToString() + "-" + item.DVPROVEDIGITOVERIFICADORPROVEEDOR;
                    info.dvContratoConsultaRat = item.DVPROVEDIGITOVERIFICADORPROVEEDOR;
                    info.nombreRazonSocial = item.NOMBREPROVEEDOR;
                    info.montoContrato = (int)item.MONTOTOTALAPAGARSOLICITUDPAGOATP;
                    info.nombreServicio = item.TIPOSERVICIOTIPOACTIVIDADMONTO;
                    info.nombreMaestroServicio = item.NOMBREMAESTROESTADOSOLICITUD;
                    info.fechaBolFactSolicitud = Convert.ToDateTime(item.FECHACREACIONSOLICITUDPAGOATP).ToString("dd/MM/yyyy");
                    info.idSolicitudRegion = item.IDSOLICITUDATP.ToString() + item.CODIGOREGIONSOLICITUDPAGOATP.ToString().PadLeft(2, '0') + annoPresupuesto.ToString();

                    contratoSolicitudPagoEntities.Add(info);
                }

                return contratoSolicitudPagoEntities;
            }
            else
                return contratoSolicitudPagoEntities;

        }

        internal static List<contratoSolicitudPagoEntities> obtieneListadoContrato(int accion, int annoPresupuesto, int codigoRegion, long tipoServicio)
        {
            var _obtieneContrato = contratoDAO.GetListaContrato(accion, annoPresupuesto, codigoRegion, tipoServicio);

            List<contratoSolicitudPagoEntities> contratoSolicitudPagoEntities = new List<contratoSolicitudPagoEntities>();
            if (_obtieneContrato != null)
            {
                foreach (var item in _obtieneContrato)
                {
                    contratoSolicitudPagoEntities info = new contratoSolicitudPagoEntities();

                    info.idContrato = item.IDCONTRATOATP;
                    info.idRegion = (int)item.CODIGOREGIONTERRITORIOCONTRATO;
                    info.rutContratoConsultaRat = item.RUTPROVEEDOR.ToString() + "-" + item.DVPROVEDIGITOVERIFICADORPROVEEDOR;
                    info.dvContratoConsultaRat = item.DVPROVEDIGITOVERIFICADORPROVEEDOR;
                    info.nombreRazonSocial = item.NOMBREPROVEEDOR;
                    info.montoContrato = (int)item.MONTOCONTRATOCONTRATOATP;
                    info.nombreServicio = item.TIPOSERVICIOMAESTROTIPOSERVICIOIGT;
                    info.fechaBolFactSolicitud = Convert.ToDateTime(item.FECHACREACIONCONTRATOATP).ToString("dd/MM/yyyy");
                    info.idContratoRegion = item.IDCONTRATOATP.ToString() + item.CODIGOREGIONTERRITORIOCONTRATO.ToString().PadLeft(2, '0') + annoPresupuesto.ToString();
                    info.cantidadContratos = (int)item.CONTRATOS;
                    info.numeroResolucionContrato = (int)item.NUMERORESOLUCIONCONTRATOATP;
                    info.nombreMaestroEstadoContrato = item.NOMBREMAESTROESTADOCONTRATO;

                    contratoSolicitudPagoEntities.Add(info);
                }

                return contratoSolicitudPagoEntities;
            }
            else
                return contratoSolicitudPagoEntities;

        }

        internal static contratoEntities obtieneContrato(int idContrato)
        {
            var _obtieneContrato = contratoDAO.GetContrato(idContrato);

            contratoEntities contratoSolicitudPagoEntities = new contratoEntities();
            if (_obtieneContrato != null)
            {
                return new contratoEntities
                {
                    idContrato = (int)_obtieneContrato.IDCONTRATOATP,
                    rutProfesional = _obtieneContrato.RUTPROVEEDOR.ToString() + "-" + _obtieneContrato.DVPROVEDIGITOVERIFICADORPROVEEDOR,
                    dvProfesional = _obtieneContrato.DVPROVEDIGITOVERIFICADORPROVEEDOR,
                    nombreRazonSocial = _obtieneContrato.NOMBREPROVEEDOR,
                    nombreTipoProveedor = _obtieneContrato.NOMBREMAESTROTIPOPROVEEDOR,
                    annoPresupuesto = (int)_obtieneContrato.AGNOPRESUPUESTOCONTRATOATP,
                    idRegion = (int)_obtieneContrato.CODIGOREGIONTERRITORIOCONTRATO,
                    idProvincia = (int)_obtieneContrato.CODIGOPROVINCIATERRITORIOCONTRATO,
                    idComuna = (int)_obtieneContrato.CODIGOCOMUNATERRITORIOCONTRATO,
                    nombreArchivo = _obtieneContrato.NOMBREARCHIVOCONTRATOATP,
                    nombrePropiedadTerreno = _obtieneContrato.NOMBREPROPIEDADTERRENO,
                    numeroResolucion = (int)_obtieneContrato.NUMERORESOLUCIONCONTRATOATP,
                    fechaResolucionContrato = Convert.ToDateTime(_obtieneContrato.FECHARESOLUCIONCONTRATOATP),
                    fechaInicioContrato = Convert.ToDateTime(_obtieneContrato.FECHAINICIOCONTRATOATP),
                    plazoEjecucion = (int)_obtieneContrato.PLAZOEJECUCIONCONTRATOATP,
                    tipoServicio = _obtieneContrato.NOMBREMAESTROTIPOSERVICIOIGT,
                    producto = _obtieneContrato.PRODUCTOCONTRATOATP,
                    descripcionProducto = _obtieneContrato.DESCRIPCIONPRODUCTOCONTRATOATP,
                    montoContrato = (int)_obtieneContrato.MONTOCONTRATOCONTRATOATP,
                    observacion = _obtieneContrato.OBSERVACIONCONTRATOATP,
                };

            }
            return contratoSolicitudPagoEntities;

        }

        internal static contratoSolicitudPagoEntities insertaSolicitudATP(long iDSOLICITUDATP, long iDCONTRATO, int aNNOPRESUPUESTO, long mONTOPRESUPESTO, int cODIGOREGION,
                                                                    long mONTOPAGADO, long mONTOCOMPROMETIDO, long mONTOPORPAGAR, string aCTIVIDADES, long? nUMEROBOLFACT, DateTime? fECHABOLETAFACT, long mONTOTOTALAPAGAR,
                                                                    bool vBSERVICIO, string vBRESPONSABLE, DateTime? vBFECHA, long eSTADOSOLICITUD, long? iDMAESTROTIPOPAGO)
        {
            var _insertaSolicitudATP = contratoDAO.InsertaSolicitud(iDSOLICITUDATP, iDCONTRATO, aNNOPRESUPUESTO, mONTOPRESUPESTO, cODIGOREGION, mONTOPAGADO, mONTOCOMPROMETIDO,
                                                                    mONTOPORPAGAR, aCTIVIDADES, nUMEROBOLFACT, fECHABOLETAFACT, mONTOTOTALAPAGAR, vBSERVICIO,
                                                                    vBRESPONSABLE, vBFECHA, eSTADOSOLICITUD, iDMAESTROTIPOPAGO);


            contratoSolicitudPagoEntities contratoSolicitudPagoEntities = new contratoSolicitudPagoEntities();
            if (_insertaSolicitudATP != null)
            {
                return new contratoSolicitudPagoEntities
                {
                    mensajeSalida = _insertaSolicitudATP.MSG,
                    codigoSalida = _insertaSolicitudATP.err.ToString()
                };
            }
            else
                return contratoSolicitudPagoEntities;


        }

        internal static contratoSolicitudPagoEntities insertaRatificaSolicitud(long codigoOrdenAT, int lineaOrigen, int lineaAplicacion, int tipoLlamado, decimal montoUF,
                                                                               int montoPesos, string nota, int chequeRut, string chequeDgv, string chequeNombres,
                                                                               int mandatoRut, string mandatoDgv, string mandatoNombres, int funcionarioCreaRut, int organismo, int indicaATPrevia)
        {
            var _insertaRatificaSolicitud = contratoDAO.InsertaRatificacion(codigoOrdenAT, lineaOrigen, lineaAplicacion, tipoLlamado, montoUF,
                        montoPesos, nota, chequeRut, chequeDgv, chequeNombres, mandatoRut, mandatoDgv, mandatoNombres, funcionarioCreaRut, organismo, indicaATPrevia);

            contratoSolicitudPagoEntities contratoSolicitudPagoEntities = new contratoSolicitudPagoEntities();
            if (_insertaRatificaSolicitud != null)
            {
                return new contratoSolicitudPagoEntities
                {
                    mensajeSalidaRatifica = _insertaRatificaSolicitud.Mensaje,
                    codigoSalidaRatifica = _insertaRatificaSolicitud.Estado.ToString()
                };
            }
            else
                return contratoSolicitudPagoEntities;
        }

        internal static void actualizaSolicitud(long idSolicitud, int estadoSolicitud)
        {
            contratoDAO.ActualizaEstadoSolicitud(idSolicitud, estadoSolicitud);
        }

        internal static contratoSolicitudPagoEntities obtieneSolicitudATPrevia(long idContrato)
        {
            var _obtieneSolicitud = contratoDAO.GetSolicitudATPrevia(idContrato);

            contratoSolicitudPagoEntities contratoSolicitudPagoEntities = new contratoSolicitudPagoEntities();

            if (_obtieneSolicitud != null)
            {
                return new contratoSolicitudPagoEntities
                {

                    montoPagados = (int)_obtieneSolicitud.MONTOPAGADOSOLICITUDPAGOATP,
                    montoComprometidos = (int)_obtieneSolicitud.MONTOCOMPROMETIDOSOLICITUDPAGOATP,
                    montoPorPagar = (int)_obtieneSolicitud.MONTOPORPAGARSOLICITUDPAGOATP,
                    numeroBolFact = (int)_obtieneSolicitud.NUMEROBOLETAFACTURASOLICITUDPAGOATP,
                    fechaBolFact = (DateTime)_obtieneSolicitud.FECHABOLETAFACTURASOLICITUDPAGOATP,
                    montoTotalaPagar = (int)_obtieneSolicitud.MONTOTOTALAPAGARSOLICITUDPAGOATP,
                    vbServicio = (bool)_obtieneSolicitud.VBSERVICIOSOLICITUDPAGOATP,
                    fechaVB = (DateTime)_obtieneSolicitud.VBFECHASOLICITUDPAGOATP,
                };
            }
            return contratoSolicitudPagoEntities;
        }
    }
}
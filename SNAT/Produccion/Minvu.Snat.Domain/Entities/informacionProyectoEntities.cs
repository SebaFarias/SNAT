using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Minvu.Snat.IData.DAO;
using Minvu.Snat.IData.ORM;
using Minvu.Snat.Helper;



namespace Minvu.Snat.Domain.Entities
{
    public class informacionProyectoEntities
    {
        public long idInformacionProyecto { get; set; }
        public long idDireccion { get; set; }
        public long idProveedor { get; set; }
        public long idMaestroTipologia { get; set; }
        public long idMaestroTitulo { get; set; }
        public long idMaestroModalidad { get; set; }
        public long idMaestroLlamado { get; set; }
        public long idMaestroPrograma { get; set; }
        public long idMaestroResolucion { get; set; }
        public long idMaestroAlternativaPostulacion { get; set; }
       public List<OperacionesMuninEntities> _OperacionesMuninEntities2 { get; set; }


        [Display(Name = "Código proyecto:")]
        [Required(ErrorMessage = "El codigo del proyecto es obligatorio")]
        [RegularExpression(@"(^[0-9]*$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string codigoProyectoInformacionProyecto { get; set; }

        [Display(Name = "Nombre proyecto:")]
        // [Required(ErrorMessage = "El nombre del proyecto es obligatorio")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string nombreProyectoInformacionProyecto { get; set; }

        [Display(Name = "Fecha calificación definitiva:")]
        [Required(ErrorMessage = "La fecha es obligatoria")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaCalificacionDefinitivaInformacionProyecto { get; set; }

        [Display(Name = "Cantidad viviendas:")]
        [Required(ErrorMessage = "La cantidad de vivienda del proyecto es obligatorio")]
        [RegularExpression(@"(^[0-9]*$)", ErrorMessage = "Existen caracteres inválidos.")]
        public int numeroViviendasInformacionProyecto { get; set; }
        public bool estadoProyectoInformacionProyecto { get; set; }

        [Display(Name = "Resolución AT:")]
        [Required(ErrorMessage = "La resolución del proyecto es obligatorio")]
        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string resolucionATInformacionProyecto { get; set; }

        [Display(Name = "Fecha AT:")]
        [Required(ErrorMessage = "La fecha es obligatoria")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaATInformacionProyecto { get; set; }

        [Display(Name = "N° de familias a pagar:")]
        [Required(ErrorMessage = "La cantidad de beneficiarios del proyecto es obligatorio")]
        [RegularExpression(@"(^[0-9]*$)", ErrorMessage = "Existen caracteres inválidos.")]
        public int cantidadBeneficiariosInformacionProyecto { get; set; }

        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ°'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string numeroCertificadoPPPFConsulta { get; set; }

        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]+$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string codigoProyectoPPPFConsulta { get; set; }

        [RegularExpression(@"(^[0-9]*$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string idContratoPPPFConsulta { get; set; }

        [RegularExpression(@"(^[0-9A-ZñÑáéíóúüÁÉÍÓÚÜ'´\-\.\,\'a-z ]{3,200}$)", ErrorMessage = "Debe contener al menos tres caracteres.")]
        public string nombreProyectoPPPFConsulta { get; set; }
        public string numeroCertificadoPPPF { get; set; }
        public string codigoProyectoPPPF { get; set; }
        public string nombreProyectoPPPF { get; set; }
        public string idProyectoPPPF { get; set; }
        public string dvProyectoPPPF { get; set; }

        //[Required(ErrorMessage = "El rut es obligatorio")]
        [RegularExpression(@"(^[1-9]\d*$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string rutProyectoPPPF { get; set; }
        public string profesionalFTO { get; set; }
        public string profesionalReemplazoFTO { get; set; }
        public string rutProfesionalFTO { get; set; }
        public string rutprofesionalReemplazoFTO { get; set; }
        public string dvprofesionalReemplazoFTO { get; set; }
        public int idContrato { get; set; }

        [Required(ErrorMessage = "El rut es obligatorio.")]
        [RegularExpression(@"(^[0-9]*$)", ErrorMessage = "Existen caracteres inválidos.")]
        public string rutProfesionalFTOConsulta { get; set; }
        public string dvProfesionalFTOConsulta { get; set; }
        public string mensajeSalida { get; set; }
        public string codigoSalida { get; set; }
        public string nombrePSATPPPF { get; set; }
        public string nombreSupervisor { get; set; }
        public string maestroTitulo { get; set; }
        public long? idMaestroEstadoProyecto { get; set; }
        public long? idMaestroEstadoBeneficio { get; set; }
        public long? idMaestroBanco { get; set; }
        [Display(Name = "% Avance de obras:")]
        public decimal? porcentajeAvanceObra { get; set; }
        [Display(Name = "Estado de avance de obras:")]
        public string estadoAvanceObra { get; set; }
        public bool? permisoUsuarioAdministrativo { get; set; }
        public string nombreUsuarioOtorgaPermiso { get; set; }

        public bool consultaMunin { get; set; }


        public informacionProyectoEntities()
        {
            codigoProyectoPPPF = string.Empty;
            idInformacionProyecto = 0;
            idDireccion = 0;
            idProveedor = 0;
            idMaestroTipologia = 0;
            idMaestroTitulo = 0;
            idMaestroModalidad = 0;
            idMaestroLlamado = 0;
            idMaestroPrograma = 0;
            idMaestroAlternativaPostulacion = 0;
            codigoProyectoInformacionProyecto = string.Empty;
            nombreProyectoInformacionProyecto = "";
            fechaCalificacionDefinitivaInformacionProyecto = new DateTime();
            numeroViviendasInformacionProyecto = 0;
            estadoProyectoInformacionProyecto = false;
            resolucionATInformacionProyecto = "";
            fechaATInformacionProyecto = new DateTime();
            cantidadBeneficiariosInformacionProyecto = 0;
            idMaestroResolucion = 0;
            numeroCertificadoPPPF = string.Empty;
            nombreProyectoPPPF = string.Empty;
            idProyectoPPPF = string.Empty;
            dvProyectoPPPF = string.Empty;
            rutProyectoPPPF = string.Empty;
            codigoProyectoPPPFConsulta = string.Empty;
            profesionalFTO = string.Empty;
            profesionalReemplazoFTO = string.Empty;
            rutProfesionalFTO = string.Empty;
            rutprofesionalReemplazoFTO = string.Empty;
            idContrato = 0;
            rutProfesionalFTOConsulta = string.Empty;
            dvProfesionalFTOConsulta = string.Empty;
            mensajeSalida = string.Empty;
            codigoSalida = string.Empty;
            nombrePSATPPPF = string.Empty;
            nombreSupervisor = string.Empty;
            maestroTitulo = string.Empty;
            idMaestroEstadoProyecto = null;
            idMaestroEstadoBeneficio = null;
            idMaestroBanco = null;
            porcentajeAvanceObra = null;
            estadoAvanceObra = string.Empty;
            permisoUsuarioAdministrativo = null;
            nombreUsuarioOtorgaPermiso = string.Empty;

        }





        public informacionProyectoEntities(int _idInformacionProyecto, int _idDireccion, int _idProveedor, int _idMaestroTipologia, int _idMaestroTitulo, int _idMaestroModalidad,
                                           int _idMaestroLlamado, int _idMaestroPrograma, int _idMaestroAlternativaPostulacion, string _codigoProyectoInformacionProyecto,
                                           string _nombreProyectoInformacionProyecto, DateTime _fechaCalificacionDefinitivaInformacionProyecto, int _numeroViviendasInformacionProyecto,
                                           bool _estadoProyectoInformacionProyecto, string _resolucionATInformacionProyecto, DateTime _fechaATInformacionProyecto,
                                           int _cantidadBeneficiariosInformacionProyecto, int _idMaestroResolucion, string _numeroCertificadoPPPF, string _codigoProyectoPPPF,
                                           string _nombreProyectoPPPF, string _idProyectoPPPF, string _dvProyectoPPPF, int _idContrato, long? _idMaestroEstadoProyecto,
                                           long? _idMaestroEstadoBeneficio, long? _idMaestroBanco, decimal? _porcentajeAvanceObra, string _estadoAvanceObra, bool? _permisoUsuarioAdministrativo, string _nombreUsuarioOtorgaPermiso)
        {
            idInformacionProyecto = _idInformacionProyecto;
            idDireccion = _idDireccion;
            idProveedor = _idProveedor;
            idMaestroTipologia = _idMaestroTipologia;
            idMaestroTitulo = idMaestroTitulo;
            idMaestroModalidad = _idMaestroModalidad;
            idMaestroLlamado = _idMaestroLlamado;
            idMaestroPrograma = _idMaestroPrograma;
            idMaestroAlternativaPostulacion = _idMaestroAlternativaPostulacion;
            codigoProyectoInformacionProyecto = _codigoProyectoInformacionProyecto;
            nombreProyectoInformacionProyecto = _nombreProyectoInformacionProyecto;
            fechaCalificacionDefinitivaInformacionProyecto = fechaCalificacionDefinitivaInformacionProyecto;
            numeroViviendasInformacionProyecto = _numeroViviendasInformacionProyecto;
            estadoProyectoInformacionProyecto = _estadoProyectoInformacionProyecto;
            resolucionATInformacionProyecto = resolucionATInformacionProyecto;
            fechaATInformacionProyecto = _fechaATInformacionProyecto;
            cantidadBeneficiariosInformacionProyecto = _cantidadBeneficiariosInformacionProyecto;
            idMaestroResolucion = _idMaestroResolucion;
            numeroCertificadoPPPF = _numeroCertificadoPPPF;
            codigoProyectoPPPF = _codigoProyectoPPPF;
            nombreProyectoPPPF = _nombreProyectoPPPF;
            idProyectoPPPF = _idProyectoPPPF;
            dvProyectoPPPF = _dvProyectoPPPF;
            idContrato = _idContrato;
            idMaestroEstadoProyecto = _idMaestroEstadoProyecto;
            idMaestroEstadoBeneficio = _idMaestroEstadoBeneficio;
            idMaestroBanco = _idMaestroBanco;
            porcentajeAvanceObra = _porcentajeAvanceObra;
            estadoAvanceObra = _estadoAvanceObra;
            permisoUsuarioAdministrativo = _permisoUsuarioAdministrativo;
            nombreUsuarioOtorgaPermiso = _nombreUsuarioOtorgaPermiso;
        }

    }

    public class informacionProyectoEntitiesFactory
    {
        internal static informacionProyectoEntities getinformacionProyectoEntities(string codigoProyecto)
        {
            var _informacionProyectoDAO = informacionProyectoDAO.get(codigoProyecto);
            if (_informacionProyectoDAO != null)
            {
                return new informacionProyectoEntities
                {
                    idInformacionProyecto = _informacionProyectoDAO.IDINFORMACIONPROYECTO,
                    idDireccion = Convert.ToInt64(_informacionProyectoDAO.IDDIRECCION),
                    idMaestroTitulo = Convert.ToInt64(_informacionProyectoDAO.IDMAESTROTITULO),
                    idMaestroModalidad = Convert.ToInt64(_informacionProyectoDAO.IDMAESTROMODALIDAD),
                    idMaestroLlamado = Convert.ToInt64(_informacionProyectoDAO.IDMAESTROLLAMADO),
                    idMaestroAlternativaPostulacion = Convert.ToInt64(_informacionProyectoDAO.IDMAESTROALTERNATIVAPOSTULACION),
                    codigoProyectoInformacionProyecto = _informacionProyectoDAO.CODIGOPROYECTOINFORMACIONPROYECTO.ToString(),
                    nombreProyectoInformacionProyecto = _informacionProyectoDAO.NOMBREPROYECTOINFORMACIONPROYECTO,
                    fechaCalificacionDefinitivaInformacionProyecto = Convert.ToDateTime(_informacionProyectoDAO.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO),
                    numeroViviendasInformacionProyecto = Convert.ToInt32(_informacionProyectoDAO.CANTIDADVIVIENDASINFORMACIONPROYECTO),
                    estadoProyectoInformacionProyecto = Convert.ToBoolean(_informacionProyectoDAO.ESTADOPROYECTOINFORMACIONPROYECTO),
                    resolucionATInformacionProyecto = _informacionProyectoDAO.RESOLUCIONATINFORMACIONPROYECTO,
                    fechaATInformacionProyecto = Convert.ToDateTime(_informacionProyectoDAO.FECHARESOLUCIONATINFORMACIONPROYECTO),
                    cantidadBeneficiariosInformacionProyecto = Convert.ToInt32(_informacionProyectoDAO.CANTIDADBENEFICIARIOSINFORMACIONPROYECTO),
                    idMaestroEstadoBeneficio = Convert.ToInt64(_informacionProyectoDAO.IDMAESTROESTADOBENEFICIO),
                    idMaestroBanco = Convert.ToInt64(_informacionProyectoDAO.IDMAESTROBANCO),
                    permisoUsuarioAdministrativo = _informacionProyectoDAO.PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTO,
                    nombreUsuarioOtorgaPermiso = _informacionProyectoDAO.NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTO
                };
            }
            else
                return null;
        }

        //Sobre carga de metodo getinformacionProyectoEntities con parametro adicional idPrograma
        public static informacionProyectoEntities getinformacionProyectoEntities(string CodigoProyecto, long idPrograma)
        {
            try
            {
                var _informacionProyectoDAO = informacionProyectoDAO.get(CodigoProyecto, idPrograma);


                decimal? auxPorcentajeAvanceObra = 0;
                string auxEstadoAvanceObra = string.Empty;
                bool verificarExistencia = false;


                if (_informacionProyectoDAO != null)
                {
                    informacionProyectoEntities _informacionProyectoEntities = new informacionProyectoEntities
                    {
                        idInformacionProyecto = _informacionProyectoDAO.IDINFORMACIONPROYECTO,
                        idDireccion = Convert.ToInt64(_informacionProyectoDAO.IDDIRECCION),
                        idMaestroPrograma = Convert.ToInt64(_informacionProyectoDAO.IDMAESTROPROGRAMA),
                        idMaestroTitulo = Convert.ToInt64(_informacionProyectoDAO.IDMAESTROTITULO),
                        idMaestroModalidad = Convert.ToInt64(_informacionProyectoDAO.IDMAESTROMODALIDAD),
                        idMaestroLlamado = Convert.ToInt64(_informacionProyectoDAO.IDMAESTROLLAMADO),
                        idMaestroAlternativaPostulacion = Convert.ToInt64(_informacionProyectoDAO.IDMAESTROALTERNATIVAPOSTULACION),
                        codigoProyectoInformacionProyecto = _informacionProyectoDAO.CODIGOPROYECTOINFORMACIONPROYECTO.ToString(),
                        nombreProyectoInformacionProyecto = _informacionProyectoDAO.NOMBREPROYECTOINFORMACIONPROYECTO,
                        fechaCalificacionDefinitivaInformacionProyecto = Convert.ToDateTime(_informacionProyectoDAO.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO),
                        numeroViviendasInformacionProyecto = Convert.ToInt32(_informacionProyectoDAO.CANTIDADVIVIENDASINFORMACIONPROYECTO),
                        estadoProyectoInformacionProyecto = Convert.ToBoolean(_informacionProyectoDAO.ESTADOPROYECTOINFORMACIONPROYECTO),
                        resolucionATInformacionProyecto = _informacionProyectoDAO.RESOLUCIONATINFORMACIONPROYECTO,
                        fechaATInformacionProyecto = Convert.ToDateTime(_informacionProyectoDAO.FECHARESOLUCIONATINFORMACIONPROYECTO),
                        cantidadBeneficiariosInformacionProyecto = Convert.ToInt32(_informacionProyectoDAO.CANTIDADBENEFICIARIOSINFORMACIONPROYECTO),
                        idMaestroEstadoProyecto = Convert.ToInt64(_informacionProyectoDAO.IDMAESTROESTADOPROYECTO),
                        idMaestroEstadoBeneficio = Convert.ToInt64(_informacionProyectoDAO.IDMAESTROESTADOBENEFICIO),
                        idMaestroBanco = Convert.ToInt64(_informacionProyectoDAO.IDMAESTROBANCO),
                        permisoUsuarioAdministrativo = _informacionProyectoDAO.PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTO,
                        nombreUsuarioOtorgaPermiso = _informacionProyectoDAO.NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTO,
                        porcentajeAvanceObra = _informacionProyectoDAO.AVANCEOBRAINFORMACIONPROYECTO,

                        
                    };

                    if (_informacionProyectoEntities.codigoProyectoInformacionProyecto != "")
                    {

                        MAESTRO_PROGRAMA _aux = maestroProgramaDAO.get(Convert.ToInt32(_informacionProyectoEntities.idMaestroPrograma));

                        direccionEntities _direccionEntities = direccionEntitiesFactory.getDireccion(_informacionProyectoEntities.idDireccion);
                        _informacionProyectoEntities._OperacionesMuninEntities2 = OperacionesMuninFactory.getAvanceObraTrazabilidadProyecto(Convert.ToInt32(_aux.CODIGOENTRAZABILIDAD), _informacionProyectoEntities.codigoProyectoInformacionProyecto.ToString(), Convert.ToInt32(_direccionEntities.codigoRegionDireccion), null, null, null);
                        //_informacionProyectoEntities._OperacionesMuninEntities2 = OperacionesMuninFactory.getAvanceObraTrazabilidadProyecto(Convert.ToInt32(idPrograma), _informacionProyectoEntities.codigoProyectoInformacionProyecto.ToString(), Convert.ToInt32(_direccionEntities.codigoRegionDireccion), null, null, null);

                        Log.Instance.Info("Ingresado a: _OperacionesMuninEntities nivel 1 y no se cae" );
                        try
                        {
                            if (_informacionProyectoEntities._OperacionesMuninEntities2 != null && _informacionProyectoEntities._OperacionesMuninEntities2.Count > 0)
                            {
                                Log.Instance.Info("Ingresado a: _OperacionesMuninEntities nivel 2 y no se cae");
                                if (_informacionProyectoEntities._OperacionesMuninEntities2.Count > 0)
                                {
                                    Log.Instance.Info("Ingresado a: _OperacionesMuninEntities nivel 3 y no se cae");
                                    foreach (var item in _informacionProyectoEntities._OperacionesMuninEntities2)
                                    {
                                        Log.Instance.Info("Ingresado a: _OperacionesMuninEntities nivel 4 y no se cae");

                                        //OperacionesMuninEntities aux = _OperacionesMuninEntities.First(x => x.codigoProyecto == _informacionProyectoEntities.codigoProyectoInformacionProyecto.ToString());
                                        if (item.avanceRealActual != null)
                                            if (auxPorcentajeAvanceObra < Convert.ToDecimal(item.avanceRealActual))
                                            {
                                                Log.Instance.Info("Ingresado a: _OperacionesMuninEntities nivel 5 funca y no se cae");
                                                item.avanceRealActual = item.avanceRealActual.Replace('.', ',');
                                                auxPorcentajeAvanceObra = Convert.ToDecimal(item.avanceRealActual);
                                                auxEstadoAvanceObra = item.estadoGeneralDescripcion;
                                                verificarExistencia = true;
                                            }

                                    }
                                }
                                else
                                {

                                    verificarExistencia = false;
                                }
                            }
                            else
                            {

                                verificarExistencia = false;
                            }
                        }
                        catch (Exception e)
                        {
                            Log.Instance.Error("Error en getinformacionProyectoEntities pp: " + e);
                            if (!verificarExistencia)
                                verificarExistencia = false;
                        }
                    }
                    else
                    {

                        verificarExistencia = false;
                    }


                    if (!verificarExistencia)
                    {

                        if (_informacionProyectoEntities.idMaestroPrograma != 5)
                        {
                            _informacionProyectoEntities.porcentajeAvanceObra = null;
                            _informacionProyectoEntities.estadoAvanceObra = "Sin información";
                        }
                    }
                    else
                    {
                        _informacionProyectoEntities.porcentajeAvanceObra = auxPorcentajeAvanceObra;
                        _informacionProyectoEntities.estadoAvanceObra = auxEstadoAvanceObra;
                    }

                    return _informacionProyectoEntities;
                }
                else
                    //return null;
                    return (new informacionProyectoEntities());
            }
            catch (Exception ex)
            {
                Log.Instance.Error("Error en getinformacionProyectoEntities: " + ex);
                return null;


            }

        }



        internal static informacionProyectoEntities deleteProyectoEntities(long idProyecto)
        {
            INFORMACION_PROYECTO _infoProyecto = new INFORMACION_PROYECTO();
            _infoProyecto.IDINFORMACIONPROYECTO = idProyecto;

            informacionProyectoDAO.ChangeStatus(_infoProyecto);

            return null;
        }

        internal static informacionProyectoEntities deleteProyectoPPPFEntities(string codProyecto, int idContrato, string usuario)
        {
            var _informacionProyectoDAO = webAsistecSnatiiiPPPFProyectoDAO.EliminaProyecto(codProyecto, idContrato, usuario);
            informacionProyectoEntities informacionProyectoEntities = new informacionProyectoEntities();
            if (_informacionProyectoDAO != null)
            {
                return new informacionProyectoEntities
                {
                    mensajeSalida = _informacionProyectoDAO.MSG,
                    codigoSalida = _informacionProyectoDAO.err.ToString()
                };
            }
            else
                return informacionProyectoEntities;
        }

        internal static informacionProyectoEntities changeProyectoPPPFEntities(string idProyecto, string numCertifiado, string usuario)
        {
            var _informacionProyectoDAO = webAsistecSnatiiiPPPFProyectoDAO.ModificaCertificado(idProyecto, numCertifiado, usuario);
            informacionProyectoEntities informacionProyectoEntities = new informacionProyectoEntities();
            if (_informacionProyectoDAO != null)
            {
                return new informacionProyectoEntities
                {
                    mensajeSalida = _informacionProyectoDAO.MSG,
                    codigoSalida = _informacionProyectoDAO.err.ToString()
                };
            }
            else
                return informacionProyectoEntities;
        }

        internal static informacionProyectoEntities modificaProfesionalEntities(int idProyecto, int rutProfesional, int rutProfReemplazo, int tipoContrato, string usuario)
        {
            var _informacionProyectoDAO = webAsistecSnatiiiPPPFProyectoDAO.ModificaProfesional(idProyecto, rutProfesional, rutProfReemplazo, tipoContrato, usuario);
            informacionProyectoEntities informacionProyectoEntities = new informacionProyectoEntities();
            if (_informacionProyectoDAO != null)
            {
                return new informacionProyectoEntities
                {
                    mensajeSalida = _informacionProyectoDAO.MSG,
                    codigoSalida = _informacionProyectoDAO.err.ToString()
                };
            }
            else
                return informacionProyectoEntities;
        }

        internal static List<informacionProyectoEntities> getinformacionProyectoPPPFRutEntities(string rutProyecto)
        {
            var _informacionProyectoDAO = webAsistecSnatiiiPPPFProyectoDAO.GetRut(rutProyecto);
            List<informacionProyectoEntities> informacionProyectoEntities = new List<informacionProyectoEntities>();
            if (_informacionProyectoDAO != null)
            {
                foreach (var a in _informacionProyectoDAO)
                {
                    informacionProyectoEntities info = new informacionProyectoEntities();
                    {
                        info.numeroCertificadoPPPF = a.PPPF_PRY_CER;
                        info.codigoProyectoPPPF = a.PPPF_PRY_COD;
                        info.nombreProyectoPPPF = a.PPPF_PRY_NOM;
                        info.idProyectoPPPF = a.PPPF_PRY_ID.ToString();
                        info.maestroTitulo = a.PPPF_PRY_TIT.ToString().Replace("1", "Título I").Replace("2", "Título II").Replace("3", "Título III");
                        info.idContrato = Int32.Parse(a.COM_COD.ToString());
                        info.idMaestroLlamado = Convert.ToInt64(a.PPPF_NUM_LLAM);
                        info.idMaestroPrograma = Convert.ToInt64(a.PPPF_PRY_ANO);

                        informacionProyectoEntities.Add(info);
                    };
                }
                return informacionProyectoEntities;
            }
            else
                return informacionProyectoEntities;
        }

        internal static List<informacionProyectoEntities> getinformacionProyectoPPPFRutTodosEntities(string rutProyecto, string numCertifiado)
        {
            var _informacionProyectoDAO = webAsistecSnatiiiPPPFProyectoDAO.GetRutTodos(rutProyecto, numCertifiado);
            List<informacionProyectoEntities> informacionProyectoEntities = new List<informacionProyectoEntities>();
            if (_informacionProyectoDAO != null)
            {
                foreach (var a in _informacionProyectoDAO)
                {
                    informacionProyectoEntities info = new informacionProyectoEntities();
                    {
                        info.numeroCertificadoPPPF = a.PPPF_PRY_CER;
                        info.codigoProyectoPPPF = a.PPPF_PRY_COD;
                        info.nombreProyectoPPPF = a.PPPF_PRY_NOM;
                        info.idProyectoPPPF = a.PPPF_PRY_ID.ToString();
                        info.maestroTitulo = a.PPPF_PRY_TIT.ToString().Replace("1", "Título I").Replace("2", "Título II").Replace("3", "Título III");
                        info.idContrato = a.PPPF_PRY_ID;
                        info.idMaestroLlamado = Convert.ToInt64(a.PPPF_NUM_LLAM);
                        info.idMaestroPrograma = Convert.ToInt64(a.PPPF_PRY_ANO);

                        informacionProyectoEntities.Add(info);
                    };
                }
                return informacionProyectoEntities;
            }
            else
                return informacionProyectoEntities;
        }

        internal static List<informacionProyectoGrillaEntities> getConsultaProyectoPPPFEntities(int accion, int codProyecto, string rutProyecto, string nombreProyecto)
        {
            var _informacionProyectoDAO = webAsistecSnatiiiPPPFProyectoDAO.GetInfoProyecto(accion, codProyecto, rutProyecto, nombreProyecto);
            List<informacionProyectoGrillaEntities> informacionProyectoEntities = new List<informacionProyectoGrillaEntities>();
            if (_informacionProyectoDAO != null)
            {
                foreach (var a in _informacionProyectoDAO)
                {
                    informacionProyectoGrillaEntities info = new informacionProyectoGrillaEntities();

                    info.numeroCertificadoPPPF = a.PPPF_PRY_CER;
                    info.codigoProyectoPPPF = a.PPPF_PRY_COD;
                    info.nombreProyectoPPPF = a.PPPF_PRY_NOM;
                    info.idProyectoPPPF = a.PPPF_PRY_ID.ToString();
                    info.idMaestroTitulo = a.PPPF_PRY_TIT.ToString().Replace("1", "Título I").Replace("2", "Título II").Replace("3", "Título III");

                    if (a.PPPF_CTO_TIP == 0)
                    {
                        info.profesionalFTO = a.NOMBREITOSERVIU;
                        info.profesionalReemplazoFTO = a.NOMBREITORSERVIU;
                        info.rutProfesionalFTO = a.RUTITOSERVIU;
                        info.rutprofesionalReemplazoFTO = a.RUTITORSERVIU;
                    }
                    else
                    {
                        info.profesionalFTO = a.NOMBREITO;
                        info.profesionalReemplazoFTO = a.NOMBREITOR;
                        info.rutProfesionalFTO = a.RUTITO;
                        info.rutprofesionalReemplazoFTO = a.RUTITOR;
                    }

                    info.idContrato = Convert.ToInt32(a.PPPF_CTO_ID);
                    info.nombrePSATPPPF = a.PPPF_PSAT_NOM;
                    info.nombreSupervisor = a.NOMBRESPV;
                    info.tipoContrato = a.PPPF_CTO_TIP.ToString();

                    informacionProyectoEntities.Add(info);

                }

                return informacionProyectoEntities;
            }
            return informacionProyectoEntities;

        }

        internal static List<informacionProyectoGrillaEntities> getConsultaProyectoServiuPPPFEntities(int accion, int codProyecto, string rutProyecto, string nombreProyecto)
        {
            var _informacionProyectoDAO = webAsistecSnatiiiPPPFProyectoDAO.GetInfoProyectoServiu(accion, codProyecto, rutProyecto, nombreProyecto);
            List<informacionProyectoGrillaEntities> informacionProyectoEntities = new List<informacionProyectoGrillaEntities>();
            if (_informacionProyectoDAO != null)
            {
                foreach (var a in _informacionProyectoDAO)
                {
                    informacionProyectoGrillaEntities info = new informacionProyectoGrillaEntities();

                    info.numeroCertificadoPPPF = a.PPPF_PRY_CER;
                    info.codigoProyectoPPPF = a.PPPF_PRY_COD;
                    info.nombreProyectoPPPF = a.PPPF_PRY_NOM;
                    info.idProyectoPPPF = a.PPPF_PRY_ID.ToString();
                    info.idMaestroTitulo = a.PPPF_PRY_TIT.ToString().Replace("1", "Título I").Replace("2", "Título II").Replace("3", "Título III");

                    if (a.PPPF_CTO_TIP == 0)
                    {
                        info.profesionalFTO = a.NOMBREITOSERVIU;
                        info.profesionalReemplazoFTO = a.NOMBREITORSERVIU;
                        info.rutProfesionalFTO = a.RUTITOSERVIU;
                        info.rutprofesionalReemplazoFTO = a.RUTITORSERVIU;
                    }
                    else
                    {
                        info.profesionalFTO = a.NOMBREITO;
                        info.profesionalReemplazoFTO = a.NOMBREITOR;
                        info.rutProfesionalFTO = a.RUTITO;
                        info.rutprofesionalReemplazoFTO = a.RUTITOR;
                    }

                    info.idContrato = Convert.ToInt32(a.PPPF_CTO_ID);
                    info.nombrePSATPPPF = a.PPPF_PSAT_NOM;
                    info.nombreSupervisor = a.NOMBRESPV;
                    info.tipoContrato = a.PPPF_CTO_TIP.ToString();

                    informacionProyectoEntities.Add(info);

                }

                return informacionProyectoEntities;
            }
            return informacionProyectoEntities;

        }

        internal static List<informacionProyectoElimGrillaEntities> getConsultaProyectoPPPFElimEntities(int accion, int codProyecto, string rutProyecto, string nombreProyecto)
        {
            var _informacionProyectoDAO = webAsistecSnatiiiPPPFProyectoDAO.GetInfoProyecto(accion, codProyecto, rutProyecto, nombreProyecto);
            List<informacionProyectoElimGrillaEntities> informacionProyectoEntities = new List<informacionProyectoElimGrillaEntities>();
            if (_informacionProyectoDAO != null)
            {
                foreach (var a in _informacionProyectoDAO)
                {
                    informacionProyectoElimGrillaEntities info = new informacionProyectoElimGrillaEntities();

                    info.numeroCertificadoPPPF = a.PPPF_PRY_CER;
                    info.codigoProyectoPPPF = a.PPPF_PRY_COD;
                    info.nombreProyectoPPPF = a.PPPF_PRY_NOM;
                    info.idProyectoPPPF = a.PPPF_PRY_ID.ToString();
                    info.idMaestroTitulo = a.PPPF_PRY_TIT.ToString().Replace("1", "Título I").Replace("2", "Título II").Replace("3", "Título III");
                    info.profesionalFTO = a.NOMBREITO;
                    info.profesionalReemplazoFTO = a.NOMBREITOR;
                    info.idContrato = Convert.ToInt32(a.PPPF_CTO_ID);
                    info.nombrePSATPPPF = a.PPPF_PSAT_NOM;
                    info.nombreSupervisor = a.NOMBRESPV;

                    informacionProyectoEntities.Add(info);

                }

                return informacionProyectoEntities;
            }
            return informacionProyectoEntities;

        }

        internal static informacionProyectoEntities getinformacionProyectoPPPFRutRukanEntities(string rutProyecto)
        {
            var _informacionProyectoDAO = webAsistecSnatiiiPPPFProyectoDAO.GetRutRukan(rutProyecto);
            informacionProyectoEntities informacionProyectoEntities = new informacionProyectoEntities();
            if (_informacionProyectoDAO != null)
            {
                return new informacionProyectoEntities
                {
                    numeroCertificadoPPPF = _informacionProyectoDAO.PPPF_PRY_CER,
                    codigoProyectoPPPF = _informacionProyectoDAO.PPPF_PRY_COD,
                    nombreProyectoPPPF = _informacionProyectoDAO.PPPF_PRY_NOM,
                    idProyectoPPPF = _informacionProyectoDAO.PPPF_PRY_ID.ToString()
                };
            }
            else
                return informacionProyectoEntities;
        }

        internal static List<informacionProyectoEntities> getinformacionProyectoPPPFCertEntities(string numCertificado)
        {
            var _informacionProyectoDAO = webAsistecSnatiiiPPPFProyectoDAO.GetCertificado(numCertificado);
            List<informacionProyectoEntities> informacionProyectoEntities = new List<informacionProyectoEntities>();
            if (_informacionProyectoDAO != null)
            {
                foreach (var a in _informacionProyectoDAO)
                {
                    informacionProyectoEntities info = new informacionProyectoEntities();
                    {
                        info.numeroCertificadoPPPF = a.PPPF_PRY_CER;
                        info.codigoProyectoPPPF = a.PPPF_PRY_COD;
                        info.nombreProyectoPPPF = a.PPPF_PRY_NOM;
                        info.idProyectoPPPF = a.PPPF_PRY_ID.ToString();
                        info.maestroTitulo = a.PPPF_PRY_TIT.ToString().Replace("1", "Título I").Replace("2", "Título II").Replace("3", "Título III");
                        info.idContrato = a.PPPF_PRY_ID;
                        info.idMaestroLlamado = Convert.ToInt64(a.PPPF_NUM_LLAM);
                        info.idMaestroPrograma = Convert.ToInt64(a.PPPF_PRY_ANO);

                        informacionProyectoEntities.Add(info);
                    };
                }
                return informacionProyectoEntities;
            }
            else
                return informacionProyectoEntities;
        }

        internal static informacionProyectoEntities getinformacionProyectoPPPFCertRukanEntities(int accion, string rut, string numCertificado)
        {
            var _informacionProyectoDAO = rukanMigra.GetCertificadoRukan(accion, rut, numCertificado);
            informacionProyectoEntities informacionProyectoEntities = new informacionProyectoEntities();
            if (_informacionProyectoDAO != null)
            {
                return new informacionProyectoEntities
                {
                    numeroCertificadoPPPF = _informacionProyectoDAO.BNF_CER_NUM,
                    rutProyectoPPPF = _informacionProyectoDAO.PER_RUT,
                    dvProyectoPPPF = _informacionProyectoDAO.PER_DIG,
                    codigoProyectoPPPF = _informacionProyectoDAO.PER_RUT + "-" + _informacionProyectoDAO.PER_DIG,
                    nombreProyectoPPPF = _informacionProyectoDAO.ID_PER_NOM + " " + _informacionProyectoDAO.ID_PER_PAT + " " + _informacionProyectoDAO.ID_PER_MAT,
                    idMaestroLlamado = _informacionProyectoDAO.LLA_NUM,
                    idMaestroPrograma = _informacionProyectoDAO.LLA_ANO,
                    maestroTitulo = _informacionProyectoDAO.LIN_PRO_ID.ToString().Replace("15", "Título I").Replace("16", "Título II").Replace("162", "Título III"),
                    //idProyectoPPPF = _informacionProyectoDAO.ToString()
                };
            }
            else
                return informacionProyectoEntities;
        }


        //Verifica la existencia del proyecto en las otras lineas de proceso de SNAT  DS49,DS174,PPPF,DS105,DS49 reconstrución ,DS1
        internal static string verificarExistenciaProyectoSNAT(long idPrograma, string codigoProyecto)
        {
            return informacionProyectoDAO.verificarExistenciaProyectoSNAT(idPrograma, codigoProyecto);
        }
        internal static int verificarProyectoRegionUsuarioSNAT(long idPrograma, string codigoProyecto,int regionUsuario)
        {
            return informacionProyectoDAO.verificarProyectoRegionUsuarioSNAT(idPrograma, codigoProyecto, regionUsuario);
        }
    }
}

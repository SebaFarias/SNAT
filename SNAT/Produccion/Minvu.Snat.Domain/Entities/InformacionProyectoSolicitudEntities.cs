using Minvu.Snat.IData.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Domain.Entities
{
    public class InformacionProyectoSolicitudEntities
    {
        public long? idInformacionProyectoSolicitud { get; set; }
        public long? idInformacionProyecto { get; set; }
        public long? idMaestroPrograma { get; set; }
        public long? idMaestroLlamado { get; set; }
        public long? idMaestroModalidad { get; set; }
        public long? idMaestroTitulo { get; set; }
        public long? idMaestroResolucion { get; set; }
        public long? idDireccion { get; set; }
        public long? idMaestroTipologia { get; set; }
        public long? idMaestroAlternativaPostulacion { get; set; }
        public string CodigoProyectoInformacionProyectoSolicitud { get; set; }
        public string NombreProyectoInformacionProyectoSolicitud { get; set; }
        public DateTime? FechaCalificacionDefinitivaInformacionProyectoSolicitud { get; set; }
        public string ResolucionATInformacionProyectoSolicitud { get; set; }
        public DateTime? FechaResolucionATInformacionProyectoSolicitud { get; set; }
        public int? CantidadViviendasInformacionProyectoSolicitud { get; set; }
        public int? CantidadBeneficiariosInformacionProyectoSolicitud { get; set; }
        public decimal? MontoSubsidioBaseInformacionProyectoSolicitud { get; set; }
        public decimal? MontoSubsidioBaseFactibilidadInformacionProyectoSolicitud { get; set; }
        public bool? MarcaLocalizacionInformacionProyectoSolicitud { get; set; }
        public bool? MarcaFactibilizacionInformacionProyectoSolicitud { get; set; }
        public DateTime? FechaFactibilizacionInformacionProyectoSolicitud { get; set; }
        public int? NumeroFamiliasAdscritasInformacionProyectoSolicitud { get; set; }
        public int? AgnoFactibilidadInformacionProyectoSolicitud { get; set; }
        public long? idMaestroEstadoProyecto { get; set; }
        public DateTime? FechaIngresoInformacionProyectoSolicitud { get; set; }

        public InformacionProyectoSolicitudEntities()
        {
            idInformacionProyectoSolicitud = null;
            idInformacionProyecto = null;
            idMaestroPrograma = null;
            idMaestroLlamado = null;
            idMaestroModalidad = null;
            idMaestroTitulo = null;
            idMaestroResolucion = null;
            idDireccion = null;
            idMaestroTipologia = null;
            idMaestroAlternativaPostulacion = null;
            CodigoProyectoInformacionProyectoSolicitud = null;
            NombreProyectoInformacionProyectoSolicitud = String.Empty;
            FechaCalificacionDefinitivaInformacionProyectoSolicitud = null;
            ResolucionATInformacionProyectoSolicitud = String.Empty;
            FechaResolucionATInformacionProyectoSolicitud = null;
            CantidadViviendasInformacionProyectoSolicitud = null;
            CantidadBeneficiariosInformacionProyectoSolicitud = null;
            MontoSubsidioBaseInformacionProyectoSolicitud = null;
            MontoSubsidioBaseFactibilidadInformacionProyectoSolicitud = null;
            MarcaLocalizacionInformacionProyectoSolicitud = null;
            MarcaFactibilizacionInformacionProyectoSolicitud = null;
            FechaFactibilizacionInformacionProyectoSolicitud = null;
            NumeroFamiliasAdscritasInformacionProyectoSolicitud = null;
            AgnoFactibilidadInformacionProyectoSolicitud = null;
            idMaestroEstadoProyecto = null;
            FechaIngresoInformacionProyectoSolicitud = null;
        }

        public InformacionProyectoSolicitudEntities(long _idInformacionProyectoSolicitud, long _idInformacionProyecto, long _idMaestroPrograma, long _idMaestroLlamado,
                                                    long _idMaestroModalidad, long _idMaestroTitulo, long _idMaestroResolucion, long _idDireccion,
                                                    long _idMaestroTipologia, long _idMaestroAlternativaPostulacion, string _CodigoProyectoInformacionProyectoSolicitud,
                                                    string _NombreProyectoInformacionProyectoSolicitud, DateTime _FechaCalificacionDefinitivaInformacionProyectoSolicitud,
                                                    string _ResolucionATInformacionProyectoSolicitud, DateTime _FechaResolucionATInformacionProyectoSolicitud,
                                                    int _CantidadViviendasInformacionProyectoSolicitud, int _CantidadBeneficiariosInformacionProyectoSolicitud,
                                                    decimal _MontoSubsidioBaseInformacionProyectoSolicitud, decimal _MontoSubsidioBaseFactibilidadInformacionProyectoSolicitud,
                                                    bool _MarcaLocalizacionInformacionProyectoSolicitud, bool _MarcaFactibilizacionInformacionProyectoSolicitud,
                                                    DateTime _FechaFactibilizacionInformacionProyectoSolicitud, int _NumeroFamiliasAdscritasInformacionProyectoSolicitud,
                                                    int _AgnoFactibilidadInformacionProyectoSolicitud, long _idMaestroEstadoProyecto, DateTime _FechaIngresoInformacionProyectoSolicitud)
        {
            idInformacionProyectoSolicitud = _idInformacionProyectoSolicitud;
            idInformacionProyecto = _idInformacionProyecto;
            idMaestroPrograma = _idMaestroPrograma;
            idMaestroLlamado = _idMaestroLlamado;
            idMaestroModalidad = _idMaestroModalidad;
            idMaestroTitulo = _idMaestroTitulo;
            idMaestroResolucion = _idMaestroResolucion;
            idDireccion = _idDireccion;
            idMaestroTipologia = _idMaestroTipologia;
            idMaestroAlternativaPostulacion = _idMaestroAlternativaPostulacion;
            CodigoProyectoInformacionProyectoSolicitud = _CodigoProyectoInformacionProyectoSolicitud;
            NombreProyectoInformacionProyectoSolicitud = _NombreProyectoInformacionProyectoSolicitud;
            FechaCalificacionDefinitivaInformacionProyectoSolicitud = _FechaCalificacionDefinitivaInformacionProyectoSolicitud;
            ResolucionATInformacionProyectoSolicitud = _ResolucionATInformacionProyectoSolicitud;
            FechaResolucionATInformacionProyectoSolicitud = _FechaResolucionATInformacionProyectoSolicitud;
            CantidadViviendasInformacionProyectoSolicitud = _CantidadViviendasInformacionProyectoSolicitud;
            CantidadBeneficiariosInformacionProyectoSolicitud = _CantidadBeneficiariosInformacionProyectoSolicitud;
            MontoSubsidioBaseInformacionProyectoSolicitud = _MontoSubsidioBaseInformacionProyectoSolicitud;
            MontoSubsidioBaseFactibilidadInformacionProyectoSolicitud = _MontoSubsidioBaseFactibilidadInformacionProyectoSolicitud;
            MarcaLocalizacionInformacionProyectoSolicitud = _MarcaLocalizacionInformacionProyectoSolicitud;
            MarcaFactibilizacionInformacionProyectoSolicitud = _MarcaFactibilizacionInformacionProyectoSolicitud;
            FechaFactibilizacionInformacionProyectoSolicitud = _FechaFactibilizacionInformacionProyectoSolicitud;
            NumeroFamiliasAdscritasInformacionProyectoSolicitud = _NumeroFamiliasAdscritasInformacionProyectoSolicitud;
            AgnoFactibilidadInformacionProyectoSolicitud = _AgnoFactibilidadInformacionProyectoSolicitud;
            idMaestroEstadoProyecto = _idMaestroEstadoProyecto;
            FechaIngresoInformacionProyectoSolicitud = _FechaIngresoInformacionProyectoSolicitud;
        }
    }

    public class InformacionProyectoSolicitudEntitiesFactory
    {
        internal static InformacionProyectoSolicitudEntities getInformacionProyectoSolicitud(long idInformacionProyectoSolicitud)
        {
            var _informacionProyectoSolicitudDAO = InformacionProyectoSolicitudDAO.get(idInformacionProyectoSolicitud);
            if (_informacionProyectoSolicitudDAO != null)
            {
                return new InformacionProyectoSolicitudEntities
                {
                    idInformacionProyectoSolicitud = _informacionProyectoSolicitudDAO.IDINFORMACIONPROYECTOSOLICITUD,
                    idInformacionProyecto = _informacionProyectoSolicitudDAO.IDINFORMACIONPROYECTO,
                    idMaestroPrograma = _informacionProyectoSolicitudDAO.IDMAESTROPROGRAMA,
                    idMaestroLlamado = _informacionProyectoSolicitudDAO.IDMAESTROLLAMADO,
                    idMaestroModalidad = _informacionProyectoSolicitudDAO.IDMAESTROMODALIDAD,
                    idMaestroTitulo = _informacionProyectoSolicitudDAO.IDMAESTROTITULO,
                    idMaestroResolucion = _informacionProyectoSolicitudDAO.IDMAESTRORESOLUCION,
                    idDireccion = _informacionProyectoSolicitudDAO.IDDIRECCION,
                    idMaestroTipologia = _informacionProyectoSolicitudDAO.IDMAESTROTIPOLOGIA,
                    idMaestroAlternativaPostulacion = _informacionProyectoSolicitudDAO.IDMAESTROALTERNATIVAPOSTULACION,
                    CodigoProyectoInformacionProyectoSolicitud = _informacionProyectoSolicitudDAO.CODIGOPROYECTOINFORMACIONPROYECTOSOLICITUD,
                    NombreProyectoInformacionProyectoSolicitud = _informacionProyectoSolicitudDAO.NOMBREPROYECTOINFORMACIONPROYECTOSOLICITUD,
                    FechaCalificacionDefinitivaInformacionProyectoSolicitud = _informacionProyectoSolicitudDAO.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTOSOLICITUD,
                    ResolucionATInformacionProyectoSolicitud = _informacionProyectoSolicitudDAO.RESOLUCIONATINFORMACIONPROYECTOSOLICITUD,
                    FechaResolucionATInformacionProyectoSolicitud = _informacionProyectoSolicitudDAO.FECHARESOLUCIONATINFORMACIONPROYECTOSOLICITUD,
                    CantidadViviendasInformacionProyectoSolicitud = _informacionProyectoSolicitudDAO.CANTIDADVIVIENDASINFORMACIONPROYECTOSOLICITUD,
                    CantidadBeneficiariosInformacionProyectoSolicitud = _informacionProyectoSolicitudDAO.CANTIDADBENEFICIARIOSINFORMACIONPROYECTOSOLICITUD,
                    MontoSubsidioBaseInformacionProyectoSolicitud = _informacionProyectoSolicitudDAO.MONTOSUBSIDIOBASEINFORMACIONPROYECTOSOLICITUD,
                    MontoSubsidioBaseFactibilidadInformacionProyectoSolicitud = _informacionProyectoSolicitudDAO.MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTOSOLICITUD,
                    MarcaLocalizacionInformacionProyectoSolicitud = _informacionProyectoSolicitudDAO.MARCALOCALIZACIONINFORMACIONPROYECTOSOLICITUD,
                    MarcaFactibilizacionInformacionProyectoSolicitud = _informacionProyectoSolicitudDAO.MARCAFACTIBILIZACIONINFORMACIONPROYECTOSOLICITUD,
                    FechaFactibilizacionInformacionProyectoSolicitud = _informacionProyectoSolicitudDAO.FECHAFACTIBILIZACIONINFORMACIONPROYECTOSOLICITUD,
                    NumeroFamiliasAdscritasInformacionProyectoSolicitud = _informacionProyectoSolicitudDAO.NUMEROFAMILIASADSCRITASINFORMACIONPROYECTOSOLICITUD,
                    AgnoFactibilidadInformacionProyectoSolicitud = _informacionProyectoSolicitudDAO.AGNOFACTIBILIDADINFORMACIONPROYECTOSOLICITUD,
                    idMaestroEstadoProyecto = _informacionProyectoSolicitudDAO.IDMAESTROESTADOPROYECTO,
                    FechaIngresoInformacionProyectoSolicitud = _informacionProyectoSolicitudDAO.FECHAINGRESOINFORMACIONPROYECTOSOLICITUD,
                };
            }
            else
                return null;
        }

        internal static informacionProyectoEntities getInfoProyectoCodigoIdSolicitud(string CodProyecto, long idPrograma, long idSolicitudPago)
        {
            var _informacionProyectoSolicitudDAO = InformacionProyectoSolicitudDAO.getgetInfoProyectoCodigoIdSolicitud(CodProyecto, idPrograma, idSolicitudPago);

            if (_informacionProyectoSolicitudDAO != null)
            {
                informacionProyectoEntities _informacionProyectoEntities = new informacionProyectoEntities
                {
                    idInformacionProyecto = _informacionProyectoSolicitudDAO.IDINFORMACIONPROYECTO,
                    idMaestroPrograma = Convert.ToInt64(_informacionProyectoSolicitudDAO.IDMAESTROPROGRAMA),
                    idDireccion = Convert.ToInt64(_informacionProyectoSolicitudDAO.IDDIRECCION),
                    idMaestroTitulo = Convert.ToInt64(_informacionProyectoSolicitudDAO.IDMAESTROTITULO),
                    idMaestroModalidad = Convert.ToInt64(_informacionProyectoSolicitudDAO.IDMAESTROMODALIDAD),
                    idMaestroLlamado = Convert.ToInt64(_informacionProyectoSolicitudDAO.IDMAESTROLLAMADO),
                    idMaestroAlternativaPostulacion = Convert.ToInt64(_informacionProyectoSolicitudDAO.IDMAESTROALTERNATIVAPOSTULACION),
                    codigoProyectoInformacionProyecto = _informacionProyectoSolicitudDAO.CODIGOPROYECTOINFORMACIONPROYECTOSOLICITUD.ToString(),
                    nombreProyectoInformacionProyecto = _informacionProyectoSolicitudDAO.NOMBREPROYECTOINFORMACIONPROYECTOSOLICITUD,
                    fechaCalificacionDefinitivaInformacionProyecto = Convert.ToDateTime(_informacionProyectoSolicitudDAO.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTOSOLICITUD),
                    //fechaCalificacionDefinitivaInformacionProyecto = Convert.ToDateTime(_informacionProyectoSolicitudDAO.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTOSOLICITUD).ToString("dd/MM/yyyy"),
                    numeroViviendasInformacionProyecto = Convert.ToInt32(_informacionProyectoSolicitudDAO.CANTIDADVIVIENDASINFORMACIONPROYECTOSOLICITUD),
                    resolucionATInformacionProyecto = _informacionProyectoSolicitudDAO.RESOLUCIONATINFORMACIONPROYECTOSOLICITUD,
                    fechaATInformacionProyecto = Convert.ToDateTime(_informacionProyectoSolicitudDAO.FECHARESOLUCIONATINFORMACIONPROYECTOSOLICITUD),
                    cantidadBeneficiariosInformacionProyecto = Convert.ToInt32(_informacionProyectoSolicitudDAO.CANTIDADBENEFICIARIOSINFORMACIONPROYECTOSOLICITUD),
                    idMaestroEstadoProyecto = Convert.ToInt64(_informacionProyectoSolicitudDAO.IDMAESTROESTADOPROYECTO),
                    estadoAvanceObra  = _informacionProyectoSolicitudDAO.ESTADOAVANCEOBRAINFORMACIONPROYECTOSOLICITUD ,
                    porcentajeAvanceObra = _informacionProyectoSolicitudDAO.AVANCEOBRAINFORMACIONPROYECTOSOLICITUD,
                
            };


//                if (_informacionProyectoEntities.codigoProyectoInformacionProyecto != 0)
//                {
////                    direccionEntities _direccionEntities = direccionEntitiesFactory.getDireccion(_informacionProyectoEntities.idDireccion);
//                    //OperacionesMuninEntities _OperacionesMuninEntities = OperacionesMuninFactory.getAvanceObraTrazabilidadProyecto(Convert.ToInt32(_informacionProyectoEntities.idMaestroPrograma), _informacionProyectoEntities.codigoProyectoInformacionProyecto.ToString(), Convert.ToInt32(_direccionEntities.codigoRegionDireccion), null, null, null);


//                    //OperacionesMuninEntities aux = _OperacionesMuninEntities.First(x => x.codigoProyecto == _informacionProyectoEntities.codigoProyectoInformacionProyecto.ToString());

                    
//                }

                return _informacionProyectoEntities;

            }
            else
                return null;
        }
    }
}

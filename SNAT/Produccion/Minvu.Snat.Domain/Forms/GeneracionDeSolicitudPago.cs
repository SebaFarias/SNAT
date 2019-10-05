using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.Domain.Entities;
using Minvu.Snat.Domain.Controls;
using System.Web.Mvc;
using Minvu.Snat.Helper;

namespace Minvu.Snat.Domain.Forms
{
    public class GeneracionDeSolicitudPago
    {
        public GeneracionDeSolicitudPago() { }

        public List<Document> _listDocument { get; set; }
        public maestroClaseEntities _maestroClaseEntities { get; set; }
        public informacionProyectoEntities _informacionProyectoEntities { get; set; }
        public maestroTipoPagoEntities _maestroTipoPagoEntities { get; set; }
        public tipoProveedorInformacionProyectoEntities _tipoProveedorInformacionProyectoEntities { get; set; }
        public maestroLlamadoEntities _maestroLlamadoEntities { get; set; }
        public List<maestroProgramaEntities> _listMaestroProgramaEntities { get; set; }
        public List<tipoProveedorInformacionProyectoEntities> _listTipoProveedorInformacionProyectoEntities { get; set; }
        public List<maestroTipoPagoEntities> _listMaestroTipoPagoEntities { get; set; }
        public maestroProgramaEntities _maestroProgramaEntities { get; set; }
        public maestroModalidadEntities _maestroModalidadEntities { get; set; }
        public maestroTipologiaEntities _maestroTipologiaEntities { get; set; }
        public maestroTituloEntities _maestroTituloEntities { get; set; }
        public List<maestroTipoProveedorEntities> _listMaestroTipoProveedorEntities { get; set; }
        public maestroAlternativaPostulacionEntities _maestroAlternativaPostulacionEntities { get; set; }
        public maestroTipoProveedorEntities _maestroTipoProveedorEntities { get; set; }
        public maestroResolucionEntities _maestroResolucionEntities { get; set; }
        public maestroEstadoProyectoEntities _maestroEstadoProyectoEntities { get; set; }
        public MaestroEstadoBeneficioEntities _MaestroEstadoBeneficioEntities { get; set; }
        public MaestroBancoEntities _MaestroBancoEntities { get; set; }
        public proveedorEntities _proveedorTipo { get; set; }
        public proveedorEntities _proveedorEP { get; set; }
        public proveedorEntities _proveedorMandato { get; set; }
        public proveedorEntities _proveedorEC { get; set; }
        public List<proveedorEntities> _listProveedorEC { get; set; }
        public List<proveedorEntities> _listProveedorFTO { get; set; }
        public proveedorEntities _proveedorFTO { get; set; }
        public direccionEntities _direccionEntities { get; set; }
        public regionControl _regionControl { get; set; }
        public provinciaControl _provinciaControl { get; set; }
        public comunaControl _comunaControl { get; set; }
        public caracteristicasEspecialesEntities _caracteristicasEspecialesEntities { get; set; }
        public List<auxPlantillaEntities> _auxPlantillaEntities { get; set; }
        public List<auxPlantillaEntities> _aux2PlantillaEntities { get; set; }
        public programaControl _programaControl { get; set; }
        public solicitudPagoEntities _solicitudPago { get; set; }
        public solicitudPagoEntities _solicitudPagoServicios { get; set; }
        public solicitudPagoEntities _solicitudPagoFiscalizacionTecnicaDeObra { get; set; }
        public maestroIncrementoEntities _maestroIncrementoEntities { get; set; }
        public List<maestroIncrementoEntities> _listMaestroIncrementoEntities { get; set; }
        public List<tipoIncrementoEntities> _listTipoIncrementoEntities { get; set; }

        public tipoServicioParcialidadCaracteristicaEntities _tipoServicioParcialidadCaracteristicaEntities { get; set; }
        public List<tipoServicioParcialidadCaracteristicaEntities> _ListTipoServicioParcialidadCaracteristicaEntities { get; set; }

        public OperacionesMuninEntities _operacionesMuninEntities = new OperacionesMuninEntities();

        public auxPlantillaEntities _auxPlantillaEntities3 { get; set; }

        public GeneracionDeSolicitudPago(string CodigoProyecto, long idPrograma)
        {
            _informacionProyectoEntities = new informacionProyectoEntities();
            _informacionProyectoEntities.codigoProyectoInformacionProyecto = CodigoProyecto;
            _informacionProyectoEntities.idMaestroPrograma = idPrograma;
        }
    }

    public class servicio
    {
        public string nombreServicio { get; set; }

        public servicio()
        {
            nombreServicio = String.Empty;
        }
        public servicio(string _nombreServicio)
        {
            nombreServicio = _nombreServicio;
        }
    }
    public class parcialidad
    {
        public int id { get; set; }
        public int monto { get; set; }

        public parcialidad()
        {
            id = 0;
            monto = 0;
        }
        public parcialidad(int _id, int _monto)
        {
            id = _id;
            monto = _monto;
        }
    }


    public class GeneracionDeSolicitudPagoFactory
    {
        public static proveedorEntities getProveedorIdProyectoIdTipo(long idInfoProyecto, long idTipoProveedor)
        {
            proveedorEntities objProveedor = new proveedorEntities();

            objProveedor = proveedorEntitiesFactory.getProveedorIdProyectoIdTipo(idInfoProyecto, idTipoProveedor);

            return objProveedor;
        }

        public static proveedorEntities getProveedor(long idProveedor)
        {
            proveedorEntities objProveedor = new proveedorEntities();

            objProveedor = proveedorEntitiesFactory.getProveedor(idProveedor);

            return objProveedor;
        }

        public static proveedorEntities getProveedorxNombre(string NomProveedor)
        {
            proveedorEntities objProveedor = new proveedorEntities();

            objProveedor = proveedorEntitiesFactory.getProveedorxNombre(NomProveedor);

            return objProveedor;
        }

        public static proveedorEntities getProveedorxRut(int rut, string dv)
        {
            proveedorEntities objProveedor = new proveedorEntities();

            informacionProfesionalesEntities objRegistroCivil = new informacionProfesionalesEntities();
            objProveedor = proveedorEntitiesFactory.getProveedorRut(rut);

            if(objProveedor.nombreProveedor == string.Empty)
            {
                objRegistroCivil = informacionProfesionalesEntitiesFactory.getProfesionalRegistroCivil(rut, Convert.ToChar(dv));
            }

            if(objRegistroCivil.nombreProfesional != string.Empty)
            {
                objProveedor.nombreProveedor = objRegistroCivil.nombreProfesional + " " + objRegistroCivil.apellidoPaternoProfesional + " " + objRegistroCivil.apellidoMaternoProfesional;
            }

                
            return objProveedor;
        }

        public static maestroTipoProveedorEntities getMaestroTipoProveedor(long idTipoProveedor)
        {
            maestroTipoProveedorEntities objTipoProveedor = new maestroTipoProveedorEntities();

            objTipoProveedor = maestroTipoProveedorEntitiesFactory.getTipoProveedor(idTipoProveedor);

            return objTipoProveedor;
        }

        public static caracteristicasEspecialesEntities getCaracteristicaEspecial(long idCaracEspecial)
        {
            caracteristicasEspecialesEntities objCaracEspecial = new caracteristicasEspecialesEntities();

            objCaracEspecial = caracteristicasEspecialesEntitiesFactory.getCaracteristicaEspecial(idCaracEspecial);

            return objCaracEspecial;
        }

        public static string getNombrePrograma(long idPrograma)
        {
            maestroProgramaEntities _maestroProgramaEntities = maestroProgramaEntitiesFactory.getMaestroPrograma(idPrograma);

            return _maestroProgramaEntities.nombreMaestroPrograma;
        }

        public static object getInstanciasFormulario()
        {
            GeneracionDeSolicitudPago _GeneracionDeSolicitudPago = new GeneracionDeSolicitudPago();

            _GeneracionDeSolicitudPago._listMaestroTipoPagoEntities = maestroTipoPagoEntitiesFactory.getListMaestroTipoPago();
            _GeneracionDeSolicitudPago._listMaestroTipoPagoEntities = _GeneracionDeSolicitudPago._listMaestroTipoPagoEntities.OrderBy(x => x.nombreMaestroTipoPago).ToList();
            _GeneracionDeSolicitudPago._listMaestroProgramaEntities = maestroProgramaEntitiesFactory.getListMaestroPrograma();
            _GeneracionDeSolicitudPago._listMaestroProgramaEntities = _GeneracionDeSolicitudPago._listMaestroProgramaEntities.OrderBy(x => x.nombreMaestroPrograma).ToList();
            _GeneracionDeSolicitudPago._listMaestroTipoProveedorEntities = maestroTipoProveedorEntitiesFactory.getListTipoProveedor();
            _GeneracionDeSolicitudPago._listMaestroTipoProveedorEntities = _GeneracionDeSolicitudPago._listMaestroTipoProveedorEntities.OrderBy(x => x.nombreMaestroTipoProveedor).ToList();

            return _GeneracionDeSolicitudPago;
        }

        public static object getProyecto(string codProyecto, long? idPrograma, long? idSolicitudPago)
        {
            GeneracionDeSolicitudPago _GeneracionDeSolicitudPago = new GeneracionDeSolicitudPago();

            if (idSolicitudPago != null)
            {
                _GeneracionDeSolicitudPago._informacionProyectoEntities = InformacionProyectoSolicitudEntitiesFactory.getInfoProyectoCodigoIdSolicitud(codProyecto, Convert.ToInt64(idPrograma), Convert.ToInt64(idSolicitudPago));
            }
            else
            {
                _GeneracionDeSolicitudPago._informacionProyectoEntities = informacionProyectoEntitiesFactory.getinformacionProyectoEntities(codProyecto, Convert.ToInt64(idPrograma));
            }

            if (_GeneracionDeSolicitudPago._informacionProyectoEntities.idInformacionProyecto > 0)
            {
                _GeneracionDeSolicitudPago._caracteristicasEspecialesEntities = caracteristicasEspecialesEntitiesFactory.getCaracEspecialesIdInformacionProyecto(_GeneracionDeSolicitudPago._informacionProyectoEntities.idInformacionProyecto);
            }

            _GeneracionDeSolicitudPago._listMaestroProgramaEntities = maestroProgramaEntitiesFactory.getListMaestroPrograma();
            _GeneracionDeSolicitudPago._listMaestroProgramaEntities = _GeneracionDeSolicitudPago._listMaestroProgramaEntities.OrderBy(x => x.nombreMaestroPrograma).ToList();
            _GeneracionDeSolicitudPago._listMaestroTipoProveedorEntities = maestroTipoProveedorEntitiesFactory.getListTipoProveedor();
            _GeneracionDeSolicitudPago._listMaestroTipoProveedorEntities = _GeneracionDeSolicitudPago._listMaestroTipoProveedorEntities.OrderBy(x => x.nombreMaestroTipoProveedor).ToList();

            if (_GeneracionDeSolicitudPago._informacionProyectoEntities.codigoProyectoInformacionProyecto != string.Empty && _GeneracionDeSolicitudPago._informacionProyectoEntities != null)
            {
                _GeneracionDeSolicitudPago._maestroLlamadoEntities = maestroLlamadoEntitiesFactory.getLlamado(_GeneracionDeSolicitudPago._informacionProyectoEntities.idMaestroLlamado);
                _GeneracionDeSolicitudPago._maestroTipologiaEntities = maestroTipologiaEntitiesFactory.getTipologia(Convert.ToInt64(_GeneracionDeSolicitudPago._caracteristicasEspecialesEntities.idMaestroTipologia));
                _GeneracionDeSolicitudPago._maestroModalidadEntities = maestroModalidadEntitiesFactory.getMaestroModalidad(Convert.ToInt64(_GeneracionDeSolicitudPago._caracteristicasEspecialesEntities.idMaestroModalidad));
                _GeneracionDeSolicitudPago._maestroTituloEntities = maestroTituloEntitiesFactory.getMaestroTitulo(_GeneracionDeSolicitudPago._informacionProyectoEntities.idMaestroTitulo);
                _GeneracionDeSolicitudPago._maestroTituloEntities = maestroTituloEntitiesFactory.getMaestroTitulo(_GeneracionDeSolicitudPago._informacionProyectoEntities.idMaestroTitulo);

                _GeneracionDeSolicitudPago._maestroAlternativaPostulacionEntities = maestroAlternativaPostulacionEntitiesFactory.getAlternativaPostulacion(_GeneracionDeSolicitudPago._informacionProyectoEntities.idMaestroAlternativaPostulacion);
                _GeneracionDeSolicitudPago._direccionEntities = direccionEntitiesFactory.getDireccion(_GeneracionDeSolicitudPago._informacionProyectoEntities.idDireccion);
                _GeneracionDeSolicitudPago._direccionEntities.nombreRegion = regionControl.getRegion(Convert.ToInt32(_GeneracionDeSolicitudPago._direccionEntities.codigoRegionDireccion));
                _GeneracionDeSolicitudPago._direccionEntities.nombreProvincia = provinciaControl.getProvincia(Convert.ToInt32(_GeneracionDeSolicitudPago._direccionEntities.codigoProvinciaDireccion));
                _GeneracionDeSolicitudPago._direccionEntities.nombreComuna = comunaControl.getComuna(Convert.ToInt32(_GeneracionDeSolicitudPago._direccionEntities.codigoComunaDireccion));
                _GeneracionDeSolicitudPago._maestroProgramaEntities = maestroProgramaEntitiesFactory.getMaestroPrograma(Convert.ToInt64(_GeneracionDeSolicitudPago._caracteristicasEspecialesEntities.idMaestroPrograma));
                _GeneracionDeSolicitudPago._maestroResolucionEntities = maestroResolucionEntitiesFactory.getMaestroResolucion(_GeneracionDeSolicitudPago._informacionProyectoEntities.idMaestroPrograma);
                _GeneracionDeSolicitudPago._listTipoProveedorInformacionProyectoEntities = tipoProveedorInformacionProyectoEntitiesFactory.getListTipoProveedorInformacionProyecto(_GeneracionDeSolicitudPago._informacionProyectoEntities.idInformacionProyecto);
                _GeneracionDeSolicitudPago._ListTipoServicioParcialidadCaracteristicaEntities = tipoServicioParcialidadCaracteristicaEntitiesFactory.getListTipoServicioParcialidad(Convert.ToInt64(_GeneracionDeSolicitudPago._caracteristicasEspecialesEntities.idCaracteristicasEspeciales), idSolicitudPago);

                _GeneracionDeSolicitudPago._maestroClaseEntities = maestroClaseEntitiesFactory.getMaestroClase(Convert.ToInt64(_GeneracionDeSolicitudPago._caracteristicasEspecialesEntities.idMaestroClase));

                foreach (var item in _GeneracionDeSolicitudPago._ListTipoServicioParcialidadCaracteristicaEntities)
                {
                    if (item.idServicioPago != null)
                        idSolicitudPago = Convert.ToInt64(item.idServicioPago);
                    break;
                }

                _GeneracionDeSolicitudPago._listTipoIncrementoEntities = tipoIncrementoEntitiesFactory.getListaTipoIncremento(Convert.ToInt64(_GeneracionDeSolicitudPago._caracteristicasEspecialesEntities.idCaracteristicasEspeciales), idSolicitudPago);

                if (_GeneracionDeSolicitudPago._listTipoIncrementoEntities.Count > 0)
                {
                    _GeneracionDeSolicitudPago._listMaestroIncrementoEntities = new List<maestroIncrementoEntities>();
                    foreach (var item in _GeneracionDeSolicitudPago._listTipoIncrementoEntities)
                    {
                        maestroIncrementoEntities _maestroIncrementoEntities = maestroIncrementoEntitiesFactory.getMaestroIncremento(item.idMaestroIncremento);

                        _maestroIncrementoEntities.seleccionadoMaestroIncremento = item.seleccionadoTipoIncremento;



                        _GeneracionDeSolicitudPago._listMaestroIncrementoEntities.Add(_maestroIncrementoEntities);
                    }
                }


                if(_GeneracionDeSolicitudPago._listMaestroIncrementoEntities != null)
                if(_GeneracionDeSolicitudPago._listMaestroIncrementoEntities.Count> 0)
                _GeneracionDeSolicitudPago._listMaestroIncrementoEntities  = _GeneracionDeSolicitudPago._listMaestroIncrementoEntities.OrderBy(o => o.nombreMaestroIncremento).ToList();

                
                if (idSolicitudPago != null)
                {
                    _GeneracionDeSolicitudPago._solicitudPago = solicitudPagoEntitiesFactory.getSolicitudPago(idSolicitudPago);
                    if (_GeneracionDeSolicitudPago._solicitudPago.idMandatoProveedor > 0)
                    {
                        _GeneracionDeSolicitudPago._proveedorMandato = GeneracionDeSolicitudPagoFactory.getProveedor(Convert.ToInt64(_GeneracionDeSolicitudPago._solicitudPago.idMandatoProveedor));
                    }
                }
                else
                {
                    _GeneracionDeSolicitudPago._solicitudPago = new solicitudPagoEntities();
                }

                if (idSolicitudPago > 0)
                {
                    _GeneracionDeSolicitudPago._maestroTipoProveedorEntities = new maestroTipoProveedorEntities();
                    _GeneracionDeSolicitudPago._maestroTipoProveedorEntities.idMaestroTipoProveedor = 2;
                    _GeneracionDeSolicitudPago._maestroTipoProveedorEntities.nombreMaestroTipoProveedor = "EP";
                    _GeneracionDeSolicitudPago._maestroTipoProveedorEntities.estadoMaestroTipoProveedor = true;
                }
                else
                {
                    _GeneracionDeSolicitudPago._maestroTipoProveedorEntities = new maestroTipoProveedorEntities();
                    _GeneracionDeSolicitudPago._maestroTipoProveedorEntities.idMaestroTipoProveedor = 0;
                    _GeneracionDeSolicitudPago._maestroTipoProveedorEntities.nombreMaestroTipoProveedor = "";
                    _GeneracionDeSolicitudPago._maestroTipoProveedorEntities.estadoMaestroTipoProveedor = true;
                }

                _GeneracionDeSolicitudPago._solicitudPagoServicios = new solicitudPagoEntities();
                _GeneracionDeSolicitudPago._solicitudPagoServicios.montoComprometido = 0;
                _GeneracionDeSolicitudPago._solicitudPagoServicios.montoPagado = 0;

                if (_GeneracionDeSolicitudPago._maestroLlamadoEntities.idMaestroLlamado == 1)
                {
                    if (_GeneracionDeSolicitudPago._maestroProgramaEntities.idMaestroPrograma == 1)
                        _GeneracionDeSolicitudPago._maestroResolucionEntities.nombreMaestroResolucion = "HR: Resolución 3131";
                    if (_GeneracionDeSolicitudPago._maestroProgramaEntities.idMaestroPrograma == 3 || _GeneracionDeSolicitudPago._maestroProgramaEntities.idMaestroPrograma == 4)
                        _GeneracionDeSolicitudPago._maestroResolucionEntities.nombreMaestroResolucion = "FSEV (DS N°49.Incluye AVC: Resolución N° 420)";
                }



                _GeneracionDeSolicitudPago._listProveedorFTO = new List<proveedorEntities>();

                _GeneracionDeSolicitudPago._listProveedorEC = new List<proveedorEntities>();

                if (_GeneracionDeSolicitudPago._listTipoProveedorInformacionProyectoEntities.Count > 0)
                    foreach (var item in _GeneracionDeSolicitudPago._listTipoProveedorInformacionProyectoEntities)
                    {
                        _GeneracionDeSolicitudPago._proveedorTipo = proveedorEntitiesFactory.getProveedor(Convert.ToInt64(item.idProveedor));
                        if (_GeneracionDeSolicitudPago._proveedorTipo.rutProveedor != 0)
                        {
                            

                            if (_GeneracionDeSolicitudPago._proveedorTipo.idMaestroTipoProveedor == 1)
                            {
                                _GeneracionDeSolicitudPago._proveedorEP = _GeneracionDeSolicitudPago._proveedorTipo;
                            }
                            else if (_GeneracionDeSolicitudPago._proveedorTipo.idMaestroTipoProveedor == 2)
                            {
                                proveedorEntities proveedorEC = _GeneracionDeSolicitudPago._proveedorTipo;
                                proveedorEC.rutCompleto = proveedorEC.rutProveedor + "-" + proveedorEC.dvDigitoprovedor;
                                if (!_GeneracionDeSolicitudPago._listProveedorEC.Exists(c => c.rutProveedor == proveedorEC.rutProveedor))
                                {
                                    _GeneracionDeSolicitudPago._listProveedorEC.Add(proveedorEC);
                                    _GeneracionDeSolicitudPago._proveedorEC = proveedorEC;

                                    if (idSolicitudPago > 0)
                                    {
                                        _GeneracionDeSolicitudPago._proveedorEC = proveedorEntitiesFactory.getProveedor(Convert.ToInt64(_GeneracionDeSolicitudPago._solicitudPago.idProveedor));
                                    }
                                }
                            }
                            else if (_GeneracionDeSolicitudPago._proveedorTipo.idMaestroTipoProveedor == 6)
                            {

                                proveedorEntities _proveedorFTO = _GeneracionDeSolicitudPago._proveedorTipo;
                                _proveedorFTO.rutCompleto = _proveedorFTO.rutProveedor + "-" + _proveedorFTO.dvDigitoprovedor;

                                if (!_GeneracionDeSolicitudPago._listProveedorFTO.Exists(c => c.rutProveedor == _proveedorFTO.rutProveedor))
                                {

                                    _GeneracionDeSolicitudPago._listProveedorFTO.Add(_proveedorFTO);
                                    _GeneracionDeSolicitudPago._proveedorFTO = _proveedorFTO;

                                    if (idSolicitudPago > 0)
                                    {
                                        _GeneracionDeSolicitudPago._proveedorFTO = proveedorEntitiesFactory.getProveedor(Convert.ToInt64(_GeneracionDeSolicitudPago._solicitudPago.idProveedor));
                                    }
                                }

                                //_GeneracionDeSolicitudPago._proveedorFTO = _GeneracionDeSolicitudPago._proveedorTipo;
                            }
                        }
                    }



                if (_GeneracionDeSolicitudPago._informacionProyectoEntities.codigoProyectoInformacionProyecto != string.Empty && (_GeneracionDeSolicitudPago._solicitudPago.idSolicitudPago == 0 || _GeneracionDeSolicitudPago._solicitudPago == null))
                {
                    try
                    {
                        direccionEntities _direccionEntities = direccionEntitiesFactory.getDireccion(_GeneracionDeSolicitudPago._informacionProyectoEntities.idDireccion);
                        //_GeneracionDeSolicitudPago._informacionProyectoEntities._OperacionesMuninEntities2  = OperacionesMuninFactory.getAvanceObraTrazabilidadProyecto(Convert.ToInt32(_GeneracionDeSolicitudPago._informacionProyectoEntities.idMaestroPrograma), _GeneracionDeSolicitudPago._informacionProyectoEntities.codigoProyectoInformacionProyecto.ToString(), Convert.ToInt32(_direccionEntities.codigoRegionDireccion), null, null, null);
                        proveedorEntities _proveedorEC = new proveedorEntities();
                        if (_GeneracionDeSolicitudPago._informacionProyectoEntities._OperacionesMuninEntities2 != null)
                            foreach (var item in _GeneracionDeSolicitudPago._informacionProyectoEntities._OperacionesMuninEntities2)
                            {
                                if (item.rutEC != null && item.rutEC.Length > 3)
                                {

                                    _proveedorEC = proveedorEntitiesFactory.getProveedorRutProveedoroIdTipo(Convert.ToInt32(item.rutEC), 2);

                                    if (_proveedorEC.idProveedor == 0)
                                    {
                                        proveedorEntitiesFactory.saveProveedor(Convert.ToInt32(item.rutEC), Convert.ToChar(item.dvEC), item.nombreEC, 2);
                                        _proveedorEC = proveedorEntitiesFactory.getProveedorRutProveedoroIdTipo(Convert.ToInt32(item.rutEC), 2);

                                        tipoProveedorInformacionProyectoEntities _tipoProveedorInformacionProyectoEntities = new tipoProveedorInformacionProyectoEntities();

                                        _tipoProveedorInformacionProyectoEntities.estadoTipoProveedorInformacionProyecto = true;
                                        _tipoProveedorInformacionProyectoEntities.idInformacionProyecto = _GeneracionDeSolicitudPago._informacionProyectoEntities.idInformacionProyecto;
                                        _tipoProveedorInformacionProyectoEntities.idProveedor = _proveedorEC.idProveedor;

                                        tipoProveedorInformacionProyectoEntitiesFactory.saveTipoProveedorInformacionProyecto(_tipoProveedorInformacionProyectoEntities);

                                    }

                                    _proveedorEC.rutCompleto = _proveedorEC.rutProveedor + "-" + _proveedorEC.dvDigitoprovedor;
                                    if (_proveedorEC != null)
                                    {


                                        bool encontrado = false;
                                        foreach (var item2 in _GeneracionDeSolicitudPago._listProveedorEC)
                                        {
                                            if (_proveedorEC.rutProveedor == item2.rutProveedor)
                                            {
                                                encontrado = true;
                                                break;
                                            }
                                        }

                                        if (!encontrado)
                                        {
                                            _GeneracionDeSolicitudPago._listProveedorEC.Add(_proveedorEC);
                                        }

                                    }
                                    else
                                    {
                                        proveedorEntitiesFactory.saveProveedor(Convert.ToInt32(item.rutEC), Convert.ToChar(item.dvEC), item.nombreEC, 2);

                                        _proveedorEC = proveedorEntitiesFactory.getProveedorIdProyectoIdTipo(Convert.ToInt32(item.rutEC), 2);
                                        _proveedorEC.rutCompleto = _proveedorEC.rutProveedor + "-" + _proveedorEC.dvDigitoprovedor;



                                        tipoProveedorInformacionProyectoEntities _tipoProveedorInformacionProyectoEntities = new tipoProveedorInformacionProyectoEntities();

                                        _tipoProveedorInformacionProyectoEntities.estadoTipoProveedorInformacionProyecto = true;
                                        _tipoProveedorInformacionProyectoEntities.idInformacionProyecto = _GeneracionDeSolicitudPago._informacionProyectoEntities.idInformacionProyecto;
                                        _tipoProveedorInformacionProyectoEntities.idProveedor = _proveedorEC.idProveedor;

                                        tipoProveedorInformacionProyectoEntitiesFactory.saveTipoProveedorInformacionProyecto(_tipoProveedorInformacionProyectoEntities);

                                        bool encontrado = false;
                                        foreach (var item2 in _GeneracionDeSolicitudPago._listProveedorEC)
                                        {
                                            if (_proveedorEC.rutProveedor == item2.rutProveedor)
                                            {
                                                encontrado = true;
                                                break;
                                            }
                                        }

                                        if (!encontrado)
                                        {
                                            _GeneracionDeSolicitudPago._listProveedorEC.Add(_proveedorEC);
                                        }

                                    }


                                }
                            }

                        if(_GeneracionDeSolicitudPago._listProveedorEC.Count == 1)
                        {
                            foreach (var item in _GeneracionDeSolicitudPago._listProveedorEC)
                            {
                                _GeneracionDeSolicitudPago._proveedorEC = item;
                            }
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error("error a: varificar error generacion" + ex);
                        throw;
                    }


                }






                _GeneracionDeSolicitudPago._listMaestroTipoPagoEntities = maestroTipoPagoEntitiesFactory.getListMaestroTipoPago();
                _GeneracionDeSolicitudPago._auxPlantillaEntities = auxPlantillaEntitiesFactory.obtenerPlantillaServicioParcialidades(Convert.ToInt64(_GeneracionDeSolicitudPago._caracteristicasEspecialesEntities.idCaracteristicasEspeciales), idSolicitudPago);
                _GeneracionDeSolicitudPago._aux2PlantillaEntities = _GeneracionDeSolicitudPago._auxPlantillaEntities;

                List<auxPlantillaEntities> eliminarServicios = new List<auxPlantillaEntities>();

                maestroTipoProveedorEntities _maestroTipoProveedorEntities = new maestroTipoProveedorEntities();

                

                //if (_GeneracionDeSolicitudPago._listProveedorEC.Count == 0)
                //{
                //    foreach (var item in _GeneracionDeSolicitudPago._listProveedorEC)
                //    {
                //        if (item.idProveedor == 2)
                //            _maestroTipoProveedorEntities = item;

                //    }

                //    _GeneracionDeSolicitudPago._listProveedorEC.Remove(_maestroTipoProveedorEntities);
                //}

                //_GeneracionDeSolicitudPago._listMaestroTipoPagoEntities.Remove();


                // eliminarServicios = _GeneracionDeSolicitudPago._auxPlantillaEntities;
                foreach (var item1 in _GeneracionDeSolicitudPago._auxPlantillaEntities)
                {
                    if (item1.idParcialidad == 1)
                        eliminarServicios.Add(item1);
                }
                _GeneracionDeSolicitudPago._aux2PlantillaEntities = eliminarServicios;

                //foreach (var item in _GeneracionDeSolicitudPago._auxPlantillaEntities)
                //{
                //    if (item.montoParcialidad > 0)
                //        item.parcialidadSeleccionada = true;
                //}

                if (_GeneracionDeSolicitudPago._solicitudPago != null)
                {
                    if (_GeneracionDeSolicitudPago._solicitudPago.idSolicitudPago > 0)
                    {
                        foreach (var item in _GeneracionDeSolicitudPago._auxPlantillaEntities)
                        {
                            if (item.montoParcialidad > 0)
                                item.parcialidadSeleccionada = true;
                        }
                    }
                }
                else
                {
                    _GeneracionDeSolicitudPago._solicitudPago = new solicitudPagoEntities();
                }

                decimal? montoTotalProyecto = 0;

                foreach (var item in _GeneracionDeSolicitudPago._aux2PlantillaEntities)
                {
                    montoTotalProyecto = montoTotalProyecto + item.montoServicio + (item.montoAsignacionDirecta == null ? 0 : item.montoAsignacionDirecta);//Cambios DO
                }

                _GeneracionDeSolicitudPago._solicitudPagoServicios.montoTotalProyecto = montoTotalProyecto;

                //Cambios DO
                _GeneracionDeSolicitudPago._maestroEstadoProyectoEntities = maestroEstadoProyectoEntitiesFactory.getMaestroEstadoProyecto(_GeneracionDeSolicitudPago._informacionProyectoEntities.idMaestroEstadoProyecto);
                _GeneracionDeSolicitudPago._MaestroEstadoBeneficioEntities = MaestroEstadoBeneficioEntitiesFactory.getMaestroEstadoBeneficio(_GeneracionDeSolicitudPago._informacionProyectoEntities.idMaestroEstadoBeneficio);
                _GeneracionDeSolicitudPago._MaestroBancoEntities = MaestroBancoEntitiesFactory.getMaestroBanco(_GeneracionDeSolicitudPago._informacionProyectoEntities.idMaestroBanco);
                //Cambios DO

                return _GeneracionDeSolicitudPago;
            }
            else
            {
                return _GeneracionDeSolicitudPago;
            }
        }

        public static GeneracionDeSolicitudPago getCodPro(long? idSolicitud)
        {
            GeneracionDeSolicitudPago _aux = new GeneracionDeSolicitudPago();
            _aux._solicitudPago = solicitudPagoEntitiesFactory.getSolicitudPago(idSolicitud);

            if(_aux._solicitudPago.idMandatoProveedor >0)
            {
                _aux._proveedorMandato = GeneracionDeSolicitudPagoFactory.getProveedor(Convert.ToInt64(_aux._solicitudPago.idMandatoProveedor));            }
                _aux._caracteristicasEspecialesEntities = caracteristicasEspecialesEntitiesFactory.getIdCaracterisacion(Convert.ToInt64(_aux._solicitudPago.idCaracteristicasEspeciales));

            return _aux;
        }

        //public static long SaveGeneracionDeLaSolicitudPago(GeneracionDeSolicitudPago _GeneracionDeSolicitudPago, solicitudPagoEntities _solicitudPagoEntities, long idCaracteristicasEspeciales)
        public static solicitudPagoEntities GeneraSolicitudDePago(GeneracionDeSolicitudPago _GeneracionDeSolicitudPago, solicitudPagoEntities _solicitudPagoEntities,
                                                                long idCaracteristicasEspeciales, proveedorEntities objProveedorInsert)
        {
            _solicitudPagoEntities.idCaracteristicasEspeciales = idCaracteristicasEspeciales;
            _solicitudPagoEntities.idTipoPago = _GeneracionDeSolicitudPago._maestroTipoPagoEntities.idMaestroTipoPago;
            _solicitudPagoEntities.idMaestroEstadoSolicitud = _solicitudPagoEntities.idTipoPago == 2 ? 1 : 3; //Estado Creada cuando es Pago SPS, sino , es Historica 3
            _solicitudPagoEntities.numeroFamiliasPagadarSolicitudPago = _GeneracionDeSolicitudPago._informacionProyectoEntities.cantidadBeneficiariosInformacionProyecto;
            _solicitudPagoEntities.usuariosReponsableSolicitudPago = _solicitudPagoEntities.usuariosReponsableSolicitudPago;

            _GeneracionDeSolicitudPago._listTipoIncrementoEntities = tipoIncrementoEntitiesFactory.getListaTipoIncremento(Convert.ToInt64(_GeneracionDeSolicitudPago._caracteristicasEspecialesEntities.idCaracteristicasEspeciales), null);
            solicitudPagoEntities objResult = solicitudPagoEntitiesFactory.GeneraSolicitudPago(_solicitudPagoEntities, objProveedorInsert, _GeneracionDeSolicitudPago._informacionProyectoEntities.porcentajeAvanceObra, _GeneracionDeSolicitudPago._informacionProyectoEntities.estadoAvanceObra);//Guardar solicitud de pago

            if (objResult.codigoSalida == "1")
            {
                if (_GeneracionDeSolicitudPago._listTipoIncrementoEntities.Count > 0)
                {
                    if (_GeneracionDeSolicitudPago._listMaestroIncrementoEntities != null)
                    {
                        foreach (var item in _GeneracionDeSolicitudPago._listMaestroIncrementoEntities)
                        {
                            foreach (var item2 in _GeneracionDeSolicitudPago._listTipoIncrementoEntities)
                            {
                                item2.idTipoIncremento = 0;
                                if (item.idMaestroIncremento == item2.idMaestroIncremento)
                                {
                                    item2.seleccionadoTipoIncremento = item.seleccionadoMaestroIncremento;
                                    item2.idMaestroIncremento = item.idMaestroIncremento;
                                    item2.idSolicitudPago = Convert.ToInt64(objResult.idSolicitudPago);
                                }
                            }
                        }

                        tipoIncrementoEntitiesFactory.saveListaTipoIncremento(_GeneracionDeSolicitudPago._listTipoIncrementoEntities, Convert.ToInt64(objResult.idSolicitudPago));
                    }
                }

                foreach (var item in _GeneracionDeSolicitudPago._auxPlantillaEntities)
                {
                    tipoServicioParcialidadCaracteristicaEntitiesFactory.actualizarIdSolicitudPago(Convert.ToInt64(item.idTipoServicioParcialidadCaracteristica), Convert.ToInt64(objResult.idSolicitudPago));
                }
            }

            return objResult;
        }

        public static void saveParcialidades(GeneracionDeSolicitudPago _GeneracionDeSolicitudPago)
        {
            auxPlantillaEntitiesFactory.saveMontoParcialidades(_GeneracionDeSolicitudPago._auxPlantillaEntities);
        }

        public static void saveServicios(GeneracionDeSolicitudPago _GeneracionDeSolicitudPago)
        {
            auxPlantillaEntitiesFactory.saveMontosServicios(_GeneracionDeSolicitudPago._auxPlantillaEntities);
        }

        public static void ReIniciaPlantillaNew(string CodigoProyecto, long idPrograma)
        {
            informacionProyectoEntities objProyecto = new informacionProyectoEntities();
            objProyecto = informacionProyectoEntitiesFactory.getinformacionProyectoEntities(CodigoProyecto, idPrograma);

            caracteristicasEspecialesEntities objCaracEspecial = new caracteristicasEspecialesEntities();
            if (objProyecto.idInformacionProyecto > 0)
            {
                objCaracEspecial = caracteristicasEspecialesEntitiesFactory.getCaracEspecialesIdInformacionProyecto(objProyecto.idInformacionProyecto);
            }

            long? idCaracEspecial = null;
            if (objCaracEspecial != null)
                idCaracEspecial = objCaracEspecial.idCaracteristicasEspeciales;

            tipoServicioParcialidadCaracteristicaEntitiesFactory.ReIniciaPlantillaNew(idCaracEspecial);
        }

        public static solicitudPagoEntities CalculoMontosSolicitud(string CodigoProyecto, long idMaestroPrograma)
        {
            solicitudPagoEntities objSolicitudPago = new solicitudPagoEntities();

            objSolicitudPago = solicitudPagoEntitiesFactory.getLastSolicitudPago(CodigoProyecto, idMaestroPrograma);

            if (objSolicitudPago.montoTotalProyecto > 0)
            {
                decimal? MontoPagado = solicitudPagoEntitiesFactory.GetSolicitudesProyectoMontoPagadoHist(CodigoProyecto, idMaestroPrograma) +
                                                solicitudPagoEntitiesFactory.GetSolicitudesProyectoMontoPagadoSIGFE(CodigoProyecto, idMaestroPrograma);

                //Monto de las Solicitudes Generadas (Todas)
                decimal? MontoSolicitudesGeneradas = solicitudPagoEntitiesFactory.GetSolicitudesProyectoMontoSolicitudesGeneradas(CodigoProyecto, idMaestroPrograma);

                //Se restan al total las que se encuentran pagadas y asi quedan las comprometidas
                objSolicitudPago.montoComprometido = (MontoSolicitudesGeneradas - MontoPagado);
                objSolicitudPago.montoPagado = MontoPagado;
            }

            return objSolicitudPago;
        }

        public static string verificarExistenciaProyectoSNAT(long idPrograma, string codigoProyecto)
        {
            return informacionProyectoEntitiesFactory.verificarExistenciaProyectoSNAT(idPrograma, codigoProyecto);
        }

        public static int verificaProyectoRegionUsuarioSNAT(long idPrograma, string codigoProyecto, int regionUsuario)
        {
            return informacionProyectoEntitiesFactory.verificarProyectoRegionUsuarioSNAT(idPrograma, codigoProyecto, regionUsuario);
        }
    }
}

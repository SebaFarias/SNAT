using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minvu.Snat.IData.ORM;

namespace Minvu.Snat.IData.DAO
{
    public class auxPlantillaDAO
    {

        public string nombreTipologia { get; set; }
        public string nombreSubModalidad { get; set; }
        public string nombreMaestroServicio { get; set; }
        public string nombreParcialidad { get; set; }
        public int? porcentajeParcialidad { get; set; }
        public bool? parcialidadSeleccionada { get; set; }
        public long? idServicio { get; set; }
        public long? idParcialidad { get; set; }
        public long? idServicioParcialidad { get; set; }
        public long? idTipoServicioParcialidadCaracteristica { get; set; }

        public long? idCaracteristicaEspeciales { get; set; }
        public decimal? montoServicio { get; set; }
        public decimal? montoParcialidad { get; set; }
        public decimal? asignacionDirecta { get; set; }
        public bool? estadoServicio { get; set; }
        public bool? estadoParcialidad { get; set; }

        public auxPlantillaDAO()
        {
            nombreTipologia = "";
            nombreSubModalidad = "";
            nombreMaestroServicio = "";
            nombreParcialidad = "";
            porcentajeParcialidad = null;
            parcialidadSeleccionada = null;
            idServicio = null;
            idParcialidad = null;
            montoServicio = null;
            montoParcialidad = null;
            asignacionDirecta = null;
            estadoServicio = null;
            estadoParcialidad = null;
            idServicioParcialidad = null;
            idTipoServicioParcialidadCaracteristica = null;
            idCaracteristicaEspeciales = null;
        }

        public auxPlantillaDAO(string _nombreTipologia, string _nombreSubModalidad, string _nombreMaestroServicio, string _nombreParcialidad, int? _porcentajeParcialidad, bool? _parcialidadSeleccionada, long? _idServicio, long? _idParcialidad, decimal? _montoServicio, decimal? _montoParcialidad, decimal? _asignacionDirecta, bool? _estadoServicio, bool? _estadoParcialidad, long? _idServicioParcialidad, long? _idTipoServicioParcialidadCaracteristica, long? _idCaracteristicaEspeciales)
        {
            nombreTipologia = _nombreTipologia;
            nombreSubModalidad = _nombreSubModalidad;
            nombreMaestroServicio = _nombreMaestroServicio;
            nombreParcialidad = _nombreParcialidad;
            porcentajeParcialidad = _porcentajeParcialidad;
            parcialidadSeleccionada = _parcialidadSeleccionada;
            idServicio = _idServicio;
            idParcialidad = _idParcialidad;
            montoServicio = _montoServicio;
            montoParcialidad = _montoParcialidad;
            asignacionDirecta = _asignacionDirecta;
            estadoServicio = _estadoServicio;
            estadoParcialidad = _estadoParcialidad;
            idServicioParcialidad = _idServicioParcialidad;
            idTipoServicioParcialidadCaracteristica = _idTipoServicioParcialidadCaracteristica;
            idCaracteristicaEspeciales = _idCaracteristicaEspeciales;


        }

        public static List<auxPlantillaDAO> getPlantilla(long _caracteristicasEspeciales, long? idSolicitudPago)
        {
            auxPlantillaDAO objPantilla = new auxPlantillaDAO();

            try
            {
                using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
                {
                    List<auxPlantillaDAO> _listInf = new List<auxPlantillaDAO>();

                    //var numSol = (Int64?)0;
                    //var numsSol = (from sop in contexto.SOLICITUD_PAGO
                    //               where sop.IDCARACTERISTICASESPECIALES == _caracteristicasEspeciales
                    //               && sop.FECHAREALCREACIONSOLICITUDPAGO != null
                    //               orderby sop.FECHAREALCREACIONSOLICITUDPAGO
                    //               select sop.IDSOLICITUDPAGO);
                    //foreach (var item in numsSol)
                    //{
                    //    numSol = (Int64?)item;
                    //    break;
                    //}

                    //var qServicioParcialidad = from tspc in contexto.TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA
                    //                           join sp in contexto.SERVICIO_PARCIALIDAD on tspc.IDSERVICIOPARCIALIDAD equals sp.IDSERVICIOPARCIALIDAD
                    //                           join ts in contexto.TIPOLOGIA_SERVICIO on sp.IDTIPOLOGIASERVICIO equals ts.IDTIPOLOGIASERVICIO
                    //                           join mt in contexto.MAESTRO_TIPOLOGIA on ts.IDMAESTROTIPOLOGIA equals mt.IDMAESTROTIPOLOGIA
                    //                           join ms in contexto.MAESTRO_SERVICIO on ts.IDMAESTROSERVICIO equals ms.IDMAESTROSERVICIO
                    //                           join smp in contexto.SUB_MODALIDAD_PARCIALIDAD on sp.IDSUBMODALIDADPARCIALIDAD equals smp.IDSUBMODALIDADPARCIALIDAD
                    //                           join mp in contexto.MAESTRO_PARCIALIDAD on smp.IDMAESTROPARCIALIDAD equals mp.IDMAESTROPARCIALIDAD
                    //                           join msm in contexto.MAESTRO_SUB_MODALIDAD on smp.IDMAESTROSUBMODALIDAD equals msm.IDMAESTROSUBMODALIDAD
                    //                           where tspc.IDCARACTERISTICASESPECIALES == _caracteristicasEspeciales
                    //                           && tspc.IDSOLICITUDPAGO == numSol
                    //                           select new { tspc.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA, tspc.IDSERVICIOPARCIALIDAD, tspc.IDCARACTERISTICASESPECIALES, mt.NOMBREMAESTROTIPOLOGIA, ms.IDMAESTROSERVICIO, mp.IDMAESTROPARCIALIDAD, msm.NOMBREMAESTROSUBMODALIDAD, ms.NOMBREMAESTROSERVICIO, mp.NOMBREMAESTROPARCIALIDAD, tspc.PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA, tspc.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA, tspc.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA, tspc.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA };

                    //foreach (var a in qServicioParcialidad)
                    //{
                    //    auxPlantillaDAO _inf = new auxPlantillaDAO();
                    //    _inf.nombreTipologia = a.NOMBREMAESTROTIPOLOGIA;
                    //    _inf.nombreSubModalidad = a.NOMBREMAESTROSUBMODALIDAD;
                    //    _inf.nombreMaestroServicio = a.NOMBREMAESTROSERVICIO;
                    //    _inf.nombreParcialidad = a.NOMBREMAESTROPARCIALIDAD;
                    //    _inf.porcentajeParcialidad = a.PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                    //    _inf.idServicioParcialidad = a.IDSERVICIOPARCIALIDAD;
                    //    _inf.idTipoServicioParcialidadCaracteristica = a.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                    //    _inf.idCaracteristicaEspeciales = a.IDCARACTERISTICASESPECIALES;
                    //    _inf.idServicio = a.IDMAESTROSERVICIO;
                    //    _inf.idParcialidad = a.IDMAESTROPARCIALIDAD;
                    //    _inf.montoServicio = a.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                    //    _inf.montoParcialidad = a.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                    //    _inf.asignacionDirecta = a.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA;

                    //    _listInf.Add(_inf);
                    //}

                    //if (_listInf.Count == 0)
                    //{
                    var result = from tspc in contexto.TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA
                                 join sp in contexto.SERVICIO_PARCIALIDAD on tspc.IDSERVICIOPARCIALIDAD equals sp.IDSERVICIOPARCIALIDAD
                                 join ts in contexto.TIPOLOGIA_SERVICIO on sp.IDTIPOLOGIASERVICIO equals ts.IDTIPOLOGIASERVICIO
                                 join mt in contexto.MAESTRO_TIPOLOGIA on ts.IDMAESTROTIPOLOGIA equals mt.IDMAESTROTIPOLOGIA
                                 join ms in contexto.MAESTRO_SERVICIO on ts.IDMAESTROSERVICIO equals ms.IDMAESTROSERVICIO
                                 join smp in contexto.SUB_MODALIDAD_PARCIALIDAD on sp.IDSUBMODALIDADPARCIALIDAD equals smp.IDSUBMODALIDADPARCIALIDAD
                                 join mp in contexto.MAESTRO_PARCIALIDAD on smp.IDMAESTROPARCIALIDAD equals mp.IDMAESTROPARCIALIDAD
                                 join msm in contexto.MAESTRO_SUB_MODALIDAD on smp.IDMAESTROSUBMODALIDAD equals msm.IDMAESTROSUBMODALIDAD
                                 where tspc.IDCARACTERISTICASESPECIALES == _caracteristicasEspeciales
                                 && tspc.IDSOLICITUDPAGO == idSolicitudPago
                                 select new { tspc.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA, tspc.IDSERVICIOPARCIALIDAD, tspc.IDCARACTERISTICASESPECIALES, mt.NOMBREMAESTROTIPOLOGIA, ms.IDMAESTROSERVICIO, mp.IDMAESTROPARCIALIDAD, msm.NOMBREMAESTROSUBMODALIDAD, ms.NOMBREMAESTROSERVICIO, mp.NOMBREMAESTROPARCIALIDAD, tspc.PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA, tspc.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA, tspc.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA, tspc.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA };

                    foreach (var a in result)
                    {
                        auxPlantillaDAO _inf = new auxPlantillaDAO();
                        _inf.nombreTipologia = a.NOMBREMAESTROTIPOLOGIA;
                        _inf.nombreSubModalidad = a.NOMBREMAESTROSUBMODALIDAD;
                        _inf.nombreMaestroServicio = a.NOMBREMAESTROSERVICIO;
                        _inf.nombreParcialidad = a.NOMBREMAESTROPARCIALIDAD;
                        _inf.porcentajeParcialidad = a.PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                        _inf.idServicioParcialidad = a.IDSERVICIOPARCIALIDAD;
                        _inf.idTipoServicioParcialidadCaracteristica = a.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                        _inf.idCaracteristicaEspeciales = a.IDCARACTERISTICASESPECIALES;
                        _inf.idServicio = a.IDMAESTROSERVICIO;
                        _inf.idParcialidad = a.IDMAESTROPARCIALIDAD;
                        _inf.montoServicio = a.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                        _inf.montoParcialidad = a.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                        _inf.asignacionDirecta = a.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA;

                        _listInf.Add(_inf);
                    }
                    //}

                    return _listInf;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static int SaveMontoServicio(TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _tipoServicioParcialidadCaracteristica)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _inf = new TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA();
                try
                {
                    _inf = contexto.TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.Where(c => c.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA == _tipoServicioParcialidadCaracteristica.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA && c.IDSERVICIOPARCIALIDAD == _tipoServicioParcialidadCaracteristica.IDSERVICIOPARCIALIDAD && c.IDCARACTERISTICASESPECIALES == _tipoServicioParcialidadCaracteristica.IDCARACTERISTICASESPECIALES).FirstOrDefault<TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA>();

                    if (_inf != null)
                    {


                        _inf.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA = _tipoServicioParcialidadCaracteristica.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                        _inf.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA = _tipoServicioParcialidadCaracteristica.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA;


                    }

                    contexto.SaveChanges();
                    return (int)_inf.IDSERVICIOPARCIALIDAD;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }

        public static int SaveMontoParcialidad(TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _tipoServicioParcialidadCaracteristica)
        {
            using (DB_SNAT_VEntities contexto = new DB_SNAT_VEntities())
            {
                TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA _inf = new TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA();
                try
                {
                    _inf = contexto.TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA.Where(c => c.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA == _tipoServicioParcialidadCaracteristica.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA && c.IDSERVICIOPARCIALIDAD == _tipoServicioParcialidadCaracteristica.IDSERVICIOPARCIALIDAD && c.IDCARACTERISTICASESPECIALES == _tipoServicioParcialidadCaracteristica.IDCARACTERISTICASESPECIALES).FirstOrDefault<TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA>();

                    if (_inf != null)
                    {

                        _inf.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA = _tipoServicioParcialidadCaracteristica.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA;
                        _inf.PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA = _tipoServicioParcialidadCaracteristica.PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA;

                    }

                    contexto.SaveChanges();
                    return (int)_inf.IDSERVICIOPARCIALIDAD;
                }
                catch (Exception Ex)
                {
                    //   Log.Instance.Error("Error ContactoDAO.Save IDCONTACTO -> " + _producto.IDCONTACTO, Ex);
                    throw Ex;
                }
            }
        }

    }
}
